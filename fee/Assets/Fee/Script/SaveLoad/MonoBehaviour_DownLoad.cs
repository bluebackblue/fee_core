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
		/** delete_flag
		*/
		//[SerializeField]
		//private bool delete_flag;

		/** mode
		*/
		//[SerializeField]
		//private Mode mode;

		/** reqest_filename
		*/
		[SerializeField]
		private string request_filename;

		/** result_errorstring
		*/
		//[SerializeField]
		//private string result_errorstring;

		/** result_progress 
		*/
		//[SerializeField]
		//private float result_progress;

		/** result_datatype
		*/
		//[SerializeField]
		//private DataType result_datatype;

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

			//reqest_filename
			this.request_filename = null;

			//result_errorstring
			this.result_errorstring = null;

			//result_progress 
			this.result_progress = 0.0f;

			//result_datatype
			this.result_datatype = DataType.None;

			//result_binary
			this.result_binary = null;
		}

		/** リクエスト。
		*/
		public bool Request(string a_filename)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.request_filename = a_filename;

				this.result_errorstring = null;
				this.result_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_binary = null;

				return true;
			}
			return false;
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

		/** プログレス。取得。
		*/
		/*
		public float GetResultProgress()
		{
			return this.result_progress;
		}
		*/

		/** データタイプ。取得。
		*/
		/*
		public DataType GetResultDataType()
		{
			return this.result_datatype;
		}
		*/

		/** 結果。取得。
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** エラー文字。取得。
		*/
		/*
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}
		*/

		/** プログレス。設定。
		*/
		public void SetProgressFromTask(float a_progress)
		{
			Tool.Log("MonoBehaviour_DownLoad","progress = " + a_progress.ToString());
			this.result_progress = a_progress;
		}

		/** [内部からの呼び出し]実行中。ロードストリーミングアセット。バイナリファイル。
		*/
		private IEnumerator Raw_Do_LoadStreamingAssetsBinaryFile()
		{
			NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(Application.streamingAssetsPath + "/" + this.request_filename,NDownLoad.DataType.Binary);
			{
				do{
					this.result_progress = t_download_item.GetResultProgress();

					if(this.delete_flag == true){
						//キャンセル。

						Tool.LogError("Raw_Do_LoadStreamingAssetsBinaryFile","Cancel");

						this.result_errorstring = "Raw_Do_LoadStreamingAssetsBinaryFile : Cancel";
						this.mode = Mode.Fix;
						yield break;
					}
				}while(t_download_item.IsBusy() == true);

				if(t_download_item.GetResultDataType() == NDownLoad.DataType.Binary){
					this.result_binary = t_download_item.GetResultBinary();

					if(this.result_binary == null){
						//エラー。

						this.result_errorstring = "Raw_Do_LoadStreamingAssetsBinaryFile : binary == null";
						this.mode = Mode.Fix;
						yield break;
					}

					if(this.result_binary.Length <= 0){
						//エラー。

						this.result_errorstring = "Raw_Do_LoadStreamingAssetsBinaryFile : binary.length <= 0";
						this.mode = Mode.Fix;
						yield break;
					}
				}else{
					//失敗。

					this.result_errorstring = t_download_item.GetResultErrorString();
					this.mode = Mode.Fix;
					yield break;
				}
			}

			//成功。
			this.result_errorstring = null;
			this.mode = Mode.Fix;
			yield break;
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
						//リクエスト待ち。
						yield return null;

						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Start:
					{
						this.mode = Mode.Do;
					}break;
				case Mode.Fix:
					{
						yield return null;

						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				}
			}

			Tool.Log("MonoBehaviour_DownLoad","GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
		}
	}
}

