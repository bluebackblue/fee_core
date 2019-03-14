

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
	/** Item
	*/
	public class Item
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

			/** コンテキスト。
			*/
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

		/** cancel_flag
		*/
		private bool cancel_flag;

		/** result_context
		*/
		#if(USE_DEF_UNIVRM)
		private VRM.VRMImporterContext result_context;
		#endif

		/** result_animetor
		*/
		private UnityEngine.Animator result_animator;

		/** constructor
		*/
		public Item()
		{
			//result_type
			this.result_type = ResultType.None;

			//result_progress
			this.result_progress = 0.0f;

			//result_errorstring
			this.result_errorstring = null;

			//cancel_flag
			this.cancel_flag = false;

			//result_context
			#if(USE_DEF_UNIVRM)
			this.result_context = null;
			#endif

			//result_animator
			this.result_animator = null;
		}

		/** 削除。
		*/
		public void Delete()
		{
			#if(USE_DEF_UNIVRM)
			if(this.result_context != null){
				this.result_context.Dispose();
			}
			#endif
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

		/** 結果。タイプ。取得。
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
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

		/** 結果。エラー文字。設定。
		*/
		public void SetResultErrorString(string a_error_string)
		{
			this.result_type = ResultType.Error;

			this.result_errorstring = a_error_string;
		}

		/** 結果。エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** 結果。コンテキスト。設定。
		*/
		#if(USE_DEF_UNIVRM)
		public void SetResultContext(VRM.VRMImporterContext a_context)
		{
			this.result_type = ResultType.Context;
			this.result_context = a_context;
			this.result_animator = null;

			if(this.result_context != null){
				if(this.result_context.Root != null){
					this.result_animator = this.result_context.Root.GetComponent<Animator>();
				}
			}
		}
		#endif

		/** 結果。コンテキスト。取得。
		*/
		#if(USE_DEF_UNIVRM)
		public VRM.VRMImporterContext GetResultContext()
		{
			return this.result_context;
		}
		#endif

		/** [内部からの呼び出し]レイヤー。設定。
		*/
		private static void Raw_SetLayer(UnityEngine.Transform a_transform,int a_layer)
		{
			UnityEngine.GameObject t_gameobject = a_transform.gameObject;
			if(t_gameobject != null){
				t_gameobject.layer = a_layer;
			}

			foreach(UnityEngine.Transform t_transform in a_transform){
				Raw_SetLayer(t_transform,a_layer);
			}
		}

		/** レイヤー。設定。
		*/
		public void SetLayer(string a_layername)
		{
			#if(USE_DEF_UNIVRM)
			if(this.result_context != null){
				Raw_SetLayer(this.result_context.Root.transform,LayerMask.NameToLayer(a_layername));
			}
			#endif
		}

		/** 表示。設定。
		*/
		public void SetRendererEnable(bool a_flag)
		{
			#if(USE_DEF_UNIVRM)
			if(this.result_context != null){
				for(int ii=0;ii<this.result_context.Meshes.Count;ii++){
					if(this.result_context.Meshes[ii] != null){
						if(this.result_context.Meshes[ii].Renderers != null){
							for(int jj=0;jj<this.result_context.Meshes[ii].Renderers.Count;jj++){
								if(this.result_context.Meshes[ii].Renderers[jj] != null){
									this.result_context.Meshes[ii].Renderers[jj].enabled = a_flag;
								}
							}
						}
					}
				}
			}
			#endif
		}

		/** アニメータコントローラ。設定。
		*/
		public void SetAnimatorController(UnityEngine.RuntimeAnimatorController a_animator_Controller)
		{
			if(this.result_animator != null){
				this.result_animator.runtimeAnimatorController = a_animator_Controller;
			}
		}

		/** アニメ。設定。
		*/
		public bool IsAnimeEnable()
		{
			if(this.result_animator != null){
				return this.result_animator.enabled;
			}
			return false;
		}

		/** アニメ。設定。
		*/
		public void SetAnimeEnable(bool a_flag)
		{
			if(this.result_animator != null){
				this.result_animator.enabled = a_flag;
			}
		}

		/** アニメ。設定。
		*/
		public void SetAnime(int a_state_name_hash)
		{
			if(this.result_animator != null){
				this.result_animator.Play(a_state_name_hash);
			}
		}

		/** GetBoneTransform
		*/
		public UnityEngine.Transform GetBoneTransform(UnityEngine.HumanBodyBones a_bone)
		{
			if(this.result_animator != null){
				return this.result_animator.GetBoneTransform(a_bone);
			}

			return null;
		}

		/** GetTransform
		*/
		public UnityEngine.Transform GetTransform()
		{
			#if(USE_DEF_UNIVRM)
			if(this.result_context != null){
				return this.result_context.Root.gameObject.transform;
			}
			#endif

			return null;
		}

		/** GetForward
		*/
		public UnityEngine.Vector3 GetForward()
		{
			#if(USE_DEF_UNIVRM)
			if(this.result_context != null){
				return this.result_context.Root.gameObject.transform.forward;
			}
			#endif

			return UnityEngine.Vector3.zero;
		}

		/** 位置。取得。
		*/
		public UnityEngine.Vector3 GetPosition()
		{
			#if(USE_DEF_UNIVRM)
			if(this.result_context != null){
				return this.result_context.Root.gameObject.transform.position;
			}
			#endif

			return UnityEngine.Vector3.zero;
		}

		/** 位置。設定。
		*/
		public void SetPosition(ref UnityEngine.Vector3 a_position)
		{
			#if(USE_DEF_UNIVRM)
			if(this.result_context != null){
				this.result_context.Root.gameObject.transform.position = a_position;
			}
			#endif
		}
	}
}

