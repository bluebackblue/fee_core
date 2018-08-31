using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。ジョイスティック。
*/


//The variable "" is assigned but its value is never used.
#pragma warning disable 0219


/** NInput
*/
namespace NInput
{
	/** Joy

	button0   = (WindowsPS)□(left)
	button1   = (WindowsPS)×(down)
	button2   = (WindowsPS)〇(right)
	button3   = (WindowsPS)△(up)
	button4   = (WindowsPS)L1
	button5   = (WindowsPS)R1
	button6   = (WindowsPS)L2
	button7   = (WindowsPS)R2
	button8   = (WindowsPS)SHARE
	button9   = (WindowsPS)OPTION
	button10  = (WindowsPS)L3
	button11  = (WindowsPS)R3
	button12  = (WindowsPS)PS
	button13  = (WindowsPS)PANEL
	7th axis  = (WindowsPS)PadX
	8th axis  = (WindowsPS)PadY
	x axis    = (WindowsPS)Analog_1_X
	y axis    = (WindowsPS)Analog_1_Y
	4th axis  = (WindowsPS)Analog_L2
	5th axis  = (WindowsPS)Analog_R2
	3th axis  = (WindowsPS)Analog_2_X
	6th axis  = (WindowsPS)Analog_2_Y

	button0   = (WindowsONE)A(down)
	button1   = (WindowsONE)B(right)
	button2   = (WindowsONE)X(left)
	button3   = (WindowsONE)Y(up)
	button4   = (WindowsONE)L1
	button5   = (WindowsONE)R1
	button6   = (WindowsONE)MENU
	button7   = (WindowsONE)OPTION
	button8   = (WindowsONE)L3
	button9   = (WindowsONE)R3
	6th axis  = (WindowsONE)PadX
	7th axis  = (WindowsONE)PadY
	x axis    = (WindowsONE)Analog_1_X
	y axis    = (WindowsONE)Analog_1_Y
	4th axis  = (WindowsONE)Analog_2_X
	5th axis  = (WindowsONE)Analog_2_Y
	9th axis  = (WindowsONE)Analog_L2
	10th axis = (WindowsONE)Analog_R2

	*/
	public class Joy
	{
		/** [シングルトン]s_instance
		*/
		private static Joy s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Joy();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Joy GetInstance()
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

		/** ジョイタイプ。
		*/
		//private JoyType joytype;

		/** ボタン。
		*/
		public Key_Button left;
		public Key_Button right;
		public Key_Button up;
		public Key_Button down;
		public Key_Button enter;
		public Key_Button escape;

		/** [シングルトン]constructor
		*/
		private Joy()
		{
			/** ボタン。
			*/
			this.left.Reset();
			this.right.Reset();
			this.up.Reset();
			this.down.Reset();
			this.enter.Reset();
			this.escape.Reset();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				bool t_left = false;
				bool t_right = false;
				bool t_up = false;
				bool t_down = false;
				bool t_enter = false;
				bool t_escape = false;

				float t_axis_6 = 0.0f;
				float t_axis_7 = 0.0f;
				float t_axis_8 = 0.0f;

				if(Config.JOY_INPUTNAME_AXIS6 != null){
					try{
						t_axis_6 = UnityEngine.Input.GetAxis(Config.JOY_INPUTNAME_AXIS6);
					}catch(System.Exception /*t_exception*/){
						//インプットマネージャで登録が必要。
						Tool.Log("Joy","ERROR : " + Config.JOY_INPUTNAME_AXIS6);
						Config.JOY_INPUTNAME_AXIS6 = null;
					}
				}

				if(Config.JOY_INPUTNAME_AXIS7 != null){
					try{
						t_axis_7 = UnityEngine.Input.GetAxis(Config.JOY_INPUTNAME_AXIS7);
					}catch(System.Exception /*t_exception*/){
						//インプットマネージャで登録が必要。
						Tool.Log("Joy","ERROR : " + Config.JOY_INPUTNAME_AXIS7);
						Config.JOY_INPUTNAME_AXIS7 = null;
					}
				}

				if(Config.JOY_INPUTNAME_AXIS8 != null){
					try{
						t_axis_8 = UnityEngine.Input.GetAxis(Config.JOY_INPUTNAME_AXIS8);
					}catch(System.Exception /*t_exception*/){
						//インプットマネージャで登録が必要。
						Tool.Log("Joy","ERROR : " + Config.JOY_INPUTNAME_AXIS8);
						Config.JOY_INPUTNAME_AXIS8 = null;
					}
				}

				bool t_button_0 = false;
				bool t_button_1 = false;
				bool t_button_2 = false;

				if(Config.JOY_INPUTNAME_BUTTON0 != null){
					try{
						t_button_0 = UnityEngine.Input.GetButton(Config.JOY_INPUTNAME_BUTTON0);
					}catch(System.Exception /*t_exception*/){
						//インプットマネージャで登録が必要。
						Tool.Log("Joy","ERROR : " + Config.JOY_INPUTNAME_BUTTON0);
						Config.JOY_INPUTNAME_BUTTON0 = null;
					}
				}

				if(Config.JOY_INPUTNAME_BUTTON1 != null){
					try{
						t_button_1 = UnityEngine.Input.GetButton(Config.JOY_INPUTNAME_BUTTON1);
					}catch(System.Exception /*t_exception*/){
						//インプットマネージャで登録が必要。
						Tool.Log("Joy","ERROR : " + Config.JOY_INPUTNAME_BUTTON1);
						Config.JOY_INPUTNAME_BUTTON1 = null;
					}
				}

				if(Config.JOY_INPUTNAME_BUTTON2 != null){
					try{
						t_button_2 = UnityEngine.Input.GetButton(Config.JOY_INPUTNAME_BUTTON2);
					}catch(System.Exception /*t_exception*/){
						//インプットマネージャで登録が必要。
						Tool.Log("Joy","ERROR : " + Config.JOY_INPUTNAME_BUTTON2);
						Config.JOY_INPUTNAME_BUTTON2 = null;
					}
				}

				float t_pad_x = 0.0f;
				float t_pad_y = 0.0f;
				#if UNITY_EDITOR
				{
					t_pad_x = t_axis_7;
					t_pad_y = t_axis_8;

					t_enter = t_button_2;
					t_escape = t_button_1;
				}
				#elif UNITY_STANDALONE_WIN
				{
					t_pad_x = t_axis_7;
					t_pad_y = t_axis_8;

					t_enter = t_button_2;
					t_escape = t_button_1;
				}
				#elif UNITY_WEBGL
				{
					t_pad_x = t_axis_6;
					t_pad_y = t_axis_7;

					t_enter = t_button_1;
					t_escape = t_button_0;
				}
				#endif

				if(t_pad_x <= -0.9f){
					t_left = true;
				}else if(t_pad_x >= 0.9f){
					t_right = true;
				}else if(t_pad_y <= -0.9f){
					t_down = true;
				}else if(t_pad_y >= 0.9f){
					t_up = true;
				}

				//設定。
				this.left.Set(t_left);
				this.right.Set(t_right);
				this.up.Set((t_up));
				this.down.Set(t_down);
				this.enter.Set(t_enter);
				this.escape.Set(t_escape);

				//更新。
				this.left.Main();
				this.right.Main();
				this.up.Main();
				this.down.Main();
				this.enter.Main();
				this.escape.Main();
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

