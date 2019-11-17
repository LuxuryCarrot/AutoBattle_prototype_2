Shader "Custom/CustomWater"
{
    Properties
    {
		//물의 깊이값 그래디언트 조절부분
		_DGShallow ("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725) //얕은물색
		_DGDeep ("Depth Gradient Deep", Color) = (0.886, 0.407, 1, 0.749) //깊은물색
		_DMDistance ("Depth Maximun Distance", Float) = 1 //깊이조절

		//삭제구문
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard
		#pragma target 3.0

		//Depth Gradient 선언
		float4 _DGshallow;
		float4 _DGDeep;
		float _DMDistance;

		//카메라 깊이값을 받아오는 구문
		sampler2D _CameraDepthTexture;

		//삭제구문
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };


		//삭제구문
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			//삭제구문
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
