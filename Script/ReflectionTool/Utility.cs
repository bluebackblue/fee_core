

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief リフレクションツール。
*/


/** Fee.ReflectionTool
*/
namespace Fee.ReflectionTool
{
	/** Utility
	*/
	public class Utility
	{
		/** メンバーリスト。取得。

			継承元は含まない

		*/
		public static System.Reflection.MemberInfo[] GetMemberList(System.Type a_type)
		{
			System.Reflection.MemberInfo[] t_memberinfo_list = null;

			try{
				t_memberinfo_list = a_type.GetMembers(
				
					//指定した型の階層のレベルで宣言されたメンバーのみを対象にすることを指定します。 継承されたメンバーは対象になりません。
					System.Reflection.BindingFlags.DeclaredOnly |
							
					//インスタンス メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Instance |
							
					//パブリック メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Public |
							
					//パブリック メンバー以外のメンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.NonPublic

				);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_memberinfo_list;
		}

		/** メソッドリスト。取得。

			継承元は含まない

		*/
		public static System.Reflection.MethodInfo[] GetMethodList(System.Type a_type)
		{
			System.Reflection.MethodInfo[] t_methodinfo_list = null;

			try{
				//取得。
				t_methodinfo_list = a_type.GetMethods(
				
					//指定した型の階層のレベルで宣言されたメンバーのみを対象にすることを指定します。 継承されたメンバーは対象になりません。
					System.Reflection.BindingFlags.DeclaredOnly |
							
					//インスタンス メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Instance |
							
					//パブリック メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Public |
							
					//パブリック メンバー以外のメンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.NonPublic

				);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo_list;
		}

		/** メソッド。検索。

			継承元は含まない

		*/
		public static System.Reflection.MethodInfo FindMethodFromParamList(System.Type a_type,string a_name,System.Type[] a_parameter_list)
		{
			System.Reflection.MethodInfo t_result = null;

			try{
				System.Reflection.MethodInfo[] t_methodinfo_list = GetMethodList(a_type);
				if(t_methodinfo_list != null){
					foreach(System.Reflection.MethodInfo t_methodinfo in t_methodinfo_list){
						if(a_name == t_methodinfo.Name){
							System.Reflection.ParameterInfo[] t_parameterinfo_list = t_methodinfo.GetParameters();
							if(a_parameter_list.Length == t_parameterinfo_list.Length){

								//パラメータチェック。
								bool t_check = true;
								for(int ii=0;ii<t_parameterinfo_list.Length;ii++){
									if(a_parameter_list[ii] != t_parameterinfo_list[ii].ParameterType){
										t_check = false;
										break;
									}
								}

								if(t_check == true){
									t_result = t_methodinfo;
									break;
								}
							}
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_result;
		}

		/** メソッド。パラメータ１。検索。

			継承元は含まない

		*/
		public static System.Reflection.MethodInfo FindMethodFromParam1(System.Type a_type,string a_name,System.Type a_parameter_1)
		{
			System.Reflection.MethodInfo t_result = null;

			try{
				System.Reflection.MethodInfo[] t_methodinfo_list = GetMethodList(a_type);
				if(t_methodinfo_list != null){
					foreach(System.Reflection.MethodInfo t_methodinfo in t_methodinfo_list){
						if(a_name == t_methodinfo.Name){
							System.Reflection.ParameterInfo[] t_parameterinfo_list = t_methodinfo.GetParameters();
							if(1 == t_parameterinfo_list.Length){

								//パラメータチェック。
								if(a_parameter_1 != t_parameterinfo_list[0].ParameterType){
									break;
								}

								t_result = t_methodinfo;
								break;
							}
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_result;
		}

		/** メソッド。パラメータ２。検索。

			継承元は含まない

		*/
		public static System.Reflection.MethodInfo FindMethodFromParam2(System.Type a_type,string a_name,System.Type a_parameter_1,System.Type a_parameter_2)
		{
			System.Reflection.MethodInfo t_result = null;

			try{
				System.Reflection.MethodInfo[] t_methodinfo_list = GetMethodList(a_type);
				if(t_methodinfo_list != null){
					foreach(System.Reflection.MethodInfo t_methodinfo in t_methodinfo_list){
						if(a_name == t_methodinfo.Name){
							System.Reflection.ParameterInfo[] t_parameterinfo_list = t_methodinfo.GetParameters();
							if(2 == t_parameterinfo_list.Length){

								//パラメータチェック。
								if(a_parameter_1 != t_parameterinfo_list[0].ParameterType){
									break;
								}

								//パラメータチェック。
								if(a_parameter_2 != t_parameterinfo_list[1].ParameterType){
									break;
								}

								t_result = t_methodinfo;
								break;
							}
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_result;
		}

		/** メソッド。検索。

			継承元を含む。

		*/
		public static System.Reflection.MethodInfo FindMethodAll(System.Type a_type,string a_name,System.Type[] a_parameter_list)
		{
			System.Reflection.MethodInfo t_result = null;

			try{
				System.Type t_type = a_type;
				while(true){
					//終端チェック。
					if(t_type == null){
						break;
					}else if(t_type == typeof(System.Object)){
						break;
					}

					t_result = FindMethodFromParamList(a_type,a_name,a_parameter_list);
					if(t_result != null){
						break;
					}

					//次の継承元へ。
					t_type = t_type.BaseType;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_result;
		}


		/** メソッド。パラメータ１。検索。

			継承元を含む。

		*/
		public static System.Reflection.MethodInfo FindMethodAllParam1(System.Type a_type,string a_name,System.Type a_parameter_1)
		{
			System.Reflection.MethodInfo t_result = null;

			try{
				System.Type t_type = a_type;
				while(true){
					//終端チェック。
					if(t_type == null){
						break;
					}else if(t_type == typeof(System.Object)){
						break;
					}

					t_result = FindMethodFromParam1(a_type,a_name,a_parameter_1);
					if(t_result != null){
						break;
					}

					//次の継承元へ。
					t_type = t_type.BaseType;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_result;
		}

		/** メソッド。パラメータ２。検索。

			継承元を含む。

		*/
		public static System.Reflection.MethodInfo FindMethodAllParam2(System.Type a_type,string a_name,System.Type a_parameter_1,System.Type a_parameter_2)
		{
			System.Reflection.MethodInfo t_result = null;

			try{
				System.Type t_type = a_type;
				while(true){
					//終端チェック。
					if(t_type == null){
						break;
					}else if(t_type == typeof(System.Object)){
						break;
					}

					t_result = FindMethodFromParam2(a_type,a_name,a_parameter_1,a_parameter_2);
					if(t_result != null){
						break;
					}

					//次の継承元へ。
					t_type = t_type.BaseType;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_result;
		}

		/** GetGenericTypeDefinition

			現在のジェネリック型を構築する元になったジェネリック型定義を表す。

			a_type == typeof(System.Collections.Generic.IDictionary<int,int>) : return = typeof(System.Collections.Generic.IDictionary<>)

		*/
		public static System.Type GetGenericTypeDefinition(System.Type a_type)
		{
			System.Type t_type = null;

			if(a_type != null){
				if(a_type.IsGenericType == true){
					t_type = a_type.GetGenericTypeDefinition();
				}
			}

			return t_type;
		}

		/** Dictionary型のキー型。取得。
		*/
		public static System.Type GetDictionaryKeyType(System.Type a_type)
		{
			System.Type t_type = null;

			try{
				System.Type t_generic_type = GetGenericTypeDefinition(a_type);
				if(
					(t_generic_type == typeof(System.Collections.Generic.Dictionary<,>))		||
					(t_generic_type == typeof(System.Collections.Generic.SortedList<,>))		||
					(t_generic_type == typeof(System.Collections.Generic.SortedDictionary<,>))
				){
					System.Type[] t_type_list = a_type.GetGenericArguments();
					if(t_type_list != null){
						if(t_type_list.Length >= 2){
							//Dictionary
							t_type = t_type_list[0];
						}else{
							//不明。
							Tool.Assert(false);
						}
					}else{
						//不明。
						Tool.Assert(false);
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				t_type = null;
			}
			
			return t_type;
		}

		/** リスト型の値型。取得。
		*/
		public static System.Type GetListValueType(System.Type a_type)
		{
			System.Type t_type = null;

			try{
				if(a_type.IsArray){
					//x[]

					t_type = a_type.GetElementType();
				}else{
					System.Type t_generic_type = GetGenericTypeDefinition(a_type);
					if(t_generic_type != null){
						if(
							(t_generic_type == typeof(System.Collections.Generic.Dictionary<,>))		||
							(t_generic_type == typeof(System.Collections.Generic.SortedList<,>))		||
							(t_generic_type == typeof(System.Collections.Generic.SortedDictionary<,>))
						){
							System.Type[] t_type_list = a_type.GetGenericArguments();
							if(t_type_list != null){
								if(t_type_list.Length >= 2){
									//Dictionary
									t_type = t_type_list[1];
								}else{
									//不明。
									Tool.Assert(false);
								}
							}else{
								//不明。
								Tool.Assert(false);
							}
						}else if(
							(t_generic_type == typeof(System.Collections.Generic.List<>))			|| 
							(t_generic_type == typeof(System.Collections.Generic.Stack<>))			|| 
							(t_generic_type == typeof(System.Collections.Generic.LinkedList<>))		||
							(t_generic_type == typeof(System.Collections.Generic.HashSet<>))		||
							(t_generic_type == typeof(System.Collections.Generic.Queue<>))			||
							(t_generic_type == typeof(System.Collections.Generic.SortedSet<>))
						){
							System.Type[] t_type_list = a_type.GetGenericArguments();
							if(t_type_list != null){
								if(t_type_list.Length >= 1){
									//List
									t_type = t_type_list[0];
								}else{
									//不明。
									Tool.Assert(false);
								}
							}else{
								//不明。
								Tool.Assert(false);
							}
						}
					}else if(a_type == typeof(System.Collections.ArrayList)){
						t_type = null;//typeof(System.Object);
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				t_type = null;
			}
			
			return t_type;
		}
	}
}

