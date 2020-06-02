

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プレイヤーループシステム。
*/


/** Fee.PlayerLoopSystem
*/
namespace Fee.PlayerLoopSystem
{
	/** UnityEngine_PlayerLoopSystem
	*/
	#if((UNITY_2018)||(UNITY_2019_2))
	using UnityEngine_PlayerLoopSystem = UnityEngine.Experimental.LowLevel;
	#else
	using UnityEngine_PlayerLoopSystem = UnityEngine.LowLevel;
	#endif

	/** PlayerLoopSystem
	*/
	public class PlayerLoopSystem
	{
		/** [シングルトン]s_instance
		*/
		private static PlayerLoopSystem s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new PlayerLoopSystem();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static PlayerLoopSystem GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** playerloop
		*/
		private UnityEngine_PlayerLoopSystem.PlayerLoopSystem playerloop;

		/** debugview
		*/
		#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
		private UnityEngine.GameObject debugview_gameobject;
		private DebugView_MonoBehaviour debugview;
		#endif

		/** [シングルトン]constructor
		*/
		private PlayerLoopSystem()
		{
			try{
				this.playerloop = UnityEngine_PlayerLoopSystem.PlayerLoop.GetDefaultPlayerLoop();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
				if(Config.DEBUG_VIEW_FLAG == true){
					this.debugview_gameobject = new UnityEngine.GameObject("playerloopsystem_debugview");
					UnityEngine.GameObject.DontDestroyOnLoad(this.debugview_gameobject);
					this.debugview = this.debugview_gameobject.AddComponent<DebugView_MonoBehaviour>();
					this.debugview.list = new System.Collections.Generic.List<string>();
					this.Apply_DebugView();
				}else{
					this.debugview_gameobject = null;
					this.debugview = null;
				}
			}
			#endif
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
				if(this.debugview_gameobject != null){
					UnityEngine.GameObject.DestroyImmediate(this.debugview_gameobject);
					this.debugview_gameobject = null;
				}
			}
			#endif
		}

		/** Apply_DebugView
		*/
		#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
		public void Apply_DebugView()
		{
			if(this.debugview != null){
				this.debugview.list.Clear();

				if(this.playerloop.subSystemList != null){
					for(int ii=0;ii<this.playerloop.subSystemList.Length;ii++){
						this.debugview.list.Add("[" + this.playerloop.subSystemList[ii].type.ToString() + "]");
					
						if(this.playerloop.subSystemList[ii].subSystemList != null){
							for(int jj=0;jj<this.playerloop.subSystemList[ii].subSystemList.Length;jj++){
								this.debugview.list.Add("+ " + this.playerloop.subSystemList[ii].subSystemList[jj].type.ToString());

								if(this.playerloop.subSystemList[ii].subSystemList[jj].subSystemList != null){
									for(int kk=0;kk<this.playerloop.subSystemList[ii].subSystemList[jj].subSystemList.Length;kk++){
										this.debugview.list.Add("++ " + this.playerloop.subSystemList[ii].subSystemList[jj].subSystemList[kk].type.ToString());
									}
								}
							}
						}

						this.debugview.list.Add("");
					}
				}
			}
		}
		#endif

