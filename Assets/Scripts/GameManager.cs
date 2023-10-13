using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class GameManager : Singleton<GameManager>
{
	private enum ManagerType {
		LOG,
	}

    #region PublicVariables
	public static LogManager Log {
		get {
			return _managerList[ManagerType.LOG] as LogManager;
		}
	}
	#endregion

	#region PrivateVariables
	[SerializeField] private GameSetting _gameSetting;

	private static Dictionary<ManagerType, IManager> _managerList = new Dictionary<ManagerType, IManager> {
		{ManagerType.LOG, new LogManager()},
	};
	
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	protected override void Init()
	{
		base.Init();
		
		InitManagers();
	}

	private void InitManagers() {
		foreach (var manager in _managerList) {
			manager.Value.Init();
		}
	}
	#endregion
}

}