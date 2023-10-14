using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class LogManager : IManager
{
	public enum LogType {
		INFO,
		WARNING,
		ERROR,
	}

	public LogManager() {
		
	}

    #region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public void Init() {

	}

	public void Log(string message, LogType logType=LogType.INFO) {
		#if UNITY_EDITOR
		LogInEditor(message, logType);
		#else
		LogInBuild(message, logType);
		#endif
	}
	#endregion
    
	#region PrivateMethod
	private void LogInEditor(string message, LogType logType=LogType.INFO) {
		if (logType == LogType.INFO) {
			Debug.Log(message);
		} else if (logType == LogType.WARNING) {
			Debug.LogWarning(message);
		} else if (logType == LogType.ERROR) {
			Debug.LogError(message);
		}
	}

	private void LogInBuild(string message, LogType logType=LogType.INFO) {
	}
	#endregion
}

}