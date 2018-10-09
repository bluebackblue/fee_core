using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。
*/


/** NUniVrm
*/
namespace NUniVrm
{
	/** Item
	*/
	public class Item
	{
		/** ResultType
		*/
		public enum ResultType
		{
			None,

			Error,

			Context,
		}

		/** result_type
		*/
		private ResultType result_type;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_context
		*/
		#if(USE_UNIVRM)
		private VRM.VRMImporterContext result_context;
		#endif

		/** cancel_flag
		*/
		private bool cancel_flag;

		/** constructor
		*/
		public Item()
		{
			//result_type
			this.result_type = ResultType.None;

			//result_progress
			this.result_progress = 0.0f;

			//result_context
			#if(USE_UNIVRM)
			this.result_context = null;
			#endif

			//cancel_flag
			this.cancel_flag = false;
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if(this.result_type == ResultType.None){
				return true;
			}
			return false;
		}

		/** キャンセル。設定。
		*/
		public void Cancel()
		{
			this.cancel_flag = true;
		}

		/** キャンセル。取得。
		*/
		public bool IsCancel()
		{
			return this.cancel_flag;
		}

		/** プログレス。設定。
		*/
		public void SetResultProgress(float a_result_progress)
		{
			this.result_progress = a_result_progress;
		}

		/** プログレス。取得。
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** データタイプ。取得。
		*/
		public ResultType GetResultDataType()
		{
			return this.result_type;
		}

		/** 結果。設定。
		*/
		public void SetResultErrorString(string a_error_string)
		{
			this.result_type = ResultType.Error;

			this.result_errorstring = a_error_string;
		}

		/** 結果。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** 結果。設定。
		*/
		#if(USE_UNIVRM)
		public void SetResultContext(VRM.VRMImporterContext a_context)
		{
			this.result_type = ResultType.Context;

			this.result_context = a_context;
		}
		#endif

		/** TODO:[内部からの呼び出し]レイヤー。設定。
		*/
		private static void Raw_SetLayer(Transform a_transform,int a_layer)
		{
			GameObject t_gameobject = a_transform.gameObject;
			if(t_gameobject != null){
				t_gameobject.layer = a_layer;
			}

			foreach(Transform t_transform in a_transform){
				Raw_SetLayer(t_transform,a_layer);
			}
		}

		/** TODO:レイヤー。設定。
		*/
		public void SetLayer(string a_layername)
		{
			Raw_SetLayer(this.result_context.Root.transform,LayerMask.NameToLayer(a_layername));
		}

		/** 表示。
		*/
		public void Show()
		{
			this.result_context.ShowMeshes();
		} 
	}
}

