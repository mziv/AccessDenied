﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/DarkenEffect"
{
	 Properties {
        _Color ("Main Color", Color) = (0.5,0.5,0.5,0.5)
        _MainTex ("Texture", 2D) = "white" { }
    }
    SubShader {
        Tags
        {
          "Queue" = "Transparent"
        }

        Blend One OneMinusSrcAlpha

        Pass {

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        fixed4 _Color;
        sampler2D _MainTex;

        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        float4 _MainTex_ST;

        v2f vert (appdata_base v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos (v.vertex);
            o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
            return o;
        }

         fixed4 frag (v2f i) : SV_Target
        {
            fixed4 texcol = tex2D (_MainTex, i.uv);
            fixed4 outputColor = _Color;
            //outputColor.a -= texcol.r;
            //return outputColor ;
			return texcol -= (_Color * 0.1);
        }
        ENDCG

        }
    }
}