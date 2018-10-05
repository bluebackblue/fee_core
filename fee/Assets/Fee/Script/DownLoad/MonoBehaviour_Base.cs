using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。
*/


/** NDownLoad
*/
namespace NDownLoad
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
		private Mode mode;

		/** delete_flag
		*/
		[SerializeField]
		private bool delete_flag;

		/** result_progress 
		*/
		[SerializeField]
		private float result_progress;

		/** result_errorstring
		*/
		[SerializeField]
		private string result_errorstring;

		/** result_datatype
		*/
		[SerializeField]
		private DataType result_datatype;

		/** result_binary
		*/
		[SerializeField]
		private byte[] result_binary;

		/** result_text
		*/
		[SerializeField]
		private string result_text;

		/** result_texture
		*/
		[SerializeField]
		private Texture2D result_texture;

		/** result_soundpool
		*/
		[SerializeField]
		private NAudio.Pack_SoundPool result_soundpool;

		/** result_assetbundle
		*/
		[SerializeField]
		private AssetBundle result_assetbundle;

		/** 結果フラグリセット。
		*/
		protected void ResetResultFlag()
		{
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_datatype = DataType.None;

			this.result_binary = null;
			this.result_text = null;
			this.result_texture = null;

			this.result_soundpool = null;
			this.result_assetbundle = null;
		}

		/** プログレス。取得。
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** プログレス。設定。
		*/
		public void SetResultProgress(float a_progress)
		{
			this.result_progress = a_progress;
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

		/** リクエスト待ち。
		*/
		protected bool IsWaitRequest()
		{
			if(this.mode == Mode.WaitRequest){
				return true;
			}
			return false;
		}

		/** 開始。
		*/
		protected void SetModeStart()
		{
			this.mode = Mode.Start;
		}

		/** 実行。
		*/
		protected void SetModeDo()
		{
			this.mode = Mode.Do;
		}

		/** 正常終了。
		*/
		protected void SetModeDoSuccess()
		{
			this.mode = Mode.Do_Success;
		}

		/** エラー終了。
		*/
		protected void SetModeDoError()
		{
			this.mode = Mode.Do_Error;
		}

		/** 完了。
		*/
		protected void SetModeFix()
		{
			this.mode = Mode.Fix;
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

		/** 削除リクエスト。設定。
		*/
		public void DeleteRequest()
		{
			this.delete_flag = true;
		}

		/** 削除リクエスト。取得。
		*/
		public bool IsDeleteRequest()
		{
			return this.delete_flag;
		}

		/** 結果。設定。
		*/
		public void SetResultErrorString(string a_error_string)
		{
			this.result_datatype = DataType.Error;
			this.result_errorstring = a_error_string;
		}

		/** 結果。設定。
		*/
		public void SetResultBinary(byte[] a_binary)
		{
			this.result_datatype = DataType.Binary;
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
			this.result_datatype = DataType.Text;
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
			this.result_datatype = DataType.Texture;
			this.result_texture = a_texture;
		}

		/** 結果。取得。
		*/
		public Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** 結果。設定。
		*/
		public void SetResultSoundPool(NAudio.Pack_SoundPool a_soundpool)
		{
			this.result_datatype = DataType.SoundPool;
			this.result_soundpool = a_soundpool;
		}

		/** 結果。取得。
		*/
		public NAudio.Pack_SoundPool GetResultSoundPool()
		{
			return this.result_soundpool;
		}

		/** 結果。設定。
		*/
		public void SetResultAssetBundle(AssetBundle a_assetbundle)
		{
			this.result_datatype = DataType.AssetBundle;
			this.result_assetbundle = a_assetbundle;
		}

		/** 結果。取得。
		*/
		public AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
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

			this.result_binary = null;
			this.result_text = null;
			this.result_texture = null;

			this.result_soundpool = null;
			this.result_assetbundle = null;

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

