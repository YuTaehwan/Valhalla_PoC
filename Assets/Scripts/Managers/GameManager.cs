using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class GameManager : Singleton<GameManager>
{
	private enum ManagerType {
		LOG,
		STAGE
	}

    #region PublicVariables
	public static LogManager Log {
		get {
			return _managerList[ManagerType.LOG] as LogManager;
		}
	}

	public static StageManager Stage {
		get {
			return _managerList[ManagerType.STAGE] as StageManager;
		}
	}
	#endregion

	#region PrivateVariables
	[SerializeField] private GameSetting _gameSetting;

	[SerializeField] private List<StageObject> _initalStageObjectList;

	private static Dictionary<ManagerType, IManager> _managerList = new Dictionary<ManagerType, IManager> {
		{ManagerType.LOG, new LogManager()},
		{ManagerType.STAGE, new StageManager()}
	};
	
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	protected override void Init()
	{
		base.Init();
		
		InitManagers();

		foreach(var stageObject in _initalStageObjectList) {
			Stage.PlaceObject(stageObject, (int)stageObject.initialX, (int)stageObject.initialZ);
		}
	}

	private void InitManagers() {
		foreach (var manager in _managerList) {
			manager.Value.Init();
		}
	}
	#endregion
}

}