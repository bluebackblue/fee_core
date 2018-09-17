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

	スクロール

*/
public class test17 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** ScrollItem
	*/
	public class ScrollItem : NDeleter.DeleteItem_Base , NUi.ScrollItem_Base
	{
		/** deleter
		*/
		private NDeleter.Deleter deleter;

		/** sprite
		*/
		private NUi.ClipSprite sprite;

		/** text
		*/
		private NRender2D.Text2D text;

		/** 矩形。取得。
		*/
		public static int GetW()
		{
			return 100;
		}

		/** 矩形。取得。
		*/
		public static int GetH()
		{
			return 30;
		}

		/** constructor
		*/
		public ScrollItem(NDeleter.Deleter a_deleter)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//drawpriority
			long t_drawpriority = 1;

			//sprite
			this.sprite = new NUi.ClipSprite(this.deleter,null,t_drawpriority);
			this.sprite.SetRect(0,0,ScrollItem.GetW(),ScrollItem.GetH());
			this.sprite.SetTexture(Texture2D.whiteTexture);
			this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite.SetClipRect(0,0,0,0);
			this.sprite.SetColor(Random.value,Random.value,Random.value,1.0f);
			this.sprite.SetClip(true);
			this.sprite.SetVisible(false);

			//text
			this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
			this.text.SetRect(0,0,0,0);
			this.text.SetClipRect(0,0,0,0);
			this.text.SetText("text");
			this.text.SetClip(true);
			this.text.SetVisible(false);

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();
		}

		/** [ScrollItem_Base]矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.sprite.SetX(a_x);
			this.text.SetX(a_x);
		}

		/** [ScrollItem_Base]矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.sprite.SetY(a_y);
			this.text.SetY(a_y);
		}

		/** [ScrollItem_Base]クリック。矩形。
		*/
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite.SetClipRect(ref a_rect);
			this.text.SetClipRect(ref a_rect);
		}

		/** [ScrollItem_Base]表示内。
		*/
		public void OnViewIn()
		{
			this.sprite.SetVisible(true);
			this.text.SetVisible(true);
		}

		/** [ScrollItem_Base]表示外。
		*/
		public void OnViewOut()
		{
			this.sprite.SetVisible(false);
			this.text.SetVisible(false);
		}
	}

	/** scrollview
	*/
	private NUi.VerticalScroll<ScrollItem> v_scrollview;
	private NUi.HorizontalScroll<ScrollItem> h_scrollview;

	/** drag
	*/
	private bool drag;
	private int drag_old_value_x;
	private int drag_old_value_y;
	private float drag_speed_x;
	private float drag_speed_y;

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

		//v_scrollview
		this.v_scrollview = new NUi.VerticalScroll<ScrollItem>(this.deleter,0,ScrollItem.GetH());
		this.v_scrollview.SetRect(200,100,200,400);

		//h_scrollview
		this.h_scrollview = new NUi.HorizontalScroll<ScrollItem>(this.deleter,0,ScrollItem.GetW());
		this.h_scrollview.SetRect(500,100,200,400);
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
			if(this.v_scrollview != null){
				for(int ii=0;ii<10;ii++){
					this.v_scrollview.AddList(new ScrollItem(this.deleter));
				}
			}
			if(this.h_scrollview != null){
				for(int ii=0;ii<10;ii++){
					this.h_scrollview.AddList(new ScrollItem(this.deleter));
				}
			}
		}

		//ドラッグ。
		if(this.drag == true){
			if(NInput.Mouse.GetInstance().left.on == true){
				if(this.v_scrollview != null){
					int t_distance_y = NInput.Mouse.GetInstance().left.last_down_pos.y - NInput.Mouse.GetInstance().pos.y;
					this.v_scrollview.SetPosition(this.drag_old_value_y + t_distance_y);
				}
				if(this.h_scrollview != null){
					int t_distance_x = NInput.Mouse.GetInstance().left.last_down_pos.x - NInput.Mouse.GetInstance().pos.x;
					this.h_scrollview.SetPosition(this.drag_old_value_x + t_distance_x);
				}

				this.drag_speed_x = this.drag_speed_x * 0.3f + (NInput.Mouse.GetInstance().pos.x - NInput.Mouse.GetInstance().pos.x_old) * 0.7f;
				this.drag_speed_y = this.drag_speed_y * 0.3f + (NInput.Mouse.GetInstance().pos.y - NInput.Mouse.GetInstance().pos.y_old) * 0.7f;
			}else{
				this.drag = false;
			}
		}else{
			if(NInput.Mouse.GetInstance().left.down == true){
				this.drag = true;
				if(this.v_scrollview != null){
					this.drag_old_value_y = this.v_scrollview.GetPosition();
				}
				if(this.h_scrollview != null){
					this.drag_old_value_x = this.h_scrollview.GetPosition();
				}
				this.drag_speed_x = 0.0f;
				this.drag_speed_y = 0.0f;
			}else{
				if(this.drag_speed_y != 0.0f){
					int t_move = (int)this.drag_speed_y;
					this.drag_speed_y /= 1.08f;
					if(this.v_scrollview != null){
						bool t_ret = this.v_scrollview.SetPosition(this.v_scrollview.GetPosition() - t_move);
						if(t_ret == false){
							this.drag_speed_y = 0.0f;
						}
					}
				}
				if(this.drag_speed_x != 0.0f){
					int t_move = (int)this.drag_speed_x;
					this.drag_speed_x /= 1.08f;
					if(this.h_scrollview != null){
						bool t_ret = this.h_scrollview.SetPosition(this.h_scrollview.GetPosition() - t_move);
						if(t_ret == false){
							this.drag_speed_x = 0.0f;
						}
					}
				}
			}
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
}

