using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class Chopper : CookingBox
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
		_hasTo = true;

		_from = Orientation.Left;
		_to = Orientation.Right;

		_cookingTime = 3f;
		_cookingInterval = 0.5f;
	}

	protected override void ProcessFood() {
		if (_targetFood.chopped == null) {
			ShowWrongUI();
			return;
		}

		HideWrongUI();

		if (ProcessFoodTime()) {
			_targetFood = _targetFood.chopped;
			DestroyFoodObject();
			InstantiateFoodObject();
		}
	}
	#endregion
}

}