/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。９スライス。
*/


Shader "Render2D/Slice9"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		[MaterialToggle] clip_flag ("Clip Flag", Int) = 0
		clip_x1 ("Clip X1", Float) = 0
		clip_y1 ("Clip Y1", Float) = 0
		clip_x2 ("Clip X2", Float) = 0
		clip_y2 ("Clip Y2", Float) = 0
		corner_size ("Corner Size", Int) = 20
		rect_w ("Rect W", Float) = 0
		rect_h ("Rect H", Float) = 0
		texture_x ("Texture X", Float) = 0
		texture_y ("Texture Y", Float) = 0
		texture_w ("Texture W", Float) = 0.5
		texture_h ("Texture H", Float) = 0.5
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
		Cull Off
		ZWrite Off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_ST;

			/**

			x : 1.0f / width
			y : 1.0f / height
			z : width
			w : height

			*/
			float4 _MainTex_TexelSize;

			/** clip_flag
			*/
			int clip_flag;

			/** clip
			*/
			float clip_x1;
			float clip_y1;
			float clip_x2;
			float clip_y2;

			/** corner_size
			*/
			int corner_size;

			/** 描画先サイズ。
			*/
			float rect_w;
			float rect_h;

			/** テクスチャ矩形[0.0 - 1.0]。
			*/
			float texture_x;
			float texture_y;
			float texture_w;
			float texture_h;
		
			/** vert
			*/
			v2f vert(appdata v)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(v.vertex);
					t_ret.uv = TRANSFORM_TEX(v.uv,_MainTex);
					t_ret.color = v.color;
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f i) : SV_Target
			{
				//クリップ。
				if(clip_flag > 0){
					if(clip_x1>i.vertex.x){
						discard;
					}

					if(i.vertex.x>clip_x2){
						discard;
					}

					#if(UNITY_UV_STARTS_AT_TOP)
					if(clip_y2>i.vertex.y){
						discard;
					}

					if(i.vertex.y>clip_y1){
						discard;
					}
					#else
					if((_ScreenParams.y - clip_y1)>i.vertex.y){
						discard;
					}

					if(i.vertex.y>(_ScreenParams.y - clip_y2)){
						discard;
					}
					#endif
				}

				//１ピクセルのサイズ。
				float t_pix_x = 1.0 / _MainTex_TexelSize.z;
				float t_pix_y = 1.0 / _MainTex_TexelSize.w;

				//描画位置。
				int t_to_gui_x = i.uv.x * rect_w;
				int t_to_gui_y = (1.0 - i.uv.y) * rect_h;

				//描画先サイズ。
				int t_gui_w = rect_w;
				int t_gui_h = rect_h;

				//テクスチャ参照位置。
				float t_tex_x;
				if(t_to_gui_x < corner_size){
					//左corner_sizeピクセル。
					t_tex_x = t_to_gui_x * t_pix_x;
				}else if(t_to_gui_x >= t_gui_w - corner_size){
					//右corner_sizeピクセル。
					t_tex_x = (t_gui_w - t_to_gui_x) * t_pix_x;
				}else{
					//真ん中。
					float t_per = float(t_to_gui_x - corner_size) / float(t_gui_w - corner_size*2);
					t_tex_x = t_pix_x * (corner_size + t_per * (_MainTex_TexelSize.z * texture_w - corner_size*2));
				}

				//テクスチャ参照位置。
				float t_tex_y;
				if(t_to_gui_y < corner_size){
					//上corner_sizeピクセル。
					t_tex_y = t_to_gui_y * t_pix_y;
				}else if(t_to_gui_y >= t_gui_h - corner_size){
					//下corner_sizeピクセル。
					t_tex_y = (t_gui_h - t_to_gui_y) * t_pix_y;
				}else{
					//真ん中。
					float t_per = float(t_to_gui_y - corner_size) / float(t_gui_h - corner_size*2);
					t_tex_y = t_pix_y * (corner_size + t_per * (_MainTex_TexelSize.w * texture_h - corner_size*2));
				}
				
				//参照位置オフセット。
				float t_offset_x = texture_x;
				float t_offset_y = texture_y;

				return tex2D(_MainTex,float2(t_tex_x + t_offset_x,1.0f - t_tex_y - t_offset_y)) * i.color;
			}
			ENDCG
		}
	}
}

