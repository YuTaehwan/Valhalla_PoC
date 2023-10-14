using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class StageManager : IManager {
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private GameObject _hintObject;
	private List<List<StageObject>> _stageObjectList;

	private int _floorWidth = 14;
	private int _floorHeight = 10;
	#endregion

	#region PublicMethod
	public void Init() {
		_stageObjectList = new List<List<StageObject>>();
		_hintObject = GameObject.Find("HintObject");

		for (int i = 0; i < _floorWidth; i++) {
			_stageObjectList.Add(new List<StageObject>());
			for (int j = 0; j < _floorHeight; j++) {
				_stageObjectList[i].Add(null);
			}
		}
	}

	public bool IsPlaceAvailable(int x, int y) {
		int realX = x + _floorWidth / 2;
		int realY = y + _floorHeight / 2;

		if (realX < 0 || realX >= _floorWidth || realY < 0 || realY >= _floorHeight) {
			return false;
		}

		return _stageObjectList[realX][realY] == null;
	}

	public void PlaceObject(StageObject stageObject, int x, int y) {
		int realX = x + _floorWidth / 2;
		int realY = y + _floorHeight / 2;

		_stageObjectList[realX][realY] = stageObject;
		stageObject.transform.localPosition = new Vector3(
			x * 2 + 1,
			stageObject.yPos,
			y * 2 + 1
		);

		stageObject.Release(x, y);
	}

	public StageObject RemoveObject(int x, int y) {
		int realX = x + _floorWidth / 2;
		int realY = y + _floorHeight / 2;

		var stageObject = _stageObjectList[realX][realY];
		_stageObjectList[realX][realY] = null;
		return stageObject;
	}

	public StageObject GetObject(int x, int y) {
		int realX = x + _floorWidth / 2;
		int realY = y + _floorHeight / 2;

		if (realX < 0 || realX >= _floorWidth || realY < 0 || realY >= _floorHeight) {
			return null;
		}

		return _stageObjectList[realX][realY];
	}

	public void CheckTarget(int x, int y) {
		int realX = x + _floorWidth / 2;
		int realY = y + _floorHeight / 2;

		if (realX < 0 || realX >= _floorWidth || realY < 0 || realY >= _floorHeight) {
			return;
		}

		_hintObject.SetActive(true);
		_hintObject.transform.localPosition = new Vector3(
			x * 2 + 1,
			1f,
			y * 2 + 1
		);

		var hintObjMaterial = _hintObject.GetComponent<Renderer>().material;

		if (!IsPlaceAvailable(x, y)) {
			Color targetColor = Color.red;
			targetColor.a = 0.5f;
			hintObjMaterial.color = targetColor;
		} else {
			Color targetColor = Color.green;
			targetColor.a = 0.5f;
			hintObjMaterial.color = targetColor;
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}

}