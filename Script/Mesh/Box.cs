

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
	/** Box
	*/
	public class Box
	{
		/** CAPACITY
		*/
		public const int CAPACITY_VERTEX_LIST = 8;
		public const int CAPACITY_UV_LIST = 8;
		public const int CAPACITY_INDEX_LIST = 36;

		/** バーテックスリスト。作成。
		*/
		public static void CreateVertexList(System.Collections.Generic.List<UnityEngine.Vector3> a_vertex_list)
		{
			const float SIZE = 0.5f;

			a_vertex_list.Add(new UnityEngine.Vector3( SIZE, SIZE,-SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3( SIZE,-SIZE,-SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3(-SIZE,-SIZE,-SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3(-SIZE, SIZE,-SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3( SIZE, SIZE, SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3( SIZE,-SIZE, SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3(-SIZE,-SIZE, SIZE));
			a_vertex_list.Add(new UnityEngine.Vector3(-SIZE, SIZE, SIZE));
		}

		/** インデックスリスト。作成。
		*/
		public static void CreateIndexList(System.Collections.Generic.List<int> a_index_list)
		{
			a_index_list.Add(2);
			a_index_list.Add(0);
			a_index_list.Add(1);
			a_index_list.Add(0);
			a_index_list.Add(2);
			a_index_list.Add(3);

			a_index_list.Add(0);
			a_index_list.Add(4);
			a_index_list.Add(5);
			a_index_list.Add(0);
			a_index_list.Add(5);
			a_index_list.Add(1);

			a_index_list.Add(3);
			a_index_list.Add(7);
			a_index_list.Add(4);
			a_index_list.Add(3);
			a_index_list.Add(4);
			a_index_list.Add(0);

			a_index_list.Add(2);
			a_index_list.Add(6);
			a_index_list.Add(7);
			a_index_list.Add(2);
			a_index_list.Add(7);
			a_index_list.Add(3);

			a_index_list.Add(2);
			a_index_list.Add(5);
			a_index_list.Add(6);
			a_index_list.Add(2);
			a_index_list.Add(1);
			a_index_list.Add(5);

			a_index_list.Add(6);
			a_index_list.Add(5);
			a_index_list.Add(4);
			a_index_list.Add(4);
			a_index_list.Add(7);
			a_index_list.Add(6);
		}

		/** 箱。作成。
		*/
		public static UnityEngine.Mesh CreateMesh(System.Collections.Generic.List<UnityEngine.Vector3> a_vertex_list,System.Collections.Generic.List<int> a_index_list)
		{
			UnityEngine.Mesh t_mesh = new UnityEngine.Mesh();
			{
				t_mesh.SetVertices(a_vertex_list);
				t_mesh.SetTriangles(a_index_list,0);
				t_mesh.RecalculateBounds();
				t_mesh.RecalculateNormals();
				t_mesh.RecalculateTangents();
			}
			return t_mesh;
		}
	}
}

