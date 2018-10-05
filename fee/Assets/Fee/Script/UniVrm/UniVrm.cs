using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。
*/


/** NVrm
*/
namespace NUniVrm
{
	/** UniVrm
	*/
	public class UniVrm
	{
		/** [シングルトン]s_instance
		*/
		private static UniVrm s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new UniVrm();
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
		public static UniVrm GetInstance()
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

		/** [シングルトン]constructor
		*/
		private UniVrm()
		{
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main()
		{
		}

		/** 作成。
		*/
		public void Create(byte[] a_binary)
		{
			#if(USE_UNIVRM)
			{
				//context
				VRM.VRMImporterContext t_context = new VRM.VRMImporterContext();

				//parse
				t_context.ParseGlb(a_binary);

				{
					//MaterialImporter
					List<VRM.glTF_VRM_Material> t_material_list = VRM.glTF_VRM_Material.Parse(t_context.Json);
					t_context.MaterialImporter = new VRM.VRMMaterialImporter(t_context,t_material_list);

					//AddTexture
					for(int ii=0;ii<t_context.GLTF.textures.Count;ii++){
						UniGLTF.TextureItem t_texture = new UniGLTF.TextureItem(t_context.GLTF,ii);
						t_texture.Process(t_context.GLTF,t_context.Storage);
						t_context.AddTexture(t_texture);
					}

					//AddMaterial
					{
						bool t_add = false;
						if(t_context.GLTF.materials != null){
							if(t_context.GLTF.materials.Count > 0){
								t_add = true;
								for(int ii=0;ii<t_context.GLTF.materials.Count;ii++){
									t_context.AddMaterial(t_context.MaterialImporter.CreateMaterial(ii,t_context.GLTF.materials[ii]));
								}
							}
						}
						if(t_add == false){
							t_context.AddMaterial(t_context.MaterialImporter.CreateMaterial(0,null));
						}
					}

					//AddMesh
					UniGLTF.MeshImporter t_meshimporter = new UniGLTF.MeshImporter();
					for(int ii=0;ii<t_context.GLTF.meshes.Count;ii++){
						UniGLTF.MeshImporter.MeshContext t_mesh_context = t_meshimporter.ReadMesh(t_context,ii);
						UniGLTF.MeshWithMaterials t_mesh_with_material = UniGLTF.gltfImporter.BuildMesh(t_context,t_mesh_context);
						t_context.Meshes.Add(t_mesh_with_material);
					}

					//AddNode
					{
						foreach(UniGLTF.glTFNode t_item in t_context.GLTF.nodes){
							t_context.Nodes.Add(UniGLTF.gltfImporter.ImportNode(t_item).transform);
						}
					}
				
					//SetParent
					{
						List<UniGLTF.gltfImporter.TransformWithSkin> t_node_list = new List<UniGLTF.gltfImporter.TransformWithSkin>();
						for(int ii=0;ii< t_context.Nodes.Count;ii++){
							t_node_list.Add(UniGLTF.gltfImporter.BuildHierarchy(t_context,ii));
						}

						UniGLTF.gltfImporter.FixCoordinate(t_context,t_node_list);

						for(int ii=0;ii<t_node_list.Count;ii++){
							UniGLTF.gltfImporter.SetupSkinning(t_context,t_node_list,ii);
						}

						t_context.Root = new GameObject("_root_");
						foreach (int t_index in t_context.GLTF.rootnodes)
						{
							UnityEngine.Transform t_transform = t_node_list[t_index].Transform;
							t_transform.SetParent(t_context.Root.transform,false);
						}
					}

					//OnLoadModel
					VRM.VRMImporter.OnLoadModel(t_context);

					//setup
					t_context.Root.name = "Model";
					t_context.ShowMeshes();
					t_context.Root.transform.rotation = Quaternion.Euler(0,180,0);
				}
			}
			#endif
		}
	}
}

