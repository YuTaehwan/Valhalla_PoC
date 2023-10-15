using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TH.Core {

public class IngredientBox : StageObject {
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Food _ingredient;
	#endregion

	#region PublicMethod
	public Food GetIngredient() {
		return _ingredient;
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}