using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。マウス。
*/


/** NInput
*/
namespace NInput
{
	/** Mouse
	*/
	public class Mouse
	{
		/** [シングルトン]s_instance
		*/
		private static Mouse s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Mouse();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Mouse GetInstance()
		{
			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** 位置。
		*/
		public Mouse_Pos pos;

		/** ボタン。
		*/
		public Mouse_Button left;
		public Mouse_Button right;

		/** マウスホイール。
		*/
		public bool mouse_wheel_flag;
		public float mouse_wheel;
		public float mouse_wheel_old;
		public bool mouse_wheel_action;

		/** [シングルトン]constructor
		*/
		private Mouse()
		{
			//位置。
			this.pos.Reset();

			//ボタン。
			this.left.Reset();
			this.right.Reset();

			//ホイール。
			this.mouse_wheel_flag = true;
			this.mouse_wheel = 0.0f;
			this.mouse_wheel_old = 0.0f;
			this.mouse_wheel_action = false;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** マウス表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			Cursor.visible = a_flag;
		}

		/** マウスロック。設定。
		*/
		public void SetLock(bool a_flag)
		{
			if(a_flag == true){
				Cursor.lockState = CursorLockMode.Locked;
			}else{
				Cursor.lockState = CursorLockMode.None;
			}
		}

		/** [外部からの呼び出し]更新。
		*/
		public void Main(NRender2D.Render2D a_render2d)
		{
			try{
				{
					//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
					int t_x;
					int t_y;
					a_render2d.GuiScreenToVirtualScreen((int)UnityEngine.Input.mousePosition.x,(int)(Screen.height - UnityEngine.Input.mousePosition.y),out t_x,out t_y);

					//設定。
					this.pos.Set(t_x,t_y);
				}

				//設定。
				this.left.Set(UnityEngine.Input.GetMouseButton(0));
				this.right.Set(UnityEngine.Input.GetMouseButton(1));

				//マウスホイール。
				{
					this.mouse_wheel_old = this.mouse_wheel;

					if(this.mouse_wheel_flag == true){
						try{
							this.mouse_wheel = UnityEngine.Input.GetAxis(Config.MOUSE_INPUTNAME_WHEEL);
						}catch(System.Exception /*t_exception*/){
							//インプットマネージャで登録が必要。
							Tool.Log("Mouse","ERROR : " + Config.MOUSE_INPUTNAME_WHEEL);
							this.mouse_wheel_flag = false;
						}
					}

					if((this.mouse_wheel != 0.0f)&&(this.mouse_wheel != this.mouse_wheel_old)){
						this.mouse_wheel_action = true;
					}else{
						this.mouse_wheel_action = false;
					}
				}

				//更新。
				this.left.Main(ref this.pos);
				this.right.Main(ref this.pos);
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** 範囲チェック。
		*/
		public bool InRectCheck(int a_x,int a_y,int a_w,int a_h)
		{
			if((a_x <= this.pos.x) && (a_y <= this.pos.y)){
				if(((a_x + a_w) >= this.pos.x) && ((a_y + a_h) >= this.pos.y)){
					return true;
				}
			}
			return false;
		}

		/** 範囲チェック。
		*/
		public bool InRectCheck(ref NRender2D.Rect2D_R<int> a_rect)
		{
			if((a_rect.x <= this.pos.x) && (a_rect.y <= this.pos.y)){
				if(((a_rect.x + a_rect.w) >= this.pos.x) && ((a_rect.y + a_rect.h) >= this.pos.y)){
					return true;
				}
			}
			return false;
		}

		/** 移動チェック。左ボタンドラッグアップ時。
		*/
		public Dir4Type LeftDragUpMoveCheck()
		{
			if((this.left.up == true)&&(this.left.drag_dir_magnitude >= Config.DRAGUP_LENGTH_MIN)&&(this.left.drag_totallength <= (this.left.drag_dir_magnitude * Config.DRAGUP_LENGTH_SCALE))){
				{
					float t_dot = Vector2.Dot(this.left.drag_dir_normalized,Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = Vector2.Dot(this.left.drag_dir_normalized,Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = Vector2.Dot(this.left.drag_dir_normalized,Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = Vector2.Dot(this.left.drag_dir_normalized,Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}

		/** 移動チェック。左ボタンドラッグオン時。
		*/
		public Dir4Type LeftDragOnMoveCheck()
		{
			if((this.left.on == true)&&(this.left.drag_dir_magnitude >= Config.DRAGON_LENGTH_MIN)){
				{
					float t_dot = Vector2.Dot(this.left.drag_dir_normalized,Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = Vector2.Dot(this.left.drag_dir_normalized,Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = Vector2.Dot(this.left.drag_dir_normalized,Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = Vector2.Dot(this.left.drag_dir_normalized,Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}

		/** 移動チェック。左ボタンドラッグアップ時。
		*/
		public Dir4Type RightDragUpMoveCheck()
		{
			if((this.right.up == true)&&(this.right.drag_dir_magnitude >= Config.DRAGUP_LENGTH_MIN)&&(this.right.drag_totallength <= (this.right.drag_dir_magnitude * Config.DRAGUP_LENGTH_SCALE))){
				{
					float t_dot = Vector2.Dot(this.right.drag_dir_normalized,Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = Vector2.Dot(this.right.drag_dir_normalized,Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = Vector2.Dot(this.right.drag_dir_normalized,Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = Vector2.Dot(this.right.drag_dir_normalized,Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}

		/** 移動チェック。左ボタンドラッグオン時。
		*/
		public Dir4Type RightDragOnMoveCheck()
		{
			if((this.right.on == true)&&(this.right.drag_dir_magnitude >= Config.DRAGON_LENGTH_MIN)){
				{
					float t_dot = Vector2.Dot(this.right.drag_dir_normalized,Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = Vector2.Dot(this.right.drag_dir_normalized,Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = Vector2.Dot(this.right.drag_dir_normalized,Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = Vector2.Dot(this.right.drag_dir_normalized,Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}
	}
}

