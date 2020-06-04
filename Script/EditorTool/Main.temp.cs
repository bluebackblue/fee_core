

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。メイン。
*/


/** define
*/
//#define DEF_PLAYERLOOPSYSTEM
//#define DEF_RENDER2D
//#define DEF_INPUT
//#define DEF_UI
//#define DEF_SCENE
//#define DEF_FILE
//#define DEF_FOCUS
//#define DEF_FADE
//#define DEF_DATA
//#define DEF_AUDIO
//#define DEF_ASSETBUNDLE
//#define DEF_PLATFORM
//#define DEF_NETWORK
//#define DEF_BLOOM
//#define DEF_EVENTPLATE
//#define DEF_CRYPT
//#define DEF_SOUNDPOOL
//#define DEF_TASK
//#define DEF_UNIVRM
//#define DEF_BLUR
//#define DEF_DEPTH
//#define DEF_FUNCTION
//#define DEF_PERFORMANCECOUNTER
//#define DEF_VIDEO
//#define DEF_MIRROR


/** Fee.EditorTool
*/
#if(USE_DEF_FEE_TEMP)
namespace Fee.EditorTool
{
	/** Main
	*/
	public class Main : UnityEngine.MonoBehaviour
	{
		/** s_forced_quit
		*/
		private static bool s_forced_quit = false;

		/** s_is_focus
		*/
		private static bool s_is_focus = false;

		/** アプリ起動時。
		*/
		[UnityEngine.RuntimeInitializeOnLoadMethod]
		private static void AppInitialize()
		{
		}

		/** アプリ終了時。
		*/
		void OnApplicationQuit()
		{
			s_forced_quit = true;
		}

		/** アプリフォーカス変更時。
		*/
		void OnApplicationFocus(bool a_flag)
		{
			s_is_focus = a_flag;
		}

		/** IsFocus
		*/
		public static bool IsFocus()
		{
			return s_is_focus;
		}

		/** s_instance
		*/
		private static Main s_instance;

		/** GetInstance
		*/
		public static Main GetInstance()
		{
			return s_instance;
		}

		/** scene_update_wait
		*/
		#if(DEF_SCENE)
		private int scene_update_wait;
		#endif

