

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。矩形ツール。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** RectTool
	*/
	public class RectTool
	{
		/** 範囲内チェック。
		*/
		public static bool IsRectIn(ref Rect2D_R<int> a_rect,ref Pos2D<int> a_pos)
		{
			if((a_rect.x<=a_pos.x)&&(a_pos.x<=(a_rect.x + a_rect.w))&&(a_rect.y<=a_pos.y)&&(a_pos.y<=(a_rect.y + a_rect.h))){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public static bool IsRectIn(ref Rect2D_R<long> a_rect,ref Pos2D<long> a_pos)
		{
			if((a_rect.x<=a_pos.x)&&(a_pos.x<=(a_rect.x + a_rect.w))&&(a_rect.y<=a_pos.y)&&(a_pos.y<=(a_rect.y + a_rect.h))){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public static bool IsRectIn(ref Rect2D_R<float> a_rect,ref Pos2D<float> a_pos)
		{
			if((a_rect.x<=a_pos.x)&&(a_pos.x<=(a_rect.x + a_rect.w))&&(a_rect.y<=a_pos.y)&&(a_pos.y<=(a_rect.y + a_rect.h))){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public static bool IsRectIn(ref Rect2D_R<int> a_rect,int a_pos_x,int a_pos_y)
		{
			if((a_rect.x<=a_pos_x)&&(a_pos_x<=(a_rect.x + a_rect.w))&&(a_rect.y<=a_pos_y)&&(a_pos_y<=(a_rect.y + a_rect.h))){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public static bool IsRectIn(ref Rect2D_R<long> a_rect,long a_pos_x,long a_pos_y)
		{
			if((a_rect.x<=a_pos_x)&&(a_pos_x<=(a_rect.x + a_rect.w))&&(a_rect.y<=a_pos_y)&&(a_pos_y<=(a_rect.y + a_rect.h))){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public static bool IsRectIn(ref Rect2D_R<float> a_rect,float a_pos_x,float a_pos_y)
		{
			if((a_rect.x<=a_pos_x)&&(a_pos_x<=(a_rect.x + a_rect.w))&&(a_rect.y<=a_pos_y)&&(a_pos_y<=(a_rect.y + a_rect.h))){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public static bool IsRectIn(int a_rect_x,int a_rect_y,int a_rect_w,int a_rect_h,ref Pos2D<int> a_pos)
		{
			if((a_rect_x<=a_pos.x)&&(a_pos.x<=(a_rect_x + a_rect_w))&&(a_rect_y<=a_pos.y)&&(a_pos.y<=(a_rect_y + a_rect_h))){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public static bool IsRectIn(long a_rect_x,long a_rect_y,long a_rect_w,long a_rect_h,ref Pos2D<long> a_pos)
		{
			if((a_rect_x<=a_pos.x)&&(a_pos.x<=(a_rect_x + a_rect_w))&&(a_rect_y<=a_pos.y)&&(a_pos.y<=(a_rect_y + a_rect_h))){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public static bool IsRectIn(float a_rect_x,float a_rect_y,float a_rect_w,float a_rect_h,ref Pos2D<float> a_pos)
		{
			if((a_rect_x<=a_pos.x)&&(a_pos.x<=(a_rect_x + a_rect_w))&&(a_rect_y<=a_pos.y)&&(a_pos.y<=(a_rect_y + a_rect_h))){
				return true;
			}
			return false;
		}
	}
}

