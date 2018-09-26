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


/** [XTask]XSynchronizationContext
*/
public class XSynchronizationContext
{
	/** context
	*/
	public System.Threading.SynchronizationContext context;

	/** constructor
	*/
	public XSynchronizationContext()
	{
		this.context = System.Threading.SynchronizationContext.Current;
	}

	/** 同期。
	*/
	public void SetSynchronizationContext()
	{
		System.Threading.SynchronizationContext.SetSynchronizationContext(this.context);
	}

	/** Post
	*/
	public void Post(System.Threading.SendOrPostCallback a_d,object a_state)
	{
		this.context.Post(a_d,a_state);
	}

	/** 削除。
	*/
	public void Delete()
	{
		this.context = null;
	}

};

/** [XTask]XTask
*/
public class XTask
{
	public static System.Threading.Tasks.Task Delay(int millisecondsDelay)
	{
		return System.Threading.Tasks.Task.Delay(1000);
	}
}

/** [XTask]XTask
*/
public class XTask<TResult> : XTask
{
	/** task
	*/
	System.Threading.Tasks.Task<TResult> task;

	/** XTask
	*/
	public XTask(System.Threading.Tasks.Task<TResult> a_task)
	{
		this.task = a_task;
	}

	/** XTask
	*/
	public XTask(System.Func<System.Threading.Tasks.Task<TResult>> a_function)
	{
		this.task = System.Threading.Tasks.Task.Run(a_function);
	}

	/** IsCompleted
	*/
	public bool IsCompleted()
	{
		return this.task.IsCompleted;
	}

	/** IsCanceled
	*/
	public bool IsCanceled()
	{
		return this.task.IsCanceled;
	}

	/** IsFaulted
	*/
	public bool IsFaulted()
	{
		return  this.task.IsFaulted;
	}

	/** GetResult
	*/
	public TResult GetResult()
	{
		return this.task.Result;
	}

	/** Dispose
	*/
	public void Dispose()
	{
		this.task.Dispose();
		this.task = null;
	}
}

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

	/** [XTask]タスク。
	*/
	#if true
	private static XSynchronizationContext sync_context;
	private XTask<bool> task;
	private int count;
	private int count_old;
	#endif

	/** [XTask]TaskMain
	*/
	#if true
	private async System.Threading.Tasks.Task<bool> TaskMain()
	{
		try{
			await XTask.Delay(1000);

			main.sync_context.SetSynchronizationContext();
			await XTask.Delay(1);
			{
				//同期。

				/*if(this != null)*/{
					if(this.gameObject.GetComponent<Transform>().position.x > 1){
						Debug.Log("x");
					}
				}
			}

			await XTask.Delay(1);

			main.sync_context.Post((a_state) => {
				if(this != null){
					this.AddCount();
				}else{
					Debug.Log("null");
				}
			},null);

			await XTask.Delay(1000);
			await XTask.Delay(1000);
			await XTask.Delay(1000);
			await XTask.Delay(1000);

			Debug.Log("");
		}catch(System.Exception t_exception){
			Debug.Log("catch : " + t_exception.ToString());
		}

		return true;
	}
	#endif

	/** [XTask]追加。
	*/
	#if true
	public void AddCount()
	{
		Debug.Log("AddCount");
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

		//[XTask]タスク。
		#if true
		main.sync_context = new XSynchronizationContext();
		this.task = null;
		this.count = 0;
		this.count_old = 0;
		#endif
	}

	/** ライブラリ停止。
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

	/** [XTask]FiexUpdate
	*/
	#if true
	private void FixedUpdate()
	{
		if(this.task != null){
			if(this.count_old != this.count){
				if((this.task.IsCompleted() == true)||(this.task.IsCanceled() == true)||(this.task.IsFaulted() == true)){
					Debug.Log(this.count.ToString() + " " + this.task.GetResult().ToString());
					this.task = null;
				}
			}

		}else{
			Debug.Log("new Task");
			this.count_old = this.count;
			this.task = new XTask<bool>(() => {return this.TaskMain();});
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

