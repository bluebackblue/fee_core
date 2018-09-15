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

	マウス
	ジョイスティック
	キー

*/
public class test05 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** 背景。
	*/
	private NRender2D.Sprite2D sprite_bg;

	/** テキスト。
	*/
	private NRender2D.Text2D text_mouse;

	/** テキスト。
	*/
	private NRender2D.Text2D text_key;

	/** テキスト。
	*/
	private NRender2D.Text2D text_pad;

	/** Start
	*/
	private void Start()
	{
		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//キー。インスタンス作成。
		NInput.Key.CreateInstance();

		//パッド。インスタンス作成。
		NInput.Pad.CreateInstance();

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
		this.text_mouse.SetFontSize(30);

		//テキスト。
		this.text_key = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_key.SetRect(10,200 + 50 * 1,0,0);
		this.text_key.SetFontSize(30);

		//テキスト。
		this.text_pad = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_pad.SetRect(10,200 + 50 * 2,0,0);
		this.text_pad.SetFontSize(30);
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//キー。
		NInput.Key.GetInstance().Main();

		//パッド。
		NInput.Pad.GetInstance().Main();

		//マウス位置。
		{
			string t_text = "";

			if(UnityEngine.Experimental.Input.Touchscreen.current != null){
				for(int ii=0;ii<UnityEngine.Experimental.Input.Touchscreen.current.activeTouches.Count;ii++){
					if(UnityEngine.Experimental.Input.Touchscreen.current.activeTouches[ii] != null){
						t_text += UnityEngine.Experimental.Input.Touchscreen.current.activeTouches[ii].touchId.ReadValue().ToString();
					}
				}
			}

			t_text += "x = " + NInput.Mouse.GetInstance().pos.x.ToString() + " ";
			t_text += "y = " + NInput.Mouse.GetInstance().pos.y.ToString() + " ";
			t_text += "m = " + NInput.Mouse.GetInstance().mouse_wheel.y.ToString() + " ";
			t_text += NInput.Mouse.GetInstance().left.on.ToString() + " ";
			t_text += NInput.Mouse.GetInstance().right.on.ToString() + " ";
			t_text += NInput.Mouse.GetInstance().middle.on.ToString() + " ";

			this.text_mouse.SetText(t_text);
		}

		//キー。
		{
			string t_text = "";

			if(NInput.Key.GetInstance().enter.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Key.GetInstance().escape.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Key.GetInstance().sub1.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Key.GetInstance().sub2.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Key.GetInstance().left.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Key.GetInstance().right.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Key.GetInstance().up.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Key.GetInstance().down.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			this.text_key.SetText(t_text);
		}

		//パッド。
		{
			string t_text = "";

			if(NInput.Pad.GetInstance().enter.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().escape.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().sub1.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().sub2.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().left.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().right.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().up.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().down.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().left_menu.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().right_menu.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			t_text += "\n";

			if(NInput.Pad.GetInstance().left_stick_button.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().right_stick_button.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			t_text += "\n";

			t_text += ((int)(NInput.Pad.GetInstance().left_stick.x * 100)).ToString() + " " + ((int)(NInput.Pad.GetInstance().left_stick.y * 100)).ToString() + " ";
			t_text += ((int)(NInput.Pad.GetInstance().right_stick.x * 100)).ToString() + " " + ((int)(NInput.Pad.GetInstance().right_stick.y * 100)).ToString() + " ";
			t_text += ((int)(NInput.Pad.GetInstance().left_trigger2_button.value * 100)).ToString() + " " + ((int)(NInput.Pad.GetInstance().right_trigger2_button.value * 100)).ToString() + " ";
			
			t_text += "\n";

			if(NInput.Pad.GetInstance().left_trigger1_button.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().right_trigger1_button.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			t_text += "\n";

			if(NInput.Pad.GetInstance().left_trigger2_button.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			if(NInput.Pad.GetInstance().right_trigger2_button.on == true){
				t_text += "[o]";
			}else{
				t_text += "[ ]";
			}

			this.text_pad.SetText(t_text);
		}
	}

	/** 削除前。
	*/
	public override bool PreDestroy(bool a_first)
	{
		return true;
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
	[UnityEditor.MenuItem("Test/Test5/EditInputManager")]
	private static void MakeInputManager()
	{
		NInput.EditInputManager t_inputmaanger = new NInput.EditInputManager();
		{
			List<NInput.EditInputManager_Item> t_list = t_inputmaanger.GetList();

			bool t_find_axis6 = false;
			bool t_find_axis7 = false;
			bool t_find_axis8 = false;
			bool t_find_button0 = false;
			bool t_find_button1 = false;
			bool t_find_button2 = false;
			bool t_find_button3 = false;

			for(int ii=0;ii<t_list.Count;ii++){
				if(t_list[ii].m_Name == "Axis6"){
					t_find_axis6 = true;
				}
				if(t_list[ii].m_Name == "Axis7"){
					t_find_axis7 = true;
				}
				if(t_list[ii].m_Name == "Axis8"){
					t_find_axis8 = true;
				}
				if(t_list[ii].m_Name == "Button0"){
					t_find_button0 = true;
				}
				if(t_list[ii].m_Name == "Button1"){
					t_find_button1 = true;
				}
				if(t_list[ii].m_Name == "Button2"){
					t_find_button2 = true;
				}
				if(t_list[ii].m_Name == "Button3"){
					t_find_button3 = true;
				}
			}

			if(t_find_axis6 == false){
				NInput.EditInputManager_Item t_item = new NInput.EditInputManager_Item();
				t_item.CreateJoyAixs6();
				t_list.Add(t_item);
			}

			if(t_find_axis7 == false){
				NInput.EditInputManager_Item t_item = new NInput.EditInputManager_Item();
				t_item.CreateJoyAixs7();
				t_list.Add(t_item);
			}

			if(t_find_axis8 == false){
				NInput.EditInputManager_Item t_item = new NInput.EditInputManager_Item();
				t_item.CreateJoyAixs8();
				t_list.Add(t_item);
			}

			if(t_find_button0 == false){
				NInput.EditInputManager_Item t_item = new NInput.EditInputManager_Item();
				t_item.CreateJoyButton0();
				t_list.Add(t_item);
			}

			if(t_find_button1 == false){
				NInput.EditInputManager_Item t_item = new NInput.EditInputManager_Item();
				t_item.CreateJoyButton1();
				t_list.Add(t_item);
			}

			if(t_find_button2 == false){
				NInput.EditInputManager_Item t_item = new NInput.EditInputManager_Item();
				t_item.CreateJoyButton2();
				t_list.Add(t_item);
			}

			if(t_find_button3 == false){
				NInput.EditInputManager_Item t_item = new NInput.EditInputManager_Item();
				t_item.CreateJoyButton3();
				t_list.Add(t_item);
			}
		}
		t_inputmaanger.Save();
	}
	#endif
}

