

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。コールバックインターフェイス。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** OnWindow_CallBackInterface
	*/
	public interface OnWindow_CallBackInterface
	{
		/** [Fee.Ui.OnWindow_CallBackInterface]レイヤーインデックス変更。
		*/
		void OnWindowChangeLayerIndex(int a_layerindex);

		/** [Fee.Ui.OnWindow_CallBackInterface]矩形変更。
		*/
		void OnWindowChangeRect(ref Fee.Render2D.Rect2D_R<int> a_rect);

		/** [Fee.Ui.OnWindow_CallBackInterface]矩形変更。
		*/
		void OnWindowChangeXY(int a_x,int a_y);
	}
}

