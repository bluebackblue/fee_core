

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。描画前処理。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** MonoBehaviour_PreDraw
	*/
	public class MonoBehaviour_PreDraw : UnityEngine.MonoBehaviour
	{
		/** 描画前処理。
		*/
		private void Update()
		{
			try{
				if(Render2D.GetInstance() != null){
					Render2D.GetInstance().PreDraw();
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

