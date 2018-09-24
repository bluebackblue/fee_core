using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。
*/


/** NAudio
*/
namespace NAudio
{
	/** SoundPool
	*/
	public class SoundPool
	{
		/** Item
		*/
		class Item
		{
			/** sound_id
			*/
			public int sound_id;

			/** 参照カウンタ。
			*/
			public int reference_count;

			/** constructor
			*/
			public Item(int a_sound_id)
			{
				//sound_id
				this.sound_id = a_sound_id;

				//reference_count
				this.reference_count = 0;
			}
		}

		/** list
		*/
		private Dictionary<string,Item> list;

		/** サウンドプール。
		*/
		#if(UNITY_ANDROID)
		private AndroidJavaObject java_soundpool;
		#endif

		/** ステリームタイプ。
		*/
		private enum StreamType
		{
			STREAM_MUSIC = 3,
		};

		/** 読み込みプライオリティ。
		*/
		private enum LoadPriority
		{
			RESERVATION = 1,
		};

		/** サンプルレート変換品質。
		*/
		private enum SoundPoolConvertQuality
		{
			RESERVATION = 0,
		};

		/** constructor
		*/
		public SoundPool()
		{
			//list
			this.list = new Dictionary<string,Item>();

			//読み込み最大数。
			int t_max_stream = 64;

			//ストリームタイプ。
			int t_stream_type = (int)StreamType.STREAM_MUSIC;

			//0固定。
			int t_src_quality = (int)SoundPoolConvertQuality.RESERVATION;

			#if(UNITY_ANDROID)
			{
				//新。
				/*
				AudioAttributes attr = new AudioAttributes.Builder()
					.setUsage(AudioAttributes.USAGE_MEDIA)
					.setContentType(AudioAttributes.CONTENT_TYPE_MUSIC)
					.build();

				SoundPool pool = new SoundPool.Builder()
					.setAudioAttributes(attr)
					.setMaxStreams(SOUND_POOL_MAX)
					.build();
				*/

				//旧。
				this.java_soundpool = new AndroidJavaObject("android.media.SoundPool",t_max_stream,t_stream_type,t_src_quality);
			}
			#endif
		}

		/** ロード。
		*/
		public void Load(string a_name)
		{
			Item t_item = null;
			if(this.list.TryGetValue(a_name,out t_item) == true){
				//読み込み済み。
				if(t_item != null){
					t_item.reference_count++;
				}
			}

			if(t_item == null){
				//1固定。
				int t_priority = (int)LoadPriority.RESERVATION;

				//読み込み。
				#if(UNITY_ANDROID)
				int t_sound_id = this.java_soundpool.Call<int>("load",Application.persistentDataPath + "/" + a_name,t_priority);
				#else
				int t_sound_id = 0;
				#endif

				//追加。
				this.list.Add(a_name,new Item(t_sound_id));
			}
		}

		/** アンロード。
		*/
		public void UnLoad(string a_name,bool a_must = false)
		{
			Item t_item = null;
			if(this.list.TryGetValue(a_name,out t_item) == true){
				//参照カウンタチェック。
				if(a_must == false){
					if(t_item != null){
						t_item.reference_count--;
						if(t_item.reference_count > 0){
							t_item = null;
						}
					}
				}

				//リストから削除。
				this.list.Remove(a_name);
			}

			if(t_item != null){
				int t_sound_id = t_item.sound_id;

				//アンロード。
				#if(UNITY_ANDROID)
				{
					bool t_ret = this.java_soundpool.Call<bool>("unload",t_sound_id);
					if(t_ret == false){
						Tool.LogError("SoundPool","unload : error : " + a_name);
					}
				}
				#endif
			}
		}

		/** 再生。
		*/
		public void Play(string a_name,float a_volume)
		{
			Item t_item = null;
			this.list.TryGetValue(a_name,out t_item);

			#if(UNITY_ANDROID)
			if(t_item != null){

				//サウンドＩＤ。
				int t_sound_id = t_item.sound_id;

				//ボリューム。
				float t_left_volume = a_volume;
				float t_right_volume = a_volume;

				//プライオリティ。
				int t_priority = 0;

				//ループ。
				int t_loop = 0;

				//ピッチ。
				float t_pitch = 1.0f;

				int t_ret = this.java_soundpool.Call<int>("play",t_sound_id,t_left_volume,t_right_volume,t_priority,t_loop,t_pitch);
				if(t_ret == 0){
					Tool.LogError("SoundPool","play : error : " + a_name);
				}
			}
			#endif
		}

		/** 削除。
		*/
		public void Delete()
		{
			Tool.Log("SoundPool","Delete");

			//アンロード。
			{
				Dictionary<string,Item>.KeyCollection t_collection = this.list.Keys;
				string[] t_keylist = new string[t_collection.Count];
				t_collection.CopyTo(t_keylist,0);

				for(int ii=0;ii<t_keylist.Length;ii++){
					this.UnLoad(t_keylist[ii],true);
				}
			}

			//サウンドプールインスタンス。解放。
			#if(UNITY_ANDROID)
			if(this.java_soundpool != null){
				this.java_soundpool.Call("release");
				this.java_soundpool.Dispose();
				this.java_soundpool = null;
			}
			#endif
		}
	}
}

