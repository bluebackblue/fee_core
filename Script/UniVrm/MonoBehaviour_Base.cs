

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。
*/


/** Fee.UniVrm
*/
namespace Fee.UniVrm
{
	/** MonoBehaviour_Base
	*/
	public abstract class MonoBehaviour_Base : UnityEngine.MonoBehaviour
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

			/** エラー終了。
			*/
			Do_Error,

			/** 正常終了。
			*/
			Do_Success,

			/** 完了。
			*/
			Fix,
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

			/** コンテキスト。
			*/
			Context,
		};

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected abstract void OnInitialize();

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected abstract System.Collections.IEnumerator OnStart();

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected abstract System.Collections.IEnumerator OnDo();

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected abstract System.Collections.IEnumerator OnDoError();

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected abstract System.Collections.IEnumerator OnDoSuccess();

		/** mode
		*/
		[UnityEngine.SerializeField]
		private Mode mode;

		/** cancel_flag
		*/
		[UnityEngine.SerializeField]
		private bool cancel_flag;

		/** delete_flag
		*/
		[UnityEngine.SerializeField]
		private bool delete_flag;

		/** result_progress 
		*/
		[UnityEngine.SerializeField]
		private float result_progress;

		/** result_errorstring
		*/
		[UnityEngine.SerializeField]
		private string result_errorstring;

		/** result_type
		*/
		[UnityEngine.SerializeField]
		private ResultType result_type;

		/** result_context
		*/
		#if(USE_DEF_FEE_UNIVRM)
		[SerializeField]
		private VRM.VRMImporterContext result_context;
		#endif

		/** 結果フラグリセット。
		*/
		protected void ResetResultFlag()
		{
			this.cancel_flag = false;

			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;

			#if(USE_DEF_FEE_UNIVRM)
			this.result_context = null;
			#endif
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

		/** プログレス。取得。
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** プログレス。設定。
		*/
		public void SetResultProgress(float a_progress)
		{
			this.result_progress = a_progress;
		}

		/** エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** 結果タイプ。取得。
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
		}

		/** リクエスト待ち開始。
		*/
		public void WaitRequest()
		{
			if(this.mode == Mode.Fix){
				this.mode = Mode.WaitRequest;
			}else{
				Tool.Assert(false);
			}
		}

		/** リクエスト待ち。
		*/
		protected bool IsWaitRequest()
		{
			if(this.mode == Mode.WaitRequest){
				return true;
			}
			return false;
		}

		/** 開始。
		*/
		protected void SetModeStart()
		{
			this.mode = Mode.Start;
		}

		/** 実行。
		*/
		protected void SetModeDo()
		{
			this.mode = Mode.Do;
		}

		/** 正常終了。
		*/
		protected void SetModeDoSuccess()
		{
			this.mode = Mode.Do_Success;
		}

		/** エラー終了。
		*/
		protected void SetModeDoError()
		{
			this.mode = Mode.Do_Error;
		}

		/** 完了。
		*/
		protected void SetModeFix()
		{
			this.mode = Mode.Fix;
		}

		/** 完了チェック。
		*/
		public bool IsFix()
		{
			if(this.mode == Mode.Fix){
				return true;
			}
			return false;
		}

		/** 削除リクエスト。設定。
		*/
		public void DeleteRequest()
		{
			this.delete_flag = true;
		}

		/** 削除リクエスト。取得。
		*/
		public bool IsDeleteRequest()
		{
			return this.delete_flag;
		}

		/** 結果。設定。
		*/
		public void SetResultErrorString(string a_error_string)
		{
			this.result_type = ResultType.Error;
			this.result_errorstring = a_error_string;
		}

		/** 結果。設定。
		*/
		#if(USE_DEF_FEE_UNIVRM)
		public void SetResultContext(VRM.VRMImporterContext a_context)
		{
			this.result_type = ResultType.Context;
			this.result_context = a_context;
		}
		#endif

		/** 結果。取得。
		*/
		#if(USE_DEF_FEE_UNIVRM)
		public VRM.VRMImporterContext GetResultContext()
		{
			return this.result_context;
		}
		#endif

		/** Awake
		*/
		private void Awake()
		{
			this.mode = Mode.WaitRequest;
			this.cancel_flag = false;
			this.delete_flag = false;
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;

			#if(USE_DEF_FEE_UNIVRM)
			this.result_context = null;
			#endif

			this.OnInitialize();
		}

		/** Start
		*/
		private System.Collections.IEnumerator Start()
		{
			bool t_loop = true;
			while(t_loop){
				switch(this.mode){
				case Mode.WaitRequest:
					{
						yield return null;
						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Fix:
					{
						yield return null;
						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Start:
					{
						yield return this.OnStart();
					}break;
				case Mode.Do:
					{
						yield return  this.OnDo();
					}break;
				case Mode.Do_Error:
					{
						yield return this.OnDoError();
					}break;
				case Mode.Do_Success:
					{
						yield return this.OnDoSuccess();
					}break;
				}
			}

			Tool.Log(this.gameObject.name,"GameObject.Destroy");
			UnityEngine.GameObject.Destroy(this.gameObject);
			yield break;
		}
	}
}

