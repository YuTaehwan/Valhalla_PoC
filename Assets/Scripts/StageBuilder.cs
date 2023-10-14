using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class StageBuilder : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject _floorPrefab;
	[SerializeField] private GameObject _wallPrefab;
	[SerializeField] private int _floorUnitWidth;
	[SerializeField] private int _floorUnitHeight;
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private void Start() {
		Build();
	}

	private void Build() {
		BuildFloor();
	}

	private void BuildFloor() {
		for (int i = 0; i < _floorUnitWidth; i++) {
			for (int j = 0; j < _floorUnitHeight; j++) {
				var floor = Instantiate(_floorPrefab, transform);
				floor.transform.localPosition = new Vector3(
					(i - _floorUnitWidth / 2) * 4,
					0,
					(j - _floorUnitHeight / 2) * 4
				);
			}
		}
	}
	#endregion
}

}