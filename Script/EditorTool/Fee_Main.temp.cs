

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。メイン。
*/


/** define
*/
#define DEF_PLAYERLOOPSYSTEM
#define DEF_RENDER2D
#define DEF_INPUT
#define DEF_UI
#define DEF_SCENE
#define DEF_FILE
#define DEF_FOCUS
#define DEF_FADE
#define DEF_DATA
#define DEF_AUDIO
#define DEF_ASSETBUNDLE
#define DEF_PLATFORM
#define DEF_NETWORK
#define DEF_EVENTPLATE
#define DEF_CRYPT
#define DEF_SOUNDPOOL
#define DEF_TASK
#define DEF_TIME
#define DEF_UNIVRM
#define DEF_FUNCTION
#define DEF_PERFORMANCECOUNTER
#define DEF_VIDEO
#define DEF_MIRROR


/** Fee.EditorTool
*/
#if(true)
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

			//オーディオ。
			#if(DEF_AUDIO)
			{
				Fee.Audio.Audio.GetInstance().SetFocusFlag(a_flag);
			}
			#endif

			//入力。
			#if(DEF_INPUT)
			{
				Fee.Input.Input.GetInstance().SetFocusFlag(a_flag);
			}
			#endif
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
				//順序変更。
				{
					//２Ｄ描画。
					#if(DEF_RENDER2D)
					{
						Fee.Render2D.Config.ReCalcWH();
						Fee.Render2D.Render2D.CreateInstance();
					}
					#endif

					//ファイル。順序変更。
					#if(DEF_FILE)
					{
						Fee.File.Config.LOG_ENABLE = true;
						Fee.File.File.CreateInstance();
					}
					#endif

					//データ。順序変更。
					#if(DEF_DATA)
					{
						Fee.Data.Data.CreateInstance();
					}
					#endif

					//アセットバンドルリスト。順序変更。
					#if(DEF_ASSETBUNDLE)
					{
						Fee.AssetBundleList.AssetBundleList.CreateInstance();
					}
					#endif
				}

				//アセット。
				{
				}

				//アセットバンドルリスト。順序変更。
				{
				}

				//オーディオ。
				#if(DEF_AUDIO)
				{
					Fee.Audio.Audio.CreateInstance();
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
					Fee.Crypt.Crypt.CreateInstance();
				}
				#endif

				//データ。順序変更。
				{
				}

				//削除管理。
				{
				}

				//デプス。
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

				//ファイル。順序変更。
				{
				}

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

				//グラフィック。
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

				//リスト。
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
					Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof( UnityEngine.PlayerLoop.PreUpdate.SendMouseEvents));
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

				//タイム。
				#if(DEF_TIME)
				{
					Fee.Time.Time.CreateInstance();
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
			#if(false)
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
			}

			//ブラー。
			{
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

			//デプス。
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

			//グラフィック。
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

			//リスト。
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

			//タイム。
			{
				Fee.Time.Time.DeleteInstance();
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

		/** RowUpdate
		*/
		private void RowUpdate()
		{
		}

		/** 更新。

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		Update呼び出し前。

		*/
		private void FixedUpdate()
		{
			//ＵＮＩＶＲＭ。
			#if(DEF_UNIVRM)
			{
				Fee.UniVrm.UniVrm.GetInstance().Main();
			}
			#endif
		}

		/** Update

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		シーンレンダリング前。

		*/
		private void Update()
		{
		}

		/** LateUpdate

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		シーンレンダリング前。Update呼び出し後。

		*/
		private void LateUpdate()
		{
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

