

/**
* Copyright (c) blueback
* Released under the MIT License
* https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
* @brief ２Ｄ描画。ソートリストタスク。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Task_SortList
	*/
	public class Task_SortList
	{
		/** render2d
		*/
		private Fee.Render2D.Render2D render2d;

		/** layerlist
		*/
		private Fee.Render2D.LayerList layerlist;

		/** spritelist
		*/
		private Fee.Render2D.SpriteList spritelist;

		/** textlist
		*/
		private Fee.Render2D.TextList textlist;

		/** inputfieldlist
		*/
		private Fee.Render2D.InputFieldList inputfieldlist;

		/** frame
		*/
		private int frame;
		private int frame_max;

		/** canceltoken
		*/
		private Fee.TaskW.CancelToken canceltoken;

		/** task_list
		*/
		private Fee.TaskW.Task<int>[] task_list;

		/** TaskMain_SpriteList
		*/
		private int TaskMain_SpriteList(Fee.TaskW.CancelToken a_cancel_token)
		{
			try{
				//削除。
				if((this.frame % this.frame_max) == 0){
					if(this.spritelist.delete_request_flag == true){
						this.spritelist.delete_request_flag = false;
						this.spritelist.calc_index_request_flag = true;
						this.spritelist.Task_Update();
					}
				}

				//キャンセル処理。
				if(a_cancel_token.IsCancellationRequested() == true){
					a_cancel_token.ThrowIfCancellationRequested();
					return -1;
				}

				//ソート。
				if(this.spritelist.sort_request_flag == true){
					this.spritelist.sort_request_flag = false;
					this.spritelist.calc_index_request_flag = true;
					this.spritelist.Sort();
				}

				//キャンセル処理。
				if(a_cancel_token.IsCancellationRequested() == true){
					a_cancel_token.ThrowIfCancellationRequested();
					return -1;
				}

				//インデックス計算。
				if(this.spritelist.calc_index_request_flag == true){
					this.spritelist.calc_index_request_flag = false;
					this.layerlist.CalcSpriteIndex(this.spritelist);

					//全行程完了。
					this.spritelist.sortend_flag = true;
				}
			}catch(System.OperationCanceledException){
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return 0;
		}

		/** TaskMain_TextList
		*/
		private int TaskMain_TextList(Fee.TaskW.CancelToken a_cancel_token)
		{
			try{
				//削除。
				if((this.frame % this.frame_max) == 0){
					if(this.textlist.delete_request_flag == true){
						this.textlist.delete_request_flag = false;
						this.textlist.calc_index_request_flag = true;
						this.textlist.Task_Update();
					}
				}

				//キャンセル処理。
				if(a_cancel_token.IsCancellationRequested() == true){
					a_cancel_token.ThrowIfCancellationRequested();
					return -1;
				}

				//ソート。
				if(this.textlist.sort_request_flag == true){
					this.textlist.sort_request_flag = false;
					this.textlist.calc_index_request_flag = true;
					this.textlist.Sort();
				}

				//キャンセル処理。
				if(a_cancel_token.IsCancellationRequested() == true){
					a_cancel_token.ThrowIfCancellationRequested();
					return -1;
				}

				//インデックス計算。
				if(this.textlist.calc_index_request_flag == true){
					this.textlist.calc_index_request_flag = false;
					this.layerlist.CalcTextIndex(this.textlist);

					//全行程完了。
					this.textlist.sortend_flag = true;
				}
			}catch(System.OperationCanceledException){
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return 0;
		}

		/** TaskMain_InputFieldList
		*/
		private int TaskMain_InputFieldList(Fee.TaskW.CancelToken a_cancel_token)
		{
			try{
				//削除。
				if((this.frame % this.frame_max) == 0){
					if(this.inputfieldlist.delete_request_flag == true){
						this.inputfieldlist.delete_request_flag = false;
						this.inputfieldlist.calc_index_request_flag = true;
						this.inputfieldlist.Task_Update();
					}
				}

				//キャンセル処理。
				if(a_cancel_token.IsCancellationRequested() == true){
					a_cancel_token.ThrowIfCancellationRequested();
					return -1;
				}

				//ソート。
				if(this.inputfieldlist.sort_request_flag == true){
					this.inputfieldlist.sort_request_flag = false;
					this.inputfieldlist.calc_index_request_flag = true;
					this.inputfieldlist.Sort();
				}

				//キャンセル処理。
				if(a_cancel_token.IsCancellationRequested() == true){
					a_cancel_token.ThrowIfCancellationRequested();
					return -1;
				}

				//インデックス計算。
				if(this.inputfieldlist.calc_index_request_flag == true){
					this.inputfieldlist.calc_index_request_flag = false;
					this.layerlist.CalcInputFieldIndex(this.inputfieldlist);

					//全行程完了。
					this.inputfieldlist.sortend_flag = true;
				}
			}catch(System.OperationCanceledException){
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return 0;
		}

		/** constructor
		*/
		public Task_SortList(Render2D a_render2d)
		{
			//render2d
			this.render2d = a_render2d;

			//layerlist
			this.layerlist = a_render2d.GetLayerList();

			//spritelist
			this.spritelist = a_render2d.GetSpriteList();

			//textlist
			this.textlist = a_render2d.GetTextList();

			//inputfieldlist
			this.inputfieldlist = a_render2d.GetInputFieldList();

			//canceltoken
			this.canceltoken = new Fee.TaskW.CancelToken();

			//frame
			this.frame = 0;
			this.frame_max = 120;

			//関数登録。
			this.task_list = new Fee.TaskW.Task<int>[3];
			for(int ii=0;ii<this.task_list.Length;ii++){
				this.task_list[ii] = new Fee.TaskW.Task<int>(Fee.TaskW.Mode.InstanceMode_Function);
			}
			this.task_list[0].SetFunction(()=>{
				return this.TaskMain_SpriteList(this.canceltoken);
			});
			this.task_list[1].SetFunction(()=>{
				return this.TaskMain_TextList(this.canceltoken);
			});
			this.task_list[2].SetFunction(()=>{
				return this.TaskMain_InputFieldList(this.canceltoken);
			});
		}

		/** 終了待ち。

			スプライトを操作する前に呼び出す。

		*/
		public void Wait()
		{
			for(int ii=0;ii<this.task_list.Length;ii++){
				if(this.task_list[ii].IsEndFunction() == false){
					if(this.task_list[ii].IsEnd() == false){
						this.task_list[ii].Wait();
					}
					this.task_list[ii].EndFunction();
				}
			}
		}

		/** キャンセル終了待ち。
		*/
		public void CancelWait()
		{
			for(int ii=0;ii<this.task_list.Length;ii++){
				if(this.task_list[ii].IsEndFunction() == false){
					if(this.task_list[ii].IsEnd() == false){
						this.canceltoken.Cancel();
						this.task_list[ii].Wait();
					}
					this.task_list[ii].EndFunction();
				}
			}
		}

		/** 終了待ち。

			描画前に呼び出す。

		*/
		public void Wait(int a_index)
		{
			if(this.task_list[a_index].IsEndFunction() == false){
				this.task_list[a_index].Wait();
				this.task_list[a_index].EndFunction();
			}
		}

		/** 開始。

			タスク開始。

		*/
		public void Start()
		{
			this.frame = (this.frame + 1) % this.frame_max;

			if(this.canceltoken.IsCancellationRequested() == true){
				this.canceltoken.Reset();
			}

			for(int ii=0;ii<this.task_list.Length;ii++){
				this.task_list[ii].StartFunction();
			}
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.canceltoken.Cancel();
			for(int ii=0;ii<this.task_list.Length;ii++){
				if(this.task_list[ii].IsEndFunction() == false){
					this.task_list[ii].Wait();
					this.task_list[ii].EndFunction();
				}
			}
			this.task_list = null;
		}
	}
}

