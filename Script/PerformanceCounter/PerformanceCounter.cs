

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パフォーマンスカウンター。コンフィグ。
*/


/** Fee.PerformanceCounter

	https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html

*/
namespace Fee.PerformanceCounter
{
	/** PerformanceCounter
	*/
	public class PerformanceCounter
	{
		/** [シングルトン]s_instance
		*/
		private static PerformanceCounter s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new PerformanceCounter();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static PerformanceCounter GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** camera
		*/
		private UnityEngine.GameObject camera_first_gameobject;
		private UnityEngine.GameObject camera_last_gameobject;

		/** フレームデータ。
		*/
		private FrameData framedata;

		/** フレーム終了待ち。
		*/
		private UnityEngine.WaitForEndOfFrame wait_for_endframe;

		/** delete_request
		*/
		private bool delete_request;

		/** material
		*/
		private UnityEngine.Material material;

		/** [シングルトン]constructor
		*/
		private PerformanceCounter()
		{
			//マテリアル。
			this.material = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_PATH);

			//フレームデータ。
			this.framedata = new FrameData();

			//フレーム終了待ち。
			this.wait_for_endframe = new UnityEngine.WaitForEndOfFrame();

			//delete_request
			this.delete_request = false;

			//カメラ。
			{
				//最初。
				this.camera_first_gameobject = new UnityEngine.GameObject("camera_first");
				UnityEngine.GameObject.DontDestroyOnLoad(this.camera_first_gameobject);
				UnityEngine.Camera t_camera_first_camera = this.camera_first_gameobject.AddComponent<UnityEngine.Camera>();
				Camera_First_MonoBehaviour t_camera_first_monobehaviour = this.camera_first_gameobject.AddComponent<Camera_First_MonoBehaviour>();
				t_camera_first_camera.Reset();
				t_camera_first_camera.cullingMask = 0;
				t_camera_first_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
				t_camera_first_camera.depth = Config.CAMERADEPTH_FIRST;

				//最後。
				this.camera_last_gameobject = new UnityEngine.GameObject("camera_last");
				UnityEngine.GameObject.DontDestroyOnLoad(this.camera_last_gameobject);
				UnityEngine.Camera t_camera_last_camera = this.camera_last_gameobject.AddComponent<UnityEngine.Camera>();
				this.camera_last_gameobject.AddComponent<Camera_Last_MonoBehaviour>();
				t_camera_last_camera.Reset();
				t_camera_last_camera.cullingMask = 0;
				t_camera_last_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
				t_camera_last_camera.depth = Config.CAMERADEPTH_LAST;

				//StartCoroutine
				t_camera_first_monobehaviour.StartCoroutine(this.Unity_Start());
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.delete_request = true;

			if(this.camera_first_gameobject != null){
				UnityEngine.GameObject.DestroyImmediate(this.camera_first_gameobject);
				this.camera_first_gameobject = null;
			}

			if(this.camera_last_gameobject != null){
				UnityEngine.GameObject.DestroyImmediate(this.camera_last_gameobject);
				this.camera_last_gameobject = null;
			}
		}

		/** フレームデータ。取得。
		*/
		public FrameData GetFrameData()
		{
			return this.framedata;
		}

		/** Unity_Start
		*/
		public System.Collections.IEnumerator Unity_Start()
		{
			while(this.delete_request == false){
				//フレーム終了待ち。
				yield return this.wait_for_endframe;
				
				//次のフレーム。
				this.framedata.NetFrame();
			}
		}

