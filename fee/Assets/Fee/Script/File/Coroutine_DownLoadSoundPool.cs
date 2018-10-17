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
		public OnCoroutine_CallBack callback;

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

		/** サウンドプールチェック。
		*/
		public static bool CheckSoundPool(NAudio.Pack_SoundPool a_soundpool,out string a_errorstring)
		{
			//name_listチェック。
			if(a_soundpool != null){
				if(a_soundpool.name_list != null){
					for(int ii=0;ii<a_soundpool.name_list.Count;ii++){
						if(a_soundpool.name_list[ii] != null){
							if(a_soundpool.name_list[ii].Length > 0){
								if(System.Text.RegularExpressions.Regex.IsMatch(a_soundpool.name_list[ii],"[0-9a-zA-Z][0-9a-zA-Z\\.\\-_]*") == true){
									Tool.Log("Coroutine_DownLoadSoundPool",ii.ToString() + " = " + a_soundpool.name_list[ii]);
								}else{
									a_errorstring = "[" + ii.ToString() + "]Regex.IsMatch == false";
									return false;
								}
							}else{
								a_errorstring = "name_list[" + ii.ToString() + "].Length <= 0";
								return false;
							}
						}else{
							a_errorstring = "name_list[" + ii.ToString() + "] == null";
							return false;
						}
					}
				}else{
					a_errorstring = "name_list == null";
					return false;
				}
			}else{
				//null。
				a_errorstring = "soundpool == null";
				return false;
			}

			a_errorstring = null;
			return true;
		}

		/** [NFile.OnCoroutine_CallBack]コルーチン実行中。

		戻り値 == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			if(this.callback != null){
				return this.callback.OnCoroutine(a_progress);
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

			//ファイル名。
			string t_filename = null;
			string t_url_path = null;
			if(ParseUrl(a_url,ref t_filename,ref t_url_path) == false){
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
				if(t_local_soundpool!= null){
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

			//ダウンロードサウンドプール。
			NAudio.Pack_SoundPool t_download_soundpool = null;
			{
				Coroutine_DownLoadTextFile t_coroutine = new Coroutine_DownLoadTextFile();
				yield return t_coroutine.CoroutineMain(this,t_url_path + t_filename);

				if(t_coroutine.result.text != null){
					t_download_soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_coroutine.result.text));

					string t_errorstring;
					bool t_check = CheckSoundPool(t_download_soundpool,out t_errorstring);

					if(t_check == false){
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

			//ダウンロードサウンドプール。チェック。
			bool t_download_listitem = true;
			if(Config.SOUNDPOOL_CHECL_DATAHASH == true){
				if(t_local_soundpool != null){
					if(t_download_soundpool.data_hash == t_local_soundpool.data_hash){
						//ローカルサウンドプールとデータハッシュが一致。
						t_download_listitem = false;
					}
				}
			}

			if(t_download_listitem == true){
				for(int ii=0;ii<t_download_soundpool.name_list.Count;ii++){

					byte[] t_binary = null;

					//ダウンロード。
					{
						Coroutine_DownLoadBinaryFile t_coroutine = new Coroutine_DownLoadBinaryFile();
						yield return t_coroutine.CoroutineMain(this,t_url_path + t_download_soundpool.name_list[ii]);

						if(t_coroutine.result.binary != null){
							t_binary = t_coroutine.result.binary;
						}else{
							this.result.errorstring = t_coroutine.result.errorstring;
							yield break;
						}
					}

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

			//セーブローカルサウンドプール。
			{
				NJsonItem.JsonItem t_json = NJsonItem.ObjectToJson.Convert(t_download_soundpool);
				if(t_json == null){
					this.result.errorstring = "NJsonItem.ObjectToJson.Convert(t_download_soundpool) == null";
					yield break;
				}

				string t_json_string = t_json.ConvertJsonString();
				if(t_json_string == null){
					this.result.errorstring = "t_json.ConvertJsonString() == null";
					yield break;
				}

				Coroutine_SaveLocalTextFile t_coroutine = new Coroutine_SaveLocalTextFile();
				yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + t_filename,t_json_string);

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

