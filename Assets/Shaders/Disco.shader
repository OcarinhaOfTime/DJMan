Shader "Custom/Music"
{
    Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [PerRendererData] _MainTex_SO("Scale and Offset", Vector) = (1,1,0,0)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_Mask ("Mask", 2D) = "white" {}
        _Brightness("Brightness", Float) = 1
        _DanceAmmount("Brightness", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				half4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				half4 color : COLOR;
			};

            uniform half _Band[8];
            uniform half3 _BandColors[8];
			half _Brightness;
			half _DanceAmmount;
			sampler2D _MainTex;
            float4 _MainTex_SO;
			sampler2D _Mask;

            float2 TransformTex(float2 uv, float4 st){
                return uv * st.xy + st.zw;
            }

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TransformTex(v.uv, _MainTex_SO);
				o.color = v.color;
				return o;
			}			

			float4 frag (v2f i) : SV_Target
			{
				half4 tex = tex2D(_MainTex, i.uv);
                half4 col = half4(0, 0, 0, 1);
				half mask = tex2D(_Mask, i.uv).r;
				int ind = lerp(0, 7.9, mask);
				col.rgb = _BandColors[ind] * _Band[ind]* _Brightness;
                // for(int i=0; i<8; i++){
                //     col.rgb += _BandColors[i] * _Band[i]* _Brightness;
                // }
				return lerp(tex, col, _DanceAmmount);
			}
			ENDCG
		}
	}
}