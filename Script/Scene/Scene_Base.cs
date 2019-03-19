

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シーン。ベース。
*/


/** Fee.Scene
*/
namespace Fee.Scene
{
	/** Scene_Base
	*/
	public interface Scene_Base
	{
		/** [Scene_Base]更新。

		戻り値 = true : 終了。

		*/
		bool Main();

		/** [Scene_Base]更新。
		*/
		void Unity_Update(float a_delta);

		/** [Scene_Base]更新。
		*/
		void Unity_LastUpdate();

		/** [Scene_Base]削除。
		*/
		void Delete();

		/** [Scene_Base]シーン開始。

		戻り値 = true : 開始処理完了。

		*/
		bool SceneStart();

		/** [Scene_Base]シーン終了。

		戻り値 = true : 終了処理完了。

		*/
		bool SceneEnd();
	}
}

