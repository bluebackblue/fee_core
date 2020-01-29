

/**
* Copyright (c) blueback
* Released under the MIT License
* https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
* @brief ＵＩ。フォーカス。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Focus
	*/
	public class Focus
	{
		/** gameobject
		*/
		private UnityEngine.GameObject gameobject;

		/** constructor
		*/
		public Focus()
		{
			this.gameobject = null;
		}

		/** 更新。
		*/
		public void Main()
		{
			UnityEngine.GameObject t_new_gameobject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

			if(t_new_gameobject != this.gameobject){

				//OnCheckFocusの呼び出し。
				if(this.gameobject != null){
					Fee.Ui.Focus_MonoBehaviour t_focus = this.gameobject.GetComponent<Fee.Ui.Focus_MonoBehaviour>();
					if(t_focus != null){
						t_focus.CallOnFocusCheck();
					}
				}

				this.gameobject = t_new_gameobject;

				//OnCheckFocusの呼び出し。
				if(this.gameobject != null){
					Fee.Ui.Focus_MonoBehaviour t_focus = this.gameobject.GetComponent<Fee.Ui.Focus_MonoBehaviour>();
					if(t_focus != null){
						t_focus.CallOnFocusCheck();
					}
				}
			}
		}
	}
}


