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
		public SpriteRenderer Renderer { get; private set; }
		private Material _material => Renderer.material;
		private string _valueName = "_Alpha";

		private void Start() {
			Renderer = GetComponent<SpriteRenderer>();
		}

		public override void DirectlySet(float value) {
			_material.SetFloat(_valueName, value);
		}

		public override void Translate(float value, int modID = 0, Action callBack = null) {
			SetTarget(Target + value, _curMod, callBack);
		}

		protected override void DealPosition() {
			var cgDt = ChangePresets[_curMod];
			_material.SetFloat(_valueName, Mathf.Clamp01(_material.GetFloat(_valueName) + _distance * (cgDt.SpeedCurve.Evaluate(_elapsedTime / cgDt.ChangeTime) * Time.deltaTime / (cgDt.ChangeTime * cgDt.Integral))));
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