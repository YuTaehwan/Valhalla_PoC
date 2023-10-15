using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TH.Core {

public class Character : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _rotationSpeed = 10f;
	private Rigidbody _rb;

	private float _horizontalInput;
	private float _verticalInput;

	private float _targetAngle;

	private StageObject _target = null;
	private Food _targetFood = null;
	private GameObject _targetFoodObject = null;
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private void Start() {
		_rb = GetComponent<Rigidbody>();
		_targetAngle = transform.eulerAngles.y;
	}	

	private void Update() {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

		CheckTargetPos();

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (_target == null && _targetFood == null) {
				HoldTarget();
			} else {
				if (_target != null) {
					PlaceTarget();
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.F)) {
			if (_targetFood == null && _target == null) {
				GetFood();
			} else {
				if (_targetFood != null) {
					ReleaseFood();
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			RotateTarget();
		}

		if (_target != null) {
			_target.transform.position = transform.position + new Vector3(0, 3, 0);
		}
	}

	void FixedUpdate() {
        Move();
    }

	private void Move() {
		Vector3 move = new Vector3(_horizontalInput, 0, _verticalInput).normalized;

        _rb.velocity = new Vector3(move.x * _moveSpeed, _rb.velocity.y, move.z * _moveSpeed);

        if (move.magnitude >= 0.1f)
        {
            if (Mathf.Abs(_horizontalInput) > 0.1f) 
            {
                _targetAngle = (_horizontalInput > 0) ? 90f : 270f;
            }
            else if (Mathf.Abs(_verticalInput) > 0.1f) 
            {
                _targetAngle = (_verticalInput > 0) ? 0f : 180f;
            }
        }

		if (Math.Abs(_targetAngle - transform.eulerAngles.y) > 1f) {
			Quaternion rotation = Quaternion.Euler(0, _targetAngle, 0);
			_rb.rotation = Quaternion.Lerp(_rb.rotation, rotation, Time.deltaTime * _rotationSpeed);
		} else {
			_rb.rotation = Quaternion.Euler(0, _targetAngle, 0);
		}
	}

	private Vector2Int GetTarget() {
		int currentX = Mathf.FloorToInt(transform.position.x / 2);
		int currentY = Mathf.FloorToInt(transform.position.z / 2);

		if (_targetAngle == 0f) {
			return new Vector2Int(currentX, currentY + 1);
		} else if (_targetAngle == 90f) {
			return new Vector2Int(currentX + 1, currentY);
		} else if (_targetAngle == 180f) {
			return new Vector2Int(currentX, currentY - 1);
		} else if (_targetAngle == 270f) {
			return new Vector2Int(currentX - 1, currentY);
		}

		return new Vector2Int(currentX, currentY + 1);
	}

	private void CheckTargetPos() {
		var targetPos = GetTarget();
		GameManager.Stage.CheckTarget(targetPos.x, targetPos.y);
	}

	private void HoldTarget() {
		var targetPos = GetTarget();

		if (GameManager.Stage.GetObject(targetPos.x, targetPos.y) == null) {
			return;
		}

		_target = GameManager.Stage.RemoveObject(targetPos.x, targetPos.y);
		_target.Hold();
	}

	private void PlaceTarget() {
		var targetPos = GetTarget();

		if (!GameManager.Stage.IsPlaceAvailable(targetPos.x, targetPos.y)) {
			return;
		}

		GameManager.Stage.PlaceObject(_target, targetPos.x, targetPos.y);
		_target = null;
	}

	private void RotateTarget() {
		var targetPos = GetTarget();

		if (GameManager.Stage.GetObject(targetPos.x, targetPos.y) == null) {
			return;
		}

		GameManager.Stage.RotateObject(targetPos.x, targetPos.y);
	}

	private void GetFood() {
		if (_targetFood != null) {
			return;
		}

		if (_target != null) {
			return;
		}

		var targetPos = GetTarget();

		if (GameManager.Stage.GetObject(targetPos.x, targetPos.y) == null) {
			return;
		}

		var stageObject = GameManager.Stage.GetObject(targetPos.x, targetPos.y);
		if (stageObject is not CookingBox) {
			return;
		}

		var cookingBox = stageObject as CookingBox;
		_targetFood = cookingBox.ExtractFood();

		if (_targetFood == null) {
			return;
		}

		InstantiateFoodObject();
	}

	private void ReleaseFood() {
		if (_targetFood == null) {
			return;
		}

		if (_target != null) {
			return;
		}

		var targetPos = GetTarget();

		if (GameManager.Stage.GetObject(targetPos.x, targetPos.y) == null) {
			return;
		}

		var stageObject = GameManager.Stage.GetObject(targetPos.x, targetPos.y);
		if (stageObject is not CookingBox) {
			return;
		}

		var cookingBox = stageObject as CookingBox;
		cookingBox.RecieveFood(_targetFood);
		DestroyFoodObject();
		_targetFood = null;
	}

	private void InstantiateFoodObject() {
		if (_targetFoodObject != null) {
			DestroyFoodObject();
		}

		_targetFoodObject = Instantiate(_targetFood.prefab, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
		_targetFoodObject.transform.SetParent(transform);
		_targetFoodObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

	private void DestroyFoodObject() {
		if (_targetFoodObject == null) {
			return;
		}

		Destroy(_targetFoodObject);
		_targetFoodObject = null;
	}
	#endregion
}

}