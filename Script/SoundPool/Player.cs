

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。ファイル。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** Player
	*/
	public class Player
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
				this.reference_count = 1;
			}
		}

		/** list
		*/
		private System.Collections.Generic.Dictionary<string,Item> list;

		/** サウンドプール。
		*/
		#if(UNITY_ANDROID)
		private UnityEngine.AndroidJavaObject java_soundpool;
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

		private enum UsageType
		{
			USAGE_MEDIA = 1,
			USAGE_GAME = 14,
		};

		private enum ContentType
		{
			CONTENT_TYPE_MUSIC = 2,
		};

		/** constructor
		*/
		public Player()
		{
			//list
			this.list = new System.Collections.Generic.Dictionary<string,Item>();

			//サウンドプールインスタンス。作成。
			#if(UNITY_ANDROID)
			{
				this.java_soundpool = null;

				//読み込み最大数。
				int t_max_stream = 64;

				#if(true)
				{
					//UsageType
					int t_usage_type = (int)UsageType.USAGE_GAME;

					//ContentType
					int t_content_type = (int)ContentType.CONTENT_TYPE_MUSIC;

					try{
						UnityEngine.AndroidJavaObject t_attribute = null;

						{
							using(UnityEngine.AndroidJavaObject t_jave_attribute_builder = new UnityEngine.AndroidJavaObject("android.media.AudioAttributes$Builder")){
								if(t_jave_attribute_builder != null){
									t_jave_attribute_builder.Call<UnityEngine.AndroidJavaObject>("setUsage",t_usage_type);
									t_jave_attribute_builder.Call<UnityEngine.AndroidJavaObject>("setContentType",t_content_type);

									t_attribute = t_jave_attribute_builder.Call<UnityEngine.AndroidJavaObject>("build");

									t_jave_attribute_builder.Dispose();
								}else{
									Tool.Log("SoundPool","android.media.AudioAttributes$Builder == null");
								}
							}
						}

						if(t_attribute != null){
							using(UnityEngine.AndroidJavaObject t_java_soundpool_builder = new UnityEngine.AndroidJavaObject("android.media.SoundPool$Builder")){
								if(t_java_soundpool_builder != null){
									t_java_soundpool_builder.Call<UnityEngine.AndroidJavaObject>("setAudioAttributes",t_attribute);
									t_java_soundpool_builder.Call<UnityEngine.AndroidJavaObject>("setMaxStreams",t_max_stream);

									this.java_soundpool = t_java_soundpool_builder.Call<UnityEngine.AndroidJavaObject>("build");

								}else{
									Tool.Log("SoundPool","android.media.SoundPool$Builder == null");
								}
							}
						}else{
							Tool.Log("SoundPool","android.media.AudioAttributes == null");
						}
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					if(this.java_soundpool == null){
						Tool.Log("SoundPool","android.media.SoundPool == null");
					}
				}
				#else
				{
					//ストリームタイプ。
					int t_stream_type = (int)StreamType.STREAM_MUSIC;

					//0固定。
					int t_src_quality = (int)SoundPoolConvertQuality.RESERVATION;

					this.java_soundpool = new AndroidJavaObject("android.media.SoundPool",t_max_stream,t_stream_type,t_src_quality);
				}
				#endif
			}
			#endif
		}

		/** リスト数。
		*/
		public int GetCount()
		{
			return this.list.Count;
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

				int t_sound_id = 0;

				//読み込み。
				#if(UNITY_ANDROID)
				try{
					//1固定。
					int t_priority = (int)LoadPriority.RESERVATION;
					if(this.java_soundpool != null){
						Fee.File.Path t_path = Fee.File.Path.CreateLocalPath(a_name,Fee.File.Path.SEPARATOR);
						t_sound_id = this.java_soundpool.Call<int>("load",t_path.GetPath(),t_priority);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
				#endif

				Tool.Log("SoundPool","Load : " + a_name);

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
			}

			if(t_item != null){

				Tool.Log("SoundPool","UnLoad : " + a_name);

				//アンロード。
				#if(UNITY_ANDROID)
				try{
					bool t_ret = false;

					if(this.java_soundpool != null){
						t_ret = this.java_soundpool.Call<bool>("unload",t_item.sound_id);
					}

					if(t_ret == false){
						Tool.Log("SoundPool","unload : error : " + a_name);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
				#endif

				//リストから削除。
				this.list.Remove(a_name);
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

				try{
					int t_ret = 0;

					if(this.java_soundpool != null){
						t_ret = this.java_soundpool.Call<int>("play",t_sound_id,t_left_volume,t_right_volume,t_priority,t_loop,t_pitch);
					}

					if(t_ret == 0){
						Tool.Log("SoundPool","play : error : " + a_name);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
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
				System.Collections.Generic.Dictionary<string,Item>.KeyCollection t_collection = this.list.Keys;
				string[] t_keylist = new string[t_collection.Count];
				t_collection.CopyTo(t_keylist,0);

				for(int ii=0;ii<t_keylist.Length;ii++){
					this.UnLoad(t_keylist[ii],true);
				}
			}

			//サウンドプールインスタンス。解放。
			#if(UNITY_ANDROID)
			try{
				if(this.java_soundpool != null){
					this.java_soundpool.Call("release");
					this.java_soundpool.Dispose();
					this.java_soundpool = null;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
			#endif
		}
	}
}

