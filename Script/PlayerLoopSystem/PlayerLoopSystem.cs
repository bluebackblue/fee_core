

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
		public static void CreateInstance(CallBack a_callback)
		{
			if(s_instance == null){
				s_instance = new PlayerLoopSystem(a_callback);
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

		/** callback
		*/
		private CallBack callback;

		/** raw
		*/
		private UnityEngine_PlayerLoopSystem.PlayerLoopSystem raw;

		/** debugview
		*/
		#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
		private UnityEngine.GameObject debugview_gameobject;
		private DebugView_MonoBehaviour debugview;
		#endif

		/** [シングルトン]constructor
		*/
		private PlayerLoopSystem(CallBack a_callback)
		{
			try{
				this.callback = a_callback;

				if(this.callback != null){
					this.raw = this.callback.Get();
				}else{
					this.raw = UnityEngine_PlayerLoopSystem.PlayerLoop.GetDefaultPlayerLoop();
				}
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
					this.Apply_DebugView(ref this.raw);
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
		private void Apply_DebugView(ref UnityEngine_PlayerLoopSystem.PlayerLoopSystem a_playerloopsystem)
		{
			if(this.debugview != null){
				this.debugview.list.Clear();

				if(a_playerloopsystem.subSystemList != null){
					for(int ii=0;ii<a_playerloopsystem.subSystemList.Length;ii++){
						this.debugview.list.Add("[" + a_playerloopsystem.subSystemList[ii].type.ToString() + "]");
					
						if(a_playerloopsystem.subSystemList[ii].subSystemList != null){
							for(int jj=0;jj<a_playerloopsystem.subSystemList[ii].subSystemList.Length;jj++){
								this.debugview.list.Add("+ " + a_playerloopsystem.subSystemList[ii].subSystemList[jj].type.ToString());

								if(a_playerloopsystem.subSystemList[ii].subSystemList[jj].subSystemList != null){
									for(int kk=0;kk<a_playerloopsystem.subSystemList[ii].subSystemList[jj].subSystemList.Length;kk++){
										this.debugview.list.Add("++ " + a_playerloopsystem.subSystemList[ii].subSystemList[jj].subSystemList[kk].type.ToString());
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

		/** Find

			a_index_count == -1		: 未発見。

		*/
		private static void Find(ref UnityEngine_PlayerLoopSystem.PlayerLoopSystem a_playerloopsystem,System.Type a_target_type,out int a_index_count,out int a_index_1,out int a_index_2,out int a_index_3)
		{
			if(a_playerloopsystem.subSystemList != null){
				for(int ii=0;ii<a_playerloopsystem.subSystemList.Length;ii++){
					if(a_playerloopsystem.subSystemList[ii].type == a_target_type){
						a_index_count = 1;
						a_index_1 = ii;
						a_index_2 = -1;
						a_index_3 = -1;
						return;
					}else if(a_playerloopsystem.subSystemList[ii].subSystemList != null){
						for(int jj=0;jj<a_playerloopsystem.subSystemList[ii].subSystemList.Length;jj++){
							if(a_playerloopsystem.subSystemList[ii].subSystemList[jj].type == a_target_type){
								a_index_count = 2;
								a_index_1 = ii;
								a_index_2 = jj;
								a_index_3 = -1;
								return;
							}else if(a_playerloopsystem.subSystemList[ii].subSystemList[jj].subSystemList != null){
								for(int kk=0;kk<a_playerloopsystem.subSystemList[ii].subSystemList[jj].subSystemList.Length;kk++){
									if(a_playerloopsystem.subSystemList[ii].subSystemList[jj].subSystemList[kk].type == a_target_type){
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
			if(this.callback != null){
				this.raw = this.callback.Get();
			}

			{
				int t_index_count;
				int t_index_1;
				int t_index_2;
				int t_index_3;
				Find(ref this.raw,a_target_type,out t_index_count,out t_index_1,out t_index_2,out t_index_3);

				switch(t_index_count){
				case 1:
					{
						System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList);
						t_sub_list.RemoveAt(t_index_1);
						this.raw.subSystemList = t_sub_list.ToArray();
					}break;
				case 2:
					{
						System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[t_index_1].subSystemList);
						t_sub_list.RemoveAt(t_index_2);
						this.raw.subSystemList[t_index_1].subSystemList = t_sub_list.ToArray();
					}break;
				case 3:
					{
						System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_sub_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList);
						t_sub_list.RemoveAt(t_index_3);
						this.raw.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList = t_sub_list.ToArray();
					}break;
				default:
					{
						Tool.Assert(false);
					}break;
				}
			}

			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
				this.Apply_DebugView(ref this.raw);
			}
			#endif

			if(this.callback != null){
				this.callback.Set(in this.raw);
			}else{
				try{
					UnityEngine_PlayerLoopSystem.PlayerLoop.SetPlayerLoop(this.raw);
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}
		}

		/** 追加。
		*/
		public void Add(AddType a_addtype,System.Type a_target_type,System.Type a_add_type,UnityEngine_PlayerLoopSystem.PlayerLoopSystem.UpdateFunction a_add_callback)
		{
			if(this.callback != null){
				this.raw = this.callback.Get();
			}

			{
				int t_index_count;
				int t_index_1;
				int t_index_2;
				int t_index_3;
				Find(ref this.raw,a_target_type,out t_index_count,out t_index_1,out t_index_2,out t_index_3);

				UnityEngine_PlayerLoopSystem.PlayerLoopSystem t_item = new UnityEngine_PlayerLoopSystem.PlayerLoopSystem();
				{
					t_item.subSystemList = null;
					t_item.type = a_add_type;
					t_item.updateDelegate = a_add_callback;
				}

				System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_list;
				int t_index;

				switch(a_addtype){
				case AddType.AddFirst:
				case AddType.AddLast:
					{
						t_index = -1;

						switch(t_index_count){
						case 1:
							{
								t_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[t_index_1].subSystemList);
							}break;
						case 2:
							{
								t_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList);
							}break;
						case 3:
							{
								t_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList[t_index_3].subSystemList);
							}break;
						default:
							{
								Tool.Assert(false);
								t_list = null;
							}break;
						}
					}break;
				case AddType.AddBefore:
				case AddType.AddAfter:
					{
						switch(t_index_count){
						case 1:
							{
								t_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList);
								t_index = t_index_1;
							}break;
						case 2:
							{
								t_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[t_index_1].subSystemList);
								t_index = t_index_2;
							}break;
						case 3:
							{
								t_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList);
								t_index = t_index_3;
							}break;
						default:
							{
								Tool.Assert(false);
								t_list = null;
								t_index = -1;
							}break;
						}
					}break;
				default:
					{
						Tool.Assert(false);
						t_list = null;
						t_index = -1;
					}break;
				}

				switch(a_addtype){
				case AddType.AddFirst:
					{
						t_list.Insert(0,t_item);
					}break;
				case AddType.AddLast:
					{
						t_list.Add(t_item);
					}break;
				case AddType.AddBefore:
					{
						t_list.Insert(t_index,t_item);
					}break;
				case AddType.AddAfter:
					{
						t_list.Insert(t_index + 1,t_item);
					}break;
				}				

				switch(a_addtype){
				case AddType.AddFirst:
				case AddType.AddLast:
					{
						switch(t_index_count){
						case 1:
							{
								this.raw.subSystemList[t_index_1].subSystemList = t_list.ToArray();
							}break;
						case 2:
							{
								this.raw.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList = t_list.ToArray();
							}break;
						case 3:
							{
								this.raw.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList[t_index_3].subSystemList = t_list.ToArray();
							}break;
						default:
							{
								t_list = null;
								Tool.Assert(false);
							}break;
						}
					}break;
				case AddType.AddBefore:
				case AddType.AddAfter:
					{
						switch(t_index_count){
						case 1:
							{
								this.raw.subSystemList = t_list.ToArray();
							}break;
						case 2:
							{
								this.raw.subSystemList[t_index_1].subSystemList = t_list.ToArray();
							}break;
						case 3:
							{
								this.raw.subSystemList[t_index_1].subSystemList[t_index_2].subSystemList = t_list.ToArray();
							}break;
						default:
							{
								Tool.Assert(false);
							}break;
						}
					}break;
				}
			}

			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
				this.Apply_DebugView(ref this.raw);
			}
			#endif

			if(this.callback != null){
				this.callback.Set(in this.raw);
			}else{
				try{
					UnityEngine_PlayerLoopSystem.PlayerLoop.SetPlayerLoop(this.raw);
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}
		}
	}
}