		/** Start
		*/
		void Start()
		{
			//s_instance
			s_instance = this;

			//ライブラリ停止。
			this.DeleteLibInstance();

			//初期化。
			{
				//scene_update_wait
				#if(DEF_SCENE)
				{
					this.scene_update_wait = 3;
				}
				#endif

				//順序変更。
				{
					//２Ｄ描画。
					#if(DEF_RENDER2D)
					{
						Fee.Render2D.Config.ReCalcWH();
						Fee.Render2D.Render2D.CreateInstance();
					}
					#endif
				}

				//アセット。
				{
				}

				//アセットバンドルリスト。
				#if(DEF_ASSETBUNDLE)
				{
					Fee.AssetBundleList.AssetBundleList.CreateInstance();
				}
				#endif

				//オーディオ。
				#if(DEF_AUDIO)
				{
					Fee.Audio.Audio.CreateInstance();
				}
				#endif

				//ブルーム。
				#if(DEF_BLOOM)
				{
					Fee.Bloom.Bloom.CreateInstance();
				}
				#endif

				//ブラー。
				#if(DEF_BLUR)
				{
					Fee.Blur.Blur.CreateInstance();
				}
				#endif

				//暗号。
				#if(DEF_CRYPT)
				{
					Fee.Crypt.Crypt.CreateInstance();
				}
				#endif

				//データ。
				#if(DEF_DATA)
				{
					Fee.Data.Data.CreateInstance();
				}
				#endif

				//削除管理。
				{
				}

				//深度。
				#if(DEF_DEPTH)
				{
					Fee.Depth.Depth.CreateInstance();
				}
				#endif

				//ダイクストラ法。
				{
				}

				//ディレクトリ。
				{
				}

				//エディターツール。
				{
				}

				//イベントプレート。
				#if(DEF_EVENTPLATE)
				{
					Fee.EventPlate.EventPlate.CreateInstance();
				}
				#endif

				//エクセル。
				{
				}

				//フェード。
				#if(DEF_FADE)
				{
					Fee.Fade.Fade.CreateInstance();
					Fee.Fade.Fade.GetInstance().SetSpeed(0.05f);
					Fee.Fade.Fade.GetInstance().SetColor(0.0f,0.0f,0.0f,1.0f);
					Fee.Fade.Fade.GetInstance().SetToColor(0.0f,0.0f,0.0f,1.0f);
					Fee.Fade.Fade.GetInstance().SetAnime();
				}
				#endif

				//ファイル。
				#if(DEF_FILE)
				{
					Fee.File.Config.LOG_ENABLE = true;
					Fee.File.File.CreateInstance();
				}
				#endif

				//フォーカス。
				#if(DEF_FOCUS)
				{
					Fee.Focus.Focus.CreateInstance();
				}
				#endif

				//関数呼び出し。
				#if(DEF_FUNCTION)
				{
					Fee.Function.Function.CreateInstance();
					Fee.Function.Function.GetInstance().SetMonoBehaviour(this);
					Fee.Function.Function.GetInstance().SetRowUpdate(this.RowUpdate);
				}
				#endif

				//ジオメトリ。
				{
				}

				//入力。
				#if(DEF_INPUT)
				{
					Fee.Input.Input.CreateInstance(true,true,true,true);
				}
				#endif

				//インスタンス作成。
				{
				}

				//ＪＳＯＮ。
				{
				}

				//ＪＳＯＮシート。
				{
				}

				//キー。
				{
				}

				//マテリアル。
				{
				}

				//ＭＤ５。
				{
				}

				//ミラー。
				#if(DEF_MIRROR)
				{
					Fee.Mirror.Mirror.CreateInstance();
				}
				#endif

				//モデル。
				{
				}

				//ネットワーク。
				#if(DEF_NETWORK)
				{
					Fee.Network.Config.LOG_ENABLE = true;
					Fee.Network.Network.CreateInstance();
				}
				#endif

				//パターン。
				{
				}

				//パーセプトロン。
				{
				}

				//パフォーマンスカウンター。
				#if(DEF_PERFORMANCECOUNTER)
				{
					Fee.PerformanceCounter.PerformanceCounter.CreateInstance();
				}
				#endif

				//プラットフォーム。
				#if(DEF_PLATFORM)
				{
					Fee.Platform.Platform.CreateInstance();
				}
				#endif

				//プレイヤーループシステム。
				#if(DEF_PLAYERLOOPSYSTEM)
				{
					Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
					Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof( UnityEngine.Experimental.PlayerLoop.PreUpdate.SendMouseEvents));
				}
				#endif

				//プール。
				{
				}
				
				//リフレクションツール。
				{
				}

				//２Ｄ描画。
				{
				}

				//リスロー。
				{
				}

				//シーン。
				#if(DEF_SCENE)
				{
					Fee.Scene.Scene.CreateInstance();
				}
				#endif

				//サウンドプール。
				#if(DEF_SOUNDPOOL)
				{
					Fee.SoundPool.SoundPool.CreateInstance();
				}
				#endif

				//文字コンバート。
				{
				}

				//タスク。
				#if(DEF_TASK)
				{
					Fee.TaskW.TaskW.CreateInstance();
				}
				#endif

				//ＵＩ。
				#if(DEF_UI)
				{
					Fee.Ui.Ui.CreateInstance();
				}
				#endif

				//ＵＮＩＶＲＭ。
				#if(DEF_UNIVRM)
				{
					Fee.UniVrm.UniVrm.CreateInstance();
				}
				#endif

				//ビデオ。
				#if(DEF_VIDEO)
				{
					Fee.Video.Video.CreateInstance();
				}
				#endif
			}

			//■シーン開始。
			#if(true)
			{
				Fee.Scene.Scene.GetInstance().SetNextScene(new Game.Scene.Scene_Init());
			}
			#endif
		}

