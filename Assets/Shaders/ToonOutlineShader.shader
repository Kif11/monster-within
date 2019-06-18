﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/ToonOutline"
{
    Properties
    {
       [Header(Base Parameters)]
        _Color ("Tint", Color) = (1, 1, 1, 1)
        _AmbientLightColor ("Ambient Light Color", Color) = (1, 1, 1, 1)
        _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
        _RimAmount ("Rim Amount", Range(0, 2)) = 1.2
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

    CGPROGRAM
    #pragma surface surf Toon fullforwardshadows

    float _StepWidth;
    float _RimAmount;
    fixed4 _RimColor;
    fixed4 _Color;
    fixed4 _AmbientLightColor;
    
    struct Input {
        float2 uv_MainTex;
    };
    
    sampler2D _MainTex;
    
    void surf (Input IN, inout SurfaceOutput o) {
        o.Albedo = _Color.rgb * tex2D (_MainTex, IN.uv_MainTex).rgb;
    }
    
    half4 LightingToon (SurfaceOutput s, fixed3 viewDir, UnityGI gi) {
        half NdotL = dot (s.Normal, gi.light.dir);
        float lightIntensity = smoothstep(0, 0.05, NdotL);
        float3 lightColor = lightIntensity * gi.light.color;
        
        float4 rimDot = 1 - dot(viewDir, s.Normal);
        float rimIntensity = rimDot * _RimAmount * pow( NdotL, 3.0);

        rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
        float4 rim = rimIntensity * _RimColor;

        half4 c;
        c.rgb = s.Albedo * (lightColor + (1-lightIntensity)*_AmbientLightColor + rim.rgb);
        c.a = 1.0;
        
        return c;
    }
    
    inline void LightingToon_GI(SurfaceOutput s, UnityGIInput data, inout UnityGI gi)
    {
    }
    ENDCG
    
    Pass
    {
        Name "Outline"
        Cull front
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        struct v2f {
            float4 pos : POSITION;
        };

        v2f vert (appdata_full v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex+0.04*v.normal);   
            return o;
        }

        half4 frag( v2f i ) : COLOR
        {
            return half4(0,0,0,0);
        }
        ENDCG          
    }
    
    }
    FallBack "Diffuse"
}
