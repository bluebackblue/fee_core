using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。ダウンロード。
*/


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** MonoBehaviour_DownLoad
	*/
	public class MonoBehaviour_DownLoad : MonoBehaviour_Base
	{
		/** リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ロードストリーミングアセット。バイナリファイル。
			*/
			LoadStreamingAssetsBinaryFile,
		};

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** request_filename
		*/
		[SerializeField]
		private string request_filename;

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_filename
			this.request_filename = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.LoadStreamingAssetsBinaryFile:
				{
					Tool.Log("Monobehaviour_DownLoad",this.request_type.ToString());
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
			case RequestType.LoadStreamingAssetsBinaryFile:
				{
					yield return this.Raw_Do_LoadStreamingAssetsBinaryFile();

					if(this.GetResultType() == ResultType.SaveEnd){
						this.SetModeDoSuccess();
						yield break;
					}
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

		/** リクエスト。
		*/
		public bool RequestLoadStreamingAssetsBinaryFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.LoadStreamingAssetsBinaryFile;
				this.request_filename = a_filename;

				return true;
			}

			return false;
		}

		/** [内部からの呼び出し]ロードストリーミングアセット。バイナリファイル。
		*/
		private IEnumerator Raw_Do_LoadStreamingAssetsBinaryFile()
		{
			string t_full_path = Application.streamingAssetsPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			byte[] t_result = null;
			string t_errorstring = null;

			{
				NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(t_full_path,NDownLoad.DataType.Binary);

				do{
					//プログレス。
					this.SetResultProgress(t_download_item.GetResultProgress());

					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_download_item.Cancel();
					}

					yield return null;
				}while(t_download_item.IsBusy() == true);

				if(t_download_item.GetResultType() == NDownLoad.Item.ResultType.Binary){
					if(t_download_item.GetResultBinary() != null){
						if(t_download_item.GetResultBinary().Length > 0){
							t_result = t_download_item.GetResultBinary();
						}
					}
				}

				if(t_download_item.GetResultErrorString() != null){
					t_errorstring = t_download_item.GetResultErrorString();	
				}
			}

			//結果。
			if(t_result != null){
				this.SetResultBinary(t_result);
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}
	}
}

