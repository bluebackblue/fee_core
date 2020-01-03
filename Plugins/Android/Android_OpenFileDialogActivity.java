

/** package
*/
package fee.platform;


/** import
*/
import android.app.Activity;
import android.os.Bundle;
import android.content.Intent;
import com.unity3d.player.UnityPlayer;


/** Android_OpenFileDialogActivity
*/
public class Android_OpenFileDialogActivity extends Activity
{
	/** DO
	*/
	public static void DO(String a_mime)
	{
		final Activity t_current_activity = UnityPlayer.currentActivity;
		Intent t_intent = new Intent(t_current_activity,Android_OpenFileDialogActivity.class);
		t_intent.setAction(Intent.ACTION_MAIN);
		t_intent.putExtra("MIME",a_mime);
		t_current_activity.startActivity(t_intent);
	}

	/** onCreate
	*/
	@Override
	protected void onCreate(Bundle a_bundle)
	{
		super.onCreate(a_bundle);

		{
			String t_mime = this.getIntent().getStringExtra("MIME");

			Intent t_intent = new Intent();
			t_intent.setType(t_mime);
			t_intent.setAction(Intent.ACTION_GET_CONTENT);

			Intent t_intent_chooser = Intent.createChooser(t_intent,"open");
			startActivityForResult(t_intent_chooser,10628);
		}
	}

	/** onActivityResult
	*/
	@Override
	protected void onActivityResult(int a_request_code,int a_result_code,Intent a_data)
	{
		super.onActivityResult(a_request_code,a_result_code,a_data);

		try{
			if(a_request_code == 10628){
				if(a_result_code == RESULT_OK){
					String t_uri = a_data.getDataString();
					if(t_uri != null){
						UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack",t_uri);
					}else{
						UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","uri == null");
						UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
					}
				}else{
					UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","result_code = " + String.valueOf(a_result_code));
					UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
				}
			}else{
				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","reques_code = " + String.valueOf(a_request_code));
				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","exception = " + t_exception.getMessage());
			UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
		}

		finish();
	}
}

