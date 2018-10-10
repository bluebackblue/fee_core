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
		/** リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ロード。
			*/
			Load,
		};

		/** ProgressStep
		*/
		private enum ProgressStep
		{
			Step0 = 0,
			Step1,

			Max,
		};

		/** Work
		*/
		private class Work
		{
			#if(USE_UNIVRM)
			public VRM.VRMImporterContext context;
			#else
			public string context;
			#endif

			public int progress_step_max;
			public int progress_step;
			public int progress_substep_max;
			public int progress_substep;

			/** constructor
			*/
			public Work()
			{
				this.context = null;

				this.progress_step_max = (int)ProgressStep.Max;
				this.progress_step = (int)ProgressStep.Step0;
				this.progress_substep_max = 1;
				this.progress_substep = 0;
			}
		};

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** request_binary
		*/
		[SerializeField]
		private byte[] request_binary;

		/** work
		*/
		[SerializeField]
		private Work work;

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_binary
			this.request_binary = null;

			//work
			this.work = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.Load:
				{
					Tool.Log("MonoBehaviour_Load",this.request_type.ToString());
					this.work = new Work();
					this.SetModeDo();
				}yield break;
			}

			//不明なリクエスト。
			this.SetResultErrorString("request_type == " + this.request_type.ToString());
			this.SetModeDoError();
			
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected override IEnumerator OnDo()
		{
			#if(USE_UNIVRM)
			{
				{
					this.work.progress_step = (int)ProgressStep.Step0;
					this.work.progress_substep = 0;
					this.work.progress_substep_max = 1;
					yield return this.Raw_Do_Load_Parse();
					if(this.GetResultType() == ResultType.Error){
						this.SetModeDoError();
						yield break;
					}
				}

				{
					this.work.progress_step = (int)ProgressStep.Step1;
					this.work.progress_substep = 0;
					this.work.progress_substep_max = 1;
					yield return this.Raw_Do_Load_Create();
					if(this.GetResultType() == ResultType.Error){
						this.SetModeDoError();
						yield break;
					}
				}
			}
			#else
			if(this.work.context == null){
				this.SetModeDoError();
				yield break;
			}
			#endif

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

		/** リクエスト。
		*/
		public bool Request(byte[] a_binary)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.Load;
				this.request_binary = a_binary;
				this.work = null;

				return true;
			}

			return false;
		}

		/** プログレス計算。
		*/
		private float CalcProgress(float a_progress)
		{
			float t_progress = 0.0f;
			t_progress += ((float)this.work.progress_step) / this.work.progress_step_max;
			t_progress += (a_progress + (float)this.work.progress_substep) / (this.work.progress_step_max * this.work.progress_substep_max);
			return t_progress;
		}

		/** [内部からの呼び出し]ロード。Parse。
		*/
		#if(USE_UNIVRM)
		private IEnumerator Raw_Do_Load_Parse()
		{
			//プログレス。
			this.SetResultProgress(this.CalcProgress(0.0f));

			this.work.context = new VRM.VRMImporterContext();
			this.work.context.ParseGlb(this.request_binary);

			yield break;
		}
		#endif

		/** [内部からの呼び出し]ロード。作成。
		*/
		#if(USE_UNIVRM)
		private IEnumerator Raw_Do_Load_Create()
		{
			//プログレス。
			this.SetResultProgress(this.CalcProgress(0.0f));

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
			}

			//AddMesh
			{
				UniGLTF.MeshImporter t_meshimporter = new UniGLTF.MeshImporter();
				for(int ii=0;ii<this.work.context.GLTF.meshes.Count;ii++){
					UniGLTF.MeshImporter.MeshContext t_mesh_context = t_meshimporter.ReadMesh(this.work.context,ii);
					UniGLTF.MeshWithMaterials t_mesh_with_material = UniGLTF.gltfImporter.BuildMesh(this.work.context,t_mesh_context);
					this.work.context.Meshes.Add(t_mesh_with_material);
				}
			}

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

			//OnLoadModel
			VRM.VRMImporter.OnLoadModel(this.work.context);

			yield break;
		}
		#endif
	}
}

