

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。アセットバンドル。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** Main_AssetBundle
	*/
	public class Main_AssetBundle : Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface
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

			/** ロードパスアイテム。アセットバンドルアイテム。

				パスアイテムからアセットバンドルアイテムをロード。

			*/
			LoadPathItemAssetBundleItem,

			/** アンロード。アセットバンドルアイテム。
			*/
			UnLoadAssetBundleItem,
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

		/** request_assetbundle_name
		*/
		private string request_assetbundle_name;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_assetbundle_item
		*/
		private AssetBundleItem result_assetbundle_item;

		/** constructor
		*/
		public Main_AssetBundle()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_assetbundle_name = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_assetbundle_item = null;
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

		/** GetResultAssetBundleItem
		*/
		public AssetBundleItem GetResultAssetBundleItem()
		{
			return this.result_assetbundle_item;
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

		/** リクエスト。ロードパスアイテム。アセットバンドルアイテム。

			パスアイテムからアセットバンドルアイテムをロード。

		*/
		public bool RequestLoadPathItemAssetBundleItem(string a_assetbundle_name)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_assetbundle_item = null;

				//request
				this.request_assetbundle_name = a_assetbundle_name;

				Function.Function.StartCoroutine(this.DoLoadPathItemAssetBundleItem());
				return true;
			}

			return false;
		}

		/** 実行。ロードパスアイテム。アセットバンドルアイテム。

			パスアイテムからアセットバンドルアイテムをロード。

		*/
		private System.Collections.IEnumerator DoLoadPathItemAssetBundleItem()
		{
			Coroutine_LoadPathItemAssetBundleItem t_coroutine = new Coroutine_LoadPathItemAssetBundleItem();
			yield return t_coroutine.CoroutineMain(this,this.request_assetbundle_name);

			if(t_coroutine.result.assetbundle_item != null){
				this.result_progress = 1.0f;
				this.result_assetbundle_item = t_coroutine.result.assetbundle_item;
				this.result_type = ResultType.LoadPathItemAssetBundleItem;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。アンロード。アセットバンドルアイテム。
		*/
		public bool RequestUnLoadAssetBundleItem(string a_assetbundle_name)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_assetbundle_item = null;

				//request
				this.request_assetbundle_name = a_assetbundle_name;

				Function.Function.StartCoroutine(this.DoUnLoadAssetBundleItem());
				return true;
			}

			return false;
		}

		/** 実行。アンロード。アセットバンドルアイテム。
		*/
		private System.Collections.IEnumerator DoUnLoadAssetBundleItem()
		{
			Coroutine_UnLoadAssetBundleItem t_coroutine = new Coroutine_UnLoadAssetBundleItem();
			yield return t_coroutine.CoroutineMain(this,this.request_assetbundle_name);

			if(t_coroutine.result.unload == true){
				this.result_progress = 1.0f;
				this.result_type = ResultType.UnLoadAssetBundleItem;
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

