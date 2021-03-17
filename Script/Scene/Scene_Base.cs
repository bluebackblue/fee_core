

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
		/** [Scene_Base]ロック。
		*/
		void Lock();

		/** [Scene_Base]更新。

			return == true : 終了。

		*/
		bool Main();

		/** [Scene_Base]更新。
		*/
		void UnityUpdate();

		/** [Scene_Base]更新。
		*/
		void UnityLateUpdate();

		/** [Scene_Base]更新。
		*/
		void UnityFixedUpdate();

		/** [Scene_Base]削除。
		*/
		void Delete();

		/** [Scene_Base]シーン開始。

			return == true : 開始処理完了。

		*/
		bool SceneStart(bool a_first);

		/** [Scene_Base]シーン終了。

			return == true : 終了処理完了。

		*/
		bool SceneEnd(bool a_first);
	}
}

