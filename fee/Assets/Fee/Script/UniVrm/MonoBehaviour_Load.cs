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
	public class MonoBehaviour_Load : MonoBehaviour
	{
		/** Mode
		*/
		private enum Mode
		{
			/** リクエスト待ち。
			*/
			WaitRequest,

			/** 開始。
			*/
			Start,

			/** 実行中。
			*/
			Do,

			/** エラー終了。
			*/
			Do_Error,

			/** 正常終了。
			*/
			Do_Success,

			/** 完了。
			*/
			Fix,
		};

		/** mode
		*/
		[SerializeField]
		private Mode mode;

		/** delete_flag
		*/
		[SerializeField]
		private bool delete_flag;

		/** request_binary
		*/
		[SerializeField]
		private byte[] request_binary;

		/** result_context
		*/
		[SerializeField]
		private VRM.VRMImporterContext result_context;

		/** リクエスト待ち開始。
		*/
		private void WaitRequest()
		{
			if(this.mode == Mode.Fix){
				this.mode = Mode.WaitRequest;
			}else{
				Tool.Assert(false);
			}
		}

		/** リクエスト待ち。
		*/
		private bool IsWaitRequest()
		{
			if(this.mode == Mode.WaitRequest){
				return true;
			}
			return false;
		}

		/** 開始。
		*/
		private void SetModeStart()
		{
			this.mode = Mode.Start;
		}

		/** 実行。
		*/
		private void SetModeDo()
		{
			this.mode = Mode.Do;
		}

		/** 正常終了。
		*/
		private void SetModeDoSuccess()
		{
			this.mode = Mode.Do_Success;
		}

		/** エラー終了。
		*/
		private void SetModeDoError()
		{
			this.mode = Mode.Do_Error;
		}

		/** 完了。
		*/
		private void SetModeFix()
		{
			this.mode = Mode.Fix;
		}

		/** 完了チェック。
		*/
		private bool IsFix()
		{
			if(this.mode == Mode.Fix){
				return true;
			}
			return false;
		}

		/** 削除リクエスト。設定。
		*/
		private void DeleteRequest()
		{
			this.delete_flag = true;
		}

		/** 削除リクエスト。取得。
		*/
		private bool IsDeleteRequest()
		{
			return this.delete_flag;
		}

		/** Awake
		*/
		private void Awake()
		{
			this.mode = Mode.WaitRequest;
			this.delete_flag = false;

			this.request_binary = null;
			this.result_context = null;
		}

		/** Start
		*/
		private IEnumerator Start()
		{
			bool t_loop = true;
			while(t_loop){
				switch(this.mode){
				case Mode.WaitRequest:
					{
						yield return null;
						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Fix:
					{
						yield return null;
						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Start:
					{
						yield return this.OnStart();
					}break;
				case Mode.Do:
					{
						yield return  this.OnDo();
					}break;
				case Mode.Do_Error:
					{
						yield return this.OnDoError();
					}break;
				case Mode.Do_Success:
					{
						yield return this.OnDoSuccess();
					}break;
				}
			}

			Tool.Log(this.gameObject.name,"GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		private IEnumerator OnStart()
		{

			this.SetModeDo();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		private IEnumerator OnDo()
		{
			yield return this.Raw_Do_Load_Parse();
			yield return this.Raw_Do_Load_MaterialTexture();
			yield return this.Raw_Do_Load_Mesh();
			yield return this.Raw_Do_Load_Node();
			yield return this.Raw_Do_Load_Model();

			this.SetModeDoSuccess();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		private IEnumerator OnDoError()
		{
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		private IEnumerator OnDoSuccess()
		{
			this.SetModeFix();
			yield break;
		}

		/** [内部からの呼び出し]ロード。Parse。
		*/
		private IEnumerator Raw_Do_Load_Parse()
		{
			this.result_context = new VRM.VRMImporterContext();
			this.result_context.ParseGlb(this.request_binary);
			yield break;
		}

		/** [内部からの呼び出し]ロード。MaterialTexture。
		*/
		private IEnumerator Raw_Do_Load_MaterialTexture()
		{
			//MaterialImporter
			List<VRM.glTF_VRM_Material> t_material_list = VRM.glTF_VRM_Material.Parse(this.result_context.Json);
			this.result_context.MaterialImporter = new VRM.VRMMaterialImporter(this.result_context,t_material_list);

			//AddTexture
			for(int ii=0;ii<this.result_context.GLTF.textures.Count;ii++){
				UniGLTF.TextureItem t_texture = new UniGLTF.TextureItem(this.result_context.GLTF,ii);
				t_texture.Process(this.result_context.GLTF,this.result_context.Storage);
				this.result_context.AddTexture(t_texture);
			}

			//AddMaterial
			{
				bool t_add = false;
				if(this.result_context.GLTF.materials != null){
					if(this.result_context.GLTF.materials.Count > 0){
						t_add = true;
						for(int ii=0;ii<this.result_context.GLTF.materials.Count;ii++){
							this.result_context.AddMaterial(this.result_context.MaterialImporter.CreateMaterial(ii,this.result_context.GLTF.materials[ii]));
						}
					}
				}
				if(t_add == false){
					this.result_context.AddMaterial(this.result_context.MaterialImporter.CreateMaterial(0,null));
				}
			}

			yield break;
		}

		/** [内部からの呼び出し]ロード。Mesh。
		*/
		private IEnumerator Raw_Do_Load_Mesh()
		{
			//AddMesh
			UniGLTF.MeshImporter t_meshimporter = new UniGLTF.MeshImporter();
			for(int ii=0;ii<this.result_context.GLTF.meshes.Count;ii++){
				UniGLTF.MeshImporter.MeshContext t_mesh_context = t_meshimporter.ReadMesh(this.result_context,ii);
				UniGLTF.MeshWithMaterials t_mesh_with_material = UniGLTF.gltfImporter.BuildMesh(this.result_context,t_mesh_context);
				this.result_context.Meshes.Add(t_mesh_with_material);
			}

			yield break;
		}

		/** [内部からの呼び出し]ロード。Node。
		*/
		private IEnumerator Raw_Do_Load_Node()
		{
			//AddNode
			{
				foreach(UniGLTF.glTFNode t_item in this.result_context.GLTF.nodes){
					this.result_context.Nodes.Add(UniGLTF.gltfImporter.ImportNode(t_item).transform);
				}
			}
				
			//SetParent
			{
				List<UniGLTF.gltfImporter.TransformWithSkin> t_node_list = new List<UniGLTF.gltfImporter.TransformWithSkin>();
				for(int ii=0;ii< this.result_context.Nodes.Count;ii++){
					t_node_list.Add(UniGLTF.gltfImporter.BuildHierarchy(this.result_context,ii));
				}

				UniGLTF.gltfImporter.FixCoordinate(this.result_context,t_node_list);

				for(int ii=0;ii<t_node_list.Count;ii++){
					UniGLTF.gltfImporter.SetupSkinning(this.result_context,t_node_list,ii);
				}

				this.result_context.Root = new GameObject("_root_");
				foreach (int t_index in this.result_context.GLTF.rootnodes)
				{
					UnityEngine.Transform t_transform = t_node_list[t_index].Transform;
					t_transform.SetParent(this.result_context.Root.transform,false);
				}
			}

			yield break;
		}

		/** [内部からの呼び出し]ロード。Model。
		*/
		private IEnumerator Raw_Do_Load_Model()
		{
			//OnLoadModel
			VRM.VRMImporter.OnLoadModel(this.result_context);

			yield break;
		}

	}
}

