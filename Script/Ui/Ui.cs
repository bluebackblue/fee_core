

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Ui
	*/
	public class Ui
	{
		/** [シングルトン]s_instance
		*/
		private static Ui s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Ui();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Ui GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

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

		/** playerloop_flag
		*/
		private bool playerloop_flag;

		/** ウィンドウリスト。
		*/
		private Ui_WindowList windowlist;

		/** ターゲットリスト。
		*/
		private System.Collections.Generic.LinkedList<Fee.Ui.OnTarget_CallBackInterface> target_list;

		/** ターゲット追加リスト。
		*/
		private System.Collections.Generic.List<Fee.Ui.OnTarget_CallBackInterface> target_add_list;

		/** ターゲット削除リスト。
		*/
		private System.Collections.Generic.List<Fee.Ui.OnTarget_CallBackInterface> target_remove_list;

		/** ウィンドウレジュームリスト。
		*/
		private Ui_WindowResumeList windowresumelist;

		/** ダウン中ボタンインスタンス。
		*/
		private Button_Base down_button_instance;

		/** pool_list_sprite_clip
		*/
		private Fee.Pool.PoolList<Sprite_Clip> pool_list_sprite_clip;

		/** [シングルトン]constructor
		*/
		private Ui()
		{
			//ウィンドウリスト。
			this.windowlist = new Ui_WindowList();

			//target_list
			this.target_list = new System.Collections.Generic.LinkedList<Fee.Ui.OnTarget_CallBackInterface>();

			//target_add_list
			this.target_add_list = new System.Collections.Generic.List<Fee.Ui.OnTarget_CallBackInterface>();

			//target_remove_list
			this.target_remove_list = new System.Collections.Generic.List<Fee.Ui.OnTarget_CallBackInterface>();

			//ウィンドウレジュームリスト。
			this.windowresumelist = new Ui_WindowResumeList();

			//ダウン中ボタンインスタンス。
			this.down_button_instance = null;

			//プールリスト。
			this.pool_list_sprite_clip = new Pool.PoolList<Sprite_Clip>(0);

			//PlayerLoopType
			this.playerloop_flag = true;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_Ui_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.pool_list_sprite_clip.DeleteAllFromMemory();

			//PlayerLoopType
			this.playerloop_flag = false;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_Ui_Main));
		}

		/** 更新。
		*/
		private void Main()
		{
			try{
				if(this.playerloop_flag == true){
					//ウィンドウリスト。
					this.windowlist.Main();

					//ターゲット。
					{
						//追加。
						for(int ii=0;ii<this.target_add_list.Count;ii++){
							this.target_list.AddFirst(this.target_add_list[ii]);
						}
						this.target_add_list.Clear();

						//削除。
						for(int ii=0;ii<this.target_remove_list.Count;ii++){
							this.target_list.Remove(this.target_remove_list[ii]);
						}
						this.target_remove_list.Clear();

						//呼び出し。
						foreach(OnTarget_CallBackInterface t_target in this.target_list){
							t_target.OnTarget();
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** プールリスト。取得。
		*/
		public Fee.Pool.PoolList<Sprite_Clip> GetPoolList_Sprite_Clip()
		{
			return this.pool_list_sprite_clip;
		}

		/** 追加リクエスト。設定。

			OnTarget内でターゲットリストは変更できない。

		*/
		public void SetTargetRequest(Fee.Ui.OnTarget_CallBackInterface a_callback_interface)
		{
			if(this.target_list.Contains(a_callback_interface) == false){
				if(this.target_add_list.Contains(a_callback_interface) == false){
					this.target_add_list.Add(a_callback_interface);
				}else{
					//すでに登録リストにいる。
				}
			}else{
				//すでにリストにいる。
			}

			this.target_remove_list.Remove(a_callback_interface);
		}

		/** 削除リクエスト。解除。

			OnTarget内でターゲットリストは変更できない。

		*/
		public void UnSetTargetRequest(Fee.Ui.OnTarget_CallBackInterface a_callback_interface)
		{
			if(this.target_remove_list.Contains(a_callback_interface) == false){
				this.target_remove_list.Add(a_callback_interface);
			}else{
				//すでに削除リストにいる。
			}

			this.target_add_list.Remove(a_callback_interface);
		}

		/** ウィンドウ登録。
		*/
		public void RegistWindow(Window_Base a_window)
		{
			this.windowlist.Regist(a_window);
		}

		/** ウィンドウ解除。
		*/
		public void UnRegistWindow(Window_Base a_window)
		{
			this.windowlist.UnRegist(a_window);
		}

		/** ウィンドウを最前面にする。
		*/
		public void SetWindowPriorityTopMost(Window_Base a_window)
		{
			this.windowlist.SetWindowPriorityTopMost(a_window);
		}

		/** ウィンドウスタートレイヤーインデックス。
		*/
		public void SetWindowStartLayerIndex(int a_layerindex)
		{
			this.windowlist.SetStartLayerIndex(a_layerindex);
		}

		/** 最前面ウィンドウ矩形。取得。
		*/
		public bool GetTopWindowXY(out int a_x,out int a_y)
		{
			return this.windowlist.GetTopWindowXY(out a_x,out a_y);
		}

		/** ウィンドウ数。取得。
		*/
		public int GetWindowCount()
		{
			return this.windowlist.GetWindowCount();
		}

		/** ウィンドウレジューム。登録。

			a_new_rect : 新規作成の場合に設定する矩形。

		*/
		public WindowResumeItem RegistWindowResume(string a_label,in Fee.Geometry.Rect2D_R<int> a_new_rect)
		{
			//登録。
			bool t_is_new = false;
			if(this.windowresumelist.Regist(a_label) == true){
				t_is_new = true;
			}

			//取得。
			WindowResumeItem t_item = this.windowresumelist.GetItem(a_label);
			if(t_item != null){
				if(t_is_new == true){
					t_item.rect = a_new_rect;
				}
			}

			return t_item;
		}

		/** ウィンドウレジューム。解除。
		*/
		public void UnRegistWindowResume(string a_label)
		{
			this.windowresumelist.UnRegist(a_label);
		}

		/** ウィンドウレジューム。取得。
		*/
		public WindowResumeItem GetWindowResumeItem(string a_label)
		{
			return this.windowresumelist.GetItem(a_label);
		}

		/** ダウン中ボタンのインスタンス。設定。
		*/
		public void SetDownButtonInstance(Button_Base a_button)
		{
			this.down_button_instance = a_button;
		}

		/** ダウン中ボタンのインスタンス。取得。
		*/
		public Button_Base GetDownButtonInstance()
		{
			return this.down_button_instance;
		}
	}
}

