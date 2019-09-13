

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。パック。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** Main_Pack
	*/
	public class Main_Pack : Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface
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

			/** ロードパスアイテム。パックアイテム。

				パスアイテムからパックアイテムをロード。

			*/
			LoadPathItemPackItem,

			/** アンロード。パックアイテム。
			*/
			UnLoadPackItem,
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

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_pack_item
		*/
		private PackItem result_pack_item;

		/** constructor
		*/
		public Main_Pack()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_id = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_pack_item = null;
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

		/** GetResultPackItem
		*/
		public PackItem GetResultPackItem()
		{
			return this.result_pack_item;
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

		/** リクエスト。ロードパスアイテム。パックアイテム。

			パスアイテムからパックアイテムをロード。

		*/
		public bool RequestLoadPathItemPackItem(string a_id)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_pack_item = null;

				//request
				this.request_id = a_id;

				Function.Function.StartCoroutine(this.DoLoadPathItemPackItem());
				return true;
			}

			return false;
		}

		/** 実行。ロードパスアイテム。パックアイテム。

			パスアイテムからパックアイテムをロード。

		*/
		private System.Collections.IEnumerator DoLoadPathItemPackItem()
		{
			Coroutine_LoadPathItemPackItem t_coroutine = new Coroutine_LoadPathItemPackItem();
			yield return t_coroutine.CoroutineMain(this,this.request_id);

			if(t_coroutine.result.pack_item != null){
				this.result_progress = 1.0f;
				this.result_pack_item = t_coroutine.result.pack_item;
				this.result_type = ResultType.LoadPathItemPackItem;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。アンロード。パックアイテム。
		*/
		public bool RequestUnLoadPackItem(string a_id)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_pack_item = null;

				//request
				this.request_id = a_id;

				Function.Function.StartCoroutine(this.DoUnLoadPackItem());
				return true;
			}

			return false;
		}

		/** 実行。アンロード。パックアイテム。
		*/
		private System.Collections.IEnumerator DoUnLoadPackItem()
		{
			Coroutine_UnLoadPackItem t_coroutine = new Coroutine_UnLoadPackItem();
			yield return t_coroutine.CoroutineMain(this,this.request_id);

			if(t_coroutine.result.unload == true){
				this.result_progress = 1.0f;
				this.result_type = ResultType.UnLoadPackItem;
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

