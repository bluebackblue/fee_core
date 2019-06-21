

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。コルーチン。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** ノーマル。
	*/
	public class Coroutine_Normal
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットファイル。
			*/
			public Fee.Asset.Asset asset_file;

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
				//asset_file
				this.asset_file = null;

				//errorstring
				this.errorstring = null;

				//responseheader
				this.responseheader = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,ListItem a_listitem)
		{
			//result
			this.result = new ResultType();

			//LoadRequestType
			Fee.File.File.LoadRequestType t_loadrequest_type = File.File.LoadRequestType.None;
			switch(a_listitem.path_type){
			case PathType.Resources_Prefab:
				{
					t_loadrequest_type = File.File.LoadRequestType.LoadResourcesPrefabFile;
				}break;
			case PathType.Resources_Texture:
				{
					t_loadrequest_type = File.File.LoadRequestType.LoadResourcesTextureFile;
				}break;
			case PathType.StreamingAssets_Texture:
				{
					t_loadrequest_type = File.File.LoadRequestType.LoadStreamingAssetsTextureFile;
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}

			//RequestLoad
			Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoad(t_loadrequest_type,a_listitem.path);

			while(true){
				if(t_item.GetResultType() != File.Item.ResultType.None){
					if(t_item.GetResultType() == File.Item.ResultType.Asset){
						//成功。
						this.result.asset_file = t_item.GetResultAsset();
						yield break;
					}else if(t_item.GetResultType() == File.Item.ResultType.Error){
						//失敗。
						this.result.errorstring = "Coroutine_Normal : " + t_item.GetResultErrorString();
						yield break;
					}else{
						break;
					}
				}

				a_instance.OnCoroutine(t_item.GetResultProgressDown());

				yield return null;
			}

			//不明。
			Tool.Assert(false);
			this.result.errorstring = "Coroutine_Normal : " + "Unknown";
			yield break;
		}
	}
}

