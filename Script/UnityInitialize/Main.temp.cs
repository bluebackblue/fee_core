

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＴＹ初期化。メイン。
*/


/** Fee.UnityInitialize
*/
#if(USE_DEF_FEE_TEMP)
namespace Fee.UnityInitialize
{
	/** Main
	*/
	public class Main : UnityEngine.MonoBehaviour
	{
		/** forced_quit
		*/
		private static bool s_forced_quit = false;

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

			//インスタンス作成。
			{
				//２Ｄ描画。
				#if(false)
				{
					Fee.Render2D.Config.MAX_LAYER = Setting.RENDER_LAYER_INDEX_MAX;
					Fee.Render2D.Render2D.CreateInstance();
				}
				#endif

				//オーディオ。
				#if(false)
				{
					Fee.Audio.Audio.CreateInstance();
				}
				#endif

				//ブルーム。
				#if(false)
				{
					Fee.Bloom.Bloom.CreateInstance();
				}
				#endif

				//ブラー。
				#if(false)
				{
					Fee.Blur.Blur.CreateInstance();
				}
				#endif

				//暗号。
				#if(false)
				{
					Fee.Crypt.Crypt.CreateInstance();
				}
				#endif

				//削除管理。
				{
				}

				//ダイクストラ法。
				{
				}

				//ディレクトリ。
				{
				}

				//イベントプレート。
				#if(false)
				{
					Fee.EventPlate.EventPlate.CreateInstance();
				}
				#endif

				//フェード。
				#if(false)
				{
					Fee.Fade.Fade.CreateInstance();
					Fee.Fade.Fade.GetInstance().SetSpeed(0.05f);
					Fee.Fade.Fade.GetInstance().SetColor(0.0f,0.0f,0.0f,1.0f);
					Fee.Fade.Fade.GetInstance().SetToColor(0.0f,0.0f,0.0f,1.0f);
					Fee.Fade.Fade.GetInstance().SetAnime();
				}
				#endif

				//ファイル。
				#if(false)
				{
					Fee.File.File.CreateInstance();
				}
				#endif

				//関数呼び出し。
				#if(true)
				{
					Fee.Function.Function.SetMonoBehaviour(this);
				}
				#endif

				//入力。
				{
					#if(false)
					{
						Fee.Input.Mouse.CreateInstance();
					}
					#endif

					//キー。
					#if(false)
					{
						Fee.Input.Key.CreateInstance();
					}
					#endif

					//パッド。
					#if(false)
					{
						Fee.Input.Pad.CreateInstance();
					}
					#endif
				}

				//インスタンス作成。
				{
				}

				//ＪＳＯＮ。
				{
				}

				//アセットバンドル作成。
				{
				}

				//モデル。
				{
				}

				//ネットワーク。
				#if(false)
				{
					Fee.Network.Network.CreateInstance();
				}
				#endif

				//パフォーマンスカウンター。
				#if(false)
				{
					Fee.PerformanceCounter.PerformanceCounter.CreateInstance();
				}
				#endif

				//プラットフォーム。
				{
				}

				//シーン。
				#if(false)
				{
					Fee.Scene.Scene.CreateInstance();
				}
				#endif

				//タスク。
				#if(false)
				{
					Fee.TaskW.TaskW.CreateInstance();
				}
				#endif

				//ＵＩ。
				#if(false)
				{
					Fee.Ui.Ui.CreateInstance();
				}
				#endif

				//ＵＮＩＴＹ初期化。
				{
				}

				//ＵＮＩＶＲＭ。
				#if(false)
				{
					Fee.UniVrm.UniVrm.CreateInstance();
				}
				#endif
			}
		}

