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
	public class ScrollItem : NUi.ScrollItem_Base , NDeleter.DeleteItem_Base
	{
		/** deleter
		*/
		private NDeleter.Deleter deleter;

		/** create_id
		*/
		private int create_id;

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
		public ScrollItem(NDeleter.Deleter a_deleter,int a_create_id)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//create_id
			this.create_id = a_create_id;

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
			this.text.SetText(this.create_id.ToString());
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
		public override void SetX(int a_x)
		{
			this.sprite.SetX(a_x);
			this.text.SetX(a_x);
		}

		/** [ScrollItem_Base]矩形。設定。
		*/
		public override void SetY(int a_y)
		{
			this.sprite.SetY(a_y);
			this.text.SetY(a_y);
		}

		/** [ScrollItem_Base]クリック。矩形。
		*/
		public override void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite.SetClipRect(ref a_rect);
			this.text.SetClipRect(ref a_rect);
		}

		/** [ScrollItem_Base]表示内。
		*/
		public override  void OnViewIn()
		{
			this.sprite.SetVisible(true);
			this.text.SetVisible(true);
		}

		/** [ScrollItem_Base]表示外。
		*/
		public override void OnViewOut()
		{
			this.sprite.SetVisible(false);
			this.text.SetVisible(false);
		}
	}

	/** scrollview
	*/
	private NUi.VerticalScroll<ScrollItem> v_scrollview;
	private NUi.HorizontalScroll<ScrollItem> h_scrollview;
	private int v_scrollview_create_id;
	private int h_scrollview_create_id;

	/** drag
	*/
	private bool drag;
	private int drag_old_value_x;
	private int drag_old_value_y;
	private float drag_speed_x;
	private float drag_speed_y;

	/** button
	*/
	private NUi.Button button_push;
	private NUi.Button button_pop;

	private NUi.Button button_insert_top;
	private NUi.Button button_remove_top;

	private NUi.Button button_insert_top_5;
	private NUi.Button button_remove_top_5;

	private NUi.Button button_insert_last_5;
	private NUi.Button button_remove_last_5;

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
		this.h_scrollview.SetRect(450,100,400,200);

		int t_y_index = 0;

		//button_push
		this.button_push = new NUi.Button(this.deleter,null,0,Click,9000);
		this.button_push.SetRect(10,100 + 30 * t_y_index,100,30);
		this.button_push.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_push.SetText("最後尾追加");

		t_y_index++;

		//button_pop
		this.button_pop = new NUi.Button(this.deleter,null,0,Click,9001);
		this.button_pop.SetRect(10,100 + 30 * t_y_index,100,30);
		this.button_pop.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_pop.SetText("最後尾削除");

		t_y_index++;

		//button_insert_top
		this.button_insert_top = new NUi.Button(this.deleter,null,0,Click,9002);
		this.button_insert_top.SetRect(10,100 + 30 * t_y_index,100,30);
		this.button_insert_top.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_insert_top.SetText("先頭追加");

		t_y_index++;

		//button_remove_top
		this.button_remove_top = new NUi.Button(this.deleter,null,0,Click,9003);
		this.button_remove_top.SetRect(10,100 + 30 * t_y_index,100,30);
		this.button_remove_top.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_remove_top.SetText("先頭削除");

		t_y_index++;

		//button_insert_top_5
		this.button_insert_top_5 = new NUi.Button(this.deleter,null,0,Click,9004);
		this.button_insert_top_5.SetRect(10,100 + 30 * t_y_index,100,30);
		this.button_insert_top_5.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_insert_top_5.SetText("挿入(５番目)");

		t_y_index++;

		//button_remove_top_5
		this.button_remove_top_5 = new NUi.Button(this.deleter,null,0,Click,9005);
		this.button_remove_top_5.SetRect(10,100 + 30 * t_y_index,100,30);
		this.button_remove_top_5.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_remove_top_5.SetText("削除(５番目)");

		t_y_index++;

		//button_insert_last_5
		this.button_insert_last_5 = new NUi.Button(this.deleter,null,0,Click,9006);
		this.button_insert_last_5.SetRect(10,100 + 30 * t_y_index,100,30);
		this.button_insert_last_5.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_insert_last_5.SetText("挿入(後５)");

		t_y_index++;

		//button_remove_last_5
		this.button_remove_last_5 = new NUi.Button(this.deleter,null,0,Click,9007);
		this.button_remove_last_5.SetRect(10,100 + 30 * t_y_index,100,30);
		this.button_remove_last_5.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_remove_last_5.SetText("削除(後５)");
	}

	/** Clip
	*/
	public void Click(int a_value)
	{
		switch(a_value){
		case 9000:
			{
				//最後尾追加。

				{
					this.v_scrollview_create_id++;
					this.v_scrollview.PushItem(new ScrollItem(this.deleter,this.v_scrollview_create_id));
				}
				{
					this.h_scrollview_create_id++;
					this.h_scrollview.PushItem(new ScrollItem(this.deleter,this.h_scrollview_create_id));
				}
			}break;
		case 9001:
			{
				//最後尾削除。

				{
					ScrollItem t_item = this.v_scrollview.PopItem();
					if(t_item != null){
						this.deleter.UnRegister(t_item);
						t_item.Delete();
						t_item = null;
					}
				}
				{
					ScrollItem t_item = this.h_scrollview.PopItem();
					if(t_item != null){
						this.deleter.UnRegister(t_item);
						t_item.Delete();
						t_item = null;
					}
				}
			}break;
		case 9002:
			{
				//先頭追加。

				{
					int t_index = 0;
					this.v_scrollview_create_id++;
					this.v_scrollview.AddItem(new ScrollItem(this.deleter,this.v_scrollview_create_id),t_index);
				}
				{
					int t_index = 0;
					this.h_scrollview_create_id++;
					this.h_scrollview.AddItem(new ScrollItem(this.deleter,this.h_scrollview_create_id),t_index);
				}
			}break;
		case 9003:
			{
				//先頭削除。

				{
					int t_index = 0;
					ScrollItem t_item = this.v_scrollview.RemoveItem(t_index);
					if(t_item != null){
						this.deleter.UnRegister(t_item);
						t_item.Delete();
						t_item = null;
					}
				}
				{
					int t_index = 0;
					ScrollItem t_item = this.h_scrollview.RemoveItem(t_index);
					if(t_item != null){
						this.deleter.UnRegister(t_item);
						t_item.Delete();
						t_item = null;
					}
				}
			}break;
		case 9004:
			{
				//追加。

				{
					int t_index = 4;
					this.v_scrollview_create_id++;
					this.v_scrollview.AddItem(new ScrollItem(this.deleter,this.v_scrollview_create_id),t_index);
				}
				{
					int t_index = 4;
					this.h_scrollview_create_id++;
					this.h_scrollview.AddItem(new ScrollItem(this.deleter,this.h_scrollview_create_id),t_index);
				}
			}break;
		case 9005:
			{
				//削除。

				{
					int t_index = 4;
					ScrollItem t_item = this.v_scrollview.RemoveItem(t_index);
					if(t_item != null){
						this.deleter.UnRegister(t_item);
						t_item.Delete();
						t_item = null;
					}
				}
				{
					int t_index = 4;
					ScrollItem t_item = this.h_scrollview.RemoveItem(t_index);
					if(t_item != null){
						this.deleter.UnRegister(t_item);
						t_item.Delete();
						t_item = null;
					}
				}
			}break;
		case 9006:
			{
				//追加。

				{
					int t_index = this.v_scrollview.GetListCount() - 5;
					this.v_scrollview_create_id++;
					this.v_scrollview.AddItem(new ScrollItem(this.deleter,this.v_scrollview_create_id),t_index);
				}
				{
					int t_index = this.h_scrollview.GetListCount() - 5;
					this.h_scrollview_create_id++;
					this.h_scrollview.AddItem(new ScrollItem(this.deleter,this.h_scrollview_create_id),t_index);
				}
			}break;
		case 9007:
			{
				//削除。

				{
					int t_index = this.v_scrollview.GetListCount() - 6;
					ScrollItem t_item = this.v_scrollview.RemoveItem(t_index);
					if(t_item != null){
						this.deleter.UnRegister(t_item);
						t_item.Delete();
						t_item = null;
					}
				}
				{
					int t_index = this.h_scrollview.GetListCount() - 6;
					ScrollItem t_item = this.h_scrollview.RemoveItem(t_index);
					if(t_item != null){
						this.deleter.UnRegister(t_item);
						t_item.Delete();
						t_item = null;
					}
				}
			}break;
		}
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

		//ドラッグ。
		if(this.drag == true){
			if(NInput.Mouse.GetInstance().left.on == true){
				{
					int t_distance_y = NInput.Mouse.GetInstance().left.last_down_pos.y - NInput.Mouse.GetInstance().pos.y;
					this.v_scrollview.SetViewPosition(this.drag_old_value_y + t_distance_y);
				}
				{
					int t_distance_x = NInput.Mouse.GetInstance().left.last_down_pos.x - NInput.Mouse.GetInstance().pos.x;
					this.h_scrollview.SetViewPosition(this.drag_old_value_x + t_distance_x);
				}
				this.drag_speed_x = this.drag_speed_x * 0.3f + (NInput.Mouse.GetInstance().pos.x - NInput.Mouse.GetInstance().pos.x_old) * 0.7f;
				this.drag_speed_y = this.drag_speed_y * 0.3f + (NInput.Mouse.GetInstance().pos.y - NInput.Mouse.GetInstance().pos.y_old) * 0.7f;
			}else{
				this.drag = false;
			}
		}else{
			if(NInput.Mouse.GetInstance().left.down == true){
				this.drag = true;
				{
					this.drag_old_value_y = this.v_scrollview.GetViewPosition();
				}
				{
					this.drag_old_value_x = this.h_scrollview.GetViewPosition();
				}
				this.drag_speed_x = 0.0f;
				this.drag_speed_y = 0.0f;
			}else{
				if(this.drag_speed_y != 0.0f){
					int t_move = (int)this.drag_speed_y;
					this.drag_speed_y /= 1.08f;
					{
						if(t_move != 0){
							this.v_scrollview.SetViewPosition(this.v_scrollview.GetViewPosition() - t_move);
						}else{
							this.drag_speed_y = 0.0f;
						}
					}
				}
				if(this.drag_speed_x != 0.0f){
					int t_move = (int)this.drag_speed_x;
					this.drag_speed_x /= 1.08f;
					{
						if(t_move != 0){
							this.h_scrollview.SetViewPosition(this.h_scrollview.GetViewPosition() - t_move);
						}else{
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

