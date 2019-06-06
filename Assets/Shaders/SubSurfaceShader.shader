﻿Shader "Custom/SubSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Power ("Power", Range(1,10)) = 0.0
        _Scale ("Scale", Range(0,10)) = 0.0
        _Attenuation ("Attenuation", Range(0,1)) = 0.0
        _Ambient ("Ambient", Range(0,1)) = 0.0
        _Distortion ("Distortion", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf StandardTranslucent fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        
        #include "UnityPBSLighting.cginc"
        
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Power;
        float _Scale;
        float _Attenuation;
        float _Ambient;
        float _Distortion;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        inline void LightingStandardTranslucent_GI(SurfaceOutputStandard s, UnityGIInput data, inout UnityGI gi)
        {
         LightingStandard_GI(s, data, gi); 
        }
        inline fixed4 LightingStandardTranslucent(SurfaceOutputStandard s, fixed3 viewDir, UnityGI gi)
        {
         // Original colour
         fixed4 pbr = LightingStandard(s, viewDir, gi);
         
         //// --- Translucency ---
         float3 L = gi.light.dir;
         float3 V = viewDir;
         float3 N = s.Normal;
         
         float3 H = normalize(L + N * _Distortion);
         float VdotH = pow(saturate(dot(V, -H)), _Power) * _Scale;
         //float3 I = _Attenuation * (VdotH + _Ambient) * 0.5;
         
         //// Final add
         pbr.rgb = pbr.rgb + gi.light.color * VdotH;
         return pbr;
         
        }
        ENDCG
    }
    FallBack "Diffuse"
}
