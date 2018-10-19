using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** test20
*/
public class test20 : main_base
{
	/** Step
	*/
	private enum Step
	{
		Init,

		CreateTerrain,

		LoadVrm_Start,
		LoadVrm_Do,

		CreateVrm_Start,
		CreateVrm_Do,

		Main,
	};

	/** VrmStatus
	*/
	private enum VrmStatus
	{
		None,

		Idel,

		Walk,
	};

	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** step
	*/
	private Step step;

	/** vrm
	*/
	private NUniVrm.Item vrm;
	private NFile.Item vrm_loaditem;
	private GameObject vrm_camera;
	private VrmStatus vrm_status;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.Config.LOG_ENABLE = true;
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//キー。インスタンス作成。
		NInput.Key.CreateInstance();

		//パッド。インスタンス作成。
		NInput.Pad.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//ファイル。インスタンス作成。
		NFile.File.CreateInstance();

		//ＵＮＩＶＲＭ。インスタンス作成。
		NUniVrm.UniVrm.CreateInstance();

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			NRender2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		//step
		this.step = Step.Init;

		//vrm
		this.vrm = null;
		this.vrm_loaditem = null;
		this.vrm_status = VrmStatus.None;
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//キー。
		NInput.Key.GetInstance().Main();

		//パッド。
		NInput.Pad.GetInstance().Main();

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//ファイル。
		NFile.File.GetInstance().Main();

		//ＵＮＩＶＲＭ。
		NUniVrm.UniVrm.GetInstance().Main();

		switch(this.step){
		case Step.Init:
			{
				this.step = Step.CreateTerrain;
			}break;
		case Step.CreateTerrain:
			{
				//テレイン作成。

				GameObject t_prefab = Resources.Load<GameObject>("Terrain/TerrainPrefab");
				GameObject t_terrain = GameObject.Instantiate<GameObject>(t_prefab);
				this.step = Step.LoadVrm_Start;
			}break;
		case Step.LoadVrm_Start:
			{
				//ＶＲＭダウンロード開始。

				string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/StreamingAssets/nana.vrmx";
				this.vrm_loaditem = NFile.File.GetInstance().RequestDownLoadBinaryFile(t_url);

				this.step = Step.LoadVrm_Do;
			}break;
		case Step.LoadVrm_Do:
			{
				if(this.vrm_loaditem != null){
					if(this.vrm_loaditem.IsBusy() == true){
						//ダウンロード中。

						//キャンセル。
						if(this.IsChangeScene() == true){
							this.vrm_loaditem.Cancel();
						}
					}else{
						if(this.vrm_loaditem.GetResultType() == NFile.Item.ResultType.Binary){
							//ダウンロード成功。
						}else{
							//ダウンロード失敗。
							this.vrm_loaditem = null;
						}

						this.step = Step.CreateVrm_Start;
					}
				}else{
					this.step = Step.CreateVrm_Start;
				}
			}break;
		case Step.CreateVrm_Start:
			{
				//ＶＲＭ作成開始。

				byte[] t_binary = null;
				if(this.vrm_loaditem != null){
					t_binary = this.vrm_loaditem.GetResultBinary();
				}

				if(t_binary != null){
					this.vrm = NUniVrm.UniVrm.GetInstance().Request(t_binary);
				}

				this.vrm_loaditem = null;

				this.step = Step.CreateVrm_Do;
			}break;
		case Step.CreateVrm_Do:
			{
				if(this.vrm != null){
					if(this.vrm.IsBusy() == true){
						//ＶＲＭ作成中。
					}else{
						if(this.vrm.GetResultType() == NUniVrm.Item.ResultType.Context){
							//ＶＲＭ作成成功。

							//レイヤーをモデルに設定。
							this.vrm.SetLayer("Model");

							//表示開始。
							this.vrm.SetRendererEnable(true);

							//アニメータコントローラ設定。
							this.vrm.SetAnimatorController(Resources.Load<RuntimeAnimatorController>("Anime/AnimatorController"));

							//モーション停止。
							this.vrm.SetAnimeEnable(false);
							this.vrm_status = VrmStatus.None;

							//追従カメラ。
							this.vrm_camera = GameObject.Find("Main Camera");
						}else{
							//ＶＲＭ作成失敗。
							this.vrm = null;
						}

						this.step = Step.Main;
					}
				}else{
					this.step = Step.Main;
				}
			}break;
		case Step.Main:
			{
				if(this.vrm != null){
					Transform t_head = this.vrm.GetBoneTransform(HumanBodyBones.Head);

					Vector3 t_vrm_forward = this.vrm.GetForward();

					//ＶＲＭを見る。
					this.vrm_camera.transform.LookAt(t_head);

					//カメラの位置（後頭部）
					Vector3 t_to_camerapos = t_head.position - t_vrm_forward * 2 + Vector3.up;

					//補間。
					this.vrm_camera.transform.position = Vector3.Lerp(t_to_camerapos,this.vrm_camera.transform.position,0.1f);

					if(NInput.Key.GetInstance().up.on == true){
						//前進。
						
						//モーション設定。
						{
							if(this.vrm_status == VrmStatus.None){
								this.vrm.SetAnimeEnable(true);
							}
							this.vrm_status = VrmStatus.Walk;
							this.vrm.SetAnime(Animator.StringToHash("Base Layer.standing_walk_forward_inPlace"));
						}

						//移動。
						Vector3 t_position = this.vrm.GetPosition() + t_vrm_forward * 0.1f;
						this.vrm.SetPosition(ref t_position);
					}else{
						//停止。

						//モーション設定。
						{
							if(this.vrm_status != VrmStatus.None){
								this.vrm.SetAnimeEnable(false);
							}

							this.vrm_status = VrmStatus.None;
						}
					}

					if(NInput.Key.GetInstance().left.on == true){
						//左回転。
					}else if(NInput.Key.GetInstance().right.on == true){
						//右回転。
					}
				}
			}break;
		}
	}

	/** 削除前。
	*/
	public override bool PreDestroy(bool a_first)
	{
		return true;
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

