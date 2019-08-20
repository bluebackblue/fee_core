

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パフォーマンスカウンター。カメラ。
*/


/** Fee.PerformanceCounter
*/
namespace Fee.PerformanceCounter
{
	/** MonoBehaviour_Camera
	*/
	public class MonoBehaviour_Camera : UnityEngine.MonoBehaviour
	{
		/** mycamera
		*/
		public UnityEngine.Camera mycamera;

		/** mymaterial
		*/
		public UnityEngine.Material mymaterial;

		/** 初期化。
		*/
		public void Initialize()
		{
			//カメラ取得。
			this.mycamera = this.GetComponent<UnityEngine.Camera>();

			//マテリアル。
			this.mymaterial = UnityEngine.Resources.Load<UnityEngine.Material>("Material/PerformanceCounter/Sprite");
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** OnRenderImage
		*/
		private void OnPostRender()
		{
			//フレーム終了。
			FrameData t_framedata = Fee.PerformanceCounter.PerformanceCounter.GetInstance().GetFrameData();
			t_framedata.end_time = UnityEngine.Time.realtimeSinceStartup;

			{
				UnityEngine.GL.PushMatrix();
				{
					UnityEngine.GL.LoadOrtho();

					this.mymaterial.SetPass(0);

					UnityEngine.GL.Begin(UnityEngine.GL.TRIANGLES);

					float t_length = t_framedata.end_time - t_framedata.start_time;
					const float t_length_max = 1.0f / 60.0f;
					float t_per = t_length / t_length_max;

					float t_w = 0.3f * t_per;
					float t_h = 5.0f * 1.0f / (float)UnityEngine.Screen.height;

					float t_x_1 = 0.0f;
					float t_y_1 = 1.0f - 0.0f;

					float t_x_2 = t_w;
					float t_y_2 = 1.0f - 0.0f;

					float t_x_3 = 0.0f;
					float t_y_3 = 1.0f - t_h;

					float t_x_4 = t_w;
					float t_y_4 = 1.0f - t_h;

					if(t_per >= 2.0f){
						Tool.Log(Config.LOG_TAGNAME_STRING,t_per.ToString());
						UnityEngine.GL.Color(Config.COLOR_NORMAL);
					}else{
						UnityEngine.GL.Color(Config.COLOR_OVER);
					}

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

					UnityEngine.GL.End();
				}
				UnityEngine.GL.PopMatrix();
			}

			//新しいフレームを開始。
			t_framedata.start_time = UnityEngine.Time.realtimeSinceStartup;
		}
	}
}

