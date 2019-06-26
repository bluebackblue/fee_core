

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。コルーチン。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** ロード。サウンドプール。
	*/
	public class Coroutine_LoadSoundPool
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** サウンドプール。
			*/
			public Fee.Audio.Pack_SoundPool soundpool;

			/** エラー文字列。
			*/
			public string errorstring;

			/** レスポンスヘッダー。
			*/
			public System.Collections.Generic.Dictionary<string,string> responseheader;

			/** constructor
			*/
			public ResultType()
			{
				this.soundpool = null;
				this.errorstring = null;
				this.responseheader = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** Progress_MainStep
		*/
		private enum Progress_MainStep
		{
			Progress_MainStep_0_LoadLocal_SoundPool = 0,
			Progress_MainStep_1_Load_SoundPool = 1,
			Progress_MainStep_2_Sound = 2,
			Progress_MainStep_3_SaveLocal_SoundPool = 3,

			Max,
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnSoundPoolCoroutine_CallBackInterface a_callback_interface,File.Path a_path,UnityEngine.WWWForm a_post_data,bool a_is_streamingassets,uint a_data_version)
		{
			//result
			this.result = new ResultType();

			Progress t_progress = new Progress(new float[]{
				0.05f,
				0.1f,
				0.8f,
				0.05f
			});

			//ローカル、サウンロプール管理ファイル。相対パス。
			Fee.File.Path t_local_caoundpool_path = new File.Path(a_path.GetFileName());

			//ローカル、サウンドプール管理ファイルのロード。
			Fee.JsonItem.JsonItem t_local_soundpool_json = null;
			if(Config.USE_DOWNLOAD_SOUNDPOOL_CACHE == true){
				Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadLocalTextFile,t_local_caoundpool_path);
				while(t_item.IsBusy() == true){
					//■ステップ０。
					if(a_callback_interface != null){
						t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_0_LoadLocal_SoundPool,(int)Progress_MainStep.Max,0,1);
						a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
					}
					yield return null;

				}
				if(t_item.GetResultAssetType() == Asset.AssetType.Text){
					t_local_soundpool_json = new JsonItem.JsonItem(t_item.GetResultAssetText());
					if(t_local_soundpool_json == null){
						//コンバート失敗。
						this.result.errorstring = "Coroutine_LoadSoundPool : local_soundpool_json == null";
						yield break;
					}
				}else{
					//ローカルにキャッシュなし。
				}
			}

			//チェック。
			if(t_local_soundpool_json != null){
				if((t_local_soundpool_json.IsAssociativeArray() == true)&&(t_local_soundpool_json.IsExistItem("data_version",JsonItem.ValueType.Mask_Integer_Number))){
					uint t_data_version = t_local_soundpool_json.GetItem("data_version").GetUint();
					if(t_data_version == a_data_version){
						Fee.Audio.Pack_SoundPool t_local_soundpool = Fee.JsonItem.Convert.JsonItemToObject<Fee.Audio.Pack_SoundPool>(t_local_soundpool_json);
						if(t_local_soundpool != null){
							//最新を取得する必要なし。

							//パス解決。
							{
								t_local_soundpool.fullpath_list = new System.Collections.Generic.List<File.Path>();
								for(int ii=0;ii<t_local_soundpool.name_list.Count;ii++){
									t_local_soundpool.fullpath_list.Add(File.Path.CreateLocalPath(t_local_soundpool.name_list[ii]));
								}
							}

							this.result.soundpool = t_local_soundpool;
							this.result.responseheader = null;
							yield break;
						}else{
							//コンバート失敗。
						}
					}else{
						//最新のバージョンが必要。
					}
				}else{
					//不明なデータ。
				}

				t_local_soundpool_json = null;
			}

			//ロード。
			string t_load_stringjson = null;
			Fee.Audio.Pack_SoundPool t_load_soundpool = null;

			//サウンドプール管理ファイルのロード。
			{
				Fee.File.Item t_item = null;
				
				if(a_is_streamingassets == true){
					t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadStreamingAssetsTextFile,a_path);
				}else{
					t_item = Fee.File.File.GetInstance().RequestDownLoad(File.File.LoadRequestType.DownLoadTextFile,a_path,a_post_data);
				}
				
				while(t_item.IsBusy() == true){
					//■ステップ１。
					if(a_callback_interface != null){
						t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_1_Load_SoundPool,(int)Progress_MainStep.Max,0,1);
						a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
					}
					yield return null;

				}
				this.result.responseheader = t_item.GetResultResponseHeader();

				if(t_item.GetResultAssetType() == Asset.AssetType.Text){
					//成功。
					t_load_stringjson = t_item.GetResultAssetText();
				}else{
					//失敗。
					this.result.errorstring = t_item.GetResultErrorString();
					yield break;
				}

				t_load_soundpool = JsonItem.Convert.JsonStringToObject<Fee.Audio.Pack_SoundPool>(t_load_stringjson);

				if(t_load_soundpool == null){
					//コンバート失敗。
					this.result.errorstring = "Coroutine_LoadSoundPool : load_soundpool == null";
					yield break;
				}else if(t_load_soundpool.name_list == null){
					//不明なデータ。
					this.result.errorstring = "Coroutine_LoadSoundPool : load_soundpool.name_list == null";
					yield break;
				}else if(t_load_soundpool.volume_list == null){
					//不明なデータ。
					this.result.errorstring = "Coroutine_LoadSoundPool : load_soundpool.volume_list == null";
					yield break;
				}
			}

			//登録サウンドのロード。
			{
				for(int ii=0;ii<t_load_soundpool.name_list.Count;ii++){

					byte[] t_sound_binary = null;

					//ロード。
					{
						Fee.File.Path t_sound_url = a_path.CreateFileNameChangePath(t_load_soundpool.name_list[ii]);

						Fee.File.Item t_item = null;

						if(a_is_streamingassets){
							t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadStreamingAssetsBinaryFile,t_sound_url);
						}else{
							t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.DownLoadBinaryFile,t_sound_url);
						}

						while(t_item.IsBusy() == true){
							//■ステップ２。
							if(a_callback_interface != null){
								t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_2_Sound,(int)Progress_MainStep.Max,ii * 2 + 0,t_load_soundpool.name_list.Count * 2);
								a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
							}
							yield return null;

						}
						if(t_item.GetResultAssetType() == Asset.AssetType.Binary){
							//成功。
							t_sound_binary = t_item.GetResultAssetBinary();
						}else{
							//失敗。
							this.result.errorstring = t_item.GetResultErrorString();
							yield break;
						}
					}

					//セーブローカル。
					{
						Fee.File.Path t_sound_url = new File.Path(t_load_soundpool.name_list[ii]);

						File.Item t_item = Fee.File.File.GetInstance().RequestSaveLocalBinaryFile(t_sound_url,t_sound_binary);
						while(t_item.IsBusy() == true){
							//■ステップ２。
							if(a_callback_interface != null){
								t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_2_Sound,(int)Progress_MainStep.Max,ii * 2 + 1,t_load_soundpool.name_list.Count * 2);
								a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
							}
							yield return null;
						}
						if(t_item.GetResultType() == File.Item.ResultType.SaveEnd){
							//成功。
						}else{
							//失敗。
							this.result.errorstring = t_item.GetResultErrorString();
							yield break;
						}
					}
				}
			}

			//ローカル、サウンドプール管理ファイルのセーブ。
			{
				File.Item t_item = Fee.File.File.GetInstance().RequestSaveLocalTextFile(t_local_caoundpool_path,t_load_stringjson);
				while(t_item.IsBusy() == true){
					//■ステップ３。
					if(a_callback_interface != null){
						t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_3_SaveLocal_SoundPool,(int)Progress_MainStep.Max,0,1);
						a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
					}
					yield return null;
				}
				if(t_item.GetResultType()==File.Item.ResultType.SaveEnd){
					//成功。
				}else{
					//失敗。
					this.result.errorstring = t_item.GetResultErrorString();
					yield break;
				}
			}

			//パス解決。
			{
				t_load_soundpool.fullpath_list = new System.Collections.Generic.List<File.Path>();
				for(int ii=0;ii<t_load_soundpool.name_list.Count;ii++){
					t_load_soundpool.fullpath_list.Add(Fee.File.Path.CreateLocalPath(t_load_soundpool.name_list[ii]));
				}
			}

			this.result.soundpool = t_load_soundpool;
			yield break;
		}
	}
}

