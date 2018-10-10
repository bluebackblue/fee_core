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
	public class MonoBehaviour_SoundPool : MonoBehaviour_Base
	{
		/** リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ダウンロード。サウンドプール。
			*/
			DownLoadSoundPool,
		};

		/** ProgressStep
		*/
		private enum ProgressStep
		{
			Step0 = 0,
			Step1,
			Step2,
			Step3,

			Max,
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
				this.progress_step_max = (int)ProgressStep.Max;
				this.progress_step = (int)ProgressStep.Step0;
				this.progress_substep_max = 1;
				this.progress_substep = 0;
			}
		}

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** request_url
		*/
		[SerializeField]
		private string request_url;

		/** request_data_version
		*/
		[SerializeField]
		private uint request_data_version;

		/** work
		*/
		[SerializeField]
		private Work work;

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

			//work
			this.work = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.DownLoadSoundPool:
				{
					Tool.Log("MonoBehaviour_SoundPool",this.request_type.ToString());
					this.work = new Work();
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
			case RequestType.DownLoadSoundPool:
				{
					yield return this.Raw_Do_DownLoadSoundPool();

					if(this.GetResultType() == ResultType.SoundPool){
						if(this.GetResultSoundPool() != null){
							this.SetModeDoSuccess();
							yield break;
						}
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

		/** リクエスト。ダウンロード。
		*/
		public bool RequestDownLoad(string a_url,uint a_data_version)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DownLoadSoundPool;
				this.request_url = a_url;
				this.request_data_version = a_data_version;
				this.work = null;

				return true;
			}

			return false;
		}

		/** プログレス計算。
		*/
		private float CalcProgress(float a_progress)
		{
			float t_progress = 0.0f;
			t_progress += ((float)this.work.progress_step) / this.work.progress_step_max;
			t_progress += (a_progress + (float)this.work.progress_substep) / (this.work.progress_step_max * this.work.progress_substep_max);
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

				if(System.Text.RegularExpressions.Regex.IsMatch(this.work.filename,"[0-9a-zA-Z][0-9a-zA-Z\\.\\-_]*") == true){
				}else{
					//失敗。
					return false;
				}
			}

			//成功。
			return true;
		}

		/** [内部からの呼び出し]ダウンロード。サウンドプール。ロードローカルサウンドプール。
		*/
		private IEnumerator Raw_Do_DownLoadSoundPool_LoadLocalSoundPool()
		{
			NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclTextFile(this.work.filename);
			{
				do{
					//プログレス。
					this.SetResultProgress(this.CalcProgress(t_saveload_item.GetResultProgress()));
					
					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_saveload_item.Cancel();
					}

					yield return null;
				}while(t_saveload_item.IsBusy() == true);

				if(t_saveload_item.GetResultDataType() == NSaveLoad.DataType.Text){
					this.work.local_soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_saveload_item.GetResultText()));

					if(this.work.local_soundpool == null){
						//コンバート失敗。

						//ローカルサウンドプールなし。
						this.work.local_soundpool = null;
						yield break;
					}

					if(this.work.local_soundpool.name_list == null){
						//不正なサウンドプール。

						//ローカルサウンドプールなし。
						this.work.local_soundpool = null;
						yield break;
					}

					for(int ii=0;ii<this.work.local_soundpool.name_list.Count;ii++){
						if(this.work.local_soundpool.name_list[ii] == null){
							//不正なサウンドプール。

							//ローカルサウンドプールなし。
							this.work.local_soundpool = null;
							yield break;
						}

						if(System.Text.RegularExpressions.Regex.IsMatch(this.work.local_soundpool.name_list[ii],"[0-9a-zA-Z][0-9a-zA-Z\\.\\-_]*") == false){
							//不正な名前。

							//ローカルサウンドプールなし。
							this.work.local_soundpool = null;
							yield break;
						}
					}
				}else{
					//失敗。

					//ローカルサウンドプールなし。
					this.work.local_soundpool = null;
					yield break;
				}
			}

			//成功。
			yield break;
		}

		/** [内部からの呼び出し]ダウンロード。サウンドプール。ダウンロード最新サウンドプール。
		*/
		private IEnumerator Raw_Do_DownLoadSoundPool_DownLoadNewSoundPool()
		{
			NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(this.work.urlpath + this.work.filename,NDownLoad.DataType.Text);
			{
				do{
					//プログレス。
					this.SetResultProgress(this.CalcProgress(t_download_item.GetResultProgress()));
					
					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_download_item.Cancel();
					}

					yield return null;
				}while(t_download_item.IsBusy() == true);

				if(t_download_item.GetResultDataType() == DataType.Text){
					//コンバート。
					this.work.soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_download_item.GetResultText()));

					if(this.work.soundpool == null){
						//コンバート失敗。

						this.work.soundpool = null;

						this.SetResultErrorString("Raw_Do_DownLoadNewSoundPool : Convert == null");
						yield break;
					}

					if(this.work.soundpool.name_list == null){
						//不正なサウンドプール。

						this.work.soundpool = null;

						this.SetResultErrorString("Raw_Do_DownLoadNewSoundPool : name_list == null");
						yield break;
					}

					for(int ii=0;ii<this.work.soundpool.name_list.Count;ii++){
						if(this.work.soundpool.name_list[ii] == null){
							//不正なサウンドプール。

							this.work.soundpool = null;

							this.SetResultErrorString("Raw_Do_DownLoadNewSoundPool : name_list[" + ii.ToString() + "] == null");
							yield break;
						}

						if(System.Text.RegularExpressions.Regex.IsMatch(this.work.soundpool.name_list[ii],"[0-9a-zA-Z][0-9a-zA-Z\\.\\-_]*") == false){
							//不正な名前。

							this.work.soundpool = null;

							this.SetResultErrorString("Raw_Do_DownLoadNewSoundPool : name_list[" + ii.ToString() + "] = " + this.work.soundpool.name_list[ii]);
							yield break;
						}
					}
				}else{
					//失敗。

					this.work.soundpool = null;

					this.SetResultErrorString(t_download_item.GetResultErrorString());
					yield break;
				}
			}

			//成功。
			yield break;
		}

		/** [内部からの呼び出し]ダウンロード。サウンドプール。ダウンロードアイテム。
		*/
		private IEnumerator Raw_Do_DownLoadSoundPool_DownLoadItem(int a_index)
		{
			this.work.download_binary = null;

			NDownLoad.Item t_download_item = NDownLoad.DownLoad.GetInstance().Request(this.work.urlpath + this.work.soundpool.name_list[a_index],NDownLoad.DataType.Binary);
			{
				do{
					//プログレス。
					this.SetResultProgress(this.CalcProgress(t_download_item.GetResultProgress()));
					
					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_download_item.Cancel();
					}

					yield return null;
				}while(t_download_item.IsBusy() == true);

				if(t_download_item.GetResultDataType() == DataType.Binary){
					this.work.download_binary = t_download_item.GetResultBinary();

					if(this.work.download_binary == null){
						//不正なバイナリ。

						this.work.download_binary = null;

						this.SetResultErrorString("Raw_Do_DownLoadListItem : binary == null");
						yield break;
					}
				}else{
					//失敗。

					this.work.download_binary = null;

					this.SetResultErrorString(t_download_item.GetResultErrorString());
					yield break;
				}
			}

			//成功。
			yield break;
		}

		/** [内部からの呼び出し]ダウンロード。サウンドプール。セーブローカルアイテム。
		*/
		private IEnumerator Raw_Do_DownLoadSoundPool_SaveLocalItem(int a_index)
		{
			NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalBinaryFile(this.work.soundpool.name_list[a_index],this.work.download_binary);
			{
				do{
					//プログレス。
					this.SetResultProgress(this.CalcProgress(t_saveload_item.GetResultProgress()));
					
					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_saveload_item.Cancel();
					}

					yield return null;
				}while(t_saveload_item.IsBusy() == true);

				if(t_saveload_item.GetResultDataType() == NSaveLoad.DataType.SaveEnd){
				}else{
					//失敗。

					this.SetResultErrorString("Raw_Do_SaveListItem : namelist[" + a_index.ToString() + "] : " + this.work.soundpool.name_list[a_index]);
					yield break;
				}
			}

			this.work.download_binary = null;

			//成功。
			yield break;
		}

		/** [内部からの呼び出し]ダウンロード。サウンドプール。セーブローカルサウンドプール。
		*/
		private IEnumerator Raw_Do_DownLoadSoundPool_SaveLocalSoundPool()
		{
			NJsonItem.JsonItem t_json = NJsonItem.ObjectToJson.Convert(this.work.soundpool);
			if(t_json == null){
				//失敗。
				this.SetResultErrorString("error : Raw_Do_SaveSoundPool : NJsonItem.ObjectToJson.Convert");
				yield break;
			}

			string t_json_string = t_json.ConvertJsonString();
			if(t_json_string == null){
				//失敗。
				this.SetResultErrorString("error : Raw_Do_SaveSoundPool : ConvertJsonString");
				yield break;
			}

			NSaveLoad.Item t_saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalTextFile(this.work.filename,t_json_string);
			{
				do{
					//プログレス。
					this.SetResultProgress(this.CalcProgress(t_saveload_item.GetResultProgress()));
					
					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_saveload_item.Cancel();
					}

					yield return null;
				}while(t_saveload_item.IsBusy() == true);

				if(t_saveload_item.GetResultDataType() == NSaveLoad.DataType.SaveEnd){
				}else{
					//失敗。
					this.SetResultErrorString("error : Raw_Do_SaveSoundPool != NSaveLoad.DataType.SaveEnd");
					yield break;
				}
			}

			//成功。
			yield break;
		}

		/** [内部からの呼び出し]ダウンロード。サウンドプール。
		*/
		private IEnumerator Raw_Do_DownLoadSoundPool()
		{
			//サウンドプール名。
			if(this.GetSoundPoolName() == true){
				Tool.Log("GetSoundPoolName",this.work.urlpath + " : " + this.work.filename);
			}else{
				//失敗。
				this.SetResultErrorString("GetSoundPoolName : " + this.request_url);
				yield break;
			}

			//ローカルサウンドプール。ロード。
			this.work.progress_substep = 0;
			this.work.progress_substep_max = 1;
			this.work.progress_step = (int)ProgressStep.Step0;
			yield return this.Raw_Do_DownLoadSoundPool_LoadLocalSoundPool();
			if(this.GetResultType() == ResultType.Error){
				//失敗。
				yield break;
			}

			//ローカルサウンドプール。チェック。
			if(Config.SOUNDPOOL_CHECK_DATAVERSION == true){
				if(this.work.local_soundpool != null){
					if(this.work.local_soundpool.data_version == this.request_data_version){
						//ローカルサウンドプールとデータバージョンが一致。

						//最新を取得する必要なし。
						this.SetResultSoundPool(this.work.local_soundpool);
						yield break;
					}else{
						//ローカルサウンドプール。バージョンが古い。
					}
				}else{
					//ローカルサウンドプール。なし。
				}
			}
	
			//最新サウンドプール。ダウンロード。
			this.work.progress_substep = 0;
			this.work.progress_substep_max = 1;
			this.work.progress_step = (int)ProgressStep.Step1;
			yield return this.Raw_Do_DownLoadSoundPool_DownLoadNewSoundPool();
			if(this.GetResultType() == ResultType.Error){
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
			this.work.progress_step = (int)ProgressStep.Step2;
			if(t_download_listitem == true){
				this.work.progress_substep_max = this.work.soundpool.name_list.Count * 2;
				this.work.progress_substep = 0;

				for(int ii=0;ii<this.work.soundpool.name_list.Count;ii++){
					//リストアイテム。ダウンロード。
					this.work.progress_substep = ii * 2;
					yield return this.Raw_Do_DownLoadSoundPool_DownLoadItem(ii);
					if(this.GetResultType() == ResultType.Error){
						//失敗。
						yield break;
					}

					//リストアイテム。セーブ。
					this.work.progress_substep = ii * 2 + 1;
					yield return this.Raw_Do_DownLoadSoundPool_SaveLocalItem(ii);
					if(this.GetResultType() == ResultType.Error){
						//失敗。
						yield break;
					}
				}
			}

			//サウンドプール。セーブ。
			this.work.progress_substep = 0;
			this.work.progress_substep_max = 1;
			this.work.progress_step = (int)ProgressStep.Step3;
			yield return this.Raw_Do_DownLoadSoundPool_SaveLocalSoundPool();
			if(this.GetResultType() == ResultType.Error){
				//失敗。
				yield break;
			}

			this.SetResultSoundPool(this.work.soundpool);
			yield break;
		}
	}
}

