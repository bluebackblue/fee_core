using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。ＩＯ。
*/


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** MonoBehaviour_Base
	*/
	public class MonoBehaviour_Base : MonoBehaviour
	{
		/** Mode
		*/
		protected enum Mode
		{
			/** リクエスト待ち。
			*/
			WaitRequest,

			/** 開始。
			*/
			Start,

			/** 実行中。
			*/
			Do,

			/** 完了。
			*/
			Fix,
		};

		/** mode
		*/
		[SerializeField]
		protected Mode mode;

		/** delete_flag
		*/
		[SerializeField]
		protected bool delete_flag;

		/** result_progress 
		*/
		[SerializeField]
		protected float result_progress;

		/** result_errorstring
		*/
		[SerializeField]
		protected string result_errorstring;

		/** result_datatype
		*/
		[SerializeField]
		protected DataType result_datatype;

		/** プログレス。取得。
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** データタイプ。取得。
		*/
		public DataType GetResultDataType()
		{
			return this.result_datatype;
		}
	}
}

