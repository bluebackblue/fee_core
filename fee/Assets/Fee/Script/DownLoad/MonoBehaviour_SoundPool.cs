using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。サウンドプール。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** MonoBehaviour_SoundPool
	*/
	public class MonoBehaviour_SoundPool : MonoBehaviour
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

		/** Work
		*/
		private class Work
		{
			public string filename;
			public string urlpath;
			public NAudio.Pack_SoundPool local_soundpool;
			public NAudio.Pack_SoundPool soundpool;
			public byte[] download_binary;
			public int progress_step_max;
			public int progress_step;
			public int progress_substep_max;
			public int progress_substep;

			/** constructor
			*/
			public Work()
			{
				this.filename = "";
				this.urlpath = "";
				this.local_soundpool = null;
				this.soundpool = null;
				this.download_binary = null;

				this.progress_step_max = 1;
				this.progress_step = 0;
			}
		}

		/** delete_flag
		*/
		[SerializeField]
		private bool delete_flag;

		/** mode
		*/
		[SerializeField]
		private Mode mode;

		/** request_url
		*/
		[SerializeField]
		private string request_url;

		[SerializeField]
		private uint request_data_version;

		/** result_errorstring
		*/
		[SerializeField]
		private string result_errorstring;

		/** result_download_progress
		*/
		[SerializeField]
		private float result_download_progress;

		/** result_upload_progress
		*/
		[SerializeField]
		private float result_upload_progress;

		/** result_datatype
		*/
		[SerializeField]
		private DataType result_datatype;

		/** result
		*/
		[SerializeField]
		private NAudio.Pack_SoundPool result_soundpool;

		/** work
		*/
		private Work work;

		/** Awake
		*/
		private void Awake()
		{
			//delete_flag
			this.delete_flag = false;

			//mode
			this.mode = Mode.WaitRequest;

			//result_datatype
			this.result_datatype = DataType.None;

			//request_url
			this.request_url = null;

			//request_data_version
			this.request_data_version = 0;

			//result_errorstring
			this.result_errorstring = "";

			//result_download_progress
			this.result_download_progress = 0.0f;

			//result_upload_progress
			this.result_upload_progress = 0.0f;

			//result_soundpool
			this.result_soundpool = null;

			//work
			this.work = null;
		}

		/** プログレス計算。
		*/
		private float CalcProgress(float a_progress)
		{
			float t_progress = 0.0f;
			t_progress += ((float)this.work.progress_step) / this.work.progress_step_max;
			t_progress += (float)a_progress / this.work.progress_step_max;
			return t_progress;
		}

		/** サウンドプール名取得。
		*/
		private bool GetSoundPoolName()
		{
			this.work.filename = System.IO.Path.GetFileName(this.request_url);
			this.work.urlpath = this.request_url.Substring(0,this.request_url.Length - this.work.filename.Length);
			{
				if((this.work.urlpath[this.work.urlpath.Length - 1] != '\\')&&(this.work.urlpath[this.work.urlpath.Length - 1] != '/')){
					this.work.urlpath += "/";
				}

				if(System.Text.RegularExpressions.Regex.IsMatch(this.work.filename,"[0-9a-zA-Z][0-9a-zA-Z\\.\\_\\-]*") == true){
				}else{
					//失敗。
					return false;
				}
			}

			//成功。
			return true;
		}

		/** ローカルサウンドプール。ロード。
		*/
		private IEnumerator Raw_Do_LoadLocalSoundPool()
		{
			this.work.local_soundpool = null;

			NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclTextFile(this.work.filename);
			{
				do{
					this.result_download_progress = this.CalcProgress(0.0f);
					yield return null;
				}while(t_saveload_item.IsBusy() == true);
						
				if(t_saveload_item.GetDataType() == NSaveLoad.DataType.Text){
					this.work.local_soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_saveload_item.GetResultText()));

					if(this.work.local_soundpool == null){
						//コンバート失敗。

						this.work.local_soundpool = null;
						yield break;
					}

					if(this.work.local_soundpool.name_list == null){
						//不正なサウンドプール。

						this.work.local_soundpool = null;
						yield break;
					}

					for(int ii=0;ii<this.work.local_soundpool.name_list.Count;ii++){
						if(this.work.local_soundpool.name_list[ii] == null){
							//不正なサウンドプール。

							this.work.local_soundpool = null;
							yield break;
						}

						if(System.Text.RegularExpressions.Regex.IsMatch(this.work.local_soundpool.name_list[ii],"[0-9a-zA-Z][0-9a-zA-Z\\.\\_\\-]*") == false){
							//不正な名前。

							this.work.local_soundpool = null;
							yield break;
						}
					}
				}	
			}

			//成功。
			yield break;
		}

		/** 最新サウンドプール。ダウンロード。
		*/
		private IEnumerator Raw_Do_DownLoadNewSoundPool()
		{
			this.work.soundpool = null;

			NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(this.work.urlpath + this.work.filename,NDownLoad.DataType.Text);
			{
				do{
					this.result_download_progress = this.CalcProgress(t_download_item.GetProgress());
					yield return null;
				}while(t_download_item.IsBusy() == true);

				if(t_download_item.GetDataType() == DataType.Text){
					//コンバート。
					this.work.soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_download_item.GetResultText()));

					if(this.work.soundpool == null){
						//コンバート失敗。

						this.work.soundpool = null;
						this.result_errorstring = "Raw_Do_DownLoadSoundPool : Convert == null";
						this.mode = Mode.Do_Error;
						yield break;
					}

					if(this.work.soundpool.name_list == null){
						//不正なサウンドプール。

						this.work.soundpool = null;
						this.result_errorstring = "Raw_Do_DownLoadSoundPool : name_list == null";
						this.mode = Mode.Do_Error;
						yield break;
					}

					for(int ii=0;ii<this.work.soundpool.name_list.Count;ii++){
						if(this.work.soundpool.name_list[ii] == null){
							//不正なサウンドプール。

							this.work.soundpool = null;
							this.result_errorstring = "Raw_Do_DownLoadSoundPool : name_list[" + ii.ToString() + "] == null";
							this.mode = Mode.Do_Error;
							yield break;
						}

						if(System.Text.RegularExpressions.Regex.IsMatch(this.work.soundpool.name_list[ii],"[0-9a-zA-Z][0-9a-zA-Z\\.\\_\\-]*") == false){
							//不正な名前。

							this.work.soundpool = null;
							this.result_errorstring = "Raw_Do_DownLoadSoundPool : name_list[" + ii.ToString() + "] = " + this.work.soundpool.name_list[ii];
							this.mode = Mode.Do_Error;
							yield break;
						}
					}
				}else{
					//失敗。

					this.work.soundpool = null;
					this.result_errorstring = t_download_item.GetResultErrorString();
					this.mode = Mode.Do_Error;
					yield break;
				}
			}

			//成功。
			yield break;
		}

		/** リストアイテム。ダウンロード。
		*/
		private IEnumerator Raw_Do_DownLoadListItem(int a_index)
		{
			this.work.download_binary = null;

			NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(this.work.urlpath + this.work.soundpool.name_list[a_index],NDownLoad.DataType.Binary);
			{
				do{
					this.result_download_progress = this.CalcProgress(t_download_item.GetProgress());
					yield return null;
				}while(t_download_item.IsBusy() == true);

				if(t_download_item.GetDataType() == DataType.Binary){
					this.work.download_binary = t_download_item.GetResultBinary();

					if(this.work.download_binary == null){
						//不正なバイナリ。

						this.work.download_binary = null;
						this.result_errorstring = "Raw_Do_DownLoadListItem : binary == null";
						this.mode = Mode.Do_Error;
						yield break;
					}
				}else{
					//失敗。

					this.work.download_binary = null;
					this.result_errorstring = t_download_item.GetResultErrorString();
					this.mode = Mode.Do_Error;
					yield break;
				}
			}

			//成功。
			yield break;
		}

		/** リストアイテム。セーブ。
		*/
		private IEnumerator Raw_Do_SaveListItem(int a_index)
		{
			NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalBinaryFile(this.work.soundpool.name_list[a_index],this.work.download_binary);
			{
				do{
					this.result_download_progress = this.CalcProgress(0.0f);
					yield return null;
				}while(t_saveload_item.IsBusy() == true);

				if(t_saveload_item.GetDataType() == NSaveLoad.DataType.SaveEnd){
				}else{
					//失敗。

					this.result_errorstring = "Raw_Do_SaveListItem : namelist[" + a_index.ToString() + "] : " + this.work.soundpool.name_list[a_index];
					this.mode = Mode.Do_Error;
					yield break;
				}
			}

			this.work.download_binary = null;

			//成功。
			yield break;
		}

		/** サウンドプール。セーブ。
		*/
		private IEnumerator Raw_Do_SaveSoundPool()
		{
			NJsonItem.JsonItem t_json = NJsonItem.ObjectToJson.Convert(this.work.soundpool);
			if(t_json == null){
				//失敗。

				Tool.LogError("Raw_Do_SaveSoundPool","Convert");
				yield break;
			}

			string t_json_string = t_json.ConvertJsonString();
			if(t_json_string == null){
				//失敗。

				Tool.LogError("Raw_Do_SaveSoundPool","ConvertJsonString");
				yield break;
			}

			NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalTextFile(this.work.filename,t_json_string);
			{
				do{
					this.result_download_progress = this.CalcProgress(0.0f);
					yield return null;
				}while(t_saveload_item.IsBusy() == true);

				if(t_saveload_item.GetDataType() == NSaveLoad.DataType.SaveEnd){
				}else{
					//失敗。
					Tool.LogError("Raw_Do_SaveSoundPool",t_saveload_item.GetDataType().ToString());
					yield break;
				}
			}

			//成功。
			yield break;
		}

		/** [内部からの呼び出し]実行。
		*/
		private IEnumerator Raw_Do()
		{
			//サウンドプール名。
			if(this.GetSoundPoolName() == true){
				Tool.Log("GetSoundPoolName",this.work.urlpath + " : " + this.work.filename);
			}else{
				//失敗。
				this.result_errorstring = "GetSoundPoolName : " + this.request_url;
				this.mode = Mode.Do_Error;
				yield break;
			}

			//ローカルサウンドプール。ロード。
			this.work.progress_step = 0;
			this.work.progress_step_max = 1;
			this.work.progress_step = 0;
			yield return this.Raw_Do_LoadLocalSoundPool();
			if(this.mode != Mode.Do){
				//失敗。
				yield break;
			}

			//ローカルサウンドプール。チェック。
			if(Config.SOUNDPOOL_CHECK_DATAVERSION == true){
				if(this.work.local_soundpool != null){
					if(this.work.local_soundpool.data_version == this.request_data_version){
						//ローカルサウンドプールとデータバージョンが一致。

						//最新を取得する必要なし。
						this.result_datatype = DataType.SoundPool;
						this.result_soundpool = this.work.local_soundpool;
						this.mode = Mode.Do_Success;
						yield break;
					}else{
						//ローカルサウンドプール。バージョンが古い。
					}
				}else{
					//ローカルサウンドプール。なし。
				}
			}
	
			//最新サウンドプール。ダウンロード。
			this.work.progress_step = 1;
			this.work.progress_step_max = 1;
			this.work.progress_step = 0;
			yield return this.Raw_Do_DownLoadNewSoundPool();
			if(this.mode != Mode.Do){
				//失敗。
				yield break;
			}

			//チェック。
			bool t_download_listitem = true;
			if(Config.SOUNDPOOL_CHECL_DATAHASH == true){
				for(int ii=0;ii<this.work.soundpool.name_list.Count;ii++){
					Tool.Log("SoundPool",ii.ToString() + " : " + this.work.soundpool.name_list[ii]);
				}

				if(this.work.local_soundpool != null){
					if(this.work.soundpool.data_hash == this.work.local_soundpool.data_hash){
						//ローカルサウンドプールとデータハッシュが一致。
						t_download_listitem = false;
					}
				}
			}

			//リストアイテム。
			this.work.progress_step = 2;
			if(t_download_listitem == true){
				this.work.progress_step_max = this.work.soundpool.name_list.Count * 2;
				this.work.progress_step = 0;

				for(int ii=0;ii<this.work.soundpool.name_list.Count;ii++){
					//リストアイテム。ダウンロード。
					this.work.progress_step = ii * 2;
					yield return this.Raw_Do_DownLoadListItem(ii);
					if(this.mode != Mode.Do){
						//失敗。
						yield break;
					}

					//リストアイテム。セーブ。
					this.work.progress_step = ii * 2 + 1;
					yield return this.Raw_Do_SaveListItem(ii);
					if(this.mode != Mode.Do){
						//失敗。
						yield break;
					}
				}
			}

			//サウンドプール。セーブ。
			this.work.progress_step = 3;
			this.work.progress_step_max = 1;
			this.work.progress_step = 0;
			yield return this.Raw_Do_SaveSoundPool();
			if(this.mode != Mode.Do){
				//失敗。
				yield break;
			}

			this.result_datatype = DataType.SoundPool;
			this.result_soundpool = this.work.soundpool;
			this.mode = Mode.Do_Success;			
			yield break;
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
						yield return null;
					}break;
				case Mode.Start:
					{
						this.work = new Work();
						this.work.progress_step_max = 3;
						this.work.progress_step_max = 1;

						this.mode = Mode.Do;
					}break;
				case Mode.Do:
					{
						yield return this.Raw_Do();
					}break;
				case Mode.Do_Error:
					{
						//エラー終了。

						if(this.result_errorstring == null){
							this.result_errorstring = "error == null";
						}

						this.result_datatype = DataType.Error;
						this.result_soundpool = null;

						//終了。
						this.result_download_progress = 1.0f;
						this.result_upload_progress = 1.0f;

						this.work = null;
						this.mode = Mode.Fix;
					}break;
				case Mode.Do_Success:
					{
						//正常終了。

						this.result_errorstring = null;

						//終了。
						this.result_download_progress = 1.0f;
						this.result_upload_progress = 1.0f;

						this.work = null;
						this.mode = Mode.Fix;
					}break;
				case Mode.Fix:
					{
						yield return null;
					}break;
				}
			}

			//削除。
			Tool.Log("MonoBehaviour_WebRequest","GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
		}

		/** リクエスト。
		*/
		public bool Request(string a_url,uint a_data_version)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				//request
				this.request_url = a_url;
				this.request_data_version = a_data_version;

				//result
				this.result_errorstring = "";
				this.result_download_progress = 0.0f;
				this.result_upload_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_soundpool = null;

				return true;
			}else{
				return false;
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

		/** データタイプ。取得。
		*/
		public DataType GetResultDataType()
		{
			return this.result_datatype;
		}

		/** エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** プログレス。取得。
		*/
		public float GetDownloadProgress()
		{
			return this.result_download_progress;
		}

		/** プログレス。取得。
		*/
		public float GetUploadProgress()
		{
			return this.result_upload_progress;
		}

		/** 結果。取得。
		*/
		public NAudio.Pack_SoundPool GetResultSoundPool()
		{
			return this.result_soundpool;
		}
	}
}

