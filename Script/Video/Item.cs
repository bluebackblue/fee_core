

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ムービー。
*/


/** Fee.Video
*/
namespace Fee.Video
{
	/** Item
	*/
	public class Item : Fee.Deleter.OnDelete_CallBackInterface
	{
		/** rendertexture
		*/
		private UnityEngine.RenderTexture rendertexture;

		/** videoplayer
		*/
		private UnityEngine.Video.VideoPlayer videoplayer;

		/** constructor

			プール用に作成。

		*/
		public Item()
		{
		}

		/** レンダーテクスチャ。作成。
		*/
		private void CreateRenderTexture(int a_width,int a_height)
		{
			this.rendertexture = new UnityEngine.RenderTexture(a_width,a_height,0,UnityEngine.RenderTextureFormat.RGB565,UnityEngine.RenderTextureReadWrite.Default);
			bool t_ret_create = this.rendertexture.Create();

			if(t_ret_create == false){
				this.rendertexture.Release();
				this.rendertexture = null;
			}
		}

		/** ビデオプレイヤ。作成。
		*/
		private void CreateVideoPlayer(Fee.File.Path a_full_path)
		{
			this.videoplayer = Fee.Video.Video.GetInstance().GetRootGameObject().AddComponent<UnityEngine.Video.VideoPlayer>();
			this.videoplayer.playOnAwake = false;

			//描画先。
			this.videoplayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
			this.videoplayer.targetTexture = this.rendertexture;

			//音声。
			this.videoplayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;

			//ループ。
			this.videoplayer.isLooping = false;

			//最初のフレームが準備できるまで待つ。
			this.videoplayer.waitForFirstFrame = true;

			//フレームスキップ。
			this.videoplayer.skipOnDrop = true;

			//再生速度。
			this.videoplayer.playbackSpeed = 1.0f;

			//パス。
			this.videoplayer.source = UnityEngine.Video.VideoSource.Url;
			this.videoplayer.url = a_full_path.GetPath();
			
		}

		/** 作成。
		*/
		public static Item Create(Fee.Deleter.Deleter a_deleter,int a_width,int a_height,Fee.File.Path a_full_path)
		{
			Item t_item = new Item();

			//レンダーテクスチャ。作成。
			t_item.CreateRenderTexture(a_width,a_height);

			//ビデオプレイヤ。作成。
			t_item.CreateVideoPlayer(a_full_path);

			//登録。
			if(a_deleter != null){
				a_deleter.Regist(t_item);
			}

			//登録。
			Fee.Video.Video.GetInstance().Regist(t_item);

			return t_item;
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			if(this.videoplayer != null){
				UnityEngine.GameObject.Destroy(this.videoplayer);
				this.videoplayer = null;
			}

			if(this.rendertexture != null){
				this.rendertexture.Release();
				this.rendertexture = null;
			}

			//解除。
			Fee.Video.Video.GetInstance().UnRegist(this);
		}

		/** GetRenderTexture
		*/
		public UnityEngine.RenderTexture GetRenderTexture()
		{
			return this.rendertexture;
		}

		/** ループ。設定。
		*/
		public void SetLoop(bool a_flag)
		{
			if(this.videoplayer != null){
				this.videoplayer.isLooping = a_flag;
			}
		}

		/** ループ。取得。
		*/
		public bool IsLoop()
		{
			bool t_flag = false;

			if(this.videoplayer != null){
				t_flag = this.videoplayer.isLooping;
			}

			return t_flag;
		}

		/** フレーム。取得。
		*/
		public long GetFrame()
		{
			long t_frame = -1;

			if(this.videoplayer != null){
				t_frame = this.videoplayer.frame;
			}

			return t_frame;
		}

		/** フレーム。設定。
		*/
		public void SetFrame(long a_frame)
		{
			if(this.videoplayer != null){
				this.videoplayer.frame = a_frame;
			}
		}

		/** フレーム。取得。
		*/
		public long GetFrameMax()
		{
			long t_frame = 0;

			if(this.videoplayer != null){
				t_frame = (long)this.videoplayer.frameCount;
			}

			return t_frame;
		}

		/** フレームレート。取得。
		*/
		public float GetFrameRate()
		{
			float t_frame_rate = 0;

			if(this.videoplayer != null){
				t_frame_rate = this.videoplayer.frameRate;
			}

			return t_frame_rate;
		}

		/** タイム。取得。
		*/
		public double GetTime()
		{
			double t_time = 0;

			if(this.videoplayer != null){
				t_time = this.videoplayer.time;
			}

			return t_time;
		}

		/** 再生。チェック。
		*/
		public bool IsPlay()
		{
			bool t_is_play = false;

			if(this.videoplayer != null){
				t_is_play = this.videoplayer.isPlaying;
			}

			return t_is_play;
		}

		/** 準備。チェック。
		*/
		public bool IsPrepared()
		{
			bool t_is_prepared = false;

			if(this.videoplayer != null){
				t_is_prepared = this.videoplayer.isPrepared;
			}

			return t_is_prepared;
		}

		/** ポーズ。チェック。
		*/
		public bool IsPause()
		{
			bool t_is_pause = false;

			if(this.videoplayer != null){
				t_is_pause = this.videoplayer.isPaused;
			}

			return t_is_pause;
		}

		/** 再生。
		*/
		public void Play()
		{
			if(this.videoplayer != null){
				this.videoplayer.Play();
			}
		}

		/** 一時停止。
		*/
		public void Pause()
		{
			if(this.videoplayer != null){
				this.videoplayer.Pause();
			}
		}
	}
}

