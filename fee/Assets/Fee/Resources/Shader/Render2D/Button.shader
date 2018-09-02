/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/brownie/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダー。ボタン。
*/


Shader "Render2D/Button"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		clip_x1 ("Clip X1", Float) = 0
		clip_y1 ("Clip Y1", Float) = 0
		clip_x2 ("Clip X2", Float) = 0
		clip_y2 ("Clip Y2", Float) = 0
		mode ("Mode", int) = 0
		rect_w ("Rect W", Float) = 0
		rect_h ("Rect H", Float) = 0
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

			/** clip
			*/
			float clip_x1;
			float clip_y1;
			float clip_x2;
			float clip_y2;

			/** mode
			*/
			int mode;

			/** rect
			*/
			float rect_w;
			float rect_h;
		
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

				if(clip_x1>i.vertex.x){
					discard;
				}

				if(i.vertex.x>clip_x2){
					discard;
				}

				if(clip_y1>i.vertex.y){
					discard;
				}

				if(i.vertex.y>clip_y2){
					discard;
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
				if(t_to_gui_x < 20){
					//左２０ピクセル。
					t_tex_x = t_to_gui_x * t_pix_x;
				}else if(t_to_gui_x >= t_gui_w - 20){
					//右２０ピクセル。
					t_tex_x = (t_gui_w - t_to_gui_x) * t_pix_x;
				}else{
					//真ん中。
					float t_per = float(t_to_gui_x - 20) / float(t_gui_w - 40);
					t_tex_x = t_pix_x * (20 + t_per * (_MainTex_TexelSize.z / 2 - 40));
				}

				//テクスチャ参照位置。
				float t_tex_y;
				if(t_to_gui_y < 20){
					//上２０ピクセル。
					t_tex_y = t_to_gui_y * t_pix_y;
				}else if(t_to_gui_y >= t_gui_h - 20){
					//下２０ピクセル。
					t_tex_y = (t_gui_h - t_to_gui_y) * t_pix_y;
				}else{
					//真ん中。
					float t_per = float(t_to_gui_y - 20) / float(t_gui_h - 40);
					t_tex_y = t_pix_y * (20 + t_per * (_MainTex_TexelSize.w / 2 - 40));
				}
				
				//参照位置オフセット。
				float t_offset_x;
				float t_offset_y;
				if(mode == 0){
					//通常。
					t_offset_x = 0.0f;
					t_offset_y = 0.0f;
				}else if(mode == 1){
					//オン。
					t_offset_x = 0.5f;
					t_offset_y = 0.0f;
				}else if(mode == 2){
					//ダウン。
					t_offset_x = 0.0f;
					t_offset_y = 0.5f;
				}else if(mode == 3){
					//ロック。
					t_offset_x = 0.5f;
					t_offset_y = 0.5f;
				}

				return tex2D(_MainTex,float2(t_tex_x + t_offset_x,1.0f - t_tex_y - t_offset_y)) * i.color;
			}
			ENDCG
		}
	}
}

