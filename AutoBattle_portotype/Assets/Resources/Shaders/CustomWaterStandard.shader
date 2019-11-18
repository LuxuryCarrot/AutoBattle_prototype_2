Shader "Custom/CustomWaterStandard"
{
    Properties
    {
		//물의 깊이값 그래디언트 조절부분
		_DGShallow ("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725) //얕은물색
		_DGDeep ("Depth Gradient Deep", Color) = (0.886, 0.407, 1, 0.749) //깊은물색
		_DMDistance ("Depth Maximun Distance", Float) = 1 //깊이조절

		//움직임 노이즈 구문
		_SufNoise("Surface Noise", 2D) = "white" {} //움직임 노이즈
    }
    SubShader
    {
        Tags
		{
			"RenderType"="Transparent"
			"Queue"="Transparent"
		}

        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _SufNoise;

		//카메라 깊이값을 받아오는 구문
		sampler2D _CameraDepthTexture;

        struct Input
        {
            float2 uv_SufNoise;
        };


        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = 1;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
