using NSFrame;
using UnityEngine;

namespace LogicUtilities 
{
	public class SmoothScale : ISmoothChange {
		private Vector3 _Target;
		private float _elapsedTime;
		private Vector3 _distance;
		public override Vector3 Target {
			get => _Target;
			set {
				_Target = value;
				_distance = _Target - transform.localScale;
				_elapsedTime = 0f;
				_updateAction = DealPosition;
			}
		}

		public override void DirectlySet(Vector3 vec3) {
			_Target = transform.localScale = vec3;
			_updateAction = null;
		}

		protected override void DealPosition() {
			transform.localScale += _distance * (SpeedCurve.Evaluate(_elapsedTime / ChangeTime) * Time.deltaTime / (ChangeTime * _integral));
			_elapsedTime += Time.deltaTime;
			if (transform.localScale.IsApproximatelyEqual(Target))
				_updateAction = null;
		}
	}
}