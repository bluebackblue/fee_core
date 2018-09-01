using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** test05
*/
public class test05 : main_base
{
	/** 削除管理。
	*/
	NDeleter.Deleter deleter;

	/** 背景。
	*/
	NRender2D.Sprite2D sprite_bg;

	/** テキスト。
	*/
	NRender2D.Text2D text_mouse;

	/** テキスト。
	*/
	NRender2D.Text2D text_joy;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//ジョイスティック。インスタンス作成。
		NInput.Joy.CreateInstance();

		//キー。インスタンス作成。
		NInput.Key.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//背景。
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
		this.sprite_bg = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
		this.sprite_bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
		this.sprite_bg.SetTexture(Texture2D.whiteTexture);
		this.sprite_bg.SetRect(ref NRender2D.Render2D.VIRTUAL_RECT_MAX);
		this.sprite_bg.SetMaterialType(NRender2D.Config.MaterialType.Alpha);
		this.sprite_bg.SetColor(0.0f,0.0f,0.0f,0.5f);

		//テキスト。
		this.text_mouse = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_mouse.SetRect(10,200 + 50 * 0,0,0);

		//テキスト。
		this.text_joy = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_joy.SetRect(10,200 + 50 * 1,0,0);
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//ジョイスティック。
		NInput.Joy.GetInstance().Main();

		//キー。
		NInput.Key.GetInstance().Main();

		//ホイール。
		if(NInput.Mouse.GetInstance().mouse_wheel_action == true){
			if(NInput.Mouse.GetInstance().mouse_wheel > 0){
				Debug.Log("mouse_wheel : -");
			}else{
				Debug.Log("mouse_wheel : +");
			}
		}

		//キー。
		if(NInput.Key.GetInstance().enter.down == true){
			NInput.Mouse.GetInstance().SetVisible(false);
			NInput.Mouse.GetInstance().SetLock(true);
			Debug.Log("key.enter.down");
		}

		if(NInput.Key.GetInstance().escape.down == true){
			NInput.Mouse.GetInstance().SetVisible(true);
			NInput.Mouse.GetInstance().SetLock(false);
			Debug.Log("key.escape.down");
		}

		//マウス位置。
		this.text_mouse.SetText("x = " + NInput.Mouse.GetInstance().pos.x.ToString() + " y = " + NInput.Mouse.GetInstance().pos.y.ToString());

		//ジョイスティック。
		{
			string t_text = "";
			if(NInput.Joy.GetInstance().enter.on == true){
				t_text += "Enter[o] ";
			}else{
				t_text += "Enter[ ] ";
			}

			if(NInput.Joy.GetInstance().escape.on == true){
				t_text += "Escape[o]";
			}else{
				t_text += "Escape[ ]";
			}

			if(NInput.Joy.GetInstance().sub1.on == true){
				t_text += "Sub1[o]";
			}else{
				t_text += "Sub1[ ]";
			}

			if(NInput.Joy.GetInstance().sub2.on == true){
				t_text += "Sub2[o]";
			}else{
				t_text += "Sub2[ ]";
			}

			this.text_joy.SetText(t_text);
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}

	/** 追加。
	*/
	#if UNITY_EDITOR
	[UnityEditor.MenuItem("Test/Test5/MakeInputManager")]
	private static void MakeInputManager()
	{
		NInput.InputManage t_inputmaanger = new NInput.InputManage();
		{
			List<NInput.InputManager_Item> t_list = t_inputmaanger.GetList();

			bool t_find_axis6 = false;
			bool t_find_axis7 = false;
			bool t_find_axis8 = false;
			bool t_find_button0 = false;
			bool t_find_button1 = false;
			bool t_find_button2 = false;
			bool t_find_button3 = false;

			for(int ii=0;ii<t_list.Count;ii++){
				if(t_list[ii].m_Name == NInput.Input.JOY_INPUTNAME_AXIS6){
					t_find_axis6 = true;
				}
				if(t_list[ii].m_Name == NInput.Input.JOY_INPUTNAME_AXIS7){
					t_find_axis7 = true;
				}
				if(t_list[ii].m_Name == NInput.Input.JOY_INPUTNAME_AXIS8){
					t_find_axis8 = true;
				}
				if(t_list[ii].m_Name == NInput.Input.JOY_INPUTNAME_BUTTON0){
					t_find_button0 = true;
				}
				if(t_list[ii].m_Name == NInput.Input.JOY_INPUTNAME_BUTTON1){
					t_find_button1 = true;
				}
				if(t_list[ii].m_Name == NInput.Input.JOY_INPUTNAME_BUTTON2){
					t_find_button2 = true;
				}
				if(t_list[ii].m_Name == NInput.Input.JOY_INPUTNAME_BUTTON3){
					t_find_button3 = true;
				}
			}

			if(t_find_axis6 == false){
				NInput.InputManager_Item t_item = new NInput.InputManager_Item();
				t_item.CreateJoyAixs6();
				t_list.Add(t_item);
			}

			if(t_find_axis7 == false){
				NInput.InputManager_Item t_item = new NInput.InputManager_Item();
				t_item.CreateJoyAixs7();
				t_list.Add(t_item);
			}

			if(t_find_axis8 == false){
				NInput.InputManager_Item t_item = new NInput.InputManager_Item();
				t_item.CreateJoyAixs8();
				t_list.Add(t_item);
			}

			if(t_find_button0 == false){
				NInput.InputManager_Item t_item = new NInput.InputManager_Item();
				t_item.CreateJoyButton0();
				t_list.Add(t_item);
			}

			if(t_find_button1 == false){
				NInput.InputManager_Item t_item = new NInput.InputManager_Item();
				t_item.CreateJoyButton1();
				t_list.Add(t_item);
			}

			if(t_find_button2 == false){
				NInput.InputManager_Item t_item = new NInput.InputManager_Item();
				t_item.CreateJoyButton2();
				t_list.Add(t_item);
			}

			if(t_find_button3 == false){
				NInput.InputManager_Item t_item = new NInput.InputManager_Item();
				t_item.CreateJoyButton3();
				t_list.Add(t_item);
			}
		}
		t_inputmaanger.Save();
	}
	#endif
}

