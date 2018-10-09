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


/** NUniVrm
*/
namespace NUniVrm
{
	/** MonoBehaviour_Load
	*/
	public class MonoBehaviour_Load : MonoBehaviour_Base
	{

		/** Work
		*/
		private class Work
		{
			#if(USE_UNIVRM)
			public VRM.VRMImporterContext context;
			#endif

			/** constructor
			*/
			public Work()
			{
				this.context = null;
			}
		};

		/** cancel_flag
		*/
		[SerializeField]
		private bool cancel_flag;

		/** request_binary
		*/
		[SerializeField]
		private byte[] request_binary;

		/** work
		*/
		private Work work;

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//cancel_flag
			this.cancel_flag = false;

			//request_binary
			this.request_binary = null;

			//work
			this.work = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			this.work = new Work();

			this.SetModeDo();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected override IEnumerator OnDo()
		{
			#if(USE_UNIVRM)
			yield return this.Raw_Do_Load_Parse();
			yield return this.Raw_Do_Load_MaterialTexture();
			yield return this.Raw_Do_Load_Mesh();
			yield return this.Raw_Do_Load_Node();
			yield return this.Raw_Do_Load_Model();
			#endif

			if(this.GetResultType() == ResultType.Error){
				this.SetModeDoError();
				yield break;
			}	

			this.SetResultContext(this.work.context);
			this.SetModeDoSuccess();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override IEnumerator OnDoError()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override IEnumerator OnDoSuccess()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** キャンセル。
		*/
		public void Cancel()
		{
			this.cancel_flag = true;
		}

		/** リクエスト。
		*/
		public bool Request(byte[] a_binary)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.cancel_flag = false;

				this.request_binary = a_binary;
				this.work = null;

				return true;
			}

			return false;
		}

		/** [内部からの呼び出し]ロード。Parse。
		*/
		#if(USE_UNIVRM)
		private IEnumerator Raw_Do_Load_Parse()
		{
			this.work.context = new VRM.VRMImporterContext();
			this.work.context.ParseGlb(this.request_binary);

			yield break;
		}
		#endif

		/** [内部からの呼び出し]ロード。MaterialTexture。
		*/
		#if(USE_UNIVRM)
		private IEnumerator Raw_Do_Load_MaterialTexture()
		{
			//MaterialImporter
			List<VRM.glTF_VRM_Material> t_material_list = VRM.glTF_VRM_Material.Parse(this.work.context.Json);
			this.work.context.MaterialImporter = new VRM.VRMMaterialImporter(this.work.context,t_material_list);

			//AddTexture
			for(int ii=0;ii<this.work.context.GLTF.textures.Count;ii++){
				UniGLTF.TextureItem t_texture = new UniGLTF.TextureItem(this.work.context.GLTF,ii);
				t_texture.Process(this.work.context.GLTF,this.work.context.Storage);
				this.work.context.AddTexture(t_texture);
			}

			//AddMaterial
			{
				bool t_add = false;
				if(this.work.context.GLTF.materials != null){
					if(this.work.context.GLTF.materials.Count > 0){
						t_add = true;
						for(int ii=0;ii<this.work.context.GLTF.materials.Count;ii++){
							this.work.context.AddMaterial(this.work.context.MaterialImporter.CreateMaterial(ii,this.work.context.GLTF.materials[ii]));
						}
					}
				}
				if(t_add == false){
					this.work.context.AddMaterial(this.work.context.MaterialImporter.CreateMaterial(0,null));
				}
			}

			yield break;
		}
		#endif

		/** [内部からの呼び出し]ロード。Mesh。
		*/
		#if(USE_UNIVRM)
		private IEnumerator Raw_Do_Load_Mesh()
		{
			//AddMesh
			UniGLTF.MeshImporter t_meshimporter = new UniGLTF.MeshImporter();
			for(int ii=0;ii<this.work.context.GLTF.meshes.Count;ii++){
				UniGLTF.MeshImporter.MeshContext t_mesh_context = t_meshimporter.ReadMesh(this.work.context,ii);
				UniGLTF.MeshWithMaterials t_mesh_with_material = UniGLTF.gltfImporter.BuildMesh(this.work.context,t_mesh_context);
				this.work.context.Meshes.Add(t_mesh_with_material);
			}

			yield break;
		}
		#endif

		/** [内部からの呼び出し]ロード。Node。
		*/
		#if(USE_UNIVRM)
		private IEnumerator Raw_Do_Load_Node()
		{
			//AddNode
			{
				foreach(UniGLTF.glTFNode t_item in this.work.context.GLTF.nodes){
					this.work.context.Nodes.Add(UniGLTF.gltfImporter.ImportNode(t_item).transform);
				}
			}
				
			//SetParent
			{
				List<UniGLTF.gltfImporter.TransformWithSkin> t_node_list = new List<UniGLTF.gltfImporter.TransformWithSkin>();
				for(int ii=0;ii< this.work.context.Nodes.Count;ii++){
					t_node_list.Add(UniGLTF.gltfImporter.BuildHierarchy(this.work.context,ii));
				}

				UniGLTF.gltfImporter.FixCoordinate(this.work.context,t_node_list);

				for(int ii=0;ii<t_node_list.Count;ii++){
					UniGLTF.gltfImporter.SetupSkinning(this.work.context,t_node_list,ii);
				}

				this.work.context.Root = new GameObject("_root_");
				foreach (int t_index in this.work.context.GLTF.rootnodes)
				{
					UnityEngine.Transform t_transform = t_node_list[t_index].Transform;
					t_transform.SetParent(this.work.context.Root.transform,false);
				}
			}

			yield break;
		}
		#endif

		/** [内部からの呼び出し]ロード。Model。
		*/
		#if(USE_UNIVRM)
		private IEnumerator Raw_Do_Load_Model()
		{
			//OnLoadModel
			VRM.VRMImporter.OnLoadModel(this.work.context);

			yield break;
		}
		#endif
	}
}

