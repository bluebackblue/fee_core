

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。リフレクションツール。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ReflectionTool
	*/
	public class ReflectionTool
	{

		/** GetMethod_Stack_Push

			System.Collections.Generic.Stack<>.Push

		*/
		public static System.Reflection.MethodInfo GetMethod_Stack_Push(System.Type a_class_type)
		{
			System.Reflection.MethodInfo t_result = null;

			System.Type t_type = a_class_type;

			while(true){

				//終端チェック。
				if(t_type == null){
					break;
				}else if(t_type == typeof(System.Object)){
					break;
				}

				//取得。
				System.Reflection.MethodInfo[] t_methodinfo_list = t_type.GetMethods(
				
					//指定した型の階層のレベルで宣言されたメンバーのみを対象にすることを指定します。 継承されたメンバーは対象になりません。
					System.Reflection.BindingFlags.DeclaredOnly |
							
					//インスタンス メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Instance |
							
					//パブリック メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Public |
							
					//パブリック メンバー以外のメンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.NonPublic
				);

				foreach(System.Reflection.MethodInfo t_methodinfo in t_methodinfo_list){
					if(t_methodinfo.Name == "Push"){
						t_result = t_methodinfo;
					}
				}

				//次の継承元へ。
				t_type = t_type.BaseType;
			}

			return t_result;
		}

		/** GetGenericTypeDefinition

			現在のジェネリック型を構築する元になるジェネリック型定義を表す。

		*/
		public static System.Type GetGenericTypeDefinition(System.Type a_type)
		{
			System.Type t_type = null;

			if(a_type != null){
				t_type = a_type.GetGenericTypeDefinition();
			}

			return t_type;
		}

		/** GetDictionaryValueType

			Dictionaryの値型。

		*/
		public static System.Type GetDictionaryValueType(System.Type a_dictionary_type)
		{
			System.Type t_type = null;

			System.Type[] t_typelist_value = a_dictionary_type.GetGenericArguments();
			if(t_typelist_value.Length > 1){
				t_type = t_typelist_value[1];
			}

			if(t_type == null){
				Tool.Assert(false);
				t_type = typeof(System.Object);
			}

			return t_type;
		}

		/** GetListValueType

			リスト型の値型。

		*/
		public static System.Type GetListValueType(System.Type a_list_type)
		{
			System.Type t_type = null;

			if(a_list_type.IsArray == true){
				//type == x[]

				t_type = a_list_type.GetElementType();
			}else{
				//List

				System.Type[] t_type_list = a_list_type.GetGenericArguments();
				if(t_type_list != null){
					if(t_type_list.Length > 0){
						t_type = t_type_list[0];
					}else{
						t_type = a_list_type.GetElementType();
					}
				}else{
					t_type = a_list_type.GetElementType();
				}
			}

			if(t_type == null){
				Tool.Assert(false);
				t_type = typeof(System.Object);
			}

			return t_type;
		}

		/** GetMethodList
		*/
		public static System.Collections.Generic.List<System.Reflection.MethodInfo> GetMethodList(System.Type a_class_type)
		{
			System.Collections.Generic.List<System.Reflection.MethodInfo> t_result = new System.Collections.Generic.List<System.Reflection.MethodInfo>();

			System.Type t_type = a_class_type;

			while(true){

				//終端チェック。
				if(t_type == null){
					break;
				}else if(t_type == typeof(System.Object)){
					break;
				}

				//取得。
				System.Reflection.MethodInfo[] t_methodinfo_list = t_type.GetMethods(
				
					//指定した型の階層のレベルで宣言されたメンバーのみを対象にすることを指定します。 継承されたメンバーは対象になりません。
					System.Reflection.BindingFlags.DeclaredOnly |
							
					//インスタンス メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Instance |
							
					//パブリック メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Public |
							
					//パブリック メンバー以外のメンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.NonPublic
				);

				foreach(System.Reflection.MethodInfo t_methodinfo in t_methodinfo_list){
					t_result.Add(t_methodinfo);
				}

				//次の継承元へ。
				t_type = t_type.BaseType;
			}

			return t_result;
		}

		/** GetFieldInfoList
		*/
		public static System.Collections.Generic.List<System.Reflection.FieldInfo> GetFieldInfoList(System.Type a_class_type)
		{
			System.Collections.Generic.List<System.Reflection.FieldInfo> t_result = new System.Collections.Generic.List<System.Reflection.FieldInfo>();

			System.Type t_type = a_class_type;

			while(true){

				//終端チェック。
				if(t_type == null){
					break;
				}else if(t_type == typeof(System.Object)){
					break;
				}

				//取得。
				System.Reflection.MemberInfo[] t_memberinfo_list = t_type.GetMembers(
				
					//指定した型の階層のレベルで宣言されたメンバーのみを対象にすることを指定します。 継承されたメンバーは対象になりません。
					System.Reflection.BindingFlags.DeclaredOnly |
							
					//インスタンス メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Instance |
							
					//パブリック メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Public |
							
					//パブリック メンバー以外のメンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.NonPublic
				);

				foreach(System.Reflection.MemberInfo t_memberinfo in t_memberinfo_list){
					if(t_memberinfo.MemberType == System.Reflection.MemberTypes.Field){
						System.Reflection.FieldInfo t_fieldinfo = t_memberinfo as System.Reflection.FieldInfo;
						if(t_fieldinfo != null){

							if(t_fieldinfo.IsDefined(typeof(Fee.JsonItem.Ignore),false) == true){
								//オブジェクト化しない。
								continue;
							}

							System.Type t_field_type = t_fieldinfo.FieldType;
							if((t_field_type == typeof(System.IntPtr))||(t_field_type == typeof(System.UIntPtr))){
								//オブジェクト化しない。
								continue;
							}

							t_result.Add(t_fieldinfo);
						}
					}
				}

				//次の継承元へ。
				t_type = t_type.BaseType;
			}

			return t_result;
		}
	}
}

