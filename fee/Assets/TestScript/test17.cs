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


/** test17
*/
public class test17 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** scrollview
	*/
	private NUi.VerticalScroll scrollview;

	/** drag
	*/
	private bool drag;
	private int drag_value_old;
	private Vector2 drag_speed;

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

		//イベントテンプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//scrollview
		this.scrollview = new NUi.VerticalScroll(this.deleter);
		this.scrollview.SetItemHight(30);
		this.scrollview.SetRect(300,100,200,400);
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

		//イベントテンプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//アイテム追加。
		if(NInput.Mouse.GetInstance().right.down == true){
			this.scrollview.AddList();
		}

		//移動。
		if(NInput.Key.GetInstance().up.on == true){
			this.scrollview.SetPosition(this.scrollview.GetPosition() - 1);
		}else if(NInput.Key.GetInstance().down.on == true){
			this.scrollview.SetPosition(this.scrollview.GetPosition() + 1);
		}

		//ドラッグ。
		if(this.drag == true){
			if(NInput.Mouse.GetInstance().left.on == true){
				int t_distance = NInput.Mouse.GetInstance().left.last_down_pos.y - NInput.Mouse.GetInstance().pos.y;
				this.scrollview.SetPosition(this.drag_value_old + t_distance);
				this.drag_speed.x = this.drag_speed.x * 0.3f + (NInput.Mouse.GetInstance().pos.x - NInput.Mouse.GetInstance().pos.x_old) * 0.7f;
				this.drag_speed.y = this.drag_speed.y * 0.3f + (NInput.Mouse.GetInstance().pos.y - NInput.Mouse.GetInstance().pos.y_old) * 0.7f;
			}else{
				this.drag = false;
			}
		}else{
			if(NInput.Mouse.GetInstance().left.down == true){
				this.drag = true;
				this.drag_value_old = this.scrollview.GetPosition();
				this.drag_speed.Set(0.0f,0.0f);
			}else{
				if(this.drag_speed.y != 0.0f){
					int t_move = (int)this.drag_speed.y;
					this.drag_speed.y /= 1.08f;
					bool t_ret = this.scrollview.SetPosition(this.scrollview.GetPosition() - t_move);
					if(t_ret == false){
						this.drag_speed.Set(0.0f,0.0f);
					}
				}
			}
		}

		this.scrollview.Main();
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
}

