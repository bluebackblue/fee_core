

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
	/** セーブローカル。サウンドプール
	*/
	public class Coroutine_SaveLocalSoundPool
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** セーブ完了。
			*/
			public bool saveend;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.saveend = false;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,Fee.File.Path a_path,Fee.Audio.Pack_SoundPool a_soundpool)
		{
			//result
			this.result = new ResultType();

			//コンバート。
			string t_jsonstring = Fee.JsonItem.Convert.ObjectToJsonString<Fee.Audio.Pack_SoundPool>(a_soundpool);
			if(t_jsonstring == null){
				this.result.errorstring = "Coroutine_SaveLocalSoundPool : jsonstring == null";
				yield break;
			}

			//セーブローカル。
			Fee.File.Item t_item = Fee.File.File.GetInstance().RequestSaveLocalTextFile(a_path,t_jsonstring);
			while(t_item.IsBusy() == true){
				yield return null;
			}
			if(t_item.GetResultType()==File.Item.ResultType.SaveEnd){
				//成功。
				this.result.saveend = true;
				yield break;
			}else{
				//失敗。
				this.result.errorstring = t_item.GetResultErrorString();
				yield break;
			}
		}
	}
}

