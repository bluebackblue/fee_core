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
	/** MonoBehaviour_Www
	*/
	public class MonoBehaviour_Www : MonoBehaviour
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
		private bool request_cache;

		[SerializeField]
		private int request_cache_version;

		/** www
		*/
		public WWW www;

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

			//request_cache_version
			this.request_cache_version = 0;

			//request_cache
			this.request_cache = false;

			//www
			this.www = null;

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

							if(this.request_cache == true){
								Tool.Log("MonoBehaviour_WWW","VERSION = " + this.request_cache_version.ToString() + " REQUEST :" + this.request_url);
								this.www = WWW.LoadFromCacheOrDownload(this.request_url,this.request_cache_version);
							}else{
								Tool.Log("MonoBehaviour_WWW","REQUEST : " + this.request_url);
								this.www = new WWW(this.request_url);
							}

							this.mode = Mode.Do;
						}
					}break;
				case Mode.Do:
					{
						//実行中。

						if(this.www.error != null){
							//ダウンロードエラー。
							this.datatype = DataType.Error;

							this.result_errorstring = this.www.error;
							if(this.result_errorstring == null){
								this.result_errorstring = "";
							}

							this.request_url = null;

							this.result_progress = 1.0f;

							this.result_header = new Dictionary<string,string>();

							//解放。
							this.www.Dispose();
							this.www = null;

							this.mode = Mode.WaitRequest;
						}else if(this.www.isDone == true){
							//ダウンロード完了。

							for(int ii=0;ii<3;ii++){
								yield return null;
							}

							//ヘッダ。
							string t_header_contenttype = "";
							{
								this.result_header = this.www.responseHeaders;

								foreach(KeyValuePair<string,string> t_pair in this.result_header){
									Tool.Log("MonoBehaviour_WWW",t_pair.Key + " = " + t_pair.Value);
								}

								if(this.result_header.TryGetValue("Content-Type",out t_header_contenttype) == false){
									t_header_contenttype = "";
								}
							}

							//コンバート。
							{
								DataType t_datatype = Mime.GetDataTypeFromContentType(t_header_contenttype);
								switch(t_datatype){
								case DataType.Texture:
									{
										try{
											this.result_texture = this.www.texture;
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
											this.result_text = this.www.text;
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
								default:
									{
										try{
											this.result_assetbundle = this.www.assetBundle;
										}catch(System.Exception t_exception){
											Tool.LogError(t_exception);
										}

										if(this.result_assetbundle != null){
											this.datatype = DataType.AssetBundle;
										}else{
											this.result_errorstring = "assetbundle convert error";
											this.datatype = DataType.Error;
										}
									}break;
								}

								this.result_errorstring = "";

								this.request_url = null;

								this.result_progress = 1.0f;

								Tool.Log("MonoBehaviour_WWW","Convert : " + this.datatype.ToString());
							}

							//解放。
							this.www.Dispose();
							this.www = null;

							this.mode = Mode.WaitRequest;
						}else{
							//ダウンロード中。
							this.result_progress = this.www.progress;
							if(this.result_progress >= 0.999f){
								this.result_progress = 0.999f;
							}
						}
					}break;
				}

				yield return null;
			}

			Tool.Log("DownLoad","GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
		}

		/** リクエスト。
		*/
		public bool Request(string a_url,bool a_cache,int a_cache_version)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.datatype = DataType.None;

				this.request_url = a_url;
				this.request_cache = a_cache;
				this.request_cache_version = a_cache_version;

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

