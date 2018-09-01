using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シーン。ベース。
*/


/** NScene
*/
namespace NScene
{
	/** Scene_Base
	*/
	public interface Scene_Base
	{
		/** 更新。

		戻り値 = true : 終了。

		*/
		bool Main();

		/** 削除。
		*/
		void Delete();

		/** シーン開始。

		戻り値 = true : 開始処理完了。

		*/
		bool SceneStart();

		/** シーン終了。

		戻り値 = true : 終了処理完了。

		*/
		bool SceneEnd();
	}
}

