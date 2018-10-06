using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief メイン。
*/


/** main
*/
public class main : MonoBehaviour
{
	/** scene_list
	*/
	private string[] scene_list;

	/** シーン数。
	*/
	private static int SCENE_COUNT = 20;

	/** deleter
	*/
	NDeleter.Deleter deleter;

	/** button
	*/
	NUi.Button[] button;

	/** Start
	*/
	void Start()
	{
		//シーン列挙。
		this.scene_list = new string[SCENE_COUNT];
		for(int ii=0;ii<scene_list.Length;ii++){
			this.scene_list[ii] = "test" + string.Format("{0:D2}",ii+1);
		}

		//ライブラリ停止。
		this.DeleteLibInstance();

		//インスタンス作成。
		{
			//２Ｄ描画。
			NRender2D.Render2D.CreateInstance();

			//ＵＩ。
			NUi.Ui.CreateInstance();

			//イベントプレート。
			NEventPlate.EventPlate.CreateInstance();

			//マウス。
			NInput.Mouse.CreateInstance();
		}

		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//button
			this.button = new NUi.Button[this.scene_list.Length];
			for(int ii=0;ii<this.button.Length;ii++){

				int t_xx_max = 9;

				int t_xx = ii % t_xx_max;
				int t_yy = ii / t_xx_max;

				int t_x = 30 + t_xx * 100;
				int t_y = 30 + t_yy * 60;
				int t_w = 80;
				int t_h = 40;

				string t_name = null;

				if(ii < this.scene_list.Length){
					t_name = this.scene_list[ii];
				}else{
					t_name = "-";
				}

				this.button[ii] = new NUi.Button(this.deleter,null,0,Click,ii);
				this.button[ii].SetTexture(Resources.Load<Texture2D>("button"));
				this.button[ii].SetRect(t_x,t_y,t_w,t_h);
				this.button[ii].SetText(t_name);
			}
		}
	}

	/** ライブラリ停止。
	*/
	public void DeleteLibInstance()
	{
		//オーディオ。
		NAudio.Audio.DeleteInstance();

		//ブルーム。
		NBloom.Bloom.DeleteInstance();

		//ブラー。
		NBlur.Blur.DeleteInstance();

		//ダウンロード。
		NDownLoad.DownLoad.DeleteInstance();

		//イベントプレート。
		NEventPlate.EventPlate.DeleteInstance();

		//フェード。
		NFade.Fade.DeleteInstance();

		//マスウ。
		NInput.Mouse.DeleteInstance();

		//キー。
		NInput.Key.DeleteInstance();

		//パッド。
		NInput.Pad.DeleteInstance();

		//ネットワーク。
		NNetwork.Network.DeleteInstance();

		//パフォーマンスカウンター。
		NPerformanceCounter.PerformanceCounter.DeleteInstance();

		//２Ｄ描画。
		NRender2D.Render2D.DeleteInstance();

		//セーブロード。
		NSaveLoad.SaveLoad.DeleteInstance();

		//シーン。
		NScene.Scene.DeleteInstance();

		//タスク。
		NTaskW.TaskW.DeleteInstance();

		//ＵＩ。
		NUi.Ui.DeleteInstance();

		//ＵＮＩＶＲＭ。
		NUniVrm.UniVrm.DeleteInstance();
	}

	/** 更新。
	*/
	private void FixedUpdate()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();
	}

	/** クリック。
	*/
	public void Click(int a_id)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(this.scene_list[a_id]);
	}

	/** シーン遷移。
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();

		//ライブラリ停止。
		this.DeleteLibInstance();
	}

	/** シーン名。
	*/
	public void CallFromHTML(string a_scene_name)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(a_scene_name);
	}

	/** 追加。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/main/EditSceneList")]
	private static void EditSceneList()
	{
		List<UnityEditor.EditorBuildSettingsScene> t_list = new List<UnityEditor.EditorBuildSettingsScene>();

		t_list.Add(new UnityEditor.EditorBuildSettingsScene("Assets/TestScene/main.unity",true));

		for(int ii=0;ii<SCENE_COUNT;ii++){
			t_list.Add(new UnityEditor.EditorBuildSettingsScene(string.Format("Assets/TestScene/test{0:D2}.unity",ii+1),true));
		}

		UnityEditor.EditorBuildSettings.scenes = t_list.ToArray();
	}
	#endif
}


/** main_base
*/
public class main_base : MonoBehaviour
{
	/** is_changescene
	*/
	public bool is_changescene = false;

	/** 戻るボタン。
	*/
	NUi.Button return_button = null;

	/** 戻るボタン作成。
	*/
	public void CreateReturnButton(NDeleter.Deleter a_deleter,long a_drawpriority)
	{
		this.return_button = new NUi.Button(a_deleter,null,a_drawpriority,Click,0);
		this.return_button.SetTexture(Resources.Load<Texture2D>("button"));
		this.return_button.SetText("Return");
		this.return_button.SetRect(0,0,80,40);
	}

	/** クリック。
	*/
	public void Click(int a_id)
	{
		this.is_changescene = true;
		this.StartCoroutine(ChangeScene());	
	}

	/** 削除前。
	*/
	public virtual bool PreDestroy(bool a_first)
	{
		return true;
	}

	/** シーン切り替え。チェック。
	*/
	public bool IsChangeScene()
	{
		return this.is_changescene;
	}

	/** シーン切り替え。
	*/
	public IEnumerator ChangeScene()
	{
		bool t_first = true;

		while(this.PreDestroy(t_first) == false){
			t_first = false;
			yield return null;
		}

		bool t_ok = false;
		while(t_ok == false){
			t_ok = true;

			if(NSaveLoad.SaveLoad.IsCreateInstance() == true){
				if(NSaveLoad.SaveLoad.GetInstance().IsBusy() == true){
					t_ok = false;
				}
			}

			if(NDownLoad.DownLoad.IsCreateInstance() == true){
				if(NDownLoad.DownLoad.GetInstance().IsBusy() == true){
					t_ok = false;
				}
			}
		
			if(NNetwork.Network.IsCreateInstance() == true){
				if(NNetwork.Network.GetInstance().IsBusy() == true){
					t_ok = false;
				}
			}

			if(t_ok == false){
				yield return null;
			}
		}

		GameObject.Destroy(this.gameObject);

		UnityEngine.SceneManagement.SceneManager.LoadScene("main");

		yield break;
	}
}

