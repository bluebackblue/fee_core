

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
	/** UnityOnRenderImage_CallBackInterface
	*/
	public interface UnityOnRenderImage_CallBackInterface<T>
	{
		/** [Fee.Function.UnityOnRenderImage_CallBackInterface]UnityOnRenderImage
		*/
		void UnityOnRenderImage(T a_id,UnityEngine.RenderTexture a_source,UnityEngine.RenderTexture a_dest);
	}
}

