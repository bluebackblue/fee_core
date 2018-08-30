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
							Tool.Log("MonoBehaviour_WWW",this.request_url);

							this.www = new WWW(this.request_url);
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
							this.result_progress = 1.0f;
						}else if(this.www.isDone == true){
							//ダウンロード完了。
							this.result_errorstring = "";

							this.request_url = null;

							this.result_progress = 1.0f;

							this.result_header = this.www.responseHeaders;

							//ヘッダ。
							string t_header_contenttype = "";
							{
								if(this.result_header.TryGetValue("Content-Type",out t_header_contenttype) == false){
									t_header_contenttype = "";
								}
							}

							//コンバート。
							try{
								this.datatype = Mime.GetDataTypeFromContentType(t_header_contenttype);
								switch(this.datatype){
								case DataType.Texture:
									{
										this.result_texture = this.www.texture;
									}break;
								case DataType.Text:
									{
										this.result_text = this.www.text;
									}break;
								default:
									{
										this.datatype = DataType.Error;
									}break;
								}

								Tool.Log("MonoBehaviour_WWW","Convert : " + this.datatype.ToString());
							}catch(System.Exception t_exception){
								Tool.LogError(t_exception);
								this.datatype = DataType.Error;
							}

							//解放。
							this.www = null;

							this.mode = Mode.WaitRequest;
						}else{
							//ダウンロード中。
							this.result_progress = this.www.progress;
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
		public bool Request(string a_url)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.datatype = DataType.None;
				this.request_url = a_url;
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
		public string GetErrorString()
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
	}
}

