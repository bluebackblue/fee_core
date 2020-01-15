

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** ロードリソース。プレハブファイル。
	*/
	public class Coroutine_LoadResourcesPrefabFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** プレハブファイル。
			*/
			public UnityEngine.GameObject prefab_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType(UnityEngine.GameObject a_prefab_file,string a_errorstring)
			{
				//prefab_file
				this.prefab_file = a_prefab_file;

				//errorstring
				this.errorstring = a_errorstring;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path)
		{
			//result
			this.result = null;

			//ロード。
			UnityEngine.Object t_result_object = null;
			{
				UnityEngine.ResourceRequest t_request = null;

				try{
					t_request = UnityEngine.Resources.LoadAsync(a_path.GetPath());
					if(t_request == null){
						//エラー。
						this.result = new ResultType(null,"Unknown Error : LoadResourcesPrefabFile : " + a_path.GetPath());
						yield break;
					}
				}catch(System.Exception t_exception){
					//エラー。
					this.result = new ResultType(null,"Unknown Error : LoadResourcesPrefabFile : " + a_path.GetPath() + " : " + t_exception.Message);
					yield break;
				}

				do{
					//キャンセルチェック。
					{
						if(a_callback_interface != null){
							if(a_callback_interface.OnFileCoroutine(t_request.progress) == false){
								//t_request.Cancel();
							}
						}
					}

					yield return null;
				}while(t_request.isDone == false);

				if(t_request.asset == null){
					//エラー。
					this.result = new ResultType(null,"Load Error : LoadResourcesPrefabFile : " + a_path.GetPath());
					yield break;
				}

				t_result_object = t_request.asset;
			}

			//コンバート。
			UnityEngine.GameObject t_result_prefab = null;
			{
				UnityEngine.GameObject t_result = t_result_object as UnityEngine.GameObject;

				if(t_result == null){
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadResourcesPrefabFile : " + a_path.GetPath());
					yield break;
				}

				t_result_prefab = t_result;
			}

			//成功。
			this.result = new ResultType(t_result_prefab,null);
			yield break;
		}
	}
}

