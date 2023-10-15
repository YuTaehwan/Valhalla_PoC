using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TH.Core {

public abstract class CookingBox : StageObject
{
	public enum CookingState {
		Ready,
		Cooking,
		Done,
		Interval
	}

    #region PublicVariables
	#endregion

	#region PrivateVariables
	[ShowInInspector, ReadOnly] protected Orientation _from = Orientation.Left;
	[ShowInInspector, ReadOnly] protected Orientation _to = Orientation.Right;

	[ShowInInspector, ReadOnly] protected bool _hasFrom;
	[ShowInInspector, ReadOnly] protected bool _hasTo;

	[ShowInInspector, ReadOnly] protected Food _targetFood = null;

	[SerializeField] protected float _cookingTime = 3f;
	[SerializeField] protected float _cookingInterval = 0.5f;
	[ShowInInspector, ReadOnly] protected float _currentCookingTime = 0f;

	[ShowInInspector, ReadOnly] protected CookingState _cookingState = CookingState.Interval;

	protected GameObject _targetFoodObject = null;
	protected ComponentGetter<RectTransform> _gaugeBar = new ComponentGetter<RectTransform>(
		TypeOfGetter.ChildByName, "OrientationUI/Gauge"
	);
	protected ObjectGetter _wrongUI = new ObjectGetter(
		TypeOfGetter.ChildByName, "OrientationUI/Wrong"
	);
	#endregion

	#region PublicMethod
	public bool RecieveFood(Food food) {
		if (_cookingState != CookingState.Ready) {
			return false;
		}

		if (_targetFood != null) {
			return false;
		}

		_targetFood = food;
		InstantiateFoodObject();
		return true;
	}

	public Food ExtractFood() {
		if (_targetFood == null) {
			return null;
		}

		var result = _targetFood;
		_targetFood = null;
		DestroyFoodObject();

		_cookingState = CookingState.Interval;
		return result;
	}
	#endregion
    
	#region PrivateMethod
	protected virtual void Start() {
		_hasFrom = true;
		_hasTo = true;
	}

	protected virtual void Update() {
		if (_hasHolded) {
			return;
		}

		if (_cookingState == CookingState.Ready) {
			HideGauge();
			HideWrongUI();

			if (_targetFood != null) {
				_cookingState = CookingState.Cooking;

				return;
			}

			if (_hasFrom) {
				GetFoodFrom();
			}
		} else if (_cookingState == CookingState.Cooking) {
			ShowGauge();

			ProcessFood();
		} else if (_cookingState == CookingState.Done) {
			HideGauge();
			HideWrongUI();

			if (_hasTo) {
				bool result = TryGiveFood();
				if (result) {
					_cookingState = CookingState.Interval;
					_targetFood = null;
					DestroyFoodObject();
				}
			}
		} else if (_cookingState == CookingState.Interval) {
			HideGauge();
			HideWrongUI();

			ProcessIntervalTime();
		}
	}
	
	private Orientation GetRotatedFrom() {
		return (Orientation)(((int)_from + (int)_orientation) % 4);
	}

	private Orientation GetRotatedTo() {
		return (Orientation)(((int)_to + (int)_orientation) % 4);
	}

	private StageObject GetFromObject() {
		var rotatedFrom = GetRotatedFrom();
		var rotatedFromX = _currentX + (rotatedFrom == Orientation.Right ? 1 : (rotatedFrom == Orientation.Left ? -1 : 0));
		var rotatedFromY = _currentY + (rotatedFrom == Orientation.Up ? 1 : (rotatedFrom == Orientation.Down ? -1 : 0));

		return GameManager.Stage.GetObject(rotatedFromX, rotatedFromY);
	}

	private StageObject GetToObject() {
		var rotatedTo = GetRotatedTo();
		var rotatedToX = _currentX + (rotatedTo == Orientation.Right ? 1 : (rotatedTo == Orientation.Left ? -1 : 0));
		var rotatedToY = _currentY + (rotatedTo == Orientation.Up ? 1 : (rotatedTo == Orientation.Down ? -1 : 0));

		return GameManager.Stage.GetObject(rotatedToX, rotatedToY);
	}

	private Food GetFoodFrom() {
		if (!_hasFrom) {
			return null;
		}

		var fromObject = GetFromObject();
		if (fromObject == null) {
			return null;
		}

		if (fromObject is not IngredientBox) {
			return null;
		}

		_targetFood = (fromObject as IngredientBox).GetIngredient();
		InstantiateFoodObject();
		return _targetFood;
	}

	private bool TryGiveFood() {
		if (!_hasTo) {
			return false;
		}

		var toObject = GetToObject();
		if (toObject == null) {
			return false;
		}

		if (toObject is not CookingBox) {
			return false;
		}

		var target = toObject as CookingBox;
		bool result = target.RecieveFood(_targetFood);

		return result;
	}

	protected virtual bool ProcessFoodTime() {
		if (_cookingState != CookingState.Cooking) {
			return false;
		}

		_currentCookingTime += Time.deltaTime;

		_gaugeBar.Get(gameObject).sizeDelta = new Vector2(
			_currentCookingTime / _cookingTime * 1.8f,
			_gaugeBar.Get(gameObject).sizeDelta.y
		);

		if (_currentCookingTime >= _cookingTime) {
			_cookingState = CookingState.Done;
			_currentCookingTime = 0f;
			return true;
		}

		return false;
	}

	protected virtual bool ProcessIntervalTime() {
		if (_cookingState != CookingState.Interval) {
			return false;
		}

		_currentCookingTime += Time.deltaTime;
		if (_currentCookingTime >= _cookingInterval) {
			_cookingState = CookingState.Ready;
			_currentCookingTime = 0f;
			return true;
		}

		return false;
	}

	protected virtual void InstantiateFoodObject() {
		if (_targetFoodObject != null) {
			return;
		}

		_targetFoodObject = Instantiate(_targetFood.prefab, transform);
		_targetFoodObject.transform.localPosition = new Vector3(0, 1f, 0);
	}

	protected virtual void DestroyFoodObject() {
		if (_targetFoodObject == null) {
			return;
		}

		Destroy(_targetFoodObject);
		_targetFoodObject = null;
	}

	protected abstract void ProcessFood();

	protected void ShowGauge() {
		_gaugeBar.Get(gameObject).gameObject.SetActive(true);
	}

	protected void HideGauge() {
		_gaugeBar.Get(gameObject).gameObject.SetActive(false);
	}

	protected void ShowWrongUI() {
		_wrongUI.Get(gameObject).SetActive(true);
	}

	protected void HideWrongUI() {
		_wrongUI.Get(gameObject).SetActive(false);
	}
	#endregion
}

}