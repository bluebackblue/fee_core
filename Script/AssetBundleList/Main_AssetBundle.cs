

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。ファイル。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** Main_AssetBundle
	*/
	public class Main_AssetBundle : Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** アセットバンドルアイテム。
			*/
			AssetBundleItem,
		};

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

			/** ロードパス。アセットバンドルアイテム。
			*/
			LoadPathAssetBundleItem,

			/** アンロードパス。アセットバンドルアイテム。
			*/
			UnLoadPathAssetBundleItem,

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

		/** request_type
		*/
		private RequestType request_type;

		/** request_id
		*/
		private string request_id;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_assetbundleitem
		*/
		private AssetBundlePackList_AssetBundleItem result_assetbundleitem;

		/** constructor
		*/
		public Main_AssetBundle()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_id = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_assetbundleitem = null;
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
		public AssetBundlePackList_AssetBundleItem GetResultAssetBundleItem()
		{
			return this.result_assetbundleitem;
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

		/** リクエスト。ロードパス。アセットバンドルアイテム。
		*/
		public bool RequestLoadPathAssetBundleItem(string a_id)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_assetbundleitem = null;

				//request
				this.request_type = RequestType.AssetBundleItem;
				this.request_id = a_id;

				Function.Function.StartCoroutine(this.DoLoadPathAssetBundleItem());
				return true;
			}

			return false;
		}

		/** 実行。ロードパス。アセットバンドルアイテム。
		*/
		private System.Collections.IEnumerator DoLoadPathAssetBundleItem()
		{
			Tool.Assert(this.request_type == RequestType.AssetBundleItem);

			Coroutine_LoadPathAssetBundleItem t_coroutine = new Coroutine_LoadPathAssetBundleItem();
			yield return t_coroutine.CoroutineMain(this,this.request_id);

			if(t_coroutine.result.assetbundleitem != null){
				this.result_progress = 1.0f;
				this.result_assetbundleitem = t_coroutine.result.assetbundleitem;
				this.result_type = ResultType.LoadPathAssetBundleItem;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。アンロードパス。アセットバンドルアイテム。
		*/
		public bool RequestUnLoadPathAssetBundleItem(string a_id)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_assetbundleitem = null;

				//request
				this.request_type = RequestType.AssetBundleItem;
				this.request_id = a_id;

				Function.Function.StartCoroutine(this.DoUnLoadPathAssetBundleItem());
				return true;
			}

			return false;
		}

		/** 実行。アンロードパス。アセットバンドルアイテム。
		*/
		private System.Collections.IEnumerator DoUnLoadPathAssetBundleItem()
		{
			Tool.Assert(this.request_type == RequestType.AssetBundleItem);

			Coroutine_UnloadPathAssetBundleItem t_coroutine = new Coroutine_UnloadPathAssetBundleItem();
			yield return t_coroutine.CoroutineMain(this,this.request_id);

			if(t_coroutine.result.unload == true){
				this.result_progress = 1.0f;
				this.result_type = ResultType.UnLoadPathAssetBundleItem;
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

