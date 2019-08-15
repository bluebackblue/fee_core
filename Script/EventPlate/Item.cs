

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief イベントプレート。アイテム。
*/


/** Fee.EventPlate
*/
namespace Fee.EventPlate
{
	/** Item
	*/
	public class Item : Fee.Deleter.OnDelete_CallBackInterface
	{
		/** priority
		*/
		private long priority;

		/** eventtype
		*/
		private EventType eventtype;

		/** enable
		*/
		private bool enable;

		/** rect
		*/
		private Fee.Render2D.Rect2D_R<int> rect;

		/** clip_rect
		*/
		private Fee.Render2D.Rect2D_R<int> clip_rect;

		/** clip
		*/
		private bool clip;

		/** callbackparam_over
		*/
		private Fee.EventPlate.OnEventPlateOver_CallBackParam callbackparam_over;

		/** 削除済み。
		*/
		private bool deleted = false;

		/** constructor
		*/
		public Item(Fee.Deleter.Deleter a_deleter,EventType a_eventtype,long a_priority)
		{
			//priority
			this.priority = a_priority;

			//eventtype
			this.eventtype = a_eventtype;

			//enable
			this.enable = true;

			//rect
			//this.rect.Set(0,0,0,0);

			//clip_rect
			//this.clip_rect.Set(0,0,0,0);

			//clip
			this.clip = false;

			//callbackparam_over
			this.callbackparam_over = null;

			//deleted
			this.deleted = false;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}

			EventPlate.GetInstance().Add(this,this.eventtype);
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			Tool.Assert(this.deleted == false);
			this.deleted = true;

			EventPlate.GetInstance().Remove(this,this.eventtype);
		}

		/** ソート関数。
		*/
		public static int Sort_InvPriority(Item a_test,Item a_target)
		{
			return (int)(a_target.priority - a_test.priority);
		}

		/** 矩形。設定。
		*/
		public void SetRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.rect = a_rect;
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.rect.Set(a_x,a_y,a_w,a_h);
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.clip_rect = a_rect;
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.clip_rect.x = a_x;
			this.clip_rect.y = a_y;
			this.clip_rect.w = a_w;
			this.clip_rect.h = a_h;
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			this.clip = a_flag;
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.rect.x = a_x;
			this.rect.y = a_y;
		}

		/** 矩形。設定。
		*/
		public void SetWH(int a_w,int a_h)
		{
			this.rect.w = a_w;
			this.rect.h = a_h;
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.rect.x = a_x;
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.rect.y = a_y;
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.rect.w = a_w;
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.rect.h = a_h;
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.rect.x;
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.rect.y;
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.rect.w;
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.rect.h;
		}

		/** プライオリティ。設定。
		*/
		public void SetPriority(long a_priority)
		{
			if(this.priority != a_priority){
				this.priority = a_priority;
				EventPlate.GetInstance().SortRequest(this.eventtype);
			}
		}

		/** プライオリティ。取得。
		*/
		public long GetPriority()
		{
			return this.priority;
		}

		/** 有効。設定。
		*/
		public void SetEnable(bool a_flag)
		{
			this.enable = a_flag;
		}

		/** 有効。取得。
		*/
		public bool IsEnable()
		{
			return this.enable;
		}

		/** 更新。

		return = true : カレント。

		*/
		public bool Main(ref Fee.Render2D.Pos2D<int> a_pos)
		{
			if(this.enable == true){
				if((this.rect.x <= a_pos.x)&&(this.rect.y <= a_pos.y)&&(a_pos.x <= (this.rect.x + this.rect.w))&&(a_pos.y <= (this.rect.y + this.rect.h))){
					if(this.clip == true){
						if((this.clip_rect.x <= a_pos.x)&&(this.clip_rect.y <= a_pos.y)&&(a_pos.x <= (this.clip_rect.x + this.clip_rect.w))&&(a_pos.y <= (this.clip_rect.y + this.clip_rect.h))){
							return true;
						}
					}else{
						return true;
					}
				}
			}
			return false;
		}

		/** コールバックインターフェイス。設定。
		*/
		public void SetOnEventPlateOver<T>(Fee.EventPlate.OnEventPlateOver_CallBackInterface<T> a_callback_interface,T a_id)
		{
			if(a_callback_interface != null){
				this.callbackparam_over = new Fee.EventPlate.OnEventPlateOver_CallBackParam_Generic<T>(a_callback_interface,a_id);
			}else{
				this.callbackparam_over = null;
			}
		}

		/** コールバック。呼び出し。
		*/
		public void CallEnter()
		{
			if(this.callbackparam_over != null){
				this.callbackparam_over.CallEnter();
			}
		}

		/** コールバック。呼び出し。
		*/
		public void CallLeave()
		{
			if(this.callbackparam_over != null){
				this.callbackparam_over.CallLeave();
			}
		}
	}
}

