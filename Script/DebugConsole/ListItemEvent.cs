

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デバッグコンソール。
*/


/** Fee.DebugConsole
*/
namespace Fee.DebugConsole
{
	/** ListItemEvent
	*/
	public class ListItemEvent : UnityEngine.UIElements.Manipulator
	{
		/** item
		*/
		private Item item;

		/** constructor
		*/
		public ListItemEvent(Item a_item)
		{
			this.item = a_item;
		}

		/** RegisterCallbacksOnTarget
		*/
		protected override void RegisterCallbacksOnTarget()
		{
			this.target.RegisterCallback<UnityEngine.UIElements.MouseDownEvent>(this.OnMouseEvent);
		}

		/** UnregisterCallbacksFromTarget
		*/
		protected override void UnregisterCallbacksFromTarget()
		{
			target.UnregisterCallback<UnityEngine.UIElements.MouseDownEvent>(this.OnMouseEvent);
		}

		/** OnMouseEvent
		*/
		private void OnMouseEvent(UnityEngine.UIElements.MouseEventBase<UnityEngine.UIElements.MouseDownEvent> a_event)
		{
			switch(a_event.clickCount){
			case 2:
				{
					//コードジャンプ。
					{
						System.Text.RegularExpressions.Regex t_regex = new System.Text.RegularExpressions.Regex("^(?<path>.*)\\((?<line>[0-9]*)\\)(\\:)?(?<comment>.*)$");
						System.Text.RegularExpressions.Match t_match = t_regex.Match(this.item.text);
						if(t_match.Success == true){
							string t_path = "";
							int t_line = 0;
							string t_comment = "";

							foreach(System.Text.RegularExpressions.Group t_group in t_match.Groups){
								switch(t_group.Name){
								case "path":
									{
										t_path = t_group.Value;
									}break;
								case "line":
									{
										if(int.TryParse(t_group.Value,out t_line) == false){
											t_line = 0;
										}
									}break;
								case "comment":
									{
										t_comment = t_group.Value;
									}break;
								}
							}

							Unity.CodeEditor.CodeEditor.CurrentEditor.OpenProject(t_path,t_line,-1);
						}
					}
				}break;
			}
		}
	}
}