		/** ライブラリ停止。
		*/
		public void DeleteLibInstance()
		{
			//アセット。
			{
			}

			//アセットバンドルリスト。
			{
				Fee.AssetBundleList.AssetBundleList.DeleteInstance();
			}

			//オーディオ。
			{
				Fee.Audio.Audio.DeleteInstance();
			}

			//ブルーム。
			{
				Fee.Bloom.Bloom.DeleteInstance();
			}

			//ブラー。
			{
				Fee.Blur.Blur.DeleteInstance();
			}

			//暗号。
			{
				Fee.Crypt.Crypt.DeleteInstance();
			}

			//データ。
			{
				Fee.Data.Data.DeleteInstance();
			}

			//削除管理。
			{
			}

			//深度。
			{
				Fee.Depth.Depth.DeleteInstance();
			}

			//ダイクストラ法。
			{
			}

			//ディレクトリ。
			{
			}

			//エディターツール。
			{
			}

			//イベントプレート。
			{
				Fee.EventPlate.EventPlate.DeleteInstance();
			}

			//エクセル。
			{
			}

			//フェード。
			{
				Fee.Fade.Fade.DeleteInstance();
			}

			//ファイル。
			{
				Fee.File.File.DeleteInstance();
			}

			//フォーカス。
			{
				Fee.Focus.Focus.DeleteInstance();
			}

			//関数呼び出し。
			{
				Fee.Function.Function.DeleteInstance();
			}

			//ジオメトリ。
			{
			}

			//入力。
			{
				Fee.Input.Input.DeleteInstance();
			}

			//インスタンス作成。
			{
			}

			//ＪＳＯＮ。
			{
			}

			//ＪＳＯＮシート。
			{
			}

			//キー。
			{
			}

			//マテリアル。
			{
			}

			//ＭＤ５。
			{
			}

			//ミラー。
			{
				Fee.Mirror.Mirror.DeleteInstance();
			}

			//モデル。
			{
			}

			//ネットワーク。
			{
				Fee.Network.Network.DeleteInstance();
			}

			//パターン。
			{
			}

			//パーセプトロン。
			{
			}

			//パフォーマンスカウンター。
			{
				Fee.PerformanceCounter.PerformanceCounter.DeleteInstance();
			}

			//プラットフォーム。
			{
				Fee.Platform.Platform.DeleteInstance();
			}

			//プレイヤーループシステム。
			{
				Fee.PlayerLoopSystem.PlayerLoopSystem.DeleteInstance();
			}

			//プール。
			{
			}

			//リフレクションツール。
			{
			}

			//２Ｄ描画。
			{
				Fee.Render2D.Render2D.DeleteInstance();
			}

			//リスロー。
			{
			}

			//シーン。
			{
				Fee.Scene.Scene.DeleteInstance();
			}

			//サウンドプール。
			{
				Fee.SoundPool.SoundPool.DeleteInstance();
			}

			//文字列コンバート。
			{
			}

			//タスク。
			{
				Fee.TaskW.TaskW.DeleteInstance();
			}

			//ＵＩ。
			{
				Fee.Ui.Ui.DeleteInstance();
			}

			//ＵＮＩＶＲＭ。
			{
				Fee.UniVrm.UniVrm.DeleteInstance();
			}

			//ビデオ。
			{
				Fee.Video.Video.DeleteInstance();
			}
		}

