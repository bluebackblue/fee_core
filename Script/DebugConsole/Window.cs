

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デバッグコンソール。ウィンドウ。
*/


/** Fee.DebugConsole
*/
#if(UNITY_EDITOR)
namespace Fee.DebugConsole
{
	/** Window
	*/
	public class Window : UnityEditor.EditorWindow
	{
		/** constructor
		*/
		private Window()
		{
			UnityEngine.Debug.Log("Window.constructor");
			s_instance = this;

			this.head_box = null;
		}

		/** s_instance
		*/
		private static Window s_instance = null;

		/** head
		*/
		private UnityEngine.UIElements.Button head_clear_button;
		private UnityEngine.UIElements.Button head_test1_button;
		private UnityEngine.UIElements.Button head_test2_button;

		/** head
		*/
		private UnityEngine.UIElements.Box head_box;

		/** リストアイテム作成。
		*/
		private static System.Func<UnityEngine.UIElements.VisualElement> s_func_makeitem = () => {
			UnityEngine.UIElements.Box t_box = new UnityEngine.UIElements.Box();

			{
				UnityEngine.UIElements.Label t_label = new UnityEngine.UIElements.Label();
				t_box.Add(t_label);
			}

			{
				UnityEngine.UIElements.Label t_label = new UnityEngine.UIElements.Label();
				t_box.Add(t_label);
			}

			return t_box;
		};

		/** リストアイテム設定。
		*/
		private static System.Action<UnityEngine.UIElements.VisualElement,int> s_func_binditem = (a_a_viewitem,a_a_index) => {
			Item t_item = Data.GetList()[a_a_index];
			UnityEngine.UIElements.Box t_box = a_a_viewitem as UnityEngine.UIElements.Box;

			{
				UnityEngine.UIElements.Label t_label = t_box.ElementAt(0) as UnityEngine.UIElements.Label;
				t_label.text = t_item.text;
				t_label.style.backgroundColor = new UnityEngine.UIElements.StyleColor(new UnityEngine.Color(0.0f,0.0f,0.0f,0.3f));
			}

			{
				UnityEngine.UIElements.Label t_label = t_box.ElementAt(1) as UnityEngine.UIElements.Label;
				t_label.text = "multi";
			}

			ListItemEvent t_mouse = new ListItemEvent(t_item);
			t_mouse.target = t_box;
		};

		/** OnEnable
		*/
		private void OnEnable()
		{
			UnityEngine.Debug.Log("Window.OnEnable");

			//ヘッド。
			{
				UnityEngine.UIElements.Box t_head_box = new UnityEngine.UIElements.Box();
				{
					//クリア。
					{
						UnityEngine.UIElements.Button t_button = new UnityEngine.UIElements.Button(() => {
							DebugConsole.Clear();
						});
						t_button.text = "Clear";
						this.head_clear_button = t_button;
						t_head_box.Add(t_button);
					}

					//テスト１。
					{
						UnityEngine.UIElements.Button t_button = new UnityEngine.UIElements.Button(() => {
							DebugConsole.Log("Assets/ThirdParty/Open/Fee/Script/DebugConsole/ListItemEvent.cs(51):テスト");
						});
						t_button.text = "LogReCreate";
						this.head_test1_button = t_button;
						t_head_box.Add(t_button);
					}

					//テスト２。
					{
						UnityEngine.UIElements.Button t_button = new UnityEngine.UIElements.Button(() => {
							DebugConsole.Log("Assets/ThirdParty/Open/Fee/Script/DebugConsole/ListItemEvent.cs(51)");
						});
						t_button.text = "LogAdd";
						this.head_test2_button = t_button;
						t_head_box.Add(t_button);
					}
				}
				this.head_box = t_head_box;
				this.rootVisualElement.Add(t_head_box);
			}

			//リスト。
			{
				UnityEngine.UIElements.ListView t_listview = new UnityEngine.UIElements.ListView(Data.GetList(),64,s_func_makeitem,s_func_binditem);
				{
					t_listview.style.flexGrow = 1.0f;
					t_listview.selectionType = UnityEngine.UIElements.SelectionType.Single;
				}
				this.rootVisualElement.Add(t_listview);
			}
		}

		/** ClearList
		*/
		public static void ClearList()
		{
			if(s_instance != null){
				//リスト。
				{
					s_instance.rootVisualElement.RemoveAt(1);
					UnityEngine.UIElements.ListView t_listview = new UnityEngine.UIElements.ListView();
					{
						t_listview.style.flexGrow = 1.0f;
						t_listview.selectionType = UnityEngine.UIElements.SelectionType.Single;
					}
					s_instance.rootVisualElement.Insert(1,t_listview);
				}
			}
		}

		/** ReCreate
		*/
		public static void ReCreateList()
		{
			if(s_instance != null){
				//リスト。
				{
					s_instance.rootVisualElement.RemoveAt(1);
					UnityEngine.UIElements.ListView t_listview = new UnityEngine.UIElements.ListView(Data.GetList(),64,s_func_makeitem,s_func_binditem);
					{
						t_listview.style.flexGrow = 1.0f;
						t_listview.selectionType = UnityEngine.UIElements.SelectionType.Single;
					}
					s_instance.rootVisualElement.Insert(1,t_listview);
				}
			}
		}
	}
}
#endif

