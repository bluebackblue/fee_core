

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief メッシュ。
*/


/** Fee.Mesh
*/
namespace Fee.Mesh
{
	/** Plate
	*/
	public class Plate
	{
		/** CAPACITY
		*/
		public const int CAPACITY_VERTEX_LIST = 4;
		public const int CAPACITY_INDEX_LIST = 6;
		public const int CAPACITY_UV_LIST = 4;

		/** バーテックスリスト。作成。
		*/
		public static void CreateVertexList(System.Collections.Generic.List<UnityEngine.Vector3> a_vertex_list)
		{
			const float SIZE = 0.5f;

			a_vertex_list.Add(new UnityEngine.Vector3(-SIZE,0.0f,-SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3(-SIZE,0.0f, SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3( SIZE,0.0f,-SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3( SIZE,0.0f, SIZE));
		}

		/** ＵＶリスト。作成。
		*/
		public static void CreateUvList(System.Collections.Generic.List<UnityEngine.Vector2> a_uv_list)
		{
			a_uv_list.Add(new UnityEngine.Vector2(1.0f,1.0f));
			a_uv_list.Add(new UnityEngine.Vector2(1.0f,0.0f));
			a_uv_list.Add(new UnityEngine.Vector2(0.0f,1.0f));
			a_uv_list.Add(new UnityEngine.Vector2(0.0f,0.0f));
		}


		/** インデックスリスト。作成。
		*/
		public static void CreateIndexList(System.Collections.Generic.List<int> a_index_list)
		{
			a_index_list.Add(0);
			a_index_list.Add(1);
			a_index_list.Add(2);
			a_index_list.Add(2);
			a_index_list.Add(1);
			a_index_list.Add(3);
		}

		/** 箱。作成。
		*/
		public static UnityEngine.Mesh CreateMesh(System.Collections.Generic.List<UnityEngine.Vector3> a_vertex_list,System.Collections.Generic.List<int> a_index_list,System.Collections.Generic.List<UnityEngine.Vector2> a_uv_list)
		{
			UnityEngine.Mesh t_mesh = new UnityEngine.Mesh();
			{
				t_mesh.SetVertices(a_vertex_list);
				t_mesh.SetTriangles(a_index_list,0);
				t_mesh.SetUVs(0,a_uv_list);
				t_mesh.RecalculateBounds();
				t_mesh.RecalculateNormals();
				t_mesh.RecalculateTangents();
			}
			return t_mesh;
		}
	}
}

