

/** package
*/
package fee.platform;


/** import
*/
import android.app.Activity;
import android.os.Bundle;
import android.content.Intent;
import com.unity3d.player.UnityPlayer;


/** Android_LoadContentFile
*/
public class Android_LoadContentFile
{
	/** IsVirtualFile
	*/
	public static boolean IsVirtualFile(android.content.Context a_context,android.content.ContentResolver a_context_resolver,android.net.Uri a_uri)
	{
		if(!android.provider.DocumentsContract.isDocumentUri(a_context,a_uri)){
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
	public static java.io.InputStream CreateInputStream(android.content.Context a_context,android.content.ContentResolver a_context_resolver,android.net.Uri a_uri,String a_mime)
	{
		java.io.InputStream t_input_stream = null;

		try{
			if(IsVirtualFile(a_context,a_context_resolver,a_uri) == true){
				String[] t_stream_type = a_context_resolver.getStreamTypes(a_uri,a_mime);
				if(t_stream_type != null){
					if(t_stream_type.length > 0){
						t_input_stream = a_context_resolver.openTypedAssetFileDescriptor(a_uri,t_stream_type[0],null).createInputStream();
					}
				}
			}
		}catch(Exception t_exception){
			UnityPlayer.UnitySendMessage("Platform","LoadContentFile_Log","exception : virtual input stream : " + t_exception.getMessage());
		}
		
		if(t_input_stream == null){
			try{
				t_input_stream = a_context_resolver.openInputStream(a_uri);
			}catch(Exception t_exception){
				UnityPlayer.UnitySendMessage("Platform","LoadContentFile_Log","exception : input stream : " + t_exception.getMessage());
			}
		}

		return t_input_stream;
	}

	/** LoadContentFile
	*/
	public static byte[] LoadContentFile(String a_uri)
	{
		byte[] t_result_bainry = null;

		try{
			final Activity t_current_activity = UnityPlayer.currentActivity;

			android.net.Uri t_uri = android.net.Uri.parse(a_uri);
			android.content.Context t_context = t_current_activity.getApplicationContext();
			android.content.ContentResolver t_content_resolver = t_context.getContentResolver();
			java.io.InputStream t_input_stream = CreateInputStream(t_context,t_content_resolver,t_uri,"*/*");

			java.io.BufferedInputStream t_in = null;
			java.io.ByteArrayOutputStream t_out = null;
			try{
				t_in = new java.io.BufferedInputStream(t_input_stream);
				t_out = new java.io.ByteArrayOutputStream();

				byte[] t_buffer = new byte[1024];

				int t_ret_read = 0;
				while((t_ret_read = t_in.read(t_buffer)) != -1){
					t_out.write(t_buffer,0,t_ret_read);
				}

				t_result_bainry = t_out.toByteArray();

				t_in.close();
				t_out.close();
			}catch(Exception t_exception){
				t_result_bainry = null;
				UnityPlayer.UnitySendMessage("Platform","LoadContentFile_Log","exception : read : " + t_exception.getMessage());
			}

			try{
				if(t_in != null){
					t_in.close();
				}
				if(t_out != null){
					t_out.close();
				}
			}catch(Exception t_exception){
				t_result_bainry = null;
				UnityPlayer.UnitySendMessage("Platform","LoadContentFile_Log","exception : close : " + t_exception.getMessage());
			}
		}catch(Exception t_exception){
			t_result_bainry = null;
			UnityPlayer.UnitySendMessage("Platform","LoadContentFile_Log","exception : " + t_exception.getMessage());
		}

		return t_result_bainry;
	}
}

