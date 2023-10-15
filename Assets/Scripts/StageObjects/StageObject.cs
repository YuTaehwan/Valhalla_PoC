using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;

namespace TH.Core {

public class StageObject : MonoBehaviour
{
	public enum Orientation {
		Up,
		Right,
		Down,
		Left,
	}

    #region PublicVariables
	public float yPos;

	public float initialX;
	public float initialZ;
	#endregion

	#region PrivateVariables
	protected int _currentX;
	protected int _currentY;

	protected Orientation _orientation = Orientation.Up;
	protected bool _hasHolded = false;
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

	public void Rotate() {
		switch (_orientation) {
			case Orientation.Up:
				_orientation = Orientation.Right;
				break;
			case Orientation.Right:
				_orientation = Orientation.Down;
				break;
			case Orientation.Down:
				_orientation = Orientation.Left;
				break;
			case Orientation.Left:
				_orientation = Orientation.Up;
				break;
		}

		transform.DORotate(new Vector3(0, (int)_orientation * 90, 0), 0.2f);
		transform.DOScale(1.1f, 0.1f).SetEase(Ease.InOutBounce);
		transform.DOScale(1f, 0.1f).SetEase(Ease.InOutBounce).SetDelay(0.1f);
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