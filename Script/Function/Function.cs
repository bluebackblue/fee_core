

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 関数呼び出し。
*/


/** Fee.Function
*/
namespace Fee.Function
{
	/** Function
	*/
	public class Function
	{
		/** [シングルトン]s_instance
		*/
		private static Function s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Function();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Function GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** monobehaviour
		*/
		private UnityEngine.MonoBehaviour monobehaviour;

		/** [シングルトン]constructor
		*/
		private Function()
		{

		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** MonoBehaviour。設定。
		*/
		public void SetMonoBehaviour(UnityEngine.MonoBehaviour a_monobehaviour)
		{
			this.monobehaviour = a_monobehaviour;
		}

		/** 戻り値。
		*/
		public enum ReturnType
		{
			/** Continue
			*/
			Continue = 0,

			/** End
			*/
			End,
		}

		/** コルーチンから呼び出し。
		*/
		public void CallFromCoroutine(System.Func<ReturnType> a_function)
		{
			if(this.monobehaviour != null){
				this.monobehaviour.StartCoroutine(Do_Coroutine(a_function));
			}else{
				Tool.Assert(false);
			}
		}

		/** 処理。コルーチン。
		*/
		private System.Collections.IEnumerator Do_Coroutine(System.Func<ReturnType> a_function)
		{
			while(a_function() == ReturnType.Continue){
				yield return null;
			}
			yield break;
		}

		/** コルーチン。開始。
		*/
		public void StartCoroutine(System.Collections.IEnumerator a_coroutine)
		{
			if(this.monobehaviour != null){
				this.monobehaviour.StartCoroutine(a_coroutine);
			}else{
				Tool.Assert(false);
			}
		}
	}
}

