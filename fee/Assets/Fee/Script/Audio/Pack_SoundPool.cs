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

		/** name_list
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

		/** サウンドプールチェック。
		*/
		public static bool CheckSoundPool(NAudio.Pack_SoundPool a_soundpool,out string a_errorstring)
		{
			//name_listチェック。
			if(a_soundpool != null){
				if(a_soundpool.name_list != null){
					for(int ii=0;ii<a_soundpool.name_list.Count;ii++){
						if(a_soundpool.name_list[ii] != null){
							if(a_soundpool.name_list[ii].Length > 0){
								try{
									if(System.Text.RegularExpressions.Regex.IsMatch(a_soundpool.name_list[ii],"[0-9a-zA-Z][0-9a-zA-Z\\.\\-_]*") == true){
									}else{
										a_errorstring = "CheckSoundPool : [" + ii.ToString() + "]Regex.IsMatch == false";
										return false;
									}
								}catch(System.Exception t_exception){
									a_errorstring = "CheckSoundPool : " + t_exception.Message;
									return false;
								}
							}else{
								a_errorstring = "CheckSoundPool : name_list[" + ii.ToString() + "].Length <= 0";
								return false;
							}
						}else{
							a_errorstring = "CheckSoundPool : name_list[" + ii.ToString() + "] == null";
							return false;
						}
					}
				}else{
					a_errorstring = "CheckSoundPool : name_list == null";
					return false;
				}
			}else{
				//null。
				a_errorstring = "CheckSoundPool : soundpool == null";
				return false;
			}

			a_errorstring = null;
			return true;
		}
	}
}

