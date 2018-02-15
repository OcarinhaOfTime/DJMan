Shader "Custom/Tile"
{
    Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [PerRendererData] _MainTex_SO("Scale and Offset", Vector) = (1,1,0,0)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
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

			sampler2D _MainTex;
            float4 _MainTex_SO;

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
				return i.color * tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}
}