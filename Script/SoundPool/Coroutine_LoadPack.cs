

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
	/** ロード。パック。
	*/
	public class Coroutine_LoadPack
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** パック。
			*/
			public Pack pack;

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
				this.pack = null;
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
			Progress_MainStep_0_LoadLocal_Pack = 0,
			Progress_MainStep_1_Load_Pack = 1,
			Progress_MainStep_2_Sound = 2,
			Progress_MainStep_3_SaveLocal_Pack = 3,

			Max,
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnSoundPoolCoroutine_CallBackInterface a_callback_interface,File.Path a_path,System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler,bool a_is_streamingassets,uint a_data_version)
		{
			//result
			this.result = new ResultType();

			Fee.Pattern.Progress t_progress = new Fee.Pattern.Progress(new float[]{
				0.05f,
				0.1f,
				0.8f,
				0.05f
			});

			//ローカル、パックＪＳＯＮ。相対パス。
			Fee.File.Path t_local_pack_path = new File.Path(a_path.GetFileName());

			//ローカル、パックＪＳＯＮのロード。
			Fee.JsonItem.JsonItem t_local_pack_json = null;
			if(Config.USE_LOADURL_PACK_CACHE == true){
				Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadLocalTextFile,t_local_pack_path);

				do{
					//■ステップ０。
					if(a_callback_interface != null){
						t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_0_LoadLocal_Pack,0,1);
						a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
					}
					yield return null;
				}while(t_item.IsBusy() == true);

				if(t_item.GetResultAssetType() == Asset.AssetType.Text){
					t_local_pack_json = new JsonItem.JsonItem(t_item.GetResultAssetText());
					if(t_local_pack_json == null){
						//コンバート失敗。
						this.result.errorstring = "Coroutine_LoadPack : local_pack_json == null";
						yield break;
					}
				}else{
					//ローカルにキャッシュなし。
				}
			}

			//チェック。
			if(t_local_pack_json != null){
				if((t_local_pack_json.IsAssociativeArray() == true)&&(t_local_pack_json.IsExistItem("data_version"))){
					uint t_data_version = t_local_pack_json.GetItem("data_version").CastToUint16();
					if(t_data_version == a_data_version){
						Pack t_local_pack = Fee.JsonItem.Convert.JsonItemToObject<Pack>(t_local_pack_json);
						if(t_local_pack != null){
							//最新を取得する必要なし。

							//パス解決。
							{
								t_local_pack.fullpath_list = new System.Collections.Generic.List<File.Path>();
								for(int ii=0;ii<t_local_pack.name_list.Count;ii++){
									t_local_pack.fullpath_list.Add(File.Path.CreateLocalPath(t_local_pack.name_list[ii],Fee.File.Path.SEPARATOR));
								}
							}

							this.result.pack = t_local_pack;
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

				t_local_pack_json = null;
			}

			//ロード。
			string t_load_stringjson = null;
			Pack t_load_pack = null;

			//パックＪＳＯＮのロード。
			{
				Fee.File.Item t_item = null;
				
				if(a_is_streamingassets == true){
					t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadStreamingAssetsTextFile,a_path);
				}else{
					t_item = Fee.File.File.GetInstance().RequestLoadUrl(File.File.LoadRequestType.LoadUrlTextFile,a_path,a_post_data,a_certificate_handler);
				}
				
				do{
					//■ステップ１。
					if(a_callback_interface != null){
						t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_1_Load_Pack,0,1);
						a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
					}
					yield return null;
				}while(t_item.IsBusy() == true);

				this.result.responseheader = t_item.GetResultResponseHeader();

				if(t_item.GetResultAssetType() == Asset.AssetType.Text){
					//成功。
					t_load_stringjson = t_item.GetResultAssetText();
				}else{
					//失敗。
					this.result.errorstring = t_item.GetResultErrorString();
					yield break;
				}

				t_load_pack = JsonItem.Convert.JsonStringToObject<Pack>(t_load_stringjson);

				if(t_load_pack == null){
					//コンバート失敗。
					this.result.errorstring = "Coroutine_LoadPack : load_pack == null";
					yield break;
				}else if(t_load_pack.name_list == null){
					//不明なデータ。
					this.result.errorstring = "Coroutine_LoadPack : load_pack.name_list == null";
					yield break;
				}else if(t_load_pack.volume_list == null){
					//不明なデータ。
					this.result.errorstring = "Coroutine_LoadPack : load_pack.volume_list == null";
					yield break;
				}
			}

			//登録サウンドのロード。
			{
				for(int ii=0;ii<t_load_pack.name_list.Count;ii++){

					byte[] t_sound_binary = null;

					//ロード。
					{
						Fee.File.Path t_sound_url = a_path.CreateFileNameChangePath(t_load_pack.name_list[ii],Fee.File.Path.SEPARATOR);

						Fee.File.Item t_item = null;

						if(a_is_streamingassets){
							t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadStreamingAssetsBinaryFile,t_sound_url);
						}else{
							t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlBinaryFile,t_sound_url);
						}

						do{
							//■ステップ２。
							if(a_callback_interface != null){
								t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_2_Sound,ii * 2 + 0,t_load_pack.name_list.Count * 2);
								a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
							}
							yield return null;
						}while(t_item.IsBusy() == true);

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
						Fee.File.Path t_sound_url = new File.Path(t_load_pack.name_list[ii]);

						File.Item t_item = Fee.File.File.GetInstance().RequestSaveBinaryFile(File.File.SaveRequestType.SaveLocalBinaryFile,t_sound_url,t_sound_binary);

						do{
							//■ステップ２。
							if(a_callback_interface != null){
								t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_2_Sound,ii * 2 + 1,t_load_pack.name_list.Count * 2);
								a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
							}
							yield return null;
						}while(t_item.IsBusy() == true);

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

			//ローカル、パックＪＳＯＮのセーブ。
			{
				File.Item t_item = Fee.File.File.GetInstance().RequestSaveTextFile(File.File.SaveRequestType.SaveLocalTextFile,t_local_pack_path,t_load_stringjson);

				do{
					//■ステップ３。
					if(a_callback_interface != null){
						t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_3_SaveLocal_Pack,0,1);
						a_callback_interface.OnSoundPoolCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
					}
					yield return null;
				}while(t_item.IsBusy() == true);

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
				t_load_pack.fullpath_list = new System.Collections.Generic.List<File.Path>();
				for(int ii=0;ii<t_load_pack.name_list.Count;ii++){
					t_load_pack.fullpath_list.Add(Fee.File.Path.CreateLocalPath(t_load_pack.name_list[ii],Fee.File.Path.SEPARATOR));
				}
			}

			this.result.pack = t_load_pack;
			yield break;
		}
	}
}

