 //Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Post Outline"
{
    Properties
    {
        _MainTex("Main Texture",2D)="black"{}
        _SceneTex("Scene Texture",2D)="black"{}
        _PaletteTex("Palette",2D)="black"{}
        _Delta ("Delta", Range(0, 1)) = 0.025
        _VignetteAmount ("Vignette Amount", Range(0, 10)) = 0
        _VignetteColor ("Vignette Color", Color) = (1,1,1,1)
        _BlinkAmount ("Blink Amount", Range(0, 500)) = 0
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
            float _VignetteAmount;
            fixed4 _VignetteColor;
            float _BlinkAmount;
            

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

                float r2 = (i.uv.x - 0.5) * (i.uv.x - 0.5) + (i.uv.y - 0.5) * (i.uv.y - 0.5);
                float2 inputDistort = i.uv;
                
                /***  DISTORTION ***/
                //float k = -0.15;
                //float f = 0;
                //float _Distortion = -1+sin(_Time.y);
                ////only compute the cubic distortion if necessary
                //if (_Distortion == 0.0)
                //{
                //    f = 1 + r2 * k;
                //}
                //else 
                //{
                //    f = 1 + r2 * (k + _Distortion * sqrt(r2));
                //};
                //// get the right pixel for the current position
                //float x = f*(i.uv.x - 0.5) + 0.5;
                //float y = f*(i.uv.y - 0.5) + 0.5;
                //float2 inputDistort = float2(x,y);
                
                /***  VIGNETTE ***/
                float vig = pow(r2,2);
                
                /***  BLINK ***/
                float blink = clamp(abs(i.uv.y-0.5)+_BlinkAmount,0,1);
                
                /***  OUTLINE ***/
                //float d = tex2D(_CameraDepthTexture, inputDistort).r;
                //float vdepth = LinearEyeDepth(d);
                //float2 adjustedUvs = UnityStereoTransformScreenSpaceTex(inputDistort);
                //float s = pow(1 - saturate(sobel(adjustedUvs)), 50);
                 //s+=pow(2,(vdepth-15)/35);
                //if(vdepth > 15){
                    //s+=pow(2,(vdepth-30)/5);
                //}
                
                /***  COLOR PALETTE ***/
                //half4 col = pow(tex2D(_MainTex, i.uv),0.4545);
                //float brightness = 0.2126 * col.r + 0.7152 * col.g + 0.0722 * col.b;
                //col = tex2D(_PaletteTex, float2(brightness, 0.1));
                
                
                /*** FINAL COMPOSITE ***/
                half4 col = tex2D(_MainTex, inputDistort);
                col += _VignetteAmount*vig*_VignetteColor;
                return (1-blink)*col;
            }
             
            ENDCG
 
        }  
    }
    //end subshader
}
//end shader