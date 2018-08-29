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
	private readonly int SCENE_COUNT = 10;

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
	}

	/** //ライブラリ停止。
	*/
	public void DeleteLibInstance()
	{
		//TODO:
	}
	
	/** デバッグ表示。
	*/
    void OnGUI()
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
}


/** main_base
*/
public class main_base : MonoBehaviour
{
	/** デバッグ描画。
	*/
    void OnGUI()
    {
		int t_x = 30;
		int t_y = 30;
		int t_w = 80;
		int t_h = 40;

		if(GUI.Button(new Rect(t_x,t_y,t_w,t_h),"return") == true){
			GameObject.Destroy(this.gameObject);
			UnityEngine.SceneManagement.SceneManager.LoadScene("main");
		}
	}
}

