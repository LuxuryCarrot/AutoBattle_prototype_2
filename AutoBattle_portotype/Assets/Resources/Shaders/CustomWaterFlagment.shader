Shader "Custom/CustomWaterFlagment"
{
    Properties
    {
		//물의 깊이값 그래디언트 조절부분
		_DGShallow ("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725) //얕은물색
		_DGDeep ("Depth Gradient Deep", Color) = (0.886, 0.407, 1, 0.749) //깊은물색
		_WaterAlpha ("Water Color Alpha", Range(0,1)) = 0.5 //물 투명도
		_DMDistance ("Depth Maximun Distance", Float) = 1 //깊이조절
		_SkyRefDistance ("Sky Reflection Distance", Range(0,1)) = 0.5 //하늘 반사

		//움직임 노이즈 구문
		_SufNoise ("Surface Noise", 2D) = "white" {} //움직임 노이즈
		_SufNoiseCutoff ("Surface Noise Cutoff", Range(0,1)) = 0.777 //노이즈 컷 오프
		//경계면 구문
		_FoDistance ("Foam Distance", Float) = 0.4 //경계면 표시크기
		//흐르는 속도 구문
		_SufNoiseScroll ("Surface Noise Scroll (z,w no work)", Vector) = (0.03, 0.03, 0, 0) //속도조절
		//움직임 2차원 벡터 구문
		_SufDistortion ("Surface Distortion", 2D) = "white" {} //2차원벡터텍스쳐
		_SufDistortionAmount ("Surface Distortion Amount", Range(0,1)) = 0.27 //벡터강도

		//표면거품제어
		_FoColor ("Foam Color", Color) = (1,1,1,1) //거품색
		_FoAlpha ("Foam Alpha", Range(0,1)) = 1 //거품알파
		_FoDistanceMax ("Foam Maximun Distance", Float) = 0.4 //최대거품크기
		_FoDistanceMin ("Foam Minimum Distance", Float) = 0.04 //최소거품크기

		//흔들림제어
		_WavePower ("Wave Power", float) = 0 //흔들림
	}
		SubShader
	{
		Tags
		{ 
			"Queue" = "Transparent"
		}
		LOD 200

		Pass
		{
			//알파블렌드구문
			Blend SrcAlpha OneMinusSrcAlpha
			
			//Z쓰기 끄기
			ZWrite Off

			CGPROGRAM

			//???????????????????
			#define SMOOTHSTEP_AA 0.01

			//프레그먼트 쉐이더임 (아무래도 그래보임)
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			//--------- 알파블렌드 넘밝게 안나오게 막는구문----------
			float4 alphaBlend(float4 top, float4 bottom)
			{
				float3 color = (top.rgb * top.a) + (bottom.rgb * (1 - top.a));
				float alpha = top.a + bottom.a * (1 - top.a);

				return float4(color, alpha);
			}


			//------------ 프레그먼트 쉐이더 준비구문 ----------------

			struct APPdata //프레그먼트 쉐이더의 appdata개념
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};


			struct v2f //프레그먼트 v2f정의개념 (UV정의포함)
			{
				float4 vertex : SV_POSITION;
				float2 noiseUV : TEXCOORD0;
				float2 distortUV : TEXCOORD1;

				float3 viewNormal : NORMAL;
				float4 screenPosition : TEXCOORD2;
				float3 WorldRef : TEXCOORD3;
			};

			//움직임 노이즈 받아오는 구문
			sampler2D _SufNoise;
			float4 _SufNoise_ST;

			//움직임 노이즈 Warp벡터 받아오는 구문
			sampler2D _SufDistortion;
			float4 _SufDistortion_ST;

			v2f vert (APPdata v) //사전 데이터로드및 주연산 전 연산개념(UV배치포함)
			{
				v2f o;

				//화면포지션 전달
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPosition = ComputeScreenPos(o.vertex);
				o.noiseUV = TRANSFORM_TEX(v.uv, _SufNoise);
				o.distortUV = TRANSFORM_TEX(v.uv, _SufDistortion);
				o.viewNormal = COMPUTE_VIEW_NORMAL;
				float3 worldPos = mul(unity_ObjectToWorld, o.vertex).xyz;
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				float3 worldNormal = UnityObjectToWorldNormal(o.viewNormal);
				o.WorldRef = reflect(-worldViewDir, worldNormal);

				//v.vertex.y += abs(v.texcoord.x * 2 - 1);

				return o;
			}

			//------------- 선언구문 --------------

			//Depth Gradient 선언문
			float4 _DGShallow;
			float4 _DGDeep;
			float _DMDistance;
			float _WaterAlpha;
			float _SkyRefDistance;

			//Surface Noise 선언문
			float _SufNoiseCutoff;
			float _FoDistance;
			float2 _SufNoiseScroll;
			float _SufDistortionAmount;
			float _FoDistanceMax;
			float _FoDistanceMin;
			float4 _FoColor;
			float _FoAlpha;

			//카메라 깊이값을 받아오는 구문
			sampler2D _CameraDepthTexture;
			//카메라 노멀벡터를 받아오는 구문
			sampler2D _CameraNormalsTexture;


			//-------------- 실연산 구문 -----------------

			float4 frag(v2f q) : SV_Target //실제 라이팅연산이 되는 곳
			{
				//깊이텍스쳐 샘플링
				//tex2Dproj란 tex2D랑은 차이가 거의없지만 UV를 xy에있는 w로 나누는개념
				float EXDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(q.screenPosition));
				float EXDepthLinear = LinearEyeDepth(EXDepth01);

				//수면의 수심계산
				float4 DefDiffer = EXDepthLinear - q.screenPosition.w;

				//물색상 계산
				float WaterDefDiffer01 = saturate(DefDiffer / _DMDistance);
				float4 WaterColor = lerp(_DGShallow, _DGDeep, WaterDefDiffer01);

				//표면 노이즈 Warp
				float2 DistSample = (tex2D(_SufDistortion, q.distortUV).xy * 2 - 1) * _SufDistortionAmount;
				//표면 노이즈 UV스크롤링
				float2 noiseUV = float2
				(
					(q.noiseUV.x + _Time.y * _SufNoiseScroll.x) + DistSample.x
					,
					(q.noiseUV.y + _Time.y * _SufNoiseScroll.y) + DistSample.y
				);
				//표면 노이즈 표시
				float SufNoiseSample = tex2D(_SufNoise, noiseUV).r;

				//노멀변환
				float3 ExNormal = tex2Dproj(_CameraNormalsTexture, UNITY_PROJ_COORD(q.screenPosition));
				//NdotV
				float3 NdotV = saturate(dot(ExNormal, q.viewNormal));
				//거품크기제어
				float FoDistance = lerp(_FoDistanceMax, _FoDistanceMin, NdotV);

				//경계면 깊이 계산
				float FoDepthDiffer01 = saturate(DefDiffer / FoDistance);
				float SufNoiseCutoff = FoDepthDiffer01 * _SufNoiseCutoff;

				//표면 노이즈 컷오프
				float SufNoise = SufNoiseSample > SufNoiseCutoff ? _FoAlpha : 0; //노이즈샘플을 지정한값보다 클땐 1이고 작을땐 0으로 만듬
				float4 SufNoiseColor = _FoColor;
				SufNoiseColor.a *= SufNoise;

				//Franel 공식
				float Rim = dot(q.viewNormal, q.WorldRef);
				Rim = pow(1 - Rim, 1.5);
				//WaterColor.rgb = Rim;
				WaterColor.a = saturate(1 - Rim + _WaterAlpha);
				//SkyBox 받아오기
				half4 SkyCUBE = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, q.WorldRef);
				half3 SkyColor = DecodeHDR(SkyCUBE, unity_SpecCube0_HDR);
				Rim = Rim * SkyColor;
				
				WaterColor.rgb = WaterColor.rgb + (Rim * _SkyRefDistance);
				//WaterColor.rgb = SkyColor;	

				//알파블렌드구문 받아와서 계산
				float4 Out = alphaBlend(SufNoiseColor, WaterColor);
				return Out;
			}
			ENDCG
		}
    }
}
