

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フェード。スプライト。
*/


/** Fee.Fade
*/
namespace Fee.Fade
{
	/** Sprite2D_Fade
	*/
	public class Sprite2D_Fade : Fee.Render2D.Sprite2D_Mapping , Fee.Render2D.OnSprite2DMaterialUpdate_CallBackInterface
	{
		/** constructor
		*/
		public Sprite2D_Fade(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
			:
			base(Fee.Render2D.Render2D.GetInstance().Sprite2D_PoolNew(a_deleter,a_drawpriority))
		{
			this.sprite.SetMaterialType(Fee.Render2D.Config.MaterialType.Simple);
			this.sprite.SetOnSprite2DMaterialUpdate(this);
		}

		/** [Fee.Render2D.OnSprite2DMaterialUpdate_CallBackInterface]マテリアル更新。
		*/
		public bool OnSprite2DMaterialUpdate(Fee.Render2D.Sprite2D a_sprite2d,Fee.Render2D.Material_Item a_material_item)
		{
			bool t_setpass = true;

			//メインテクスチャー設定。
			a_material_item.SetProperty_MainTexture(UnityEngine.Texture2D.whiteTexture);

			//SetPass要求。
			return t_setpass;
		}
	}
}

