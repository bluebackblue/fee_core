

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ウィンドウ。コールバック。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** OnWindowCallBack_Base
	*/
	public interface OnWindowCallBack_Base
	{
		/** [Fee.Ui.OnWindowCallBack_Base]レイヤーインデックス変更。
		*/
		void OnChangeLayerIndex(int a_layerindex);

		/** [Fee.Ui.OnWindowCallBack_Base]矩形変更。
		*/
		void OnChangeRect(ref Fee.Render2D.Rect2D_R<int> a_rect);

		/** [Fee.Ui.OnWindowCallBack_Base]矩形変更。
		*/
		void OnChangeXY(int a_x,int a_y);
	}
}

