

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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

			return == true : 終了。

		*/
		bool Main();

		/** [Scene_Base]更新。
		*/
		void Unity_Update();

		/** [Scene_Base]更新。
		*/
		void Unity_LateUpdate();

		/** [Scene_Base]削除。
		*/
		void Delete();

		/** [Scene_Base]シーン開始。

			return == true : 開始処理完了。

		*/
		bool SceneStart();

		/** [Scene_Base]シーン終了。

			return == true : 終了処理完了。

		*/
		bool SceneEnd();
	}
}

