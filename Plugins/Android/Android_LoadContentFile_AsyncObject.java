

/** package
*/
package fee.platform;


/** import
*/
import com.unity3d.player.UnityPlayer;


/** Android_LoadContentFile_AsyncObject
*/
public class Android_LoadContentFile_AsyncObject
{
	/** LoadTask
	*/
	public class LoadTask implements Runnable
	{
		/** lock_object
		*/
		private Object lock_object;

		/** is_complate
		*/
		private boolean is_complate;

		/** uri
		*/
		private String uri;

		/** binary
		*/
		private byte[] binary;

		/** constructor
		*/
		public LoadTask(String a_uri)
		{
			//lock_object
			this.lock_object = new Object();

			synchronized(this.lock_object){
				//is_complate
				this.is_complate = false;

				//uri
				this.uri = a_uri;

				//binary
				this.binary = null;
			}
		}

		/** run
		*/
		@Override
		public void run()
		{
			byte[] t_binary = null;

			try{
				t_binary = Android_LoadContentFile.LoadContentFile(this.uri);
			}catch(Exception t_exception){
				UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
			}

			synchronized(this.lock_object){
				//binary
				this.binary = t_binary;
			}

			synchronized(this.lock_object){
				//is_complate
				this.is_complate = true;
			}
		}

		/** IsComplate
		*/
		public boolean IsComplate()
		{
			boolean t_is_complate = false;

			synchronized(this.lock_object){
				t_is_complate = this.is_complate;
			}

			return t_is_complate;
		}

		/** GetResult
		*/
		public byte[] GetResult()
		{
			byte[] t_binary = null;
			boolean t_is_complate = false;

			synchronized(this.lock_object){
				t_binary = this.binary;
				t_is_complate = this.is_complate;
			}

			return t_binary;
		}
	}

	/** thread
	*/
	public Thread thread;

	/** loadtask
	*/
	public LoadTask loadtask;

	/** constructor
	*/
	public Android_LoadContentFile_AsyncObject(String a_uri)
	{
		//loadtask
		this.loadtask = new LoadTask(a_uri);

		//thread
		this.thread = new Thread(this.loadtask);
		this.thread.start();
	}

	/** Delete
	*/
	public void Delete()
	{
		try{
			this.thread.join();
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
		}

		this.thread = null;
		this.loadtask = null;
	}

	/** IsComplate
	*/
	public boolean IsComplate()
	{
		boolean t_is_complate = false;

		if(this.loadtask != null){
			if(this.loadtask.IsComplate() == true){
				t_is_complate = true;
			}
		}

		return t_is_complate;
	}

	/** GetResult
	*/
	public byte[] GetResult()
	{
		byte[] t_binary = null;

		try{
			if(this.loadtask != null){
				t_binary = this.loadtask.GetResult();
			}else{
				UnityPlayer.UnitySendMessage("Platform","Log_CallBack","error : loadtask == null");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
		}

		return t_binary;
	}

	/** GetThreadStatus
	*/
	public int GetThreadStatus()
	{
		int t_status = -1;

		try{
			if(this.thread != null){
				Thread.State t_state = this.thread.getState();

				if(t_state == Thread.State.TERMINATED){
					//Thread state for a terminated thread. 
					t_status = 0;
				}else if(t_state == Thread.State.BLOCKED){
					//Thread state for a thread blocked waiting for a monitor lock. 
					t_status = 1;
				}else if(t_state == Thread.State.NEW){
					//Thread state for a thread which has not yet started. 
					t_status = 2;
				}else if(t_state == Thread.State.RUNNABLE){
					//Thread state for a runnable thread. 
					t_status = 3;
				}else if(t_state == Thread.State.TIMED_WAITING){
					//Thread state for a waiting thread with a specified waiting time. 
					t_status = 4;
				}else if(t_state == Thread.State.WAITING){
					//Thread state for a waiting thread. 
					t_status = 5;
				}else{
					UnityPlayer.UnitySendMessage("Platform","Log_CallBack","error : status : " + t_state.name());
				}
			}else{
				UnityPlayer.UnitySendMessage("Platform","Log_CallBack","error : thread == null");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
		}

		return t_status;
	}

	/** Start
	*/
	public static Android_LoadContentFile_AsyncObject Start(String a_uri)
	{
		Android_LoadContentFile_AsyncObject t_asyncobject = null;

		try{
			t_asyncobject = new Android_LoadContentFile_AsyncObject(a_uri);
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
		}

		return t_asyncobject;
	}

	/** End
	*/
	public static void End(Android_LoadContentFile_AsyncObject a_asyncobject)
	{
		try{
			if(a_asyncobject != null){
				a_asyncobject.Delete();
			}else{
				UnityPlayer.UnitySendMessage("Platform","Log_CallBack","error : asyncobject == null");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
		}
	}

	/** IsComplate
	*/
	public static boolean IsComplate(Android_LoadContentFile_AsyncObject a_asyncobject)
	{
		boolean t_is_complate = false;

		try{
			if(a_asyncobject != null){
				t_is_complate = a_asyncobject.IsComplate();
			}else{
				UnityPlayer.UnitySendMessage("Platform","Log_CallBack","error : asyncobject == null");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
		}

		return t_is_complate;
	}

	/** GetResult
	*/
	public static byte[] GetResult(Android_LoadContentFile_AsyncObject a_asyncobject)
	{
		byte[] t_binary = null;

		try{
			if(a_asyncobject != null){
				t_binary = a_asyncobject.GetResult();
			}else{
				UnityPlayer.UnitySendMessage("Platform","Log_CallBack","error : asyncobject == null");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
		}

		return t_binary;
	}

	/** GetThreadStatus
	*/
	public static int GetThreadStatus(Android_LoadContentFile_AsyncObject a_asyncobject)
	{
		int t_status = -1;

		try{
			if(a_asyncobject != null){
				t_status = a_asyncobject.GetThreadStatus();
			}else{
				UnityPlayer.UnitySendMessage("Platform","Log_CallBack","error : asyncobject == null");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception : " + t_exception.getMessage());
		}

		return t_status;
	}
}

