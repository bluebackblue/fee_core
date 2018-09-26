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

	/** タスク。
	*/
	#if false
	private System.Threading.SynchronizationContext sync_contest;
	private System.Threading.Tasks.Task<bool> task;
	private int count;
	private int count_old;
	#endif

	/** TaskMain
	*/
	#if false
	private async System.Threading.Tasks.Task<bool> TaskMain()
	{
		await System.Threading.Tasks.Task.Delay(1000);


		System.Threading.SynchronizationContext.SetSynchronizationContext(this.sync_contest);
		await System.Threading.Tasks.Task.Delay(1);
		{
			//同期。
			if(this.gameObject.GetComponent<Transform>().position.x > 1){
				Debug.Log("x");
			}
		}

		await System.Threading.Tasks.Task.Delay(1);

		this.sync_contest.Post((a_state) => {
			if(a_state != null){
				Debug.Log("state = " + a_state.ToString());
			}
			this.AddCount();
		},null);

		await System.Threading.Tasks.Task.Delay(1000);
		await System.Threading.Tasks.Task.Delay(1000);
		await System.Threading.Tasks.Task.Delay(1000);
		await System.Threading.Tasks.Task.Delay(1000);

		Debug.Log("");

		return true;
	}
	#endif

	/** 追加。
	*/
	#if false
	public void AddCount()
	{
		this.count++;
	}
	#endif

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

		//タスク。
		#if false
		this.task = null;
		this.count = 0;
		this.count_old = 0;
		#endif
	}

	/** //ライブラリ停止。
	*/
	public void DeleteLibInstance()
	{
		//オーディオ。
		NAudio.Audio.DeleteInstance();

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

		//２Ｄ描画。インスタンス削除。
		NRender2D.Render2D.DeleteInstance();

		//セーブロード。
		NSaveLoad.SaveLoad.DeleteInstance();

		//シーン。
		NScene.Scene.DeleteInstance();

		//ネットワーク。
		NNetwork.Network.DeleteInstance();

		//パフォーマンスカウンター。
		NPerformanceCounter.PerformanceCounter.DeleteInstance();
	}

	/** FiexUpdate
	*/
	#if false
	private void FixedUpdate()
	{
		if(this.task != null){
			if(this.count_old != this.count){
				if(this.task.IsCompleted || this.task.IsCanceled || this.task.IsFaulted){
					Debug.Log(this.count.ToString() + " " + this.task.Result.ToString());

					this.task.Wait();
					System.Threading.Tasks.Task.WaitAll(this.task);

					this.task = null;
				}
			}

		}else{
			this.count_old = this.count;
			this.sync_contest = System.Threading.SynchronizationContext.Current;
			this.task = System.Threading.Tasks.Task.Run<bool>(() => {return this.TaskMain();});
		}
	}
	#endif

	/** デバッグ表示。
	*/
	private void OnGUI()
    {
		//ii_max
		int ii_max = 50;

		for(int ii=0;ii<ii_max;ii++){
			int t_xx_max = Screen.width / 100;
			int t_xx = ii % t_xx_max;
			int t_yy = ii / t_xx_max;

			string t_name = null;
			
			if(ii < this.scene_list.Length){
				t_name = this.scene_list[ii];
			}

			int t_x = 30 + t_xx * 100;
			int t_y = 30 + t_yy * 60;
			int t_w = 80;
			int t_h = 40;

			string t_button_string = t_name;
			if(t_button_string == null){
				t_button_string = "-";
			}
			
			if(GUI.Button(new Rect(t_x,t_y,t_w,t_h),t_button_string) == true){
				if(t_name != null){
					UnityEngine.SceneManagement.SceneManager.LoadScene(t_name);
				}
			}
		}
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

	/** デバッグ描画。
	*/
	public void OnGUI()
    {
		if(this.is_changescene == false){
			int t_x = 30;
			int t_y = 30;
			int t_w = 80;
			int t_h = 40;

			if(GUI.Button(new Rect(t_x,t_y,t_w,t_h),"return") == true){
				this.is_changescene = true;
				StartCoroutine(ChangeScene());
			}
		}
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

