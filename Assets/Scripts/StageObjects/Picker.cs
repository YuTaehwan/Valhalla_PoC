using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class Picker : CookingBox {
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	protected override void Start() {
		base.Start();

		_hasFrom = true;
		_hasTo = true;

		_from = Orientation.Left;
		_to = Orientation.Right;

		_cookingTime = 1f;
	}

	protected override void ProcessFood() {
		ProcessFoodTime();
	}
	#endregion
}

}