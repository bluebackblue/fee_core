using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。ウェブリクエスト。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** MonoBehaviour_WebRequest
	*/
	public class MonoBehaviour_WebRequest : MonoBehaviour
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
		private UnityEngine.Networking.UnityWebRequest webrequest;
		private UnityEngine.Networking.UnityWebRequestAsyncOperation webrequest_async;

		/** request_flag
		*/
		[SerializeField]
		private bool request_flag;

		/** request_url
		*/
		[SerializeField]
		private string request_url;

		[SerializeField]
		private DataType request_datatype;

		[SerializeField]
		private uint request_data_version;

		[SerializeField]
		private long request_assetbundle_id;

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

		/** result_assetbundle
		*/
		[SerializeField]
		private AssetBundle result_assetbundle;

		/** result_text
		*/
		[SerializeField]
		private string result_text;

		/** result_texture
		*/
		[SerializeField]
		private Texture2D result_texture;

		/** result_binary
		*/
		[SerializeField]
		private byte[] result_binary;

		/** Awake
		*/
		private void Awake()
		{
			//delete_flag
			this.delete_flag = false;

			//mode
			this.mode = Mode.WaitRequest;

			//webrequest
			this.webrequest = null;
			this.webrequest_async = null;

			//result_datatype
			this.result_datatype = DataType.None;

			//request_url
			this.request_url = null;

			//request_data_version
			this.request_data_version = 0;

			//request_datatype
			this.request_datatype = DataType.None;

			//request_assetbundle_id
			this.request_assetbundle_id = Config.INVALID_ASSSETBUNDLE_ID;

			//result_errorstring
			this.result_errorstring = "";

			//result_download_progress
			this.result_download_progress = 0.0f;

			//result_upload_progress
			this.result_upload_progress = 0.0f;

			//result_assetbundle
			this.result_assetbundle = null;

			//result_text
			this.result_text = null;

			//result_texture
			this.result_texture = null;

			//result_binary
			this.result_binary = null;
		}

		/**  [内部からの呼び出し]開始。
		*/
		private IEnumerator Raw_Start()
		{
			if(this.request_flag == true){
				switch(this.request_datatype){
				case DataType.AssetBundle:
					{
						Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_data_version.ToString() + " : " + this.request_url);
						this.webrequest = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(this.request_url,this.request_data_version,0);
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

		/** [内部からの呼び出し]実行中。
		*/
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

		/** [内部からの呼び出し]エラー終了。
		*/
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

		/** [内部からの呼び出し]終了。
		*/
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
						//開始。
						yield return this.Raw_Start();
					}break;
				case Mode.Do:
					{
						//実行。
						yield return this.Raw_Do();
					}break;
				case Mode.Do_Error:
					{
						//エラー終了。
						yield return this.Raw_DoError();
					}break;
				case Mode.Do_Fix:
					{
						//正常終了。
						yield return this.Raw_DoFix();
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
		public bool Request(string a_url,DataType a_datatype,uint a_data_version,long a_assetbundle_id)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				//開始。
				this.request_flag = true;

				//request
				this.request_url = a_url;
				this.request_datatype = a_datatype;
				this.request_data_version = a_data_version;
				this.request_assetbundle_id = a_assetbundle_id;

				//result
				this.result_errorstring = "";
				this.result_download_progress = 0.0f;
				this.result_upload_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_assetbundle = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_binary = null;

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
		public AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
		}

		/** 結果。取得。
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** 結果。取得。
		*/
		public Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** 結果。取得。
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}
	}
}