		/** 適応。
		*/
		public void Apply()
		{
			try{
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					this.Apply_DebugView();
				}
				#endif

				UnityEngine_PlayerLoopSystem.PlayerLoop.SetPlayerLoop(this.playerloop);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** Find

			a_index_count == -1		: 未発見。

		*/
		private void Find(System.Type a_target_type,out int a_index_count,out int a_index_1,out int a_index_2,out int a_index_3)
		{
			if(this.playerloop.subSystemList != null){
				for(int ii=0;ii<this.playerloop.subSystemList.Length;ii++){
					if(this.playerloop.subSystemList[ii].type == a_target_type){
						a_index_count = 1;
						a_index_1 = ii;
						a_index_2 = -1;
						a_index_3 = -1;
						return;
					}else if(this.playerloop.subSystemList[ii].subSystemList != null){
						for(int jj=0;jj<this.playerloop.subSystemList[ii].subSystemList.Length;jj++){
							if(this.playerloop.subSystemList[ii].subSystemList[jj].type == a_target_type){
								a_index_count = 2;
								a_index_1 = ii;
								a_index_2 = jj;
								a_index_3 = -1;
								return;
							}else if(this.playerloop.subSystemList[ii].subSystemList[jj].subSystemList != null){
								for(int kk=0;kk<this.playerloop.subSystemList[ii].subSystemList[jj].subSystemList.Length;kk++){
									if(this.playerloop.subSystemList[ii].subSystemList[jj].subSystemList[kk].type == a_target_type){
										a_index_count = 3;
										a_index_1 = ii;
										a_index_2 = jj;
										a_index_3 = -1;
										return;
									}else{
										Tool.Assert(false);
									}
								}
							}
						}
					}
				}
			}

			a_index_count = -1;
			a_index_1 = -1;
			a_index_2 = -1;
			a_index_3 = -1;
			return;
		}

		/** 削除。
		*/
		public void RemoveFromType(System.Type a_target_type)
		{
			int t_index_count;
			int t_index_1;
			int t_index_2;
			int t_index_3;
			this.Find(a_target_type,out t_index_count,out t_index_1,out t_index_2,out t_index_3);

			switch(t_index_count){
			case 1:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList);
					t_sub_list.RemoveAt(t_index_1);
					this.playerloop.subSystemList = t_sub_list.ToArray();
				}break;
			case 2:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList);
					t_sub_list.RemoveAt(t_index_2);
					this.playerloop.subSystemList[t_index_1].subSystemList = t_sub_list.ToArray();
				}break;
			case 3:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList);
					t_sub_list.RemoveAt(t_index_3);
					this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList = t_sub_list.ToArray();
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}
		}

		/** ターゲットのサブリストの最初に追加する。
		*/
		public void AddFirst(System.Type a_target_type,System.Type a_add_type,UnityEngine_PlayerLoopSystem.PlayerLoopSystem.UpdateFunction a_add_callback)
		{
			int t_index_count;
			int t_index_1;
			int t_index_2;
			int t_index_3;
			this.Find(a_target_type,out t_index_count,out t_index_1,out t_index_2,out t_index_3);

			UnityEngine_PlayerLoopSystem.PlayerLoopSystem t_item = new UnityEngine_PlayerLoopSystem.PlayerLoopSystem();
			{
				t_item.subSystemList = null;
				t_item.type = a_add_type;
				t_item.updateDelegate = a_add_callback;
			}

			switch(t_index_count){
			case 1:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_target_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList);
					t_target_list.Insert(0,t_item);
					this.playerloop.subSystemList[t_index_1].subSystemList = t_target_list.ToArray();
				}break;
			case 2:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_target_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList);
					t_target_list.Insert(0,t_item);
					this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList = t_target_list.ToArray();
				}break;
			case 3:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_target_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList[t_index_3].subSystemList);
					t_target_list.Insert(0,t_item);
					this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList[t_index_3].subSystemList = t_target_list.ToArray();
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}
		}

		/** ターゲットの前に追加。
		*/
		public void AddBefor(System.Type a_target_type,System.Type a_add_type,UnityEngine_PlayerLoopSystem.PlayerLoopSystem.UpdateFunction a_add_callback)
		{
			int t_index_count;
			int t_index_1;
			int t_index_2;
			int t_index_3;
			this.Find(a_target_type,out t_index_count,out t_index_1,out t_index_2,out t_index_3);

			UnityEngine_PlayerLoopSystem.PlayerLoopSystem t_item = new UnityEngine_PlayerLoopSystem.PlayerLoopSystem();
			{
				t_item.subSystemList = null;
				t_item.type = a_add_type;
				t_item.updateDelegate = a_add_callback;
			}

			switch(t_index_count){
			case 1:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList);
					t_sub_list.Insert(t_index_1,t_item);
					this.playerloop.subSystemList = t_sub_list.ToArray();
				}break;
			case 2:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList);
					t_sub_list.Insert(t_index_2,t_item);
					this.playerloop.subSystemList[t_index_1].subSystemList = t_sub_list.ToArray();
				}break;
			case 3:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList);
					t_sub_list.Insert(t_index_3,t_item);
					this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList = t_sub_list.ToArray();
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}
		}

		/** ターゲットの次の追加。
		*/
		public void AddAfter(System.Type a_target_type,System.Type a_add_type,UnityEngine_PlayerLoopSystem.PlayerLoopSystem.UpdateFunction a_add_callback)
		{
			int t_index_count;
			int t_index_1;
			int t_index_2;
			int t_index_3;
			this.Find(a_target_type,out t_index_count,out t_index_1,out t_index_2,out t_index_3);

			UnityEngine_PlayerLoopSystem.PlayerLoopSystem t_item = new UnityEngine_PlayerLoopSystem.PlayerLoopSystem();
			{
				t_item.subSystemList = null;
				t_item.type = a_add_type;
				t_item.updateDelegate = a_add_callback;
			}

			switch(t_index_count){
			case 1:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList);
					t_sub_list.Insert(t_index_1 + 1,t_item);
					this.playerloop.subSystemList = t_sub_list.ToArray();
				}break;
			case 2:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList);
					t_sub_list.Insert(t_index_2 + 1,t_item);
					this.playerloop.subSystemList[t_index_1].subSystemList = t_sub_list.ToArray();
				}break;
			case 3:
				{
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList);
					t_sub_list.Insert(t_index_3 + 1,t_item);
					this.playerloop.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList = t_sub_list.ToArray();
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}
		}
	}
}

