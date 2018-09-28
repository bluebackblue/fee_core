using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** test06

	シーン遷移
	フェード

*/
public class test06 : main_base
{
	/** シーンＡ。
	*/
	class SceneA : NScene.Scene_Base
	{
		/** [Scene.Scene_Base]更新。

		戻り値 = true : 終了。

		*/
		public bool Main()
		{
			if(NFade.Fade.GetInstance().IsAnime() == false){
				if(NInput.Mouse.GetInstance().left.down == true){
					Debug.Log("SceneA : request");
					//遷移リクエスト。
					NScene.Scene.GetInstance().SetNextScene(new SceneB());
				}
			}

			return NScene.Scene.GetInstance().IsNextScene();
		}

		/** [Scene.Scene_Base]削除。
		*/
		public void Delete()
		{
			Debug.Log("SceneA : Delete");
		}

		/** [Scene.Scene_Base]シーン開始。

		戻り値 = true : 開始処理完了。

		*/
		public bool SceneStart()
		{
			Debug.Log("SceneA : SceneStart");

			NFade.Fade.GetInstance().FadeIn();
			return true;
		}

		/** [Scene.Scene_Base]シーン終了。

		戻り値 = true : 終了処理完了。

		*/
		public bool SceneEnd()
		{
			Debug.Log("SceneA : SceneEnd");

			NFade.Fade.GetInstance().FadeOut();

			if(NFade.Fade.GetInstance().IsAnime() == false){
				return true;
			}

			return false;
		}
	};

	/** シーンＢ。
	*/
	class SceneB : NScene.Scene_Base
	{
		/** [Scene.Scene_Base]更新。

		戻り値 = true : 終了。

		*/
		public bool Main()
		{
			if(NFade.Fade.GetInstance().IsAnime() == false){
				if(NInput.Mouse.GetInstance().left.down == true){
					//遷移リクエスト。
					Debug.Log("SceneB : request");
					NScene.Scene.GetInstance().SetNextScene(new SceneA());
				}
			}

			return NScene.Scene.GetInstance().IsNextScene();
		}

		/** [Scene.Scene_Base]削除。
		*/
		public void Delete()
		{
			Debug.Log("SceneB : Delete");
		}

		/** [Scene.Scene_Base]シーン開始。

		戻り値 = true : 開始処理完了。

		*/
		public bool SceneStart()
		{
			Debug.Log("SceneB : SceneStart");

			NFade.Fade.GetInstance().FadeIn();
			return true;
		}

		/** [Scene.Scene_Base]シーン終了。

		戻り値 = true : 終了処理完了。

		*/
		public bool SceneEnd()
		{
			Debug.Log("SceneB : SceneEnd");

			NFade.Fade.GetInstance().FadeOut();

			if(NFade.Fade.GetInstance().IsAnime() == false){
				return true;
			}
			return false;
		}
	};

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//フェード。インスタンス作成。
		NFade.Fade.CreateInstance();
		NFade.Fade.GetInstance().SetSpeed(0.05f);
		NFade.Fade.GetInstance().SetColor(0.0f,0.0f,0.0f,1.0f);
		NFade.Fade.GetInstance().SetToColor(0.0f,0.0f,0.0f,1.0f);
		NFade.Fade.GetInstance().SetAnime();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//シーン。インスタンス作成。
		NScene.Scene.CreateInstance();

		//シーンＡ。
		NScene.Scene.GetInstance().SetNextScene(new SceneA());
	}

	/** Update
	*/
	private void Update()
	{
		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.Config.LOG_ENABLE = true;
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//フェード。
		NFade.Fade.GetInstance().Main_PreDraw();
		NFade.Fade.GetInstance().Main();

		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//シーン。
		NScene.Scene.GetInstance().Main();
	}

	/** 削除前。
	*/
	public override bool PreDestroy(bool a_first)
	{
		return true;
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
	}
}

