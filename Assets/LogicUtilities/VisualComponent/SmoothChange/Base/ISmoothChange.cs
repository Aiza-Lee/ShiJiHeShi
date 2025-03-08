using System;
using System.Collections.Generic;
using UnityEngine;

namespace LogicUtilities 
{
	public abstract class ISmoothChange<T> : MonoBehaviour where T : struct {

		[Serializable]
		public class ChangeData {
			public AnimationCurve SpeedCurve;
			public float ChangeTime;
			[HideInInspector] public float Integral;
		}
		public List<ChangeData> ChangePresets;


		protected Action _updateAction;
		protected Action _targetCallBack;
		protected int _curMod = 0;
		protected T _target;
		public T Target => _target;

		protected float _elapsedTime;
		protected T _distance;


		private void Awake() {
			_updateAction = null;
			ChangePresets.ForEach( moveData => moveData.Integral = CalculateIntegralSimpson(moveData.SpeedCurve, 0.0f, 1.0f) );
		}
		void Update() {
			_updateAction?.Invoke();
		}

		public virtual void SetTarget(T tValue, int modID = 0, Action callBack = null) {
			_target = tValue;
			_curMod = modID;
			_elapsedTime = 0f;
			_updateAction = DealPosition;
			_targetCallBack = callBack;
		}
		public abstract void Translate(T tValue, int modID = 0, Action callBack = null);
		public abstract void DirectlySet(T tValue);

		protected abstract void DealPosition();

		protected float CalculateIntegralSimpson(AnimationCurve speedCurve, float startTime, float endTime, int numberOfIntervals = 1000) {
			if (numberOfIntervals % 2 != 0) {
				numberOfIntervals += 1;
				Debug.LogWarning("Number of intervals must be even. Adjusted to " + numberOfIntervals);
			}

			float h = (endTime - startTime) / numberOfIntervals;
			float integral = 0f;

			for (int i = 0; i <= numberOfIntervals; i++) {
				float t = startTime + i * h;
				float y = speedCurve.Evaluate(t);

				if (i == 0 || i == numberOfIntervals) {
					integral += y;
				} else if (i % 2 == 1) {
					integral += 4 * y;
				} else {
					integral += 2 * y;
				}
			}

			integral *= h / 3.0f;
			return integral;
		}
	}
}