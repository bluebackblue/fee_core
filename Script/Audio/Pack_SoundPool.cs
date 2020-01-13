

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オーディオ。パック。
*/


/** Fee.Audio
*/
namespace Fee.Audio
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

		/** name_list
		*/
		public System.Collections.Generic.List<string> name_list;

		/** volume_list
		*/
		public System.Collections.Generic.List<float> volume_list;

		/** fullpath_list
		*/
		[Fee.JsonItem.Ignore]
		public System.Collections.Generic.List<File.Path> fullpath_list;

		/** constructor
		*/
		public Pack_SoundPool()
		{
			//data_hash
			this.data_hash = 0;

			//data_version
			this.data_version = 0;

			//name_list
			this.name_list = new System.Collections.Generic.List<string>();

			//volume_list
			this.volume_list = new System.Collections.Generic.List<float>();

			//fullpath_list
			this.fullpath_list = new System.Collections.Generic.List<File.Path>();
		}
	}
}

