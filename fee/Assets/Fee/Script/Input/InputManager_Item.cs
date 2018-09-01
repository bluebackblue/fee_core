using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。インプットマネージャ。
*/


/** NInput
*/
namespace NInput
{
	/** InputManager_Item
	*/
	public class InputManager_Item
	{
		/** member
		*/
		public string m_Name;
		public string descriptiveName;
		public string descriptiveNegativeName;
		public string negativeButton;
		public string positiveButton;
		public string altNegativeButton;
		public string altPositiveButton;
		public float gravity;
		public float dead;
		public float sensitivity;
		public bool snap;
		public bool invert;
		public int type;
		public int axis;
		public int joyNum;

		/** constructor
		*/
		public InputManager_Item()
		{
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.0f;
			this.sensitivity = 0.0f;
			this.snap = false;
			this.invert = false;
			this.type = 0;
			this.axis = 0;
			this.joyNum = 0;
		}
	}
}

