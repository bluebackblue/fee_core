

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ワイヤーフレーム。
*/


Shader "Fee/WireFrame/WireFrame"
{
	Properties
	{
		_Color("_Color",Color) = (1,1,1,1)
		limit("limit",Range(0.0,1.0)) = 0.02
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		Pass
		{
			//パス１。

			Cull Back
			ZTest LEqual
			ZWrite On

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom

			/** include
			*/
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex		: POSITION;
			};

			/** v2g
			*/
			struct v2g
			{
				float4 vertex		: SV_POSITION;
			};
			
			/** g2f
			*/
			struct g2f
			{
				float4 vertex		: SV_POSITION;
				float3 abc			: TEXCOORD0;
			};

			/** _Color
			*/
			fixed4 _Color;

			/** limit
			*/
			float limit;

			/** vert
			*/
			v2g vert(appdata a_appdata)
			{
				v2g t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
				}
				return t_ret;
			}

			/** geom
			*/
			[maxvertexcount(3)]
			void geom(triangle v2g a_v2g_list[3],inout TriangleStream<g2f> a_out_list)
			{
				{
					g2f t_ret;
					t_ret.vertex = a_v2g_list[0].vertex;
					t_ret.abc = float3(1.0f,0.0f,0.0f);
					a_out_list.Append(t_ret);
				}

				{
					g2f t_ret;
					t_ret.vertex = a_v2g_list[1].vertex;
					t_ret.abc = float3(0.0f,1.0f,0.0f);
					a_out_list.Append(t_ret);
				}

				{
					g2f t_ret;
					t_ret.vertex = a_v2g_list[2].vertex;
					t_ret.abc = float3(0.0f,0.0f,1.0f);
					a_out_list.Append(t_ret);
				}
			}

			/** frag
			*/
			fixed4 frag(g2f a_g2f) : SV_Target
			{
				if((a_g2f.abc.x < limit)||(a_g2f.abc.y < limit)||(a_g2f.abc.z < limit)){
				}else{
					discard;
				}
				return _Color;
			}

			ENDCG
		}
		Pass
		{
			//パス２。

			Cull Front
			ZTest LEqual
			ZWrite On

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom

			/** include
			*/
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex		: POSITION;
			};

			/** v2g
			*/
			struct v2g
			{
				float4 vertex		: SV_POSITION;
			};
			
			/** g2f
			*/
			struct g2f
			{
				float4 vertex		: SV_POSITION;
				float3 abc			: TEXCOORD0;
			};

			/** _Color
			*/
			fixed4 _Color;

			/** limit
			*/
			float limit;

			/** vert
			*/
			v2g vert(appdata a_appdata)
			{
				v2g t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
				}
				return t_ret;
			}

			/** geom
			*/
			[maxvertexcount(3)]
			void geom(triangle v2g a_v2g_list[3],inout TriangleStream<g2f> a_out_list)
			{
				{
					g2f t_ret;
					t_ret.vertex = a_v2g_list[0].vertex;
					t_ret.abc = float3(1.0f,0.0f,0.0f);
					a_out_list.Append(t_ret);
				}

				{
					g2f t_ret;
					t_ret.vertex = a_v2g_list[1].vertex;
					t_ret.abc = float3(0.0f,1.0f,0.0f);
					a_out_list.Append(t_ret);
				}

				{
					g2f t_ret;
					t_ret.vertex = a_v2g_list[2].vertex;
					t_ret.abc = float3(0.0f,0.0f,1.0f);
					a_out_list.Append(t_ret);
				}
			}

			/** frag
			*/
			fixed4 frag(g2f a_g2f) : SV_Target
			{
				if((a_g2f.abc.x < limit)||(a_g2f.abc.y < limit)||(a_g2f.abc.z < limit)){
				}else{
					discard;
				}
				return _Color;
			}

			ENDCG
		}
	}
}