		/** ライブラリ停止。
		*/
		public void DeleteLibInstance()
		{
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

			//削除管理。
			{
			}

			//ダイクストラ法。
			{
			}

			//ディレクトリ。
			{
			}

			//イベントプレート。
			{
				Fee.EventPlate.EventPlate.DeleteInstance();
			}

			//フェード。
			{
				Fee.Fade.Fade.DeleteInstance();
			}

			//ファイル。
			{
				Fee.File.File.DeleteInstance();
			}

			//入力。
			{
				//マスウ。
				Fee.Input.Mouse.DeleteInstance();
	
				//キー。
				Fee.Input.Key.DeleteInstance();
	
				//パッド。
				Fee.Input.Pad.DeleteInstance();
			}

			//インスタンス作成。
			{
			}

			//ＪＳＯＮ。
			{
			}

			//アセットバンドル作成。
			{
			}

			//モデル。
			{		
			}

			//ネットワーク。
			{
				Fee.Network.Network.DeleteInstance();
			}

			//パフォーマンスカウンター。
			{
				Fee.PerformanceCounter.PerformanceCounter.DeleteInstance();
			}

			//プラットフォーム。
			{
			}

			//２Ｄ描画。
			{
				Fee.Render2D.Render2D.DeleteInstance();
			}

			//シーン。
			{
				Fee.Scene.Scene.DeleteInstance();
			}

			//タスク。
			{
				Fee.TaskW.TaskW.DeleteInstance();
			}

			//ＵＩ。
			{
				Fee.Ui.Ui.DeleteInstance();
			}

			//ＵＮＩＴＹ初期化。
			{
			}

			//ＵＮＩＶＲＭ。
			{
				Fee.UniVrm.UniVrm.DeleteInstance();
			}
		}

		/** 更新。

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		Update呼び出し前。

		*/
		private void FixedUpdate()
		{
			try{
				{
					//入力。
					{
						#if(false)
						Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());
						#endif

						//キー。
						#if(false)
						Fee.Input.Key.GetInstance().Main();
						#endif

						//パッド。
						#if(false)
						Fee.Input.Pad.GetInstance().Main();
						#endif
					}

					//イベントプレート。
					#if(false)
					{
						Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);
					}
					#endif

					//ＵＩ。
					#if(false)
					{
						Fee.Ui.Ui.GetInstance().Main();
					}
					#endif

					//シーン。
					#if(false)
					{
						Fee.Scene.Scene.GetInstance().Main();
					}
					#endif
				}

				//オーディオ。
				{
				}

				//ブルーム。
				{
				}

				//ブラー。
				{
				}

				//暗号。
				#if(false)
				{
					Fee.Crypt.Crypt.GetInstance().Main();
				}
				#endif

				//削除管理。
				{
				}

				//ダイクストラ法。
				{
				}

				//ディレクトリ。
				{
				}

				//フェード。
				#if(false)
				{
					Fee.Fade.Fade.GetInstance().Main_PreDraw();
					Fee.Fade.Fade.GetInstance().Main();
				}
				#endif

				//ファイル。
				#if(false)
				{
					Fee.File.File.GetInstance().Main();
				}
				#endif

				//インスタンス作成。
				{
				}

				//ＪＳＯＮ。
				{
				}

				//アセットバンドル作成。
				{
				}

				//モデル。
				{
				}

				//ネットワーク。
				#if(false)
				{
					Fee.Network.Network.GetInstance().Main();
				}
				#endif

				//パフォーマンスカウンター。
				{
				}

				//プラットフォーム。
				{
				}

				//２Ｄ描画。
				{
				}

				//タスク。
				{
				}

				//ＵＮＩＴＹ初期化。
				{
				}

				//ＵＮＩＶＲＭ。
				#if(false)
				{
					Fee.UniVrm.UniVrm.GetInstance().Main();
				}
				#endif

			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** Update

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		シーンレンダリング前。

		*/
		private void Update()
		{
			//シーン。
			Fee.Scene.Scene.GetInstance().Unity_Update(UnityEngine.Time.deltaTime);
		}

		/** LastUpdate

		https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html
		
		シーンレンダリング前。Update呼び出し後。

		*/
		private void LastUpdate()
		{
			//シーン。
			Fee.Scene.Scene.GetInstance().Unity_Update(UnityEngine.Time.deltaTime);
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

