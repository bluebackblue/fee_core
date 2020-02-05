

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ミラー。
*/


/** Fee.Mirror
*/
namespace Fee.Mirror
{
	/** MirrorCamera
	*/
	public class MirrorCamera_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** raw_gameobjet
		*/
		public UnityEngine.GameObject raw_gameobjet;

		/** rendertexture
		*/
		public UnityEngine.RenderTexture rendertexture;

		/** raw_camera
		*/
		public UnityEngine.Camera raw_camera;

		/** target_camera
		*/
		public UnityEngine.Camera target_camera;

		/** plane
		*/
		public UnityEngine.Vector4 plane;

		/** 反転マトリックス。
		*/
		public UnityEngine.Matrix4x4 matrix;

		/** init
		*/
		public int init;

		/** GetRenderTexture
		*/
		public UnityEngine.RenderTexture GetRenderTexture()
		{
			return this.rendertexture;
		}

		/** SetTarget
		*/
		public void SetTarget(UnityEngine.Camera a_camera)
		{
			this.target_camera = a_camera;
		}

		/** ミラーの法線、位置。設定。
		*/
		public void SetMirrorPlane(UnityEngine.Vector3 a_normal,UnityEngine.Vector3 a_position)
		{
			float t_distance = -UnityEngine.Vector3.Dot(a_normal,a_position);
			this.plane = new UnityEngine.Vector4(a_normal.x,a_normal.y,a_normal.z,t_distance);

			this.matrix.m00 = (1.0f - 2.0f * this.plane[0] * this.plane[0]);
			this.matrix.m01 = (     - 2.0f * this.plane[0] * this.plane[1]);
			this.matrix.m02 = (     - 2.0f * this.plane[0] * this.plane[2]);
			this.matrix.m03 = (     - 2.0f * this.plane[3] * this.plane[0]);
			this.matrix.m10 = (     - 2.0f * this.plane[1] * this.plane[0]);
			this.matrix.m11 = (1.0f - 2.0f * this.plane[1] * this.plane[1]);
			this.matrix.m12 = (     - 2.0f * this.plane[1] * this.plane[2]);
			this.matrix.m13 = (     - 2.0f * this.plane[3] * this.plane[1]);
			this.matrix.m20 = (     - 2.0f * this.plane[2] * this.plane[0]);
			this.matrix.m21 = (     - 2.0f * this.plane[2] * this.plane[1]);
			this.matrix.m22 = (1.0f - 2.0f * this.plane[2] * this.plane[2]);
			this.matrix.m23 = (     - 2.0f * this.plane[3] * this.plane[2]);
 			this.matrix.m30 = 0.0f;
			this.matrix.m31 = 0.0f;
			this.matrix.m32 = 0.0f;
			this.matrix.m33 = 1.0f;
		}

		/** Delete
		*/
		public void Delete()
		{
			UnityEngine.GameObject.Destroy(this.raw_gameobjet);
			
			this.raw_camera.targetTexture = null;

			if(this.rendertexture != null){
				UnityEngine.GameObject.DestroyImmediate(this.rendertexture);
				this.rendertexture = null;
			}
		}

		/** Awake
		*/
		private void Awake()
		{
			this.init = 1;
		}

		/** OnPreRender

			OnPreRenderはカメラがシーンのレンダリングを始める前に呼び出されます。

		*/
		private void OnPreRender()
		{
			UnityEngine.GL.invertCulling = true;

			//worldToCameraMatrix
			this.raw_camera.worldToCameraMatrix = this.target_camera.worldToCameraMatrix * this.matrix;

			//position
			this.raw_camera.transform.position = this.matrix * this.target_camera.transform.position;

			{
				UnityEngine.Vector3 t_euler_angle = this.target_camera.transform.eulerAngles;
				this.raw_camera.transform.eulerAngles = new UnityEngine.Vector3(0,t_euler_angle.y,t_euler_angle.z);
			}

			if(this.init > 0){
				this.init--;
				return;
			}

			//projectionMatrix
			{
				UnityEngine.Matrix4x4 t_matrix = this.raw_camera.worldToCameraMatrix;
				UnityEngine.Vector4 t_mirror_clip_plane;
				{
					UnityEngine.Vector3 t_mirror_position = t_matrix.MultiplyPoint(new UnityEngine.Vector3(this.plane.x,plane.y,plane.z) * this.plane.z);
					UnityEngine.Vector3 t_mirror_normal = t_matrix.MultiplyVector(new UnityEngine.Vector3(this.plane.x,this.plane.y,this.plane.z)).normalized;
					t_mirror_clip_plane  = new UnityEngine.Vector4(t_mirror_normal.x,t_mirror_normal.y,t_mirror_normal.z,-UnityEngine.Vector3.Dot(t_mirror_position,t_mirror_normal));
				}
				this.raw_camera.projectionMatrix = this.target_camera.CalculateObliqueMatrix(t_mirror_clip_plane);
			}
		}

		/** OnPostRender

			OnPostRenderはカメラがシーンのレンダリングを完了した後に呼び出されます。

		*/
		private void OnPostRender()
		{
			UnityEngine.GL.invertCulling = false;
		}

		/** 作成。
		*/
		public static MirrorCamera_MonoBehaviour Create(Fee.Mirror.RenderTextureSizeType a_size_type)
		{
			MirrorCamera_MonoBehaviour t_this;
			{
				UnityEngine.GameObject t_gameobject = new UnityEngine.GameObject("MirrorCamera");
				t_this = t_gameobject.AddComponent<MirrorCamera_MonoBehaviour>();
				t_this.raw_gameobjet = t_gameobject;
				{
					//rendertexture
					{
						int t_size ;
						switch(a_size_type){
						case Fee.Mirror.RenderTextureSizeType.Size_128:
							{
								t_size = 128;
							}break;
						case Fee.Mirror.RenderTextureSizeType.Size_256:
							{
								t_size = 256;
							}break;
						case Fee.Mirror.RenderTextureSizeType.Size_512:
							{
								t_size = 512;
							}break;
						case Fee.Mirror.RenderTextureSizeType.Size_1024:
							{
								t_size = 1024;
							}break;
						case Fee.Mirror.RenderTextureSizeType.Size_2048:
							{
								t_size = 2048;
							}break;
						default:
							{
								Tool.Assert(false);
								t_size = 1024;
							}break;
						}

						t_this.rendertexture = new UnityEngine.RenderTexture(t_size,t_size,16,UnityEngine.RenderTextureFormat.ARGB32);
						t_this.rendertexture.isPowerOfTwo = true;
					}

					//raw_camera
					{
						t_this.raw_camera = t_gameobject.AddComponent<UnityEngine.Camera>();
						t_this.raw_camera.Reset();
						t_this.raw_camera.SetTargetBuffers(t_this.rendertexture.colorBuffer,t_this.rendertexture.depthBuffer);
					}
				}
			}
			return t_this;
		}
	}
}

