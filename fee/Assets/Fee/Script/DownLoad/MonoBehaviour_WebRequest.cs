using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。ＷＷＷ。
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
		};

		/** delete_flag
		*/
		[SerializeField]
		private bool delete_flag;

		/** mode
		*/
		[SerializeField]
		private Mode mode;

		/** datatype
		*/
		[SerializeField]
		private DataType datatype;

		/** request_url
		*/
		[SerializeField]
		private string request_url;

		[SerializeField]
		private DataType request_datatype;

		[SerializeField]
		private uint request_assetbundle_version;

		[SerializeField]
		private long request_assetbundle_id;

		/** webrequest
		*/
		#if(USE_WWW)
		public WWW www;
		#else
		public UnityEngine.Networking.UnityWebRequest webrequest;
		#endif

		/** result_header
		*/
		[SerializeField]
		private Dictionary<string,string> result_header;

		/** result_errorstring
		*/
		[SerializeField]
		private string result_errorstring;

		/** result_progress
		*/
		[SerializeField]
		private float result_progress;

		/** result_text
		*/
		[SerializeField]
		private string result_text;

		/** result_texture
		*/
		[SerializeField]
		private Texture2D result_texture;

		/** result_assetbundle
		*/
		[SerializeField]
		private AssetBundle result_assetbundle;

		/** Awake
		*/
		private void Awake()
		{
			//delete_flag
			this.delete_flag = false;

			//mode
			this.mode = Mode.WaitRequest;

			//datatype
			this.datatype = DataType.None;

			//request_url
			this.request_url = null;

			//request_assetbundle_version
			this.request_assetbundle_version = 0;

			//request_datatype
			this.request_datatype = DataType.None;

			//request_assetbundle_id
			this.request_assetbundle_id = Config.INVALID_ASSSETBUNDLE_ID;

			//webrequest
			#if(USE_WWW)
			this.www = null;
			#else
			this.webrequest = null;
			#endif

			//result_header
			this.result_header = null;

			//result_errorstring
			this.result_errorstring = "";

			//result_progress
			this.result_progress = 0.0f;

			//result_text
			this.result_text = null;

			//result_texture
			this.result_texture = null;

			//result_assetbundle
			this.result_assetbundle = null;
		}

		/** [内部からの呼び出し]エラー。チェック。
		*/
		private bool Raw_IsError()
		{
			#if(USE_WWW)
			if(this.www.error != null){
				return true;
			}
			#else
			if((this.webrequest.isNetworkError == true)||(this.webrequest.isHttpError == true)){
				return true;
			}
			#endif

			return false;
		}

		/** [内部からの呼び出し]実行中。チェック。
		*/
		private bool Raw_IsDone()
		{
			#if(USE_WWW)
			return this.www.isDone;
			#else
			return this.webrequest.isDone;
			#endif
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
					}break;
				case Mode.Start:
					{
						//開始。

						if(this.request_url != null){
							//リクエストあり。

							#if(USE_WWW)
							{
								if(this.request_datatype == DataType.AssetBundle){
									Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_assetbundle_version.ToString() + " : " + this.request_url);

									this.www = WWW.LoadFromCacheOrDownload(this.request_url,this.request_assetbundle_version);
								}else{
									Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_url);

									this.www = new WWW(this.request_url);
								}
							}
							#else
							{
								switch(this.request_datatype){
								case DataType.AssetBundle:
									{
										Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_assetbundle_version.ToString() + " : " + this.request_url);
										this.webrequest = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(this.request_url,this.request_assetbundle_version,0);
									}break;
								case DataType.Texture:
									{
										Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_url);
										this.webrequest =  UnityEngine.Networking.UnityWebRequestTexture.GetTexture(this.request_url);
									}break;
								case DataType.Text:
									{
										Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_url);
										this.webrequest =  UnityEngine.Networking.UnityWebRequest.Get(this.request_url);	
									}break;
								default:
									{
										Tool.Log("MonoBehaviour_WebRequest",this.request_datatype.ToString() + " : " + this.request_url);
										this.webrequest = UnityEngine.Networking.UnityWebRequest.Get(this.request_url);
									}break;
								}
							}
							#endif

							this.mode = Mode.Do;
						}
					}break;
				case Mode.Do:
					{
						//実行中。

						if(this.Raw_IsError() == true){
							//ダウンロードエラー。
							this.datatype = DataType.Error;

							#if(USE_WWW)
							{
								this.result_errorstring = this.www.error;
							}
							#else
							{
								this.result_errorstring = this.webrequest.error;
							}
							#endif

							if(this.result_errorstring == null){
								this.result_errorstring = "this.webrequest.error == null";
							}

							this.request_url = null;

							this.result_progress = 1.0f;

							this.result_header = new Dictionary<string,string>();

							//解放。
							#if(USE_WWW)
							{
								this.www.Dispose();
								this.www = null;
							}
							#else
							{
								this.webrequest.Dispose();
								this.webrequest = null;
							}
							#endif

							this.mode = Mode.WaitRequest;
						}else if(this.Raw_IsDone() == true){
							//ダウンロード完了。

							for(int ii=0;ii<3;ii++){
								yield return null;
							}

							//ヘッダ。
							string t_header_contenttype = "";
							{
								#if(USE_WWW)
								{
									this.result_header = this.www.responseHeaders;
								}
								#else
								{
									this.result_header = this.webrequest.GetResponseHeaders();
								}
								#endif

								if(this.result_header == null){
									this.result_header = new Dictionary<string,string>();
									Tool.Log("MonoBehaviour_WebRequest","No Header");
								}else{
									foreach(KeyValuePair<string,string> t_pair in this.result_header){
										Tool.Log("MonoBehaviour_WebRequest",t_pair.Key + " = " + t_pair.Value);
									}
								}

								if(this.result_header.TryGetValue("Content-Type",out t_header_contenttype) == false){
									t_header_contenttype = "";
								}
							}

							//コンバート。
							{
								switch(this.request_datatype){
								case DataType.Texture:
									{
										try{
											#if(USE_WWW)
											{
												this.result_texture = this.www.texture;
											}
											#else
											{
												this.result_texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(this.webrequest);
											}
											#endif
										}catch(System.Exception t_exception){
											Tool.LogError(t_exception);
										}

										if(this.result_texture != null){
											this.datatype = DataType.Texture;
										}else{
											this.result_errorstring = "texture convert error";
											this.datatype = DataType.Error;
										}
									}break;
								case DataType.Text:
									{
										try{
											#if(USE_WWW)
											{
												this.result_text = this.www.text;
											}
											#else
											{
												this.result_text = this.webrequest.downloadHandler.text;
											}
											#endif
										}catch(System.Exception t_exception){
											Tool.LogError(t_exception);
										}

										if(this.result_text != null){
											this.datatype = DataType.Text;
										}else{
											this.result_errorstring = "text convert error";
											this.datatype = DataType.Error;
										}
									}break;
								case DataType.AssetBundle:
									{
										try{
											#if(USE_WWW)
											{
												this.result_assetbundle = this.www.assetBundle;
											}
											#else
											{
												this.result_assetbundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(this.webrequest);
											}
											#endif
										}catch(System.Exception t_exception){
											Tool.LogError(t_exception);
										}

										if(this.result_assetbundle != null){

											//アセットバンドルリストに登録。
											NDownLoad.DownLoad.GetInstance().GetAssetBundleList().Regist(this.request_assetbundle_id,this.result_assetbundle);

											this.datatype = DataType.AssetBundle;
										}else{
											this.result_errorstring = "assetbundle convert error";
											this.datatype = DataType.Error;
										}
									}break;
								default:
									{
										this.result_errorstring = "convert datatype error";
										this.datatype = DataType.Error;
									}break;
								}

								this.result_errorstring = "";

								this.request_url = null;

								this.result_progress = 1.0f;

								Tool.Log("MonoBehaviour_WebRequest","Convert : " + this.datatype.ToString());
							}

							//解放。
							#if(USE_WWW)
							{
								this.www.Dispose();
								this.www = null;
							}
							#else
							{
								this.webrequest.Dispose();
								this.webrequest = null;
							}
							#endif

							this.mode = Mode.WaitRequest;
						}else{
							//ダウンロード中。

							yield return this.webrequest.SendWebRequest();

							#if(USE_WWW)
							{
								this.result_progress = this.www.progress;
							}
							#else
							{
								this.result_progress = this.webrequest.downloadProgress;
							}
							#endif

							if(this.result_progress >= 0.999f){
								this.result_progress = 0.999f;
							}else if(this.result_progress < 0.0f){
								this.result_progress = 0.0f;
							}
						}
					}break;
				}

				yield return null;
			}

			//切断。
			#if(USE_WWW)
			if(this.www != null){
				this.www.Dispose();
				this.www = null;
			}
			#endif

			//削除。
			Tool.Log("MonoBehaviour_WebRequest","GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
		}

		/** リクエスト。
		*/
		public bool Request(string a_url,DataType a_datatype,uint a_assetbundle_version,long a_assetbundle_id)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.datatype = DataType.None;

				this.request_url = a_url;
				this.request_datatype = a_datatype;
				this.request_assetbundle_version = a_assetbundle_version;
				this.request_assetbundle_id = a_assetbundle_id;

				this.result_errorstring = "";
				this.result_progress = 0.0f;
				this.result_text = null;
				this.result_texture = null;
				this.result_header = null;

				return true;
			}else{
				return false;
			}
		}

		/** 処理中チェック。
		*/
		public bool IsBusy()
		{
			if(this.mode == Mode.WaitRequest){
				return false;
			}
			return true;
		}

		/** DeleteRequest
		*/
		public void DeleteRequest()
		{
			this.delete_flag = true;
		}

		/** データタイプ。取得。
		*/
		public DataType GetDataType()
		{
			return this.datatype;
		}

		/** エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** プログレス。取得。
		*/
		public float GetProgress()
		{
			return this.result_progress;
		}

		/** 結果。取得。
		*/
		public Dictionary<string,string> GetHeader()
		{
			return this.result_header;
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
		public AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
		}
	}
}

