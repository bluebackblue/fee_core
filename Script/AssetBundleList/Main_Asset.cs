

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。アセット。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** Main_Asset
	*/
	public class Main_Asset : Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface
	{
		/** ResultType
		*/
		public enum ResultType
		{
			/** 未定義。
			*/
			None,

			/** エラー。
			*/
			Error,

			/** アセット。
			*/
			Asset,
		};

		/** is_busy
		*/
		private bool is_busy;

		/** キャンセル。チェック。
		*/
		private bool is_cancel;

		/** シャットダウン。チェック。
		*/
		private bool is_shutdown;

		/** request_id
		*/
		private string request_id;

		/** request_assetname
		*/
		private string request_assetname;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_asset
		*/
		private Fee.Asset.Asset result_asset;

		/** constructor
		*/
		public Main_Asset()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_id = null;
			this.request_assetname = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_asset = null;
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.is_shutdown = true;
		}

		/** キャンセル。
		*/
		public void Cancel()
		{
			this.is_cancel = true;
		}

		/** 完了。
		*/
		public void Fix()
		{
			this.is_busy = false;
		}

		/** GetResultProgress
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** GetResultErrorString
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** GetResultType
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
		}

		/** GetResultAsset
		*/
		public Fee.Asset.Asset GetResultAsset()
		{
			return this.result_asset;
		}

		/** [Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		public bool OnAssetBundleListCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。ロードパックアイテム。テキストファイル。
		*/
		public bool RequestLoadPackItemTextFile(string a_id,string a_assetname)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;

				//request
				this.request_id = a_id;
				this.request_assetname = a_assetname;

				Function.Function.StartCoroutine(this.DoLoadPackItemTextFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードパックアイテム。テキストファイル。
		*/
		private System.Collections.IEnumerator DoLoadPackItemTextFile()
		{
			Coroutine_LoadPackItemTextFile t_coroutine = new Coroutine_LoadPackItemTextFile();
			yield return t_coroutine.CoroutineMain(this,this.request_id,this.request_assetname);

			if(t_coroutine.result.asset_file != null){
				this.result_progress = 1.0f;
				this.result_asset = t_coroutine.result.asset_file;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードパックアイテム。テクスチャファイル。
		*/
		public bool RequestLoadPackItemTextureFile(string a_id,string a_assetname)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;

				//request
				this.request_id = a_id;
				this.request_assetname = a_assetname;

				Function.Function.StartCoroutine(this.DoLoadPackItemTextureFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードパックアイテム。テクスチャファイル。
		*/
		private System.Collections.IEnumerator DoLoadPackItemTextureFile()
		{
			Coroutine_LoadPackItemTextureFile t_coroutine = new Coroutine_LoadPackItemTextureFile();
			yield return t_coroutine.CoroutineMain(this,this.request_id,this.request_assetname);

			if(t_coroutine.result.asset_file != null){
				this.result_progress = 1.0f;
				this.result_asset = t_coroutine.result.asset_file;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードパックアイテム。プレハブファイル。
		*/
		public bool RequestLoadPackItemPrefabFile(string a_id,string a_assetname)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;

				//request
				this.request_id = a_id;
				this.request_assetname = a_assetname;

				Function.Function.StartCoroutine(this.DoLoadPackItemPrefabFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードパックアイテムアイテム。プレハブファイル。
		*/
		private System.Collections.IEnumerator DoLoadPackItemPrefabFile()
		{
			Coroutine_LoadPackItemPrefabFile t_coroutine = new Coroutine_LoadPackItemPrefabFile();
			yield return t_coroutine.CoroutineMain(this,this.request_id,this.request_assetname);

			if(t_coroutine.result.asset_file != null){
				this.result_progress = 1.0f;
				this.result_asset = t_coroutine.result.asset_file;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}
	}
}

