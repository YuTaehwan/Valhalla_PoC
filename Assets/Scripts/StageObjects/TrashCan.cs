using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TH.Core {

public class TrashCan : CookingBox
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

		_cookingTime = 0.2f;
		_cookingInterval = 0f;
	}

	protected override void ProcessFood() {
		if (ProcessFoodTime()) {
			StartCoroutine(ProcessTrash());
		}
	}

	protected IEnumerator ProcessTrash() {
		_targetFoodObject.transform.DOScale(0, 0.2f).SetEase(Ease.InBack);
		yield return new WaitForSeconds(0.2f);
		_targetFood = null;
		_targetFoodObject.transform.localScale = Vector3.one;
		DestroyFoodObject();
		_cookingState = CookingState.Interval;
	}
	#endregion
}

}