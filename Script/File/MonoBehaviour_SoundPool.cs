

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。サウンドプール。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** MonoBehaviour_SoundPool
	*/
	public class MonoBehaviour_SoundPool : MonoBehaviour_Base , OnCoroutine_CallBack
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ダウンロード。サウンドプール。
			*/
			DownLoadSoundPool,
		};

		/** request_type
		*/
		[UnityEngine.SerializeField]
		private RequestType request_type;

		/** request_url
		*/
		[UnityEngine.SerializeField]
		private string request_url;

		/** request_data_version
		*/
		[UnityEngine.SerializeField]
		private uint request_data_version;

		/** [Fee.File.OnCoroutine_CallBack]コルーチン実行中。

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

			//request_url
			this.request_url = null;

			//request_data_version
			this.request_data_version = 0;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override System.Collections.IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.DownLoadSoundPool:
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
		protected override System.Collections.IEnumerator OnDo()
		{
			switch(this.request_type){
			case RequestType.DownLoadSoundPool:
				{
					Coroutine_DownLoadSoundPool t_coroutine = new Coroutine_DownLoadSoundPool();
					yield return t_coroutine.CoroutineMain(this,this.request_url,this.request_data_version);

					if(t_coroutine.result.soundpool != null){
						this.SetResultSoundPool(t_coroutine.result.soundpool);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			}

			this.SetModeDoError();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override System.Collections.IEnumerator OnDoError()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override System.Collections.IEnumerator OnDoSuccess()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** リクエスト。ダウンロード。サウンドプール。
		*/
		public bool RequestDownLoadSoundPool(string a_url,uint a_data_version)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DownLoadSoundPool;
				this.request_url = a_url;
				this.request_data_version = a_data_version;

				return true;
			}

			return false;
		}
	}
}

