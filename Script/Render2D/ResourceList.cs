

/**
* Copyright (c) blueback
* Released under the MIT License
* https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
* @brief ２Ｄ描画。リソースリスト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** ResourceList
	*/
	public class ResourceList
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

		/** constructor
		*/
		public ResourceList(Render2D a_render2d)
		{
			//render2d
			this.render2d = a_render2d;

			//screen
			this.screen = a_render2d.GetScreen();

			//layerlist
			this.layerlist = a_render2d.GetLayerList();

			//spritelist
			this.spritelist = a_render2d.GetSpriteList();

			//textlist
			this.textlist = a_render2d.GetTextList();

			//inputfieldlist
			this.inputfieldlist = a_render2d.GetInputFieldList();

			//frame
			this.frame = 0;
			this.frame_max = 120;
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** ソート計算。

			プライオリティ変更。
			作成。
			削除。

		*/
		public void Calc_Sort()
		{
			this.frame = (this.frame + 1) % this.frame_max;

			this.Calc_SpriteList();
			this.Calc_TextList();
			this.Calc_InputFieldList();

			//表示物のないカメラを非アクティブにする。
			if((this.spritelist.sortend_flag == true)||(this.textlist.sortend_flag == true)||(this.inputfieldlist.sortend_flag == true)){
				this.layerlist.ChangeActiveCamera();
			}

			//テキスト。描画プライオリティに対応したカメラに関連付ける。
			if(this.textlist.sortend_flag == true){
				this.textlist.ChangeParentLayer(this.layerlist);
			}

			//入力フィールド。描画プライオリティに対応したカメラに関連付ける。
			if(this.inputfieldlist.sortend_flag == true){
				this.inputfieldlist.ChangeParentLayer(this.layerlist);
			}

			//フラグリセット。
			this.spritelist.sortend_flag = false;
			this.textlist.sortend_flag = false;
			this.inputfieldlist.sortend_flag = false;
		}

		/** バーテックス計算。
		*/
		public void Calc_Vertex()
		{
			for(int ii=0;ii<Config.MAX_LAYER;ii++){
				//start_index
				int a_index_start =  this.layerlist.GetStartIndex_Sprite(ii);
				int a_index_last = this.layerlist.GetLastIndex_Sprite(ii);

				//計算。
				if((a_index_start >= 0)&&(a_index_last >= 0)){
					for(int jj=a_index_start;jj<=a_index_last;jj++){
						Sprite2D t_sprite = this.spritelist.GetItem(jj);
						if((t_sprite.IsVisible() == true)&&(t_sprite.GetDrawPriority() >= 0)){
							this.screen.CalcSprite(t_sprite);
						}
					}
				}
			}
		}

		/** Calc_SpriteList
		*/
		private int Calc_SpriteList()
		{
			try{
				//削除。
				if((this.frame % this.frame_max) == 0){
					if(this.spritelist.delete_request_flag == true){
						this.spritelist.delete_request_flag = false;
						this.spritelist.calc_index_request_flag = true;
						this.spritelist.ApplyDeleteRequest();
					}
				}

				//ソート。
				if(this.spritelist.sort_request_flag == true){
					this.spritelist.sort_request_flag = false;
					this.spritelist.calc_index_request_flag = true;
					this.spritelist.Sort();
				}

				//インデックス計算。
				if(this.spritelist.calc_index_request_flag == true){
					this.spritelist.calc_index_request_flag = false;
					this.layerlist.CalcSpriteIndex(this.spritelist);

					//ソート済みフラグ。
					this.spritelist.sortend_flag = true;
				}
			}catch(System.OperationCanceledException){
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return 0;
		}

		/** Calc_TextList
		*/
		private int Calc_TextList()
		{
			try{
				//削除。
				if((this.frame % this.frame_max) == 0){
					if(this.textlist.delete_request_flag == true){
						this.textlist.delete_request_flag = false;
						this.textlist.calc_index_request_flag = true;
						this.textlist.ApplyDeleteRequest();
					}
				}

				//ソート。
				if(this.textlist.sort_request_flag == true){
					this.textlist.sort_request_flag = false;
					this.textlist.calc_index_request_flag = true;
					this.textlist.Sort();
				}

				//インデックス計算。
				if(this.textlist.calc_index_request_flag == true){
					this.textlist.calc_index_request_flag = false;
					this.layerlist.CalcTextIndex(this.textlist);

					//ソート済みフラグ。
					this.textlist.sortend_flag = true;
				}
			}catch(System.OperationCanceledException){
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return 0;
		}

		/** Calc_InputFieldList
		*/
		private int Calc_InputFieldList()
		{
			try{
				//削除。
				if((this.frame % this.frame_max) == 0){
					if(this.inputfieldlist.delete_request_flag == true){
						this.inputfieldlist.delete_request_flag = false;
						this.inputfieldlist.calc_index_request_flag = true;
						this.inputfieldlist.ApplyDeleteRequest();
					}
				}

				//ソート。
				if(this.inputfieldlist.sort_request_flag == true){
					this.inputfieldlist.sort_request_flag = false;
					this.inputfieldlist.calc_index_request_flag = true;
					this.inputfieldlist.Sort();
				}

				//インデックス計算。
				if(this.inputfieldlist.calc_index_request_flag == true){
					this.inputfieldlist.calc_index_request_flag = false;
					this.layerlist.CalcInputFieldIndex(this.inputfieldlist);

					//ソート済みフラグ。
					this.inputfieldlist.sortend_flag = true;
				}
			}catch(System.OperationCanceledException){
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return 0;
		}
	}
}

