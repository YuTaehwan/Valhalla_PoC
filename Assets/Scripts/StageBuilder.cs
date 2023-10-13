using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class StageBuilder : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private StageSetting _stageSetting;
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	public void Build() {
		if (_stageSetting == null) {
			Debug.LogError("StageSetting is null");
			return;
		}
	}
	#endregion
}

}