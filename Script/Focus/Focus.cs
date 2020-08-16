

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フォーカス。
*/


/** Fee.Focus
*/
namespace Fee.Focus
{
	/** Focus
	*/
	public class Focus
	{
		/** [シングルトン]s_instance
		*/
		private static Focus s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Focus();
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
		public static Focus GetInstance()
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

		/** ui_gameobject
		*/
		private UnityEngine.GameObject ui_gameobject;

		/** playerloop_flag
		*/
		private bool playerloop_flag;

		/** [シングルトン]constructor
		*/
		private Focus()
		{
			this.ui_gameobject = null;

			//PlayerLoopType
			this.playerloop_flag = true;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_Focus_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//PlayerLoopType
			this.playerloop_flag = false;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_Focus_Main));
		}

		/** 更新。
		*/
		private void Main()
		{
			if(this.playerloop_flag == true){
				UnityEngine.GameObject t_new_gameobject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
				if(t_new_gameobject != this.ui_gameobject){

					//OnCheckFocusの呼び出し。
					if(this.ui_gameobject != null){
						Fee.Focus.Focus_MonoBehaviour t_focus = this.ui_gameobject.GetComponent<Fee.Focus.Focus_MonoBehaviour>();
						if(t_focus != null){
							t_focus.CallOnFocusCheck();
						}
					}

					this.ui_gameobject = t_new_gameobject;

					//OnCheckFocusの呼び出し。
					if(this.ui_gameobject != null){
						Fee.Focus.Focus_MonoBehaviour t_focus = this.ui_gameobject.GetComponent<Fee.Focus.Focus_MonoBehaviour>();
						if(t_focus != null){
							t_focus.CallOnFocusCheck();
						}
					}
				}
			}
		}
	}
}

