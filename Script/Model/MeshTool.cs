

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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

		６面。２４頂点。

		*/
		public static UnityEngine.Vector2[] CreateCubeUvPattern()
		{
			return new UnityEngine.Vector2[]{
				//X_0
				new UnityEngine.Vector2(0,0),
				new UnityEngine.Vector2(1,0),
				new UnityEngine.Vector2(1,1),
				new UnityEngine.Vector2(0,1),

				//X_1
				new UnityEngine.Vector2(0,0),
				new UnityEngine.Vector2(1,0),
				new UnityEngine.Vector2(1,1),
				new UnityEngine.Vector2(0,1),

				//Y_0
				new UnityEngine.Vector2(0,0),
				new UnityEngine.Vector2(1,0),
				new UnityEngine.Vector2(1,1),
				new UnityEngine.Vector2(0,1),

				//Y_1
				new UnityEngine.Vector2(0,0),
				new UnityEngine.Vector2(1,0),
				new UnityEngine.Vector2(1,1),
				new UnityEngine.Vector2(0,1),

				//Z_0
				new UnityEngine.Vector2(0,0),
				new UnityEngine.Vector2(1,0),
				new UnityEngine.Vector2(1,1),
				new UnityEngine.Vector2(0,1),

				//Z_1
				new UnityEngine.Vector2(0,0),
				new UnityEngine.Vector2(1,0),
				new UnityEngine.Vector2(1,1),
				new UnityEngine.Vector2(0,1),
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

