

/**
 * @brief アニメーションクリップリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AnimationClipList_MonoBehaviour
	*/
	public class AnimationClipList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** animationclip_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.AnimationClip[] animationclip_list;

		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.AnimationClip> list;

		/** 初期化。
		*/
		public void Initialize()
		{
			try{
				this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.AnimationClip>();
				for(int ii=0;ii<this.tag_list.Length;ii++){
					this.list.Add(this.tag_list[ii],this.animationclip_list[ii]);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** 取得。
		*/
		public UnityEngine.AnimationClip GetItem(string a_tag)
		{
			UnityEngine.AnimationClip t_value;

			if(this.list.TryGetValue(a_tag,out t_value) == false){
				Tool.Assert(false);
				t_value = null;
			}

			return t_value;
		}
	}
}

