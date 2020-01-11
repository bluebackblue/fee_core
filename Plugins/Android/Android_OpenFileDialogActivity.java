

/** package
*/
package fee.platform;


/** import
*/
import com.unity3d.player.UnityPlayer;


/** Android_OpenFileDialogActivity
*/
public class Android_OpenFileDialogActivity extends android.app.Activity
{
	/** Open
	*/
	public static void Open(String a_title,String a_mime)
	{
		android.app.Activity t_current_activity = UnityPlayer.currentActivity;
		android.content.Intent t_intent = new android.content.Intent(t_current_activity,Android_OpenFileDialogActivity.class);
		t_intent.setAction(android.content.Intent.ACTION_MAIN);
		t_intent.putExtra("TITLE",a_title);
		t_intent.putExtra("MIME",a_mime);
		t_current_activity.startActivity(t_intent);
	}

	/** onCreate
	*/
	@Override
	protected void onCreate(android.os.Bundle a_bundle)
	{
		super.onCreate(a_bundle);

		{
			String t_title = this.getIntent().getStringExtra("TITLE");
			String t_mime = this.getIntent().getStringExtra("MIME");

			android.content.Intent t_intent = new android.content.Intent();
			t_intent.setType(t_mime);
			t_intent.setAction(android.content.Intent.ACTION_GET_CONTENT);

			android.content.Intent t_intent_chooser = android.content.Intent.createChooser(t_intent,t_title);
			startActivityForResult(t_intent_chooser,10628);
		}
	}

	/** onActivityResult
	*/
	@Override
	protected void onActivityResult(int a_request_code,int a_result_code,android.content.Intent a_intent)
	{
		super.onActivityResult(a_request_code,a_result_code,a_intent);

		try{
			if(a_request_code == 10628){
				if(a_result_code == RESULT_OK){
					String t_uri = a_intent.getDataString();
					if(t_uri != null){
						UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack",t_uri);
					}else{
						UnityPlayer.UnitySendMessage("Platform","Log_CallBack","uri == null");
						UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
					}
				}else{
					UnityPlayer.UnitySendMessage("Platform","Log_CallBack","result_code = " + String.valueOf(a_result_code));
					UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
				}
			}else{
				UnityPlayer.UnitySendMessage("Platform","Log_CallBack","reques_code = " + String.valueOf(a_request_code));
				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","Log_CallBack","exception = " + t_exception.getMessage());
			UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
		}

		finish();
	}
}

