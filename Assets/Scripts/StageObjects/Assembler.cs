using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class Assembler : CookingBox
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	protected override void Start() {
		base.Start();

		_hasFrom = false;
		_hasTo = false;

		_cookingTime = 3f;
		_cookingInterval = 0.5f;
	}

	protected override void ProcessFood() {
		if (_targetFood.inBowl == null) {
			ShowWrongUI();
			return;
		}

		HideWrongUI();

		if (ProcessFoodTime()) {
			_targetFood = _targetFood.inBowl;
			DestroyFoodObject();
			InstantiateFoodObject();
		}
	}
	#endregion
}

}