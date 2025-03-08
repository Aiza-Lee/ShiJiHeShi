using System;
using NSFrame;
using UnityEngine;

namespace LogicUtilities 
{
	public class SmoothMoveTick : ISmoothChangeTick<Vector3> {
		public override void SetTarget(Vector3 vec3, int modID = 0, Action callBack = null) {
			base.SetTarget(vec3, modID, callBack);
			_distance = _target - transform.position;
		}

		public override void DirectlySet(Vector3 vec3) {
			_target = transform.position = vec3;
			_updateAction = null;
		}

		protected override void DealPosition() {
			var cgDt = ChangePresets[_curMod];
			var deltaTime = Time.deltaTime * TimeSpeed;
			transform.Translate(_distance * (cgDt.SpeedCurve.Evaluate(_elapsedTime / cgDt.ChangeTime) * deltaTime / (cgDt.ChangeTime * cgDt.Integral)));
			_elapsedTime += deltaTime;
			if (transform.position.IsApproximatelyEqual(Target)) {
				_updateAction = null;
				_targetCallBack?.Invoke();
				_targetCallBack = null;
			}
		}

		public override void Translate(Vector3 vec3, int modID = 0, Action callBack = null) {
			SetTarget(Target + vec3, modID, callBack);
		}
	}
}