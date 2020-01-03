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
	/** type
	*/
	public static String s_type_text = "*/*";

	/** s_binary
	*/
	public static byte[] s_binary = null;

	/** DO
	*/
	public static void DO(String a_type_text)
	{
		UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","DO : " + a_type_text);

		//s_type_text
		s_type_text = a_type_text;

		//s_binary
		s_binary = null;

		final Activity t_current_activity = UnityPlayer.currentActivity;
		Intent t_intent = new Intent(t_current_activity,Android_OpenFileDialogActivity.class);
		t_intent.setAction(Intent.ACTION_MAIN);
		t_current_activity.startActivity(t_intent);
	}

	/** GET
	*/
	public static byte[] GET()
	{
		UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","GET : size = " + String.valueOf(s_binary.length));

		return s_binary;
	}

	/** IsVirtualFile
	*/
	public static boolean IsVirtualFile(android.content.Context a_context,android.content.ContentResolver a_context_resolver,android.net.Uri a_uri)
	{
		if(!android.provider.DocumentsContract.isDocumentUri(a_context,a_uri)) {
			return false;
		}

		int t_flags = 0;
		{
			android.database.Cursor t_cursor = a_context_resolver.query(a_uri,new String[]{android.provider.DocumentsContract.Document.COLUMN_FLAGS},null,null,null);
			if(t_cursor.moveToFirst()){
				t_flags = t_cursor.getInt(0);
			}
			t_cursor.close();
		}

		if((t_flags & android.provider.DocumentsContract.Document.FLAG_VIRTUAL_DOCUMENT) != 0){
			return true;
		}

		return false;
	}

	/** CreateInputStream
	*/
	public static java.io.InputStream CreateInputStream(android.content.Context a_context,android.content.ContentResolver a_context_resolver,android.net.Uri a_uri)
	{
		java.io.InputStream t_input_stream = null;

		try{
			if(IsVirtualFile(a_context,a_context_resolver,a_uri) == true){
				String[] t_stream_type = a_context_resolver.getStreamTypes(a_uri,s_type_text);
				if(t_stream_type != null){
					if(t_stream_type.length > 0){
						t_input_stream = a_context_resolver.openTypedAssetFileDescriptor(a_uri,t_stream_type[0],null).createInputStream();
					}
				}
			}
		}catch(Exception t_exception){
			//ログ。
			UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","exception : virtual input stream : " + t_exception.getMessage());
		}
		
		if(t_input_stream == null){
			try{
				t_input_stream = a_context_resolver.openInputStream(a_uri);
			}catch(Exception t_exception){
				//ログ。
				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","exception : input stream : " + t_exception.getMessage());
			}
		}

		return t_input_stream;
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

				//ログ。
				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","RESULT_OK : name = " + a_data.getDataString());

				android.net.Uri t_uri = a_data.getData();
				android.content.Context t_context = this.getApplicationContext();
				android.content.ContentResolver t_content_resolver = t_context.getContentResolver();
				android.database.Cursor t_cursor = t_content_resolver.query(t_uri,null,null,null,null);

				java.io.InputStream t_input_stream = CreateInputStream(t_context,t_content_resolver,t_uri);

				byte[] t_result_bainry = null;

				{
					java.io.BufferedInputStream t_in = new java.io.BufferedInputStream(t_input_stream);
					java.io.ByteArrayOutputStream t_out = new java.io.ByteArrayOutputStream();

					byte[] t_buffer = new byte[1024];

					int t_ret_read = 0;
					while((t_ret_read = t_in.read(t_buffer)) != -1){
						t_out.write(t_buffer,0,t_ret_read);
					}

					t_result_bainry = t_out.toByteArray();

					t_in.close();
					t_out.close();
				}

				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack",a_data.getDataString());

				s_binary = t_result_bainry;
			}else{
				//ログ。
				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","resultcode = " + String.valueOf(a_result_code));

				UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
			}
		}catch(Exception t_exception){
			//ログ。
			UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_Log","exception = " + t_exception.getMessage());

			UnityPlayer.UnitySendMessage("Platform","OpenFileDialog_CallBack","");
		}

		finish();
	}
}

