using System;
using NSFrame;
using UnityEngine;

namespace LogicUtilities
{
	/// <summary>
	/// 内部不做alpha合法性clamp
	/// </summary>
	public class SmoothFade : ISmoothChange<float>
	{
		private Material _material = null;
		private string _valueName = "_Alpha";

		public void Init(Material material) {
			_material = material;
		}

		public override void DirectlySet(float value) {
			_material.SetFloat(_valueName, value);
		}

		public override void Translate(float value, int modID = 0, Action callBack = null) {
			SetTarget(Target + value, _curMod, callBack);
		}

		protected override void DealPosition() {
			var changeData = ChangePresets[_curMod];
			_material.SetFloat(_valueName, Mathf.Clamp01(_material.GetFloat(_valueName) + _distance * (changeData.SpeedCurve.Evaluate(_elapsedTime / changeData.ChangeTime) * Time.deltaTime / (changeData.ChangeTime * changeData.Integral))));
			_elapsedTime += Time.deltaTime;
			if (_material.GetFloat(_valueName).IsApproximatelyEqual(Target)) {
				_updateAction = null;
				_targetCallBack?.Invoke();
				_targetCallBack = null;
			}
		}
		public override void SetTarget(float tValue, int modID = 0, Action callBack = null) {
			base.SetTarget(tValue, modID, callBack);
			_distance = Target - _material.GetFloat(_valueName);
		}
	}
}