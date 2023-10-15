using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		SetTargetPos();

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (_target == null) {
				HoldTarget();
			} else {
				PlaceTarget();
			}
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

	private void SetTargetPos() {
		int currentX = Mathf.FloorToInt(transform.position.x / 2);
		int currentY = Mathf.FloorToInt(transform.position.z / 2);

		if (_targetAngle == 0f) {
			GameManager.Stage.CheckTarget(currentX, currentY + 1);
		} else if (_targetAngle == 90f) {
			GameManager.Stage.CheckTarget(currentX + 1, currentY);
		} else if (_targetAngle == 180f) {
			GameManager.Stage.CheckTarget(currentX, currentY - 1);
		} else if (_targetAngle == 270f) {
			GameManager.Stage.CheckTarget(currentX - 1, currentY);
		}
	}

	private void HoldTarget() {
		int currentX = Mathf.FloorToInt(transform.position.x / 2);
		int currentY = Mathf.FloorToInt(transform.position.z / 2);

		if (_targetAngle == 0f) {
			if (GameManager.Stage.GetObject(currentX, currentY + 1) != null) {
				_target = GameManager.Stage.RemoveObject(currentX, currentY + 1);
				_target.Hold();
			}
		} else if (_targetAngle == 90f) {
			if (GameManager.Stage.GetObject(currentX + 1, currentY) != null) {
				_target = GameManager.Stage.RemoveObject(currentX + 1, currentY);
				_target.Hold();
			}
		} else if (_targetAngle == 180f) {
			if (GameManager.Stage.GetObject(currentX, currentY - 1) != null) {
				_target = GameManager.Stage.RemoveObject(currentX, currentY - 1);
				_target.Hold();
			}
		} else if (_targetAngle == 270f) {
			if (GameManager.Stage.GetObject(currentX - 1, currentY) != null) {
				_target = GameManager.Stage.RemoveObject(currentX - 1, currentY);
				_target.Hold();
			}
		}
	}

	private void PlaceTarget() {
		int currentX = Mathf.FloorToInt(transform.position.x / 2);
		int currentY = Mathf.FloorToInt(transform.position.z / 2);

		if (_targetAngle == 0f) {
			if (GameManager.Stage.IsPlaceAvailable(currentX, currentY + 1)) {
				GameManager.Stage.PlaceObject(_target, currentX, currentY + 1);
				_target = null;
			}
		} else if (_targetAngle == 90f) {
			if (GameManager.Stage.IsPlaceAvailable(currentX + 1, currentY)) {
				GameManager.Stage.PlaceObject(_target, currentX + 1, currentY);
				_target = null;
			}
		} else if (_targetAngle == 180f) {
			if (GameManager.Stage.IsPlaceAvailable(currentX, currentY - 1)) {
				GameManager.Stage.PlaceObject(_target, currentX, currentY - 1);
				_target = null;
			}
		} else if (_targetAngle == 270f) {
			if (GameManager.Stage.IsPlaceAvailable(currentX - 1, currentY)) {
				GameManager.Stage.PlaceObject(_target, currentX - 1, currentY);
				_target = null;
			}
		}
	}
	#endregion
}

}