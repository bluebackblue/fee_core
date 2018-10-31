using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** NFile
*/
namespace NFile
{
	/** ダウンロード。サウンドプール。
	*/
	public class Coroutine_DownLoadSoundPool : OnCoroutine_CallBack
	{
		/** ResultType
		*/
		public class ResultType
		{
			public NAudio.Pack_SoundPool soundpool;
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.soundpool = null;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** callback
		*/
		private OnCoroutine_CallBack callback;

		/** step
		*/
		private int step;
		private int step_max;
		private int substep;
		private int substep_max;

		/** ＵＲＬをファイル名とパスに分ける。
		*/
		public static bool ParseUrl(string a_url,ref string a_filename,ref string a_url_path)
		{
			if(a_url != null){
				if(a_url.Length > 0){
					string t_filename = System.IO.Path.GetFileName(a_url);
					string t_url_path = a_url.Substring(0,a_url.Length - t_filename.Length);

					if(System.Text.RegularExpressions.Regex.IsMatch(t_filename,"[0-9a-zA-Z][0-9a-zA-Z\\.\\-_]*") == true){
						a_filename = t_filename;
						a_url_path = t_url_path;
						return true;
					}
				}
			}

			return false;
		}

		/** [NFile.OnCoroutine_CallBack]コルーチン実行中。

		戻り値 == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			float t_progress = 0.0f;
	
			t_progress += (float)this.step / this.step_max;
			t_progress += ((float)this.substep + a_progress) / (this.step_max * this.substep_max);

			if(t_progress > 1.0f){
				t_progress = 1.0f;
			}

			if(this.callback != null){
				return this.callback.OnCoroutine(t_progress);
			}
			return true;
		}

		/** CoroutineMain
		*/
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,string a_url,uint a_data_version)
		{
			//result
			this.result = new ResultType();

			//callback
			this.callback = a_instance;

			//step
			this.step = 0;
			this.step_max = 4;
			this.substep = 0;
			this.substep_max = 1;

			//ファイル名。
			string t_filename = null;
			string t_url_path = null;
			if(Coroutine_DownLoadSoundPool.ParseUrl(a_url,ref t_filename,ref t_url_path) == false){
				//失敗。
				this.result.errorstring = "ParseUrl";
				yield break;
			}

			//ロードローカルサウンドプール。
			NAudio.Pack_SoundPool t_local_soundpool = null;
			{
				Coroutine_LoadLocalSoundPool t_coroutine = new Coroutine_LoadLocalSoundPool();
				yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + t_filename);

				if(t_coroutine.result.soundpool != null){
					t_local_soundpool = t_coroutine.result.soundpool;
				}else{
					//続行。
				}
			}

			//ローカルサウンドプール。チェック。
			if(Config.SOUNDPOOL_CHECK_DATAVERSION == true){
				if(t_local_soundpool != null){
					if(t_local_soundpool.data_version == a_data_version){
						//ローカルサウンドプールとデータバージョンが一致。

						//最新を取得する必要なし。
						this.result.soundpool = t_local_soundpool;
						yield break;
					}else{
						//ローカルサウンドプール。バージョンが古い。
					}
				}else{
					//ローカルサウンドプール。なし。
				}
			}

			this.step = 1;
			this.substep = 0;
			this.substep_max = 1;

			//ダウンロードサウンドプール。
			NAudio.Pack_SoundPool t_download_soundpool = null;
			{
				Coroutine_DownLoadTextFile t_coroutine = new Coroutine_DownLoadTextFile();
				yield return t_coroutine.CoroutineMain(this,t_url_path + t_filename,null);

				if(t_coroutine.result.text != null){
					t_download_soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_coroutine.result.text));

					string t_errorstring;
					if(NAudio.Pack_SoundPool.CheckSoundPool(t_download_soundpool,out t_errorstring) == false){
						t_download_soundpool = null;
						this.result.errorstring = t_errorstring;
						yield break;
					}
				}else{
					this.result.errorstring = t_coroutine.result.errorstring;
					yield break;
				}

				if(t_download_soundpool == null){
					this.result.errorstring = "t_download_soundpool == null";
					yield break;
				}
			}

			this.step = 2;
			this.substep = 0;
			this.substep_max = 1;

			{
				this.substep_max = t_download_soundpool.name_list.Count * 2;

				for(int ii=0;ii<t_download_soundpool.name_list.Count;ii++){

					byte[] t_binary = null;

					this.substep = ii * 2 + 0;

					//ダウンロード。
					{
						Coroutine_DownLoadBinaryFile t_coroutine = new Coroutine_DownLoadBinaryFile();
						yield return t_coroutine.CoroutineMain(this,t_url_path + t_download_soundpool.name_list[ii],null);

						if(t_coroutine.result.binary != null){
							t_binary = t_coroutine.result.binary;
						}else{
							this.result.errorstring = t_coroutine.result.errorstring;
							yield break;
						}
					}

					this.substep = ii * 2 + 1;

					//セーブ。
					{
						Coroutine_SaveLocalBinaryFile t_coroutine = new Coroutine_SaveLocalBinaryFile();
						yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + t_download_soundpool.name_list[ii],t_binary);

						if(t_coroutine.result.saveend == true){
							//続行。
						}else{
							this.result.errorstring = t_coroutine.result.errorstring;
							yield break;
						}
					}
				}
			}

			this.step = 3;
			this.substep = 0;
			this.substep_max = 1;

			//セーブローカルサウンドプール。
			{
				Coroutine_SaveLocalSoundPool t_coroutine = new Coroutine_SaveLocalSoundPool();
				yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + t_filename,t_download_soundpool);

				if(t_coroutine.result.saveend == true){
					//続行。
				}else{
					this.result.errorstring = t_coroutine.result.errorstring;
					yield break;
				}
			}

			this.result.soundpool = t_download_soundpool;
			yield break;
		}
	}
}

