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

		/** ルート。
		*/
		private GameObject root_gameobject;
		private Transform root_transform;

		/** load
		*/
		private GameObject load_gameobject;
		private MonoBehaviour_Load load_script;

		/** work_list
		*/
		private List<Work> work_list;

		/** add_list
		*/
		private List<Work> add_list;

		#if(USE_UNIVRM)
		VRM.VRMImporterContext context;
		#endif

		/** [シングルトン]constructor
		*/
		private UniVrm()
		{
			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "UniVrm";
			GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<Transform>();

			//load
			{
				this.load_gameobject = new GameObject();
				this.load_gameobject.name = "UniVrm_Load";
				this.load_script = this.load_gameobject.AddComponent<MonoBehaviour_Load>();
				this.load_gameobject.GetComponent<Transform>().SetParent(this.root_transform);
			}

			//work_list
			this.work_list = new List<Work>();

			//add_list
			this.add_list = new List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//削除リクエスト。
			this.load_gameobject.GetComponent<Transform>().SetParent(null);
			GameObject.DontDestroyOnLoad(this.load_gameobject);
			this.load_script.DeleteRequest();

			//ルート削除。
			GameObject.Destroy(this.root_gameobject);
		}

		/** Load。取得。
		*/
		public MonoBehaviour_Load GetLoad()
		{
			return this.load_script;
		}

		/** リクエスト。ロード。
		*/
		public Item Request(byte[] a_binary)
		{
			Work t_work = new Work();
			t_work.Request(a_binary);

			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** 作成。
		*/
		#if(false)
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

					Animator t_animator = t_context.Root.GetComponent<Animator>();
					if(t_animator != null){
						t_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Anime/AnimatorController");
					}

					this.context = t_context;
				}
			}
			#endif
		}
		#endif

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if((this.work_list.Count > 0)||(this.add_list.Count > 0)){
				return true;
			}
			return false;
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//追加。
				if(this.add_list.Count > 0){
					for(int ii=0;ii<this.add_list.Count;ii++){
						this.work_list.Add(this.add_list[ii]);
					}
					this.add_list.Clear();
				}

				int t_index = 0;
				while(t_index < this.work_list.Count){
					if(this.work_list[t_index].Main() == true){
						this.work_list.RemoveAt(t_index);
					}else{
						t_index++;
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			/*
			if(this.context != null){
				Animator t_animator = this.context.Root.GetComponent<Animator>();
				if(t_animator != null){
					if(NInput.Mouse.GetInstance().left.down == true){
						t_animator.Play("unarmed_run_forward_inPlace");
					}else if(NInput.Mouse.GetInstance().right.down == true){
						t_animator.Play("unarmed_walk_back_inPlace");
					}
				}
			}
			*/
		}

		/** TODO:[内部からの呼び出し]レイヤー。設定。
		*/
		private static void Raw_SetLayer(Transform a_transform,int a_layer)
		{
			GameObject t_gameobject = a_transform.gameObject;
			if(t_gameobject != null){
				t_gameobject.layer = a_layer;
			}

			foreach(Transform t_transform in a_transform){
				Raw_SetLayer(t_transform,a_layer);
			}
		}

		/** TODO:レイヤー。設定。
		*/
		public void SetLayer(string a_layername)
		{
			Raw_SetLayer(this.context.Root.transform,LayerMask.NameToLayer(a_layername));
		}
	}
}

