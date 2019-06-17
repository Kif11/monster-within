// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Post Outline"
{
    Properties
    {
        _MainTex("Main Texture",2D)="black"{}
        _SceneTex("Scene Texture",2D)="black"{}
        _PaletteTex("Palette",2D)="black"{}
        _Delta ("Delta", Range(0, 1)) = 0.025
    }
    SubShader 
    {
        ZTest Always
        ZWrite Off
        Cull Off
        Pass 
        {
            CGPROGRAM
     
            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            sampler2D _PaletteTex;


            //<SamplerName>_TexelSize is a float2 that says how much screen space a texel occupies.
            float2 _MainTex_TexelSize;
            float _Delta;
            
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

           
            float SampleDepth(float2 uv) {
               float d = tex2D(_CameraDepthTexture, uv).r;
               float depth = Linear01Depth(d);
               return depth;
            }
            
            float sobel (float2 uv) 
            {
                float2 delta = float2(0.002, 0.002);
                
                float hr = 0;
                float vt = 0;
                
                hr += SampleDepth(uv + float2(-1.0, -1.0) * delta) *  1.0;
                hr += SampleDepth(uv + float2( 1.0, -1.0) * delta) * -1.0;
                hr += SampleDepth(uv + float2(-1.0,  0.0) * delta) *  2.0;
                hr += SampleDepth(uv + float2( 1.0,  0.0) * delta) * -2.0;
                hr += SampleDepth(uv + float2(-1.0,  1.0) * delta) *  1.0;
                hr += SampleDepth(uv + float2( 1.0,  1.0) * delta) * -1.0;
                
                vt += SampleDepth(uv + float2(-1.0, -1.0) * delta) *  1.0;
                vt += SampleDepth(uv + float2( 0.0, -1.0) * delta) *  2.0;
                vt += SampleDepth(uv + float2( 1.0, -1.0) * delta) *  1.0;
                vt += SampleDepth(uv + float2(-1.0,  1.0) * delta) * -1.0;
                vt += SampleDepth(uv + float2( 0.0,  1.0) * delta) * -2.0;
                vt += SampleDepth(uv + float2( 1.0,  1.0) * delta) * -1.0;
                
                return sqrt(hr * hr + vt * vt);
            }
            half4 frag(v2f_img i) : COLOR 
            {
            
                float d = tex2D(_CameraDepthTexture, i.uv).r;
                float vdepth = LinearEyeDepth(d);
                
                float2 adjustedUvs = UnityStereoTransformScreenSpaceTex(i.uv);
                float s = clamp(pow(1 - saturate(sobel(adjustedUvs)), 50) + pow(2.0, 0.02*vdepth) - 1.0,0,1);
  
                //color palette code 
                //half4 col = pow(tex2D(_MainTex, i.uv),0.4545);
                //float brightness = 0.2126 * col.r + 0.7152 * col.g + 0.0722 * col.b;
                //col = tex2D(_PaletteTex, float2(brightness, 0.1));

                half4 col = tex2D(_MainTex, i.uv);

                return s*col;
            }
             
            ENDCG
 
        }  
    }
    //end subshader
}
//end shader