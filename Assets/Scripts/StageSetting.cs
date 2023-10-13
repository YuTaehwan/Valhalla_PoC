using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

[CreateAssetMenu(fileName = "StageSetting", menuName = "TH/StageSetting", order = 0)]
public class StageSetting : ScriptableObject
{
    public StageSettingData data;
}

public class StageSettingData {
	#region PublicVariables
	public int stageWidth;
	public int stageHeight;

	public GameObject floorPrefab;
	public float floorWidth;
	public float floorHeight;
	public float floorDepth;

	public GameObject wallPrefab;
	public float wallWidth;
	public float wallHeight;
	public float wallDepth;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion
	
	#region PrivateMethod
	#endregion
}

}