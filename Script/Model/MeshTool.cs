

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief モデル。レイヤーマスクツール。
*/


/** Fee.Model
*/
namespace Fee.Model
{
	/** MeshTool
	*/
	public class MeshTool
	{
		/**constructor
		*/
		public MeshTool()
		{
		}

		/** CreateCubeUvPattern

			１面。４頂点。

		*/
		public static UnityEngine.Vector2[] CreateCubeUvPattern(float a_zero,float a_one)
		{
			return new UnityEngine.Vector2[]{
				new UnityEngine.Vector2(a_zero,a_zero),
				new UnityEngine.Vector2(a_one,a_zero),
				new UnityEngine.Vector2(a_one,a_one),
				new UnityEngine.Vector2(a_zero,a_one),
			};
		}

		/** CreateCubeVertex

			６面。２４頂点。

		*/
		public static UnityEngine.Vector3[] CreateCubeVertexPattern(float a_zero,float a_one)
		{
			return new UnityEngine.Vector3[]{
				//X_0
				new UnityEngine.Vector3(a_zero,a_zero,a_zero),
				new UnityEngine.Vector3(a_zero,a_zero,a_one),
				new UnityEngine.Vector3(a_zero,a_one,a_one),
				new UnityEngine.Vector3(a_zero,a_one,a_zero),

				//X_1
				new UnityEngine.Vector3(a_one,a_zero,a_zero),
				new UnityEngine.Vector3(a_one,a_one,a_zero),
				new UnityEngine.Vector3(a_one,a_one,a_one),
				new UnityEngine.Vector3(a_one,a_zero,a_one),

				//Y_0
				new UnityEngine.Vector3(a_zero,a_zero,a_zero),
				new UnityEngine.Vector3(a_one,a_zero,a_zero),
				new UnityEngine.Vector3(a_one,a_zero,a_one),
				new UnityEngine.Vector3(a_zero,a_zero,a_one),

				//Y_1
				new UnityEngine.Vector3(a_zero,a_one,a_zero),
				new UnityEngine.Vector3(a_zero,a_one,a_one),
				new UnityEngine.Vector3(a_one,a_one,a_one),
				new UnityEngine.Vector3(a_one,a_one,a_zero),

				//Z_0
				new UnityEngine.Vector3(a_zero,a_zero,a_zero),
				new UnityEngine.Vector3(a_zero,a_one,a_zero),
				new UnityEngine.Vector3(a_one,a_one,a_zero),
				new UnityEngine.Vector3(a_one,a_zero,a_zero),

				//Z_1
				new UnityEngine.Vector3(a_zero,a_zero,a_one),
				new UnityEngine.Vector3(a_one,a_zero,a_one),
				new UnityEngine.Vector3(a_one,a_one,a_one),
				new UnityEngine.Vector3(a_zero,a_one,a_one),
			};
		}

		/** CreateCubeIndex

			１面。２トライアングル。６頂点。

		*/
		public static int[] CreateCubeIndexPattern()
		{
			return new int[]{
				0,1,2,
				0,2,3,
			};
		}
	}
}

