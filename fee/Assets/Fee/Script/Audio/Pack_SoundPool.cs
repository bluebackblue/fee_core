using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。パック。
*/


/** NAudio
*/
namespace NAudio
{
	/** Pack_SoundPool
	*/
	public class Pack_SoundPool
	{
		/** データハッシュ。
		*/
		public int data_hash;

		/** データバージョン。
		*/
		public uint data_version;

		/** name
		*/
		public List<string> name_list;

		/** volume_list
		*/
		public List<float> volume_list;

		/** constructor
		*/
		public Pack_SoundPool()
		{
			//name_list
			this.name_list = new List<string>();

			//volume_list
			this.volume_list = new List<float>();
		}
	}
}

