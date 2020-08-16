

/**  @brief グラフィック。テクスチャ。
*/


Shader "Fee/Graphic/Texture_CoffZaoff"
{
	Properties
	{
		_MainTex				("_MainTex",2D)					= "white"{}
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		Pass
		{
			Cull Off
			ZTest Always
			ZWrite Off

			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "ShaderTool.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex			: POSITION;
				float2 uv				: TEXCOORD0;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex			: SV_POSITION;
				float2 uv				: TEXCOORD0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.uv = a_appdata.uv;
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				fixed4 t_color = tex2D(_MainTex,a_v2f.uv);
				return t_color;
			}

			ENDCG
		}
	}
}

