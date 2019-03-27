

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief イベントプレート。イベントタイプ。
*/


/** Fee.EventPlate
*/
namespace Fee.EventPlate
{
	/** EventType
	*/
	public enum EventType
	{
		Window = 0,

		View,

		ViewItem,

		Button,

		Max
	}

	/** EventTypeMask
	*/
	public enum EventTypeMask
	{
		Window = 0x01,

		View = 0x02,

		ViewItem = 0x04,

		Button = 0x08,

		All = Window | View | ViewItem | Button,

		//3
		NotWindow =  All ^ Window,
		NotView =  All ^ Window,
		NotViewItem =  All ^ Window,
		NotButton =  All ^ Button,

		//2
		Window_View = Window | View,
		Window_ViewItem = Window | ViewItem,
		Window_Button = Window | Button,
		View_ViewItem = View | ViewItem,
		View_Button = View | Button,
		ViewItem_Button = ViewItem | Button,
	}
}

