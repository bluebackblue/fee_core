using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief フェード。スプライト。
*/


/** NFade
*/
namespace NFade
{
	/** Fade_Sprite2D
	*/
	public class Fade_Sprite2D : NRender2D.Sprite2D
	{
		/** constructor。
		*/
		public Fade_Sprite2D(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority)
			:
			base(a_deleter,a_state,a_drawpriority)
		{
		}

		/** マテリアルを更新する。

		return = true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public override bool UpdateMaterial(ref Material a_material)
		{
			//テクスチャ設定。
			a_material.mainTexture = Texture2D.whiteTexture;

			//SetPass要求。
			return true;
		}
	}
}

