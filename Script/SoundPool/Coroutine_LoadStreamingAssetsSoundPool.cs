

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief サウンドプール。コルーチン。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** ロードストリーミングアセット。サウンドプール。
	*/
	public class Coroutine_LoadStreamingAssetsSoundPool
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

		/** MainStep
		*/
		private enum MainStep
		{
			LoadLocal_SoundPool = 0,
			DownLoad_SoundPool,
			Sound,
			SaveLocal_SoundPool,
			Max,
		}

		/** mainstep_per_list
		*/
		private float[] mainstep_per_list;

		/** CalcProgress
		*/
		private float CalcProgress(float[] a_mainstep_per_list,int a_mainstep,int a_substep_max,int a_substep,float a_progress)
		{
			float t_progress = 0;

			//main
			for(int ii=0;ii<a_mainstep;ii++){
				t_progress += a_mainstep_per_list[ii];
			}

			//sub
			t_progress += a_mainstep_per_list[a_mainstep] * a_substep / a_substep_max;

			//subsub
			float t_sub_sub_use_progress = a_mainstep_per_list[a_mainstep] / a_substep_max;
			t_progress += t_sub_sub_use_progress * a_progress;

			//progress
			if(t_progress > 1.0f){
				t_progress = 1.0f;
			}
			return t_progress;
		}

		/** プログレス更新。
		*/
		private void UpdateProgress(OnCoroutine_CallBack a_instance,int a_mainstep,int a_substep_max,int a_substep,float a_progress)
		{
			float t_progress = this.CalcProgress(this.mainstep_per_list,a_mainstep,a_substep_max,a_substep,a_progress);
			if(a_instance != null){
				a_instance.OnCoroutine(1.0f,t_progress);
			}
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,Fee.File.Path a_path,uint a_data_version)
		{
			//result
			this.result = new ResultType();

			//mainstep_per_list
			this.mainstep_per_list = new float[(int)MainStep.Max]{
				0.05f,
				0.1f,
				0.8f,
				0.05f
			};

			//■ロードローカル。
			MainStep t_main_step = MainStep.LoadLocal_SoundPool;

			//ローカル、サウンロプール管理ファイル。相対パス。
			Fee.File.Path t_local_caoundpool_path = new File.Path(a_path.GetFileName());

			//ローカル、サウンドプール管理ファイルのロード。
			Fee.JsonItem.JsonItem t_local_soundpool_json = null;
			if(Config.USE_DOWNLOAD_SOUNDPOOL_CACHE == true){
				Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoadLocalTextFile(t_local_caoundpool_path);
				while(t_item.IsBusy() == true){

					this.UpdateProgress(a_instance,(int)t_main_step,1,0,t_item.GetResultProgressDown());
					yield return null;

				}
				if(t_item.GetResultType()==File.Item.ResultType.Text){
					t_local_soundpool_json = new JsonItem.JsonItem(t_item.GetResultText());
					if(t_local_soundpool_json == null){
						//コンバート失敗。
						this.result.errorstring = "Coroutine_LoadStreamingAssetsSoundPool : local_soundpool_json == null";
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
									t_local_soundpool.fullpath_list.Add(File.File.GetLocalPath(new File.Path(t_local_soundpool.name_list[ii])));
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

			//■ダウンロード。
			t_main_step = MainStep.DownLoad_SoundPool;

			//ロードorダウンロード。
			string t_loadstreamingassets_stringjson = null;
			Fee.Audio.Pack_SoundPool t_loadstreamingassets_soundpool = null;

			//サウンドプール管理ファイルのロードorダウンロード。
			{
				Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoadStreamingAssetsTextFile(a_path);
				while(t_item.IsBusy() == true){

					this.UpdateProgress(a_instance,(int)t_main_step,1,0,t_item.GetResultProgressDown());
					yield return null;

				}
				this.result.responseheader = t_item.GetResultResponseHeader();

				if(t_item.GetResultType()==File.Item.ResultType.Text){
					//成功。
					t_loadstreamingassets_stringjson = t_item.GetResultText();
				}else{
					//失敗。
					this.result.errorstring = t_item.GetResultErrorString();
					yield break;
				}

				t_loadstreamingassets_soundpool = JsonItem.Convert.JsonStringToObject<Fee.Audio.Pack_SoundPool>(t_loadstreamingassets_stringjson);

				if(t_loadstreamingassets_soundpool == null){
					//コンバート失敗。
					this.result.errorstring = "Coroutine_LoadStreamingAssetsSoundPool : loadstreamingassets_soundpool == null";
					yield break;
				}else if(t_loadstreamingassets_soundpool.name_list == null){
					//不明なデータ。
					this.result.errorstring = "Coroutine_LoadStreamingAssetsSoundPool : loadstreamingassets_soundpool.name_list == null";
					yield break;
				}else if(t_loadstreamingassets_soundpool.volume_list == null){
					//不明なデータ。
					this.result.errorstring = "Coroutine_LoadStreamingAssetsSoundPool : loadstreamingassets_soundpool.volume_list == null";
					yield break;
				}
			}

			//■サウンド。
			t_main_step = MainStep.Sound;

			//登録サウンドのロードストリーミングアセット。
			{
				int t_substep_max = t_loadstreamingassets_soundpool.name_list.Count * 2;

				for(int ii=0;ii<t_loadstreamingassets_soundpool.name_list.Count;ii++){

					byte[] t_sound_binary = null;

					//ロードストリーミングアセット。
					{
						Fee.File.Path t_sound_url = a_path.CreateUrl_ChangeFileName(t_loadstreamingassets_soundpool.name_list[ii]);

						Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoadStreamingAssetsBinaryFile(t_sound_url);
						while(t_item.IsBusy() == true){

							this.UpdateProgress(a_instance,(int)t_main_step,t_substep_max,ii*2+0,t_item.GetResultProgressDown());
							yield return null;

						}
						if(t_item.GetResultType()==File.Item.ResultType.Binary){
							//成功。
							t_sound_binary = t_item.GetResultBinary();
						}else{
							//失敗。
							this.result.errorstring = t_item.GetResultErrorString();
							yield break;
						}
					}

					//セーブローカル。
					{
						Fee.File.Path t_sound_url = new File.Path(t_loadstreamingassets_soundpool.name_list[ii]);

						File.Item t_item = Fee.File.File.GetInstance().RequestSaveLocalBinaryFile(t_sound_url,t_sound_binary);
						while(t_item.IsBusy() == true){

							this.UpdateProgress(a_instance,(int)t_main_step,t_substep_max,ii*2+1,t_item.GetResultProgressDown());
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

			//■サウンド。
			t_main_step = MainStep.SaveLocal_SoundPool;

			//ローカル、サウンドプール管理ファイルのセーブ。
			{
				File.Item t_item = Fee.File.File.GetInstance().RequestSaveLocalTextFile(t_local_caoundpool_path,t_loadstreamingassets_stringjson);
				while(t_item.IsBusy() == true){

					this.UpdateProgress(a_instance,(int)t_main_step,1,0,t_item.GetResultProgressDown());
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
				t_loadstreamingassets_soundpool.fullpath_list = new System.Collections.Generic.List<File.Path>();
				for(int ii=0;ii<t_loadstreamingassets_soundpool.name_list.Count;ii++){
					t_loadstreamingassets_soundpool.fullpath_list.Add(File.File.GetLocalPath(new File.Path(t_loadstreamingassets_soundpool.name_list[ii])));
				}
			}

			this.result.soundpool = t_loadstreamingassets_soundpool;
			yield break;
		}
	}
}

