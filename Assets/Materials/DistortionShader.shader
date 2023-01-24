Shader "Custom/DistortionShader"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _DistortionTex("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 200
        
        GrabPass{}
        
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _DistortionTex;
        sampler2D _GrabTexture;

        struct Input
        {
            float uv_DistortionTex;
            float4 screenPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_DistortionTex, IN.uv_DistortionTex + _Time.x);
            //o.Albedo = c.rgb;
            float2 screenUV = IN.screenPos.rgb / IN.screenPos.a;
            screenUV = float2(screenUV.r, screenUV.g);
            o.Emission = tex2D(_GrabTexture, screenUV + c.r * 0.01);
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Regacy shaders/Transparent/Diffuse"
}
