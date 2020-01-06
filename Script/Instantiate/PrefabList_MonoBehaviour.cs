

/**
 * @brief プレハブリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** PrefabList_MonoBehaviour
	*/
	public class PrefabList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** prefab_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.GameObject[] prefab_list;

		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.GameObject> list;

		/** 初期化。
		*/
		public void Initialize()
		{
			try{
				this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.GameObject>();
				for(int ii=0;ii<this.tag_list.Length;ii++){
					this.list.Add(this.tag_list[ii],this.prefab_list[ii]);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** 取得。
		*/
		public UnityEngine.GameObject GetItem(string a_tag)
		{
			UnityEngine.GameObject t_value;

			if(this.list.TryGetValue(a_tag,out t_value) == false){
				Tool.Assert(false);
				t_value = null;
			}

			return t_value;
		}
	}
}

