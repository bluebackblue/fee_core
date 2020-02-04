

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。オーディオボリューム。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AudioVolumeList_Tool
	*/
	#if(UNITY_EDITOR)
	public class AudioVolumeList_Tool
	{
		/** ResourceItem
		*/
		public class ResourceItem
		{
			/** tag
			*/
			public string tag;

			/** volume

				"Assets/xxx/yyy/zzz.tty"

			*/
			public float volume;

			/** constructor
			*/
			public ResourceItem(string a_tag,float a_volume)
			{
				//tag
				this.tag = a_tag;

				//volume
				this.volume = a_volume;
			}
		}

		/** 追加。
		*/
		public static UnityEngine.GameObject Add(UnityEngine.GameObject a_prefab,ResourceItem[] a_resource_list)
		{
			try{
				//audiovolume_list
				AudioVolumeList_MonoBehaviour t_audiovolume_list = a_prefab.AddComponent<AudioVolumeList_MonoBehaviour>();

				t_audiovolume_list.tag_list = new string[a_resource_list.Length];
				t_audiovolume_list.audiovolume_list = new float[a_resource_list.Length];
				for(int ii=0;ii<t_audiovolume_list.audiovolume_list.Length;ii++){
					t_audiovolume_list.tag_list[ii] = a_resource_list[ii].tag;
					t_audiovolume_list.audiovolume_list[ii] = a_resource_list[ii].volume; 
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return a_prefab;
		}
	}
	#endif
}

