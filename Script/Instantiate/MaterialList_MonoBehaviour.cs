

/**
 * @brief マテリアルリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** MaterialList_MonoBehaviour
	*/
	public class MaterialList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** material_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.Material[] material_list;

		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.Material> list;

		/** 初期化。
		*/
		public void Initialize()
		{
			try{
				this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.Material>();
				for(int ii=0;ii<this.tag_list.Length;ii++){
					this.list.Add(this.tag_list[ii],this.material_list[ii]);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** 取得。
		*/
		public UnityEngine.Material GetItem(string a_tag)
		{
			UnityEngine.Material t_value;

			if(this.list.TryGetValue(a_tag,out t_value) == false){
				Tool.Assert(false);
				t_value = null;
			}

			return t_value;
		}
	}
}

