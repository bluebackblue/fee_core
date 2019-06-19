/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief シェーダ。ワイヤーフレーム。
*/


Shader "Fee/Wireframe/Wireframe"
{
	Properties
	{
		/** _MainTex
		*/
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		/** パス１。
		*/
		Pass
		{
			//Cull Front
			Cull Back

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom

			/** include
			*/
			#include "UnityCG.cginc"
			#include "WireFrame.cginc"

			STRUCT_DEF

			/** vert
			*/
			v2g vert(appdata a_appdata)
			{
				VERT_PROC(a_appdata)
			}

			/** geom
			*/
			[maxvertexcount(3)]
			void geom(triangle v2g a_v2g_list[3],inout TriangleStream<g2f> a_out_list)
			{
				GEO_PROC(a_v2g_list,a_out_list)
			}

			/** frag
			*/
			fixed4 frag(g2f a_g2f) : SV_Target
			{
				FRAG_PROC(a_g2f)
			}

			ENDCG
		}

		/** パス２。
		*/
		Pass
		{
			Cull Front
			//Cull Back

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom

			/** include
			*/
			#include "UnityCG.cginc"
			#include "WireFrame.cginc"

			STRUCT_DEF

			/** vert
			*/
			v2g vert(appdata a_appdata)
			{
				VERT_PROC(a_appdata)
			}

			/** geom
			*/
			[maxvertexcount(3)]
			void geom(triangle v2g a_v2g_list[3],inout TriangleStream<g2f> a_out_list)
			{
				GEO_PROC(a_v2g_list,a_out_list)
			}

			/** frag
			*/
			fixed4 frag(g2f a_g2f) : SV_Target
			{
				FRAG_PROC(a_g2f)
			}

			ENDCG
		}
	}
}

