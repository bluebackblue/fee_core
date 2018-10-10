using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。ワーク。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** Work
	*/
	public class Work
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 開始。
			*/
			Start,

			/** 開始。
			*/
			Start_WebRequest,

			/**	実行中。
			*/
			Do_WebRequest,

			/** 開始。
			*/
			Start_SoundPool,

			/** 実行中。
			*/
			Do_SoundPool,

			/** 完了。
			*/
			End
		};

		/** mode
		*/
		private Mode mode;

		/** url
		*/
		private string url;

		/** datatype
		*/
		private DataType datatype;

		/** data_version
		*/
		private uint data_version;

		/** assetbundle_id
		*/
		private long assetbundle_id;

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public Work(string a_url,DataType a_datatype,uint a_data_version,long a_assetbundle_id)
		{
			//mode
			this.mode = Mode.Start;

			//url
			this.url = a_url;

			//datatype
			this.datatype = a_datatype;

			//data_version
			this.data_version = a_data_version;

			//assetbundle_id
			this.assetbundle_id = a_assetbundle_id;

			//item
			this.item = new Item();
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

		戻り値 = true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					if(this.datatype == DataType.AssetBundle){

						//アセットバンドルリストから取得。
						AssetBundle t_assetbundle = null;
						{
							AssetBundleList t_assetbundle_list = NDownLoad.DownLoad.GetInstance().GetAssetBundleList();
							if(this.assetbundle_id != Config.INVALID_ASSSETBUNDLE_ID){
								t_assetbundle = t_assetbundle_list.GetAssetBundle(this.assetbundle_id);
							}
						}

						if(t_assetbundle != null){
							Tool.Log("NDownLoad.Work","GetAssetBundle From AssetBundleList");

							this.item.SetResultProgress(1.0f);
							this.item.SetResultAssetBundle(t_assetbundle);
							this.mode = Mode.End;
							break;
						}

					}else if(this.datatype == DataType.SoundPool){
						this.mode = Mode.Start_SoundPool;
						break;
					}

					this.mode = Mode.Start_WebRequest;
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Start_WebRequest:
				{
					MonoBehaviour_WebRequest t_webrequest = NDownLoad.DownLoad.GetInstance().GetWebRequest();

					//リクエスト。
					if(t_webrequest.Request(this.url,this.datatype,this.data_version,this.assetbundle_id) == true){
						this.mode = Mode.Do_WebRequest;
					}
				}break;
			case Mode.Do_WebRequest:
				{
					MonoBehaviour_WebRequest t_webrequest = NDownLoad.DownLoad.GetInstance().GetWebRequest();

					this.item.SetResultProgress(t_webrequest.GetResultProgress());

					if(t_webrequest.IsFix() == true){
						//結果。
						switch(t_webrequest.GetResultType()){
						case MonoBehaviour_Base.ResultType.Text:
							{
								//テキスト。
								if(t_webrequest.GetResultText() != null){
									this.item.SetResultText(t_webrequest.GetResultText());
								}else{
									this.item.SetResultErrorString("null");
								}
							}break;
						case MonoBehaviour_Base.ResultType.Texture:
							{
								//テクスチャ。	
								if(t_webrequest.GetResultTexture() != null){
									this.item.SetResultTexture(t_webrequest.GetResultTexture());
								}else{
									this.item.SetResultErrorString("null");
								}
							}break;
						case MonoBehaviour_Base.ResultType.Binary:
							{
								//バイナリ。
								if(t_webrequest.GetResultBinary() != null){
									this.item.SetResultBinary(t_webrequest.GetResultBinary());
								}else{
									this.item.SetResultErrorString("null");
								}
							}break;
						case MonoBehaviour_Base.ResultType.AssetBundle:
							{
								//アセットバンドル。
								if(t_webrequest.GetResultAssetBundle() != null){
									this.item.SetResultAssetBundle(t_webrequest.GetResultAssetBundle());
								}else{
									this.item.SetResultErrorString("null");
								}
							}break;
						default:
							{
								this.item.SetResultErrorString(t_webrequest.GetResultErrorString());
							}break;
						}

						//リクエスト待ち開始。
						t_webrequest.WaitRequest();						

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_webrequest.Cancel();
					}
				}break;
			case Mode.Start_SoundPool:
				{
					MonoBehaviour_SoundPool t_soundpool = NDownLoad.DownLoad.GetInstance().GetSoundPool();

					//リクエスト。ダウンロード。
					if(t_soundpool.RequestDownLoad(this.url,this.data_version) == true){
						this.mode = Mode.Do_SoundPool;
					}
				}break;
			case Mode.Do_SoundPool:
				{
					MonoBehaviour_SoundPool t_soundpool = NDownLoad.DownLoad.GetInstance().GetSoundPool();

					this.item.SetResultProgress(t_soundpool.GetResultProgress());

					if(t_soundpool.IsFix() == true){
						//結果。
						if(t_soundpool.GetResultType() == MonoBehaviour_Base.ResultType.SoundPool){
							if(t_soundpool.GetResultSoundPool() != null){
								this.item.SetResultSoundPool(t_soundpool.GetResultSoundPool());
							}else{
								this.item.SetResultErrorString("null");
							}
						}else{
							this.item.SetResultErrorString(t_soundpool.GetResultErrorString());
						}

						//リクエスト待ち開始。
						t_soundpool.WaitRequest();						

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_soundpool.Cancel();
					}
				}break;
			}

			return false;
		}
	}
}

