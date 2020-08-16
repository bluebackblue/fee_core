

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

		TODO:MonoBehaviour

	*/
	public class MirrorCamera_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** raw_gameobjet
		*/
		public UnityEngine.GameObject raw_gameobjet;

		/** raw_rendertexture
		*/
		public UnityEngine.RenderTexture raw_rendertexture;

		/** raw_camera
		*/
		public UnityEngine.Camera raw_camera;

		/** target_camera
		*/
		public UnityEngine.Camera target_camera;

		/** target_transform
		*/
		public UnityEngine.Transform target_transform;

		/** plane
		*/
		public UnityEngine.Vector4 plane;

		/** 反転マトリックス。
		*/
		public UnityEngine.Matrix4x4 matrix;

		/** 反転マトリックス。
		*/
		public UnityEngine.Matrix4x4 matrix_rotate;

		/** plane_quaternion
		*/
		public UnityEngine.Quaternion plane_quaternion;

		/** plane_position
		*/
		public UnityEngine.Vector3 plane_position;

		/** plane_offset
		*/
		public float plane_offset;

		/** GetRenderTexture
		*/
		public UnityEngine.RenderTexture GetRenderTexture()
		{
			return this.raw_rendertexture;
		}

		/** SetTargetCamera
		*/
		public void SetTargetCamera(UnityEngine.Camera a_camera)
		{
			this.target_camera = a_camera;
			this.target_transform = a_camera.GetComponent<UnityEngine.Transform>();
		}

		/** ミラーの法線、位置。設定。
		*/
		public void SetMirrorPlane(UnityEngine.Quaternion a_quaternion,UnityEngine.Vector3 a_position)
		{
			//plane_quaternion
			this.plane_quaternion = a_quaternion;

			//plane_position
			this.plane_position = a_position;

			UnityEngine.Vector3 t_plane_normal = this.plane_quaternion * UnityEngine.Vector3.up;

			float t_distance = -UnityEngine.Vector3.Dot(t_plane_normal,this.plane_position) - this.plane_offset;
			this.plane = new UnityEngine.Vector4(t_plane_normal.x,t_plane_normal.y,t_plane_normal.z,t_distance);

			this.matrix.m00 = (1.0f - 2.0f * this.plane[0] * this.plane[0]);
			this.matrix.m01 = (	 - 2.0f * this.plane[0] * this.plane[1]);
			this.matrix.m02 = (	 - 2.0f * this.plane[0] * this.plane[2]);
			this.matrix.m03 = (	 - 2.0f * this.plane[3] * this.plane[0]);
			this.matrix.m10 = (	 - 2.0f * this.plane[1] * this.plane[0]);
			this.matrix.m11 = (1.0f - 2.0f * this.plane[1] * this.plane[1]);
			this.matrix.m12 = (	 - 2.0f * this.plane[1] * this.plane[2]);
			this.matrix.m13 = (	 - 2.0f * this.plane[3] * this.plane[1]);
			this.matrix.m20 = (	 - 2.0f * this.plane[2] * this.plane[0]);
			this.matrix.m21 = (	 - 2.0f * this.plane[2] * this.plane[1]);
			this.matrix.m22 = (1.0f - 2.0f * this.plane[2] * this.plane[2]);
			this.matrix.m23 = (	 - 2.0f * this.plane[3] * this.plane[2]);
			this.matrix.m30 = 0.0f;
			this.matrix.m31 = 0.0f;
			this.matrix.m32 = 0.0f;
			this.matrix.m33 = 1.0f;

			this.matrix_rotate.m00 = this.matrix.m00;
			this.matrix_rotate.m01 = this.matrix.m01;
			this.matrix_rotate.m02 = this.matrix.m02;
			this.matrix_rotate.m03 = 0.0f;
			this.matrix_rotate.m10 = this.matrix.m10;
			this.matrix_rotate.m11 = this.matrix.m11;
			this.matrix_rotate.m12 = this.matrix.m12;
			this.matrix_rotate.m13 = 0.0f;
			this.matrix_rotate.m20 = this.matrix.m20;
			this.matrix_rotate.m21 = this.matrix.m21;
			this.matrix_rotate.m22 = this.matrix.m22;
			this.matrix_rotate.m23 = 0.0f;
			this.matrix_rotate.m30 = 0.0f;
			this.matrix_rotate.m31 = 0.0f;
			this.matrix_rotate.m32 = 0.0f;
			this.matrix_rotate.m33 = 1.0f;
		}

		/** Delete
		*/
		public void Delete()
		{
			UnityEngine.GameObject.Destroy(this.raw_gameobjet);
			
			this.raw_camera.targetTexture = null;

			if(this.raw_rendertexture != null){
				UnityEngine.GameObject.DestroyImmediate(this.raw_rendertexture);
				this.raw_rendertexture = null;
			}
		}

		/** RenderFromOnWillRenderObject
		*/
		public void RenderFromOnWillRenderObject()
		{
			//worldToCameraMatrix
			this.raw_camera.worldToCameraMatrix = this.target_camera.worldToCameraMatrix * this.matrix;

			//projectionMatrix
			{
				UnityEngine.Matrix4x4 t_mirror_camera_matrix = this.raw_camera.worldToCameraMatrix;
				UnityEngine.Vector4 t_mirror_clip_plane;
				{
					UnityEngine.Vector3 t_pos = new UnityEngine.Vector3(this.plane.x,plane.y,plane.z) * (this.plane_offset - this.plane.w);
					UnityEngine.Vector3 t_mirror_position = t_mirror_camera_matrix.MultiplyPoint(t_pos);
					UnityEngine.Vector3 t_mirror_normal = t_mirror_camera_matrix.MultiplyVector(new UnityEngine.Vector3(this.plane.x,this.plane.y,this.plane.z)).normalized;
					t_mirror_clip_plane = new UnityEngine.Vector4(t_mirror_normal.x,t_mirror_normal.y,t_mirror_normal.z,-UnityEngine.Vector3.Dot(t_mirror_position,t_mirror_normal));
				}
				this.raw_camera.projectionMatrix = this.target_camera.CalculateObliqueMatrix(t_mirror_clip_plane);
			}

			//position
			{
				this.raw_camera.transform.position = this.matrix.MultiplyPoint(this.target_transform.position);
			}

			//eulerAngles
			{
				UnityEngine.Vector3 t_up = this.matrix_rotate.MultiplyPoint(this.target_transform.up).normalized;
				UnityEngine.Vector3 t_forward = this.matrix_rotate.MultiplyPoint(this.target_transform.forward).normalized;
				UnityEngine.Vector3 t_right = UnityEngine.Vector3.Cross(t_up,t_forward);

				UnityEngine.Matrix4x4 t_matrix = new UnityEngine.Matrix4x4(
					new UnityEngine.Vector4(t_right.x,t_right.y,t_right.z,0.0f),
					new UnityEngine.Vector4(t_up.x,t_up.y,t_up.z,0.0f),
					new UnityEngine.Vector4(t_forward.x,t_forward.y,t_forward.z,0.0f),
					new UnityEngine.Vector4(0.0f,0.0f,0.0f,1.0f)
				);

				this.raw_camera.transform.rotation = t_matrix.rotation;
			}

			UnityEngine.GL.invertCulling = true;
			this.raw_camera.Render();
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

						t_this.raw_rendertexture = new UnityEngine.RenderTexture(t_size,t_size,16,UnityEngine.RenderTextureFormat.ARGB32);
						t_this.raw_rendertexture.isPowerOfTwo = true;
					}

					//raw_camera
					{
						t_this.raw_camera = t_gameobject.AddComponent<UnityEngine.Camera>();
						t_this.raw_camera.Reset();
						t_this.raw_camera.SetTargetBuffers(t_this.raw_rendertexture.colorBuffer,t_this.raw_rendertexture.depthBuffer);
						t_this.raw_camera.enabled = false;
					}

					//plane_offset
					t_this.plane_offset = 0.0f;
				}
			}
			return t_this;
		}
	}
}

