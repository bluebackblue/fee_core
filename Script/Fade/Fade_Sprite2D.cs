

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief フェード。スプライト。
*/


/** Fee.Fade
*/
namespace Fee.Fade
{
	/** Fade_Sprite2D
	*/
	public class Fade_Sprite2D : Fee.Render2D.Sprite2D
	{
		/** constructor。
		*/
		public Fade_Sprite2D(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
			:
			base(a_deleter,a_drawpriority)
		{
		}

		/** マテリアルを更新する。

		return = true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public override bool UpdateMaterial(ref UnityEngine.Material a_material)
		{
			//テクスチャ設定。
			a_material.mainTexture = UnityEngine.Texture2D.whiteTexture;

			//SetPass要求。
			return true;
		}
	}
}

