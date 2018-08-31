using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。
*/


/** NInput
*/
namespace NInput
{
	/** Input
	*/
	public class Input : Config
	{
		/** constructor
		*/
		private Input()
		{
		}

		/** マウス範囲チェック。
		*/
		public static bool MouseRectCheck(Mouse a_mouse,int a_x,int a_y,int a_w,int a_h)
		{
			if((a_x <= a_mouse.pos.x) && (a_y <= a_mouse.pos.y)){
				if(((a_x + a_w) >= a_mouse.pos.x) && ((a_y + a_h) >= a_mouse.pos.y)){
					return true;
				}
			}
			return false;
		}

		/** キー移動チェック。ダウン時。
		*/
		public static Dir4Type KeyDownMoveCheck(Key a_key)
		{
			if(a_key.up.down == true){
				return Dir4Type.Up;
			}else if(a_key.down.down == true){
				return Dir4Type.Down;
			}else if(a_key.left.down == true){
				return Dir4Type.Left;
			}else if(a_key.right.down == true){
				return Dir4Type.Right;
			}

			return Dir4Type.None;
		}

		/** キー移動チェック。オン時。
		*/
		public static Dir4Type KeyOnMoveCheck(Key a_key)
		{
			if(a_key.up.on == true){
				return Dir4Type.Up;
			}else if(a_key.down.on == true){
				return Dir4Type.Down;
			}else if(a_key.left.on == true){
				return Dir4Type.Left;
			}else if(a_key.right.on == true){
				return Dir4Type.Right;
			}

			return Dir4Type.None;
		}

		/** キー移動チェック。ダウン時。
		*/
		public static Dir4Type KeyDownMoveCheck(Joy a_key)
		{
			if(a_key.up.down == true){
				return Dir4Type.Up;
			}else if(a_key.down.down == true){
				return Dir4Type.Down;
			}else if(a_key.left.down == true){
				return Dir4Type.Left;
			}else if(a_key.right.down == true){
				return Dir4Type.Right;
			}

			return Dir4Type.None;
		}

		/** キー移動チェック。オン時。
		*/
		public static Dir4Type KeyOnMoveCheck(Joy a_key)
		{
			if(a_key.up.on == true){
				return Dir4Type.Up;
			}else if(a_key.down.on == true){
				return Dir4Type.Down;
			}else if(a_key.left.on == true){
				return Dir4Type.Left;
			}else if(a_key.right.on == true){
				return Dir4Type.Right;
			}

			return Dir4Type.None;
		}

		/** ドラッグ移動チェック。アップ時。
		*/
		public static Dir4Type DragUpMoveCheck(Mouse a_mouse)
		{
			if((a_mouse.left.up == true)&&(a_mouse.left.drag_dir_magnitude >= Config.DRAGUP_LENGTH_MIN)&&(a_mouse.left.drag_totallength <= (a_mouse.left.drag_dir_magnitude * Config.DRAGUP_LENGTH_SCALE))){
				{
					float t_dot = Vector2.Dot(a_mouse.left.drag_dir_normalized,Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = Vector2.Dot(a_mouse.left.drag_dir_normalized,Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = Vector2.Dot(a_mouse.left.drag_dir_normalized,Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = Vector2.Dot(a_mouse.left.drag_dir_normalized,Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}

		/** ドラッグ移動チェック。オン時。
		*/
		public static Dir4Type DragOnMoveCheck(Mouse a_mouse)
		{
			if((a_mouse.left.on == true)&&(a_mouse.left.drag_dir_magnitude >= Config.DRAGON_LENGTH_MIN)){
				{
					float t_dot = Vector2.Dot(a_mouse.left.drag_dir_normalized,Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = Vector2.Dot(a_mouse.left.drag_dir_normalized,Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = Vector2.Dot(a_mouse.left.drag_dir_normalized,Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = Vector2.Dot(a_mouse.left.drag_dir_normalized,Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}
	}
}

