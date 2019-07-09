// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/HeartFillOutline"
{
    Properties
    {
       [Header(Base Parameters)]
        _Color ("Tint", Color) = (1, 1, 1, 1)
        _AmbientLightColor ("Ambient Light Color", Color) = (1, 1, 1, 1)
        _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
        _RimAmount ("Rim Amount", Range(0, 2)) = 1.2
        _FillAmount ("Fill Amount", Range(0, 1)) = 0
        _FillColor ("Fill Color", Color) = (1, 1, 1, 1)
        _BlinkColor ("Blink Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200
    CGPROGRAM
    #pragma surface surf Toon fullforwardshadows vertex:vert
    

    float _StepWidth;
    float _RimAmount;
    float _FillAmount;
    fixed4 _FillColor;
    fixed4 _RimColor;
    fixed4 _Color;
    fixed4 _AmbientLightColor;
    
    struct Input {
        float2 uv_MainTex;
        float3 vPos;
    };
    
    sampler2D _MainTex;
    
    void vert (inout appdata_full v, out Input o) {
        UNITY_INITIALIZE_OUTPUT(Input,o);
        o.uv_MainTex = v.texcoord;
        o.vPos = v.vertex;
        
        float blinkIntensity = 1 - _FillAmount;
        float b = blinkIntensity * abs(sin((1 + blinkIntensity) * _Time.y));
            
        v.vertex.xyz = v.vertex.xyz - 0.15 * b * (abs(v.vertex.y) - sin(5.5 + v.vertex.z)) * v.normal.xyz;
    }
       
    void surf (Input IN, inout SurfaceOutput o) {
        //o.Albedo = _Color.rgb * tex2D (_MainTex, IN.uv_MainTex).rgb;
        float z = (IN.vPos.z+2.328704/2)/2.328704;
        float fill = ceil(max(z-_FillAmount,0));
        o.Albedo =  fill * _Color.rgb + (1.0-fill) * _FillColor.rgb;
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
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members finalColor)
#pragma exclude_renderers d3d11
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        struct v2f {
            float4 pos : POSITION;
            float4 finalColor: TEXCOORD1;
        };
        
        float _FillAmount;
        fixed4 _BlinkColor;
        

        v2f vert (appdata_full v)
        {
            v2f o;
            float blinkIntensity = 1 - _FillAmount;
            float b = blinkIntensity * abs(sin((1 + blinkIntensity) * _Time.y));
            v.vertex.xyz = v.vertex.xyz - 0.15 * b * (abs(v.vertex.y) - sin(5.5 + v.vertex.z)) * v.normal.xyz;
            
            o.pos = UnityObjectToClipPos(v.vertex+0.1*v.normal);   
            o.finalColor = b * _BlinkColor;
            return o;
        }

        half4 frag( v2f i ) : COLOR
        {
            return i.finalColor;
        }
        ENDCG          
    }
    
    }
    FallBack "Diffuse"
}