		/** Unity_FixedUpdate_First
		*/
		public void Unity_FixedUpdate_First()
		{
			this.framedata.fixedupdate_first = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_FixedUpdate_Last
		*/
		public void Unity_FixedUpdate_Last()
		{
			this.framedata.fixedupdate_last = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_Update_First
		*/
		public void Unity_Update_First()
		{
			this.framedata.update_first = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_Update_Last
		*/
		public void Unity_Update_Last()
		{
			this.framedata.update_last = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_LateUpdate_First
		*/
		public void Unity_LateUpdate_First()
		{
			this.framedata.lateupdate_first = UnityEngine.Time.realtimeSinceStartup;
		}
		/** Unity_LateUpdate_Last
		*/
		public void Unity_LateUpdate_Last()
		{
			this.framedata.lateupdate_last = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_Render_First
		*/
		public void Unity_Render_First()
		{
			this.framedata.render_first = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_Render_Last
		*/
		public void Unity_Render_Last()
		{
			this.framedata.render_last = UnityEngine.Time.realtimeSinceStartup;
		}

		/** 描画。
		*/
		public void Draw()
		{
			if(Fee.Graphic.Gl.PushMatrix() == true){
				if(Fee.Graphic.Gl.LoadOrtho() == true){

					this.material.SetPass(0);

					if(Fee.Graphic.Gl.Begin(UnityEngine.GL.TRIANGLES) == true){
						{
							int t_index = 0;

							//前回のフレームの今回のフレームの差分。
							this.DrawBar(0,this.framedata.startframe - this.framedata.startframe_old,t_index,Config.COLOR_FRAME);
							this.DrawBar(0,Config.BAR_FRAME_TIME,t_index,Config.COLOR_FRAME_BASE);

							t_index++;

							//FixedUpdate First -- FixedUpdate Last
							this.DrawBar(this.framedata.fixedupdate_first - this.framedata.startframe,this.framedata.fixedupdate_last - this.framedata.startframe,t_index,Config.COLOR_FIXEDUPDATE);

							//Update First -- Update Last
							this.DrawBar(this.framedata.update_first - this.framedata.startframe,this.framedata.update_last - this.framedata.startframe,t_index,Config.COLOR_UPDATE);

							//LateUpdate First -- LateUpdate Last
							this.DrawBar(this.framedata.lateupdate_first - this.framedata.startframe,this.framedata.lateupdate_last - this.framedata.startframe,t_index,Config.COLOR_LATEUPDATE);

							//OnPreRender First -- OnPostRender Last
							this.DrawBar(this.framedata.render_first - this.framedata.startframe,this.framedata.render_last - this.framedata.startframe,t_index,Config.COLOR_RENDER);
						}

						Fee.Graphic.Gl.End();
					}
				}
				Fee.Graphic.Gl.PopMatrix();
			}
		}

		/** DrawBar
		*/
		private void DrawBar(float a_x1,float a_x2,int a_index,UnityEngine.Color a_color)
		{
			try{
				float t_w = ((a_x2 - a_x1) / Config.BAR_FRAME_TIME) * Config.BAR_FRAME_LENGTH;
				float t_h = 5.0f * 1.0f / (float)UnityEngine.Screen.height;
				float t_x = (a_x1 / Config.BAR_FRAME_TIME) * Config.BAR_FRAME_LENGTH;
				float t_y = (t_h + 0.0005f) * a_index;

				float t_x_1 = 0.0f + t_x;
				float t_y_1 = 1.0f - 0.0f - t_y;

				float t_x_2 = t_w + t_x;
				float t_y_2 = 1.0f - 0.0f - t_y;

				float t_x_3 = 0.0f + t_x;
				float t_y_3 = 1.0f - t_h - t_y;

				float t_x_4 = t_w + t_x;
				float t_y_4 = 1.0f - t_h - t_y;

				UnityEngine.GL.Color(a_color);
			
				{
					//1:左上。
					UnityEngine.GL.Vertex3(t_x_1,t_y_1,0.0f);

					//2:右上。
					UnityEngine.GL.Vertex3(t_x_2,t_y_2,0.0f);

					//3:左下。
					UnityEngine.GL.Vertex3(t_x_3,t_y_3,0.0f);
				}

				{
					//3:左下。
					UnityEngine.GL.Vertex3(t_x_3,t_y_3,0.0f);

					//2:右上。
					UnityEngine.GL.Vertex3(t_x_2,t_y_2,0.0f);

					//4:右下。
					UnityEngine.GL.Vertex3(t_x_4,t_y_4,0.0f);	
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

