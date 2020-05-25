

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief グラフィック。ＧＬ。
*/


/** Fee.Graphic
*/
namespace Fee.Graphic
{
	/** Gl
	*/
	public class Gl
	{
		/** PushMatrix
		*/
		public static bool PushMatrix()
		{
			try{
				UnityEngine.GL.PushMatrix();
				return true;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
			return false;
		}

		/** PopMatrix
		*/
		public static void PopMatrix()
		{
			try{
				UnityEngine.GL.PopMatrix();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** LoadOrtho
		*/
		public static bool LoadOrtho()
		{
			try{
				UnityEngine.GL.LoadOrtho();
				return true;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
			return false;
		}

		/** LoadProjectionMatrix
		*/
		public static bool LoadProjectionMatrix(in UnityEngine.Matrix4x4 a_matrix)
		{
			try{
				UnityEngine.GL.LoadProjectionMatrix(a_matrix);
				return true;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
			return false;
		}

		/** Begin
		*/
		public static bool Begin(int a_index)
		{
			try{
				UnityEngine.GL.Begin(a_index);
				return true;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
			return false;
		}

		/** End
		*/
		public static void End()
		{
			try{
				UnityEngine.GL.End();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

