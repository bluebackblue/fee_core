using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。ＩＯ。
*/


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** MonoBehaviour_Base
	*/
	public abstract class MonoBehaviour_Base : MonoBehaviour
	{
		/** Mode
		*/
		protected enum Mode
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
			Do_Success,

			/** 完了。
			*/
			Fix,
		};

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected abstract void OnInitialize();

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected abstract IEnumerator OnStart();

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected abstract IEnumerator OnDo();

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected abstract IEnumerator OnDoError();

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected abstract IEnumerator OnDoSuccess();

		/** mode
		*/
		[SerializeField]
		protected Mode mode;

		/** delete_flag
		*/
		[SerializeField]
		protected bool delete_flag;

		/** result_progress 
		*/
		[SerializeField]
		protected float result_progress;

		/** result_errorstring
		*/
		[SerializeField]
		protected string result_errorstring;

		/** result_datatype
		*/
		[SerializeField]
		protected DataType result_datatype;

		/** request_binary
		*/
		[SerializeField]
		protected byte[] request_binary;

		/** result_binary
		*/
		[SerializeField]
		private byte[] result_binary;

		/** request_text
		*/
		[SerializeField]
		protected string request_text;

		/** result_text
		*/
		[SerializeField]
		private string result_text;

		/** request_texture
		*/
		[SerializeField]
		protected Texture2D request_texture;

		/** result_texture
		*/
		[SerializeField]
		private Texture2D result_texture;

		/** フラグリセット。
		*/
		protected void ResetFlag()
		{
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_datatype = DataType.None;

			this.request_binary = null;
			this.result_binary = null;
			this.request_text = null;
			this.result_text = null;
			this.request_texture = null;
			this.result_texture = null;
		}

		/** プログレス。取得。
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** データタイプ。取得。
		*/
		public DataType GetResultDataType()
		{
			return this.result_datatype;
		}

		/** リクエスト待ち開始。
		*/
		public void WaitRequest()
		{
			if(this.mode == Mode.Fix){
				this.mode = Mode.WaitRequest;
			}else{
				Tool.Assert(false);
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

		/** DeleteRequest
		*/
		public void DeleteRequest()
		{
			this.delete_flag = true;
		}

		/** 結果。設定。
		*/
		public void SetResultBinary(byte[] a_binary)
		{
			this.result_binary = a_binary;
		}

		/** 結果。取得。
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** 結果。設定。
		*/
		public void SetResultText(string a_text)
		{
			this.result_text = a_text;
		}

		/** 結果。取得。
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** 結果。設定。
		*/
		public void SetResultTexture(Texture2D a_texture)
		{
			this.result_texture = a_texture;
		}

		/** 結果。取得。
		*/
		public Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** Awake
		*/
		private void Awake()
		{
			this.mode = Mode.WaitRequest;
			this.delete_flag = false;
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_datatype = DataType.None;

			this.request_binary = null;
			this.result_binary = null;
			this.request_text = null;
			this.result_text = null;
			this.request_texture = null;
			this.result_texture = null;

			this.OnInitialize();
		}

		/** Start
		*/
		private IEnumerator Start()
		{
			bool t_loop = true;
			while(t_loop){
				switch(this.mode){
				case Mode.WaitRequest:
					{
						yield return null;
						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Fix:
					{
						yield return null;
						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Start:
					{
						yield return this.OnStart();
					}break;
				case Mode.Do:
					{
						yield return  this.OnDo();
					}break;
				case Mode.Do_Error:
					{
						yield return this.OnDoError();
					}break;
				case Mode.Do_Success:
					{
						yield return this.OnDoSuccess();
					}break;
				}
			}

			Tool.Log(this.gameObject.name,"GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
			yield break;
		}
	}
}

