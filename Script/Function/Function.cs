

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

		/** playerloop_flag
		*/
		private bool playerloop_flag;

		/** monobehaviour
		*/
		private UnityEngine.MonoBehaviour monobehaviour;

		/** rowupdate
		*/
		private RowUpdate rowupdate;

		/** [シングルトン]constructor
		*/
		private Function()
		{
			this.rowupdate = new RowUpdate();

			//playerloop_flag
			this.playerloop_flag = true;

			//PlayerLoopSystem
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ROWUPDATE_ADDTYPE,Config.PLAYERLOOP_ROWUPDATE_TARGETTYPE,typeof(PlayerLoopSystemType.Fee_Function_RowUpdate),this.RowUpdate);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//playerloop_flag
			this.playerloop_flag = false;

			//rowupdate
			this.rowupdate.Delete();

			//PlayerLoopSystem
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopSystemType.Fee_Function_RowUpdate));
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
			try{
				if(this.playerloop_flag == true){
					this.rowupdate.Main();
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** MonoBehaviour。設定。
		*/
		public void SetMonoBehaviour(UnityEngine.MonoBehaviour a_monobehaviour)
		{
			//monobehaviour
			this.monobehaviour = a_monobehaviour;
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

		/** SetRowUpdate
		*/
		public void SetRowUpdate(RowUpdateType a_callback)
		{
			this.rowupdate.SetCallBack(a_callback);
		}

		/** UnSetRowUpdate
		*/
		public void UnSetRowUpdate(RowUpdateType a_callback)
		{
			this.rowupdate.UnSetCallBack(a_callback);
		}
	}
}

