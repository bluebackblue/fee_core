

/**
* Copyright (c) blueback
* Released under the MIT License
* https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
* @brief ２Ｄ描画。バーテックス計算タスク。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Task_CalcVertex
	*/
	public class Task_CalcVertex
	{
		/** render2d
		*/
		private Fee.Render2D.Render2D render2d;

		/** screen
		*/
		private Fee.Render2D.Screen screen;

		/** layerlist
		*/
		private Fee.Render2D.LayerList layerlist;

		/** spritelist
		*/
		private Fee.Render2D.SpriteList spritelist;

		/** task_list
		*/
		private Fee.TaskW.Task<int>[] task_list;

		/** TaskMain
		*/
		private int TaskMain(int a_layer_index)
		{
			try{
				//start_index
				int a_index_start =  this.layerlist.GetStartIndex_Sprite(a_layer_index);
				int a_index_last = this.layerlist.GetLastIndex_Sprite(a_layer_index);

				//計算。
				if((a_index_start >= 0)&&(a_index_last >= 0)){
					for(int ii=a_index_start;ii<=a_index_last;ii++){
						Sprite2D t_sprite = this.spritelist.GetItem(ii);
						if((t_sprite.IsVisible() == true)&&(t_sprite.GetDrawPriority() >= 0)){
							this.screen.CalcSprite(t_sprite);
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return 0;
		}

		/** constructor
		*/
		public Task_CalcVertex(Render2D a_render2d)
		{
			//render2d
			this.render2d = a_render2d;

			//screen
			this.screen = a_render2d.GetScreen();

			//layerlist
			this.layerlist = a_render2d.GetLayerList();

			//spritelist
			this.spritelist = a_render2d.GetSpriteList();

			//関数登録。
			this.task_list = new Fee.TaskW.Task<int>[Config.MAX_LAYER];
			for(int ii=0;ii<this.task_list.Length;ii++){
				this.task_list[ii] = new Fee.TaskW.Task<int>(Fee.TaskW.Mode.InstanceMode_Function);
				int t_layer_index = ii;
				this.task_list[ii].SetFunction(()=>{
					return this.TaskMain(t_layer_index);
				});
			}
		}

		/** 終了待ち。

			スプライトを操作する前に呼び出す。

		*/
		/*
		public void Wait()
		{
			if(Config.USE_ASYNC == true){
				for(int ii=0;ii<this.task_list.Length;ii++){
					if(this.task_list[ii].IsEndFunction() == false){
						if(this.task_list[ii].IsEnd() == false){
							this.task_list[ii].Wait();
						}
						this.task_list[ii].EndFunction();
					}
				}
			}
		}
		*/

		/** 終了待ち。

			描画前に呼び出す。

		*/
		/*
		public void Wait(int a_index)
		{
			if(Config.USE_ASYNC == true){
				if(this.task_list[a_index].IsEndFunction() == false){
					this.task_list[a_index].Wait();
					this.task_list[a_index].EndFunction();
				}
			}
		}
		*/

		/** 開始。

			タスク開始。

		*/
		public void Start()
		{
			/*
			if(Config.USE_ASYNC == true){
				for(int ii=0;ii<this.task_list.Length;ii++){
					this.task_list[ii].StartFunction();
				}
			}else{
				for(int ii=0;ii<this.task_list.Length;ii++){
					this.task_list[ii].StartFunctionDirect();
				}
			}
			*/
			for(int ii=0;ii<this.task_list.Length;ii++){
				this.task_list[ii].StartFunctionDirect();
			}
		}

		/** 削除。
		*/
		public void Delete()
		{
			/*
			if(Config.USE_ASYNC == true){
				for(int ii=0;ii<this.task_list.Length;ii++){
					if(this.task_list[ii].IsEndFunction() == false){
						this.task_list[ii].Wait();
						this.task_list[ii].EndFunction();
					}
				}
			}
			*/

			this.task_list = null;
		}
	}
}

