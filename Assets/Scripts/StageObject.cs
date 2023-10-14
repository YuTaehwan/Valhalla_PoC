using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TH.Core {

public class StageObject : MonoBehaviour
{
	public enum Type {
		BOX,
		PICKER,
		CHOPER,
		SERVER
	}
	
    #region PublicVariables
	public float yPos;

	public float initialX;
	public float initialZ;
	#endregion

	#region PrivateVariables
	private int _currentX;
	private int _currentY;

	private bool _hasHolded = false;
	#endregion

	#region PublicMethod
	public void Hold() {
		_hasHolded = true;
	}

	public void Release(int x, int y) {
		_currentX = x;
		_currentY = y;

		_hasHolded = false;
	}
	#endregion
    
	#region PrivateMethod
	private void Awake() {
		_currentX = (int)initialX;
		_currentY = (int)initialZ;
	}
	#endregion
}

}