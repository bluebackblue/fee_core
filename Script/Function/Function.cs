

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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
		/** s_monobehaviour
		*/
		static UnityEngine.MonoBehaviour s_monobehaviour;

		/** MonoBehaviour。設定。
		*/
		public static void SetMonoBehaviour(UnityEngine.MonoBehaviour a_monobehaviour)
		{
			s_monobehaviour = a_monobehaviour;
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
		public static void CallFromCoroutine(System.Func<ReturnType> a_function)
		{
			if(s_monobehaviour != null){
				s_monobehaviour.StartCoroutine(Do_Coroutine(a_function));
			}
		}

		/** 処理。コルーチン。
		*/
		private static System.Collections.IEnumerator Do_Coroutine(System.Func<ReturnType> a_function)
		{
			while(a_function() == ReturnType.Continue){
				yield return null;
			}
			yield break;
		}

		/** コルーチン。開始。
		*/
		public static void StartCoroutine(System.Collections.IEnumerator a_coroutine)
		{
			if(s_monobehaviour != null){
				s_monobehaviour.StartCoroutine(a_coroutine);
			}
		}
	}
}

