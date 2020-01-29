

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。スプライト。
*/


// The private field * is assigned but its value is never used
#if(UNITY_EDITOR)
#pragma warning disable 0414
#endif


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** OnSprite2DMaterialUpdate_CallBackInterface
	*/
	public interface OnSprite2DMaterialUpdate_CallBackInterface
	{
		/** [Fee.Render2D.OnSprite2DMaterialUpdate_CallBackInterface]マテリアルの更新。描画の直前に呼び出される。
		*/
		bool OnSprite2DMaterialUpdate(Sprite2D a_sprite2d,Material_Item a_material_item);
	}
}

