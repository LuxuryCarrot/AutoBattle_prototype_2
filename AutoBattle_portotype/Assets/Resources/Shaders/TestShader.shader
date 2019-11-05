Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _AlbedoTex ("Albedo", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf MainShading noambient

        sampler2D _AlbedoTex;

        struct Input
        {
            float2 uv_AlbedoTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_AlbedoTex, IN.uv_AlbedoTex);

			o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

		float4 LightingMainShading(SurfaceOutput s, float3 lightDir, float atten)
		{
			float4 Output = 0;
			return Output;
		}
        ENDCG
    }
    FallBack "Diffuse"
}
