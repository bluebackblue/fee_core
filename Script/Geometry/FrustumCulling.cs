

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ジオメトリ。視錐台カリング。
*/


/** Fee.Geometry
*/
namespace Fee.Geometry
{
	/** FrustumCulling
	*/
	public class FrustumCulling
	{
		/** plane_list
		*/
		private UnityEngine.Plane[] plane_list;

		/** constructor
		*/
		public FrustumCulling()
		{
			this.plane_list = null;
		}

		/** プレーンリストを更新。
		*/
		public void UpdatePlaneList(UnityEngine.Camera a_camera)
		{
			this.plane_list = UnityEngine.GeometryUtility.CalculateFrustumPlanes(a_camera);
		}

		/** カリングテスト。 
		*/
		public bool CullingTest(UnityEngine.Bounds a_bounds)
		{
			if(this.plane_list != null){
				if(UnityEngine.GeometryUtility.TestPlanesAABB(this.plane_list,a_bounds) == true){
					return true;
				}
			}
			return false;
		}

		/** カリングテスト。
		*/
		public bool CullingTest(in UnityEngine.Vector3 a_pointer)
		{
			if(this.plane_list == null){
				return false;
			}

			for(int ii=0;ii<this.plane_list.Length;ii++){
				if(this.plane_list[ii].GetSide(a_pointer) == false){
					return false;
				}
			}

			return true;
		}
	}
}