		/** 更新。

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		Update呼び出し前。

		*/
		private void FixedUpdate()
		{
			try{

				//順序変更。
				{
					//２Ｄ描画。
					#if(DEF_RENDER2D)
					{
						Fee.Render2D.Render2D.GetInstance().Main_Before();
					}
					#endif

					//イベントプレート。
					#if(DEF_EVENTPLATE)
					{
						Fee.EventPlate.EventPlate.GetInstance().Main();
					}
					#endif

					//ＵＩ。
					#if(DEF_UI)
					{
						Fee.Ui.Ui.GetInstance().Main();
					}
					#endif

					//シーン。
					#if(DEF_SCENE)
					{
						if(this.scene_update_wait <= 0){
							Fee.Scene.Scene.GetInstance().Main();
						}
					}
					#endif
				}

				//アセット。
				{
				}

				//アセットバンドルリスト。
				#if(DEF_ASSETBUNDLE)
				{
					Fee.AssetBundleList.AssetBundleList.GetInstance().Main();
				}
				#endif

				//オーディオ。
				#if(DEF_AUDIO)
				{
					Fee.Audio.Audio.GetInstance().Main(s_is_focus);
				}
				#endif

				//ブルーム。
				{
				}

				//ブラー。
				{
				}

				//暗号。
				#if(DEF_CRYPT)
				{
					Fee.Crypt.Crypt.GetInstance().Main();
				}
				#endif

				//データ。
				#if(DEF_DATA)
				{
					Fee.Data.Data.GetInstance().Main();
				}
				#endif

				//削除管理。
				{
				}

				//深度。
				{
				}

				//ダイクストラ法。
				{
				}

				//ディレクトリ。
				{
				}

				//エディターツール。
				{
				}

				//イベントプレート。順序変更。
				{
				}

				//エクセル。
				{
				}

				//フェード。
				#if(DEF_FADE)
				{
					Fee.Fade.Fade.GetInstance().Main();
				}
				#endif

				//ファイル。
				#if(DEF_FILE)
				{
					Fee.File.File.GetInstance().Main();
				}
				#endif

				//フォーカス。
				#if(DEF_FOCUS)
				{
					Fee.Focus.Focus.GetInstance().Main();
				}
				#endif

				//関数呼び出し。
				{
				}

				//ジオメトリ。
				{
				}

				//入力。
				{
				}

				//インスタンス作成。
				{
				}

				//ＪＳＯＮ。
				{
				}

				//ＪＳＯＮシート。
				{
				}

				//キー。
				{
				}

				//マテリアル。
				{
				}

				//ＭＤ５。
				{
				}

				//ミラー。
				{
				}

				//モデル。
				{
				}

				//ネットワーク。
				#if(DEF_NETWORK)
				{
					Fee.Network.Network.GetInstance().Main();
				}
				#endif

				//パターン。
				{
				}

				//パーセプトロン。
				{
				}

				//パフォーマンスカウンター。
				{
				}

				//プラットフォーム。
				{
				}

				//プレイヤーループシステム。
				{
				}

				//プール。
				{
				}

				//リフレクションツール。
				{
				}

				//２Ｄ描画。順序変更。
				{
				}

				//リスロー。
				{
				}

				//シーン。順序変更。
				{
				}

				//サウンドプール。
				#if(DEF_SOUNDPOOL)
				{
					Fee.SoundPool.SoundPool.GetInstance().Main();
				}
				#endif

				//文字列コンバート。
				{
				}

				//タスク。
				{
				}

				//ＵＩ。順序変更。
				{
				}

				//ＵＮＩＶＲＭ。
				#if(DEF_UNIVRM)
				{
					Fee.UniVrm.UniVrm.GetInstance().Main();
				}
				#endif

				//ビデオ。
				{
				}

				//順序変更。
				{
					//２Ｄ描画。
					#if(DEF_RENDER2D)
					{
						Fee.Render2D.Render2D.GetInstance().Main_After();
					}
					#endif
				}

			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.StackTrace + "\n\n" + t_exception.Message);
			}
		}

		/** Update

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		シーンレンダリング前。

		*/
		private void Update()
		{
			//シーン。
			#if(DEF_SCENE)
			{
				if(this.scene_update_wait <= 0){
					Fee.Scene.Scene.GetInstance().Unity_Update(UnityEngine.Time.deltaTime);
				}else{
					this.scene_update_wait--;
				}
			}
			#endif

			//２Ｄ描画。
			#if(DEF_RENDER2D)
			{
				Fee.Render2D.Render2D.GetInstance().Main_PreDraw();
			}
			#endif
		}

		/** LateUpdate

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		シーンレンダリング前。Update呼び出し後。

		*/
		private void LateUpdate()
		{
			//シーン。
			#if(DEF_SCENE)
			{
				if(this.scene_update_wait <= 0){
					Fee.Scene.Scene.GetInstance().Unity_LateUpdate(UnityEngine.Time.deltaTime);
				}
			}
			#endif
		}

		/** シーン遷移。
		*/
		private void OnDestroy()
		{
			if(s_forced_quit == true){
				//アプリ終了による強制終了。
			}else{
				//ライブラリ停止。
				this.DeleteLibInstance();

				//s_instance
				s_instance = null;
			}
		}
	}
}
#endif

