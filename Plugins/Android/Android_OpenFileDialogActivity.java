package fee.platform;


/** import
*/
import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.os.Bundle;
import com.unity3d.player.UnityPlayer;
import java.util.List;


/** Android_OpenFileDialogActivity
*/
public class Android_OpenFileDialogActivity extends Activity
{
	/** type
	*/
	public static String s_type_text = "*/*";

	/** DO
	*/
	public static void DO(String a_type_text)
	{
		s_type_text = a_type_text;

		Intent t_intent = new Intent();
		final Activity t_current_activity = UnityPlayer.currentActivity;

		t_intent.setAction(Intent.ACTION_MAIN);
		t_intent.setClassName(t_current_activity,"fee.platform.Android_OpenFileDialogActivity");

		t_current_activity.startActivity(t_intent);
	}

	/** onCreate
	*/
	@Override
	protected void onCreate(Bundle a_bundle)
	{
		super.onCreate(a_bundle);

		{
			Intent t_intent = new Intent();
			t_intent.setType(s_type_text);
			t_intent.setAction(Intent.ACTION_GET_CONTENT);

			Intent t_intent_chooser = Intent.createChooser(t_intent,"open");
			startActivityForResult(t_intent_chooser,1234);
		}
	}

	/** onActivityResult
	*/
	@Override
	protected void onActivityResult(int a_request_code,int a_result_code,Intent a_data)
	{
		super.onActivityResult(a_request_code,a_result_code,a_data);

		try{
			if((a_result_code == RESULT_OK)&&(a_request_code == 1234)){
				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack",a_data.getDataString());
			}else{
				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
		}

		finish();
	}
}

