

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
	/** ダウンロード。サウンドプール。
	*/
	public class Coroutine_DownLoadSoundPool
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

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,File.Path a_path,UnityEngine.WWWForm a_post_data,uint a_data_version)
		{
			//result
			this.result = new ResultType();

			//ローカル、サウンロプール管理ファイル。相対パス。
			Fee.File.Path t_local_caoundpool_path = new File.Path(a_path.GetFileName());

			//ローカル、サウンドプール管理ファイルのロード。
			Fee.JsonItem.JsonItem t_local_soundpool_json = null;
			if(Config.USE_DOWNLOAD_SOUNDPOOL_CACHE == true){
				Fee.File.Item t_item_a = Fee.File.File.GetInstance().RequestLoadLocalTextFile(t_local_caoundpool_path);
				while(t_item_a.IsBusy() == true){
					yield return null;
				}
				if(t_item_a.GetResultType()==File.Item.ResultType.Text){
					t_local_soundpool_json = new JsonItem.JsonItem(t_item_a.GetResultText());
					if(t_local_soundpool_json == null){
						//コンバート失敗。
						this.result.errorstring = "Coroutine_DownLoadSoundPool : local_soundpool_json == null";
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

			//ダウンロード。
			string t_download_stringjson = null;
			Fee.Audio.Pack_SoundPool t_download_soundpool = null;

			//サウンドプール管理ファイルのダウンロード。
			{
				Fee.File.Item t_item = Fee.File.File.GetInstance().RequestDownLoadTextFile(a_path,a_post_data,File.ProgressMode.DownLoad);
				while(t_item.IsBusy() == true){
					yield return null;
				}
				this.result.responseheader = t_item.GetResultResponseHeader();

				if(t_item.GetResultType()==File.Item.ResultType.Text){
					//成功。
					t_download_stringjson = t_item.GetResultText();
				}else{
					//失敗。
					this.result.errorstring = t_item.GetResultErrorString();
					yield break;
				}

				t_download_soundpool = JsonItem.Convert.JsonStringToObject<Fee.Audio.Pack_SoundPool>(t_download_stringjson);

				if(t_download_soundpool == null){
					//コンバート失敗。
					this.result.errorstring = "Coroutine_DownLoadSoundPool : download_soundpool == null";
					yield break;
				}else if(t_download_soundpool.name_list == null){
					//不明なデータ。
					this.result.errorstring = "Coroutine_DownLoadSoundPool : download_soundpool.name_list == null";
					yield break;
				}else if(t_download_soundpool.volume_list == null){
					//不明なデータ。
					this.result.errorstring = "Coroutine_DownLoadSoundPool : download_soundpool.volume_list == null";
					yield break;
				}
			}

			//登録サウンドのダウンロード。
			{
				for(int ii=0;ii<t_download_soundpool.name_list.Count;ii++){

					byte[] t_sound_binary = null;

					//ダウンロード。
					{
						Fee.File.Path t_sound_url = a_path.CreateUrl_ChangeFileName(t_download_soundpool.name_list[ii]);

						//TODO:ここのポスト通信のカスタマイズは必要？

						Fee.File.Item t_item = Fee.File.File.GetInstance().RequestDownLoadBinaryFile(t_sound_url,null,File.ProgressMode.DownLoad);
						while(t_item.IsBusy() == true){
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
						Fee.File.Path t_sound_url = new File.Path(t_download_soundpool.name_list[ii]);

						File.Item t_item = Fee.File.File.GetInstance().RequestSaveLocalBinaryFile(t_sound_url,t_sound_binary);
						while(t_item.IsBusy() == true){
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
				File.Item t_item = Fee.File.File.GetInstance().RequestSaveLocalTextFile(t_local_caoundpool_path,t_download_stringjson);
				while(t_item.IsBusy() == true){
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

			this.result.soundpool = t_download_soundpool;
			yield break;
		}
	}
}

