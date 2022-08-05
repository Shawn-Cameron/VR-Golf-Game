Shader "MyShaders/Flag"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Amplitude ("Amplitude", Float) = 1
        _Wavelength ("Wavelength", Float) = 5
        _Speed ("Speed", Float) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Amplitude;
        float _Wavelength;
        float _Speed;


        void vert(inout appdata_full vertexData){
            float3 pos = vertexData.vertex.xyz;
            float k = 2 * UNITY_PI/ _Wavelength;
            float f = k * (pos.x - _Speed * _Time.y);
            pos.y = _Amplitude * sin(f) * (1 - vertexData.texcoord.xy);

            float3 tangent = normalize(float3(1, k * _Amplitude * cos(f), 0));
            float3 normal = float3(-tangent.y, tangent.x, 0);
            vertexData.normal = normal;
            vertexData.vertex.xyz = pos;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
