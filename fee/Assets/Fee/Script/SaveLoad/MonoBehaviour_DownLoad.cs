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
					this.mode = Mode.Do;
				}break;
			default:
				{
					//不明なリクエスト。
					this.result_datatype = DataType.Error;
					this.result_errorstring = "request_type == " + this.request_type.ToString();
					Tool.Assert(false);
				}break;
			}

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

					if(this.result_datatype == DataType.SaveEnd){
						this.mode = Mode.Do_Success;
						yield break;
					}
				}break;
			}

			{
				this.result_datatype = DataType.Error;
				if(this.result_errorstring == null){
					this.result_errorstring = "errorstring == null";
				}
			}

			this.mode = Mode.Do_Error;
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override IEnumerator OnDoError()
		{
			this.result_progress = 1.0f;

			this.mode = Mode.Fix;
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override IEnumerator OnDoSuccess()
		{
			this.result_progress = 1.0f;

			this.mode = Mode.Fix;
			yield break;
		}

		/** リクエスト。
		*/
		public bool RequestLoadStreamingAssetsBinaryFile(string a_filename)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;
				this.ResetFlag();

				this.request_type = RequestType.LoadStreamingAssetsBinaryFile;
				this.request_filename = a_filename;

				return true;
			}else{
				return false;
			}
		}

		/** [内部からの呼び出し]実行中。ロードストリーミングアセット。バイナリファイル。
		*/
		private IEnumerator Raw_Do_LoadStreamingAssetsBinaryFile()
		{
			string t_full_path = Application.streamingAssetsPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			byte[] t_result = null;

			{
				NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(t_full_path,NDownLoad.DataType.Binary);

				do{
					this.result_progress = t_download_item.GetResultProgress();

					if(this.delete_flag == true){
						Tool.Log("Raw_Do_LoadStreamingAssetsBinaryFile","Cancel");
						t_download_item.Cancel();
					}
					yield return null;
				}while(t_download_item.IsBusy() == true);

				if(t_download_item.GetResultDataType() == NDownLoad.DataType.Binary){
					if(t_download_item.GetResultBinary() != null){
						if(t_download_item.GetResultBinary().Length > 0){
							t_result = t_download_item.GetResultBinary();
						}
					}
				}

				if(t_download_item.GetResultErrorString() != null){
					this.result_errorstring = t_download_item.GetResultErrorString();	
				}
			}

			//結果。
			if(t_result != null){
				this.SetResultBinary(t_result);
				this.result_datatype = DataType.Binary;
			}else{
				this.result_datatype = DataType.Error;
			}
		}
	}
}

