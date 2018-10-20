using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 暗号化。
*/


/** NCrypt
*/
namespace NCrypt
{
	/** MonoBehaviour_Security
	*/
	public class MonoBehaviour_Security : MonoBehaviour_Base , OnCoroutine_CallBack
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** 暗号化。パブリックキー。
			*/
			EncryptPublicKey,

			/** 複合化。プライベートキー。
			*/
			DecryptPrivateKey,
		};

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** request_binary
		*/
		[SerializeField]
		private byte[] request_binary;

		/** request_key
		*/
		[SerializeField]
		private string request_key;

		/** [NFile.OnCoroutine_CallBack]コルーチン実行中。

		戻り値 == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
				return false;
			}

			this.SetResultProgress(a_progress);
			return true;
		}

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_binary
			this.request_binary = null;

			//request_key
			this.request_key = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.EncryptPublicKey:
			case RequestType.DecryptPrivateKey:
				{
					Tool.Log("MonoBehaviour_Io",this.request_type.ToString());
					this.SetModeDo();
				}yield break;
			}

			//不明なリクエスト。
			this.SetResultErrorString("request_type == " + this.request_type.ToString());
			this.SetModeDoError();

			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected override IEnumerator OnDo()
		{
			switch(this.request_type){
			case RequestType.EncryptPublicKey:
			case RequestType.DecryptPrivateKey:
				{
					/* TODO
					Coroutine_DownLoadSoundPool t_coroutine = new Coroutine_DownLoadSoundPool();
					yield return t_coroutine.CoroutineMain(this,this.request_url,this.request_data_version);

					if(t_coroutine.result.soundpool != null){
						this.SetResultSoundPool(t_coroutine.result.soundpool);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
					*/
				}break;
			}

			this.SetModeDoError();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override IEnumerator OnDoError()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override IEnumerator OnDoSuccess()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** リクエスト。暗号化。パブリックキー。
		*/
		public bool RequestEncryptPublicKey(byte[] a_binary,string a_key)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.EncryptPublicKey;
				this.request_binary = a_binary;
				this.request_key = a_key;

				return true;
			}

			return false;
		}

		/** リクエスト。複合化。プライベートキー。
		*/
		public bool RequestDecryptPrivateKey(byte[] a_binary,string a_key)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DecryptPrivateKey;
				this.request_binary = a_binary;
				this.request_key = a_key;

				return true;
			}

			return false;
		}
	}
}

