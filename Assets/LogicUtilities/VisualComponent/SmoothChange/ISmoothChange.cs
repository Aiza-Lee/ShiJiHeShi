using System;
using UnityEngine;

namespace LogicUtilities 
{
	public abstract class ISmoothChange : MonoBehaviour {
		[Tooltip("请将起始时间分别设为0和1")] public AnimationCurve SpeedCurve;
		[Tooltip("变化持续时间")] public float ChangeTime;
		public abstract Vector3 Target { get; set; }

		protected float _integral;
		protected Action _updateAction;

		private void Awake() {
			Target = transform.position;
			_updateAction = null;
			_integral = CalculateIntegralSimpson(0.0f, 1.0f);
		}
		void Update() {
			_updateAction?.Invoke();
		}

		public void Translate(Vector3 vec3) {
			Target += vec3;
		}
		public abstract void DirectlySet(Vector3 vec3);

		protected abstract void DealPosition();

		protected float CalculateIntegralSimpson(float startTime, float endTime, int numberOfIntervals = 1000) {
			if (numberOfIntervals % 2 != 0) {
				numberOfIntervals += 1;
				Debug.LogWarning("Number of intervals must be even. Adjusted to " + numberOfIntervals);
			}

			float h = (endTime - startTime) / numberOfIntervals;
			float integral = 0f;

			for (int i = 0; i <= numberOfIntervals; i++) {
				float t = startTime + i * h;
				float y = SpeedCurve.Evaluate(t);

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