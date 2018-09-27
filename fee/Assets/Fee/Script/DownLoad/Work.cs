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

					if(t_webrequest.IsFix() == false){
						this.item.SetResultProgress(t_webrequest.GetDownloadProgress());
					}else{
						this.item.SetResultProgress(t_webrequest.GetDownloadProgress());

						//結果。
						switch(t_webrequest.GetResultDataType()){
						case DataType.AssetBundle:
							{
								this.item.SetResultAssetBundle(t_webrequest.GetResultAssetBundle());
							}break;
						case DataType.Text:
							{
								this.item.SetResultText(t_webrequest.GetResultText());
							}break;
						case DataType.Texture:
							{		
								this.item.SetResultTexture(t_webrequest.GetResultTexture());
							}break;
						case DataType.Binary:
							{
								this.item.SetResultBinary(t_webrequest.GetResultBinary());
							}break;
						default:
							{
								string t_error_string = t_webrequest.GetResultErrorString();

								if(t_error_string == null){
									t_error_string = "";
								}

								this.item.SetResultErrorString(t_error_string);
							}break;
						}

						//リクエスト待ち開始。
						t_webrequest.WaitRequest();						

						this.mode = Mode.End;
					}
				}break;
			case Mode.Start_SoundPool:
				{
					MonoBehaviour_SoundPool t_soundpool = NDownLoad.DownLoad.GetInstance().GetSoundPool();

					//リクエスト。
					if(t_soundpool.Request(this.url,this.data_version) == true){
						this.mode = Mode.Do_SoundPool;
					}
				}break;
			case Mode.Do_SoundPool:
				{
					MonoBehaviour_SoundPool t_soundpool = NDownLoad.DownLoad.GetInstance().GetSoundPool();

					if(t_soundpool.IsFix() == false){
						this.item.SetResultProgress(t_soundpool.GetDownloadProgress());
					}else{
						this.item.SetResultProgress(t_soundpool.GetDownloadProgress());

						//結果。
						if(t_soundpool.GetResultDataType() == DataType.SoundPool){
							this.item.SetResultSoundPool(t_soundpool.GetResultSoundPool());
						}else{
							string t_error_string = t_soundpool.GetResultErrorString();

							if(t_error_string == null){
								t_error_string = "";
							}

							this.item.SetResultErrorString(t_error_string);
						}

						//リクエスト待ち開始。
						t_soundpool.WaitRequest();						

						this.mode = Mode.End;
					}
				}break;
			}

			return false;
		}
	}
}

