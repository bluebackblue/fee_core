using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。サウンドプール。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** MonoBehaviour_SoundPool
	*/
	public class MonoBehaviour_SoundPool : MonoBehaviour
	{
		/** Mode
		*/
		private enum Mode
		{
			/** リクエスト待ち。
			*/
			WaitRequest,

			/** 開始。
			*/
			Start,

			/** 実行中。
			*/
			Do,

			/** エラー終了。
			*/
			Do_Error,

			/** 正常終了。
			*/
			Do_Fix,

			/** 完了。
			*/
			Fix,
		};

		/** delete_flag
		*/
		[SerializeField]
		private bool delete_flag;

		/** mode
		*/
		[SerializeField]
		private Mode mode;

		/** webrequest
		*/
		/*
		private UnityEngine.Networking.UnityWebRequest webrequest;
		private UnityEngine.Networking.UnityWebRequestAsyncOperation webrequest_async;
		*/

		/** request_flag
		*/
		[SerializeField]
		private bool request_flag;

		/** request_url
		*/
		[SerializeField]
		private string request_url;

		/*
		[SerializeField]
		private DataType request_datatype;
		*/

		[SerializeField]
		private uint request_data_version;

		/*
		[SerializeField]
		private long request_assetbundle_id;
		*/

		/** result_errorstring
		*/
		[SerializeField]
		private string result_errorstring;

		/** result_download_progress
		*/
		[SerializeField]
		private float result_download_progress;

		/** result_upload_progress
		*/
		[SerializeField]
		private float result_upload_progress;

		/** result_datatype
		*/
		[SerializeField]
		private DataType result_datatype;

		/** result
		*/
		[SerializeField]
		private NAudio.Pack_SoundPool result_soundpool;

		/** Awake
		*/
		private void Awake()
		{
			//delete_flag
			this.delete_flag = false;

			//mode
			this.mode = Mode.WaitRequest;

			//webrequest
			/*
			this.webrequest = null;
			this.webrequest_async = null;
			*/

			//result_datatype
			this.result_datatype = DataType.None;

			//request_url
			this.request_url = null;

			//request_data_version
			this.request_data_version = 0;

			//request_datatype
			/*
			this.request_datatype = DataType.None;
			*/

			//request_assetbundle_id
			/*
			this.request_assetbundle_id = Config.INVALID_ASSSETBUNDLE_ID;
			*/

			//result_errorstring
			this.result_errorstring = "";

			//result_download_progress
			this.result_download_progress = 0.0f;

			//result_upload_progress
			this.result_upload_progress = 0.0f;

			//result_soundpool
			this.result_soundpool = null;
		}

		/**  [内部からの呼び出し]開始。
		*/
		/*
		private IEnumerator Raw_Start()
		{
			if(this.request_flag == true){
				switch(this.request_datatype){
				case DataType.AssetBundle:
					{
						Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_assetbundle_version.ToString() + " : " + this.request_url);
						this.webrequest = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(this.request_url,this.request_assetbundle_version,0);
					}break;
				case DataType.Text:
					{
						Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_url);
						this.webrequest = UnityEngine.Networking.UnityWebRequest.Get(this.request_url);	
					}break;
				case DataType.Texture:
					{
						Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_url);
						this.webrequest = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(this.request_url);
					}break;
				case DataType.Binary:
					{
						Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_url);
						this.webrequest = UnityEngine.Networking.UnityWebRequest.Get(this.request_url);
					}break;
				default:
					{
						Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_url);
						this.webrequest = UnityEngine.Networking.UnityWebRequest.Get(this.request_url);
					}break;
				}

				if(this.webrequest != null){
					this.webrequest_async = this.webrequest.SendWebRequest();
				}

				this.mode = Mode.Do;
			}

			yield return null;

			yield break;
		}
		*/

		/** [内部からの呼び出し]実行中。
		*/
		/*
		private IEnumerator Raw_Do()
		{
			//エラーチェック。
			if((this.webrequest.isNetworkError == true)||(this.webrequest.isHttpError == true)){
				//エラー終了。
				this.mode = Mode.Do_Error;
				yield break;
			}

			//実行中チェック。
			if(this.webrequest.isDone == false){
				this.result_download_progress = this.webrequest.downloadProgress;
				this.result_upload_progress = this.webrequest.uploadProgress;

				if(this.result_download_progress >= 0.999f){
					this.result_download_progress = 0.999f;
				}else if(this.result_download_progress < 0.0f){
					this.result_download_progress = 0.0f;
				}

				if(this.result_upload_progress >= 0.999f){
					this.result_upload_progress = 0.999f;
				}else if(this.result_upload_progress < 0.0f){
					this.result_upload_progress = 0.0f;
				}
			}else{
				if((this.webrequest.isNetworkError == true)||(this.webrequest.isHttpError == true)){
					//エラー終了。
					this.mode = Mode.Do_Error;
					yield break;
				}else{
					//正常終了。
					this.mode = Mode.Do_Fix;
					yield break;
				}
			}

			yield return null;

			yield break;
		}
		*/

		/** [内部からの呼び出し]エラー終了。
		*/
		/*
		private IEnumerator Raw_DoError()
		{
			//終了確認。
			if(this.webrequest_async != null){
				yield return this.webrequest_async;
			}

			{
				this.result_datatype = DataType.Error;
				this.result_errorstring = this.webrequest.error;
				if(this.result_errorstring == null){
					this.result_errorstring = "error == null";
				}
			}

			//解放。
			{
				this.webrequest_async = null;
				this.webrequest.Dispose();
				this.webrequest = null;
			}

			//終了。
			this.result_download_progress = 1.0f;
			this.result_upload_progress = 1.0f;
			this.request_flag = false;

			this.mode = Mode.Fix;

			yield return null;

			yield break;
		}
		*/

		/** [内部からの呼び出し]終了。
		*/
		/*
		private IEnumerator Raw_DoFix()
		{
			//終了確認。
			if(this.webrequest_async != null){
				yield return this.webrequest_async;
			}

			//コンバート。
			{
				switch(this.request_datatype){
				case DataType.AssetBundle:
					{
						try{
							this.result_assetbundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(this.webrequest);
						}catch(System.Exception t_exception){
							Tool.LogError(t_exception);
						}

						if(this.result_assetbundle != null){

							//アセットバンドルリストに登録。
							NDownLoad.DownLoad.GetInstance().GetAssetBundleList().Regist(this.request_assetbundle_id,this.result_assetbundle);

							this.result_errorstring = "";
							this.result_datatype = DataType.AssetBundle;
						}else{
							this.result_errorstring = "ConvertError : AssetBundle";
							this.result_datatype = DataType.Error;
						}
					}break;
				case DataType.Text:
					{
						try{
							this.result_text = this.webrequest.downloadHandler.text;
						}catch(System.Exception t_exception){
							Tool.LogError(t_exception);
						}

						if(this.result_text != null){
							this.result_errorstring = "";
							this.result_datatype = DataType.Text;
						}else{
							this.result_errorstring = "ConvertError : Text";
							this.result_datatype = DataType.Error;
						}
					}break;
				case DataType.Texture:
					{
						try{
							this.result_texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(this.webrequest);
						}catch(System.Exception t_exception){
							Tool.LogError(t_exception);
						}

						if(this.result_texture != null){
							this.result_errorstring = "";
							this.result_datatype = DataType.Texture;
						}else{
							this.result_errorstring = "ConvertError : Texture";
							this.result_datatype = DataType.Error;
						}
					}break;
				case DataType.Binary:
					{
						try{
							this.result_binary = this.webrequest.downloadHandler.data;
						}catch(System.Exception t_exception){
							Tool.LogError(t_exception);
						}

						if(this.result_binary != null){
							this.result_errorstring = "";
							this.result_datatype = DataType.Binary;
						}else{
							this.result_errorstring = "ConvertError : Binary";
							this.result_datatype = DataType.Error;
						}
					}break;
				default:
					{
						this.result_errorstring = "ConvertError : Unknown";
						this.result_datatype = DataType.Error;
					}break;
				}

				Tool.Log("MonoBehaviour_WebRequest","Convert : " + this.result_datatype.ToString());
			}

			//解放。
			{
				this.webrequest_async = null;
				this.webrequest.Dispose();
				this.webrequest = null;
			}

			//終了。
			this.result_download_progress = 1.0f;
			this.result_upload_progress = 1.0f;
			this.request_flag = false;

			this.mode = Mode.Fix;

			yield return null;

			yield break;
		}
		*/

		/** Start
		*/
		private IEnumerator Start()
		{
			while(this.delete_flag == false){
				switch(this.mode){
				case Mode.WaitRequest:
					{
						//リクエスト待ち。
						yield return null;
					}break;
				case Mode.Start:
					{
						if(this.request_flag == true){

							//サウンドプール名。
							string t_soundpool_name = System.IO.Path.GetFileName(this.request_url);
							string t_soundpool_url_root = this.request_url.Substring(0,this.request_url.Length - t_soundpool_name.Length);
							{
								if((t_soundpool_url_root[t_soundpool_url_root.Length - 1] != '\\')&&(t_soundpool_url_root[t_soundpool_url_root.Length - 1] != '/')){
									t_soundpool_url_root += "/";
								}
								Tool.Log("SoundPoolUrlRoot",t_soundpool_url_root);

								if(System.Text.RegularExpressions.Regex.IsMatch(t_soundpool_name,"[0-9a-zA-Z][0-9a-zA-Z\\.\\_\\-]*") == true){
									Tool.Log("SoundPoolName",t_soundpool_name);
								}else{
									//失敗。
									this.result_errorstring = "SoundPoolName : " + t_soundpool_name;
									this.mode = Mode.Do_Error;
									break;
								}
							}
	

							//ローカルサウンドプール管理ＪＳＯＮ。ロード。
							NAudio.Pack_SoundPool t_local_soundpool = null;
							{
								{
									NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclTextFile(t_soundpool_name);
									{
										do{
											yield return null;
										}while(t_saveload_item.IsBusy() == true);

										if(t_saveload_item.GetDataType() == NSaveLoad.DataType.Text){
											t_local_soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_saveload_item.GetResultText()));
										}else{
											t_local_soundpool = null;
										}
									}
								}

								if(t_local_soundpool != null){
									if(t_local_soundpool.data_version == this.request_data_version){
										//ローカルのものとデータバージョン一致。
										this.result_datatype = DataType.SoundPool;
										this.result_soundpool = t_local_soundpool;
										this.mode = Mode.Do_Fix;
										break;
									}
								}
							}

							//サウンドプール管理ＪＳＯＮ。ダウンロード。
							NAudio.Pack_SoundPool t_soundpool = null;
							{
								NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(t_soundpool_url_root + t_soundpool_name,NDownLoad.DataType.Text);

								do{
									yield return null;
								}while(t_download_item.IsBusy() == true);

								if(t_download_item.GetDataType() == DataType.Text){
									t_soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_download_item.GetResultText()));
									if(t_soundpool == null){
										//失敗。
										this.result_errorstring = "SoundPoolObject == null";
										this.mode = Mode.Do_Error;
										break;
									}
									if(t_soundpool.name_list == null){
										//失敗。
										this.result_errorstring = "SoundPoolObject.name_list == null";
										this.mode = Mode.Do_Error;
										break;
									}

									for(int ii=0;ii<t_soundpool.name_list.Count;ii++){
										if(t_soundpool.name_list[ii] == null){
											//失敗。
											this.result_errorstring = "SoundPoolObject.name_list == null";
											this.mode = Mode.Do_Error;
										}
										if(System.Text.RegularExpressions.Regex.IsMatch(t_soundpool.name_list[ii],"[0-9a-zA-Z][0-9a-zA-Z\\.\\_\\-]*") == true){
											Tool.Log("SoundPoolObject.name_list : " + ii.ToString(),t_soundpool.name_list[ii]);
										}else{
											//失敗。
											this.result_errorstring = "SoundPoolObject.name_list : " + t_soundpool.name_list[ii];
											this.mode = Mode.Do_Error;
										}
									}
									if(this.mode == Mode.Do_Error){
										//失敗。
										break;
									}
								}else{
									//失敗。
									this.result_errorstring = t_download_item.GetResultErrorString();
									this.mode = Mode.Do_Error;
									break;
								}
							}

							bool t_need_download = true;
							if(t_local_soundpool != null){
								if(t_local_soundpool.data_hash == t_soundpool.data_hash){
									//ローカルのものとデータハッシュ一致。
									t_need_download = false;
								}
							}

							if(t_need_download == true){
								for(int ii=0;ii<t_soundpool.name_list.Count;ii++){
									//リストアイテム。ダウンロード。
									byte[] t_binary = null;
									{
										NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(t_soundpool_url_root + t_soundpool.name_list[ii],NDownLoad.DataType.Binary);
										{
											do{
												yield return null;
											}while(t_download_item.IsBusy() == true);

											if(t_download_item.GetDataType() == NDownLoad.DataType.Binary){
												t_binary = t_download_item.GetResultBinary();
												if(t_binary == null){
													//失敗。
													this.result_errorstring = "ListItemBianry == null";
													this.mode = Mode.Do_Error;
													break;
												}else if(t_binary.Length < 1){
													//失敗。
													this.result_errorstring = "ListItemBianry.Length < 1";
													this.mode = Mode.Do_Error;
													break;
												}
											}else{
												//失敗。
												this.result_errorstring = t_download_item.GetResultErrorString();
												this.mode = Mode.Do_Error;
												break;
											}
										}
									}

									//リストアイテム。保存。
									{
										NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalBinaryFile(t_soundpool.name_list[ii],t_binary);
										{
											do{
												yield return null;
											}while(t_saveload_item.IsBusy() == true);

											if(t_saveload_item.GetDataType() == NSaveLoad.DataType.Binary){
											}else{
												//失敗。
												this.result_errorstring = "RequestSaveLocalBinaryFile : error : " + t_soundpool.name_list[ii];
												this.mode = Mode.Do_Error;
												break;
											}
										}
									}
								}
							}

							//サウンドプール管理ＪＳＯＮ。保存。
							{
								t_soundpool.data_version = this.request_data_version;

								NJsonItem.JsonItem t_soundpool_json = NJsonItem.ObjectToJson.Convert(t_soundpool);
								if(t_soundpool_json != null){
									NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalTextFile(t_soundpool_name,t_soundpool_json.ConvertJsonString());
									{
										do{
											yield return null;
										}while(t_saveload_item.IsBusy() == true);

										if(t_saveload_item.GetDataType() == NSaveLoad.DataType.Text){
										}else{
											//失敗。
											this.result_errorstring = "RequestSaveLocalTextFile : error : " + t_soundpool_name;
											this.mode = Mode.Do_Error;
											break;
										}
									}
								}else{
									Tool.Assert(false);
								}
							}

							this.result_datatype = DataType.SoundPool;
							this.result_soundpool = t_soundpool;
							this.mode = Mode.Do_Fix;
						}
						
					}break;
				case Mode.Do_Error:
					{
						//エラー終了。

						if(this.result_errorstring == null){
							this.result_errorstring = "error == null";
						}

						this.result_datatype = DataType.Error;
						this.result_soundpool = null;

						//終了。
						this.result_download_progress = 1.0f;
						this.result_upload_progress = 1.0f;
						this.request_flag = false;

						this.mode = Mode.Fix;
					}break;
				case Mode.Do_Fix:
					{
						//正常終了。

						this.result_errorstring = null;

						//終了。
						this.result_download_progress = 1.0f;
						this.result_upload_progress = 1.0f;
						this.request_flag = false;

						this.mode = Mode.Fix;
					}break;
				case Mode.Fix:
					{
						yield return null;
					}break;
				}
			}

			//削除。
			Tool.Log("MonoBehaviour_WebRequest","GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
		}

		/** リクエスト。
		*/
		public bool Request(string a_url,uint a_data_version)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				//開始。
				this.request_flag = true;

				//request
				this.request_url = a_url;
				this.request_data_version = a_data_version;

				//result
				this.result_errorstring = "";
				this.result_download_progress = 0.0f;
				this.result_upload_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_soundpool = null;

				return true;
			}else{
				return false;
			}
		}

		/** 完了チェック。
		*/
		public bool IsFix()
		{
			if(this.mode == Mode.Fix){
				return true;
			}
			return false;
		}

		/** リクエスト待ち開始。
		*/
		public void WaitRequest()
		{
			if(this.mode == Mode.Fix){
				this.mode = Mode.WaitRequest;
			}
		}

		/** DeleteRequest
		*/
		public void DeleteRequest()
		{
			this.delete_flag = true;
		}

		/** データタイプ。取得。
		*/
		public DataType GetResultDataType()
		{
			return this.result_datatype;
		}

		/** エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** プログレス。取得。
		*/
		public float GetDownloadProgress()
		{
			return this.result_download_progress;
		}

		/** プログレス。取得。
		*/
		public float GetUploadProgress()
		{
			return this.result_upload_progress;
		}

		/** 結果。取得。
		*/
		public NAudio.Pack_SoundPool GetResultSoundPool()
		{
			return this.result_soundpool;
		}
	}
}

