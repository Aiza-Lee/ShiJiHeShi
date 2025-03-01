using System;
using NSFrame;
using UnityEngine;

namespace LogicUtilities 
{
	public class SmoothScale : MonoBehaviour {

		public MoveType MoveType;

		public float ConstantSpeed;
		public AnimationCurve Curve;
		public float CurveSpeed = 1.0f;

		private float Distance;
		private Action _updateAction;

		private Vector3 _Target;
		public Vector3 Target {
			get => _Target;
			set {
				_Target = value;
				Distance = DisPow2(transform.localScale, Target);
				_updateAction = DealScale;
			}
		}

		private void Awake() {
			_Target = transform.localScale;
		}

		void Update() {
			_updateAction?.Invoke();
		}

		public void DirectlySet(Vector3 vec3) {
			_updateAction = null;
			_Target = transform.localScale = vec3;
		}

		private float DisPow2(Vector3 vec1, Vector3 vec2) {
			return (vec1.x - vec2.x) * (vec1.x - vec2.x) + (vec1.y - vec2.y) * (vec1.y - vec2.y) + (vec1.z - vec2.z) * (vec1.z - vec2.z);
		}
		private Vector3 UpdateValue(Vector3 vec3, float step) {
			return new (
				vec3.x > Target.x ? MathF.Max(vec3.x - step, Target.x) : Mathf.Min(vec3.x + step, Target.x),
				vec3.y > Target.y ? MathF.Max(vec3.y - step, Target.y) : Mathf.Min(vec3.y + step, Target.y),
				vec3.z > Target.z ? MathF.Max(vec3.z - step, Target.z) : Mathf.Min(vec3.z + step, Target.z)
			);
		}

		private void DealScale() {
			if (MoveType == MoveType.Constant) {
				transform.localScale = UpdateValue(transform.localScale, ConstantSpeed * Time.deltaTime * 10);
			} else if (MoveType == MoveType.Curve) {
				var disNow = DisPow2(transform.localScale, Target);
				transform.localScale = UpdateValue(transform.localScale, Curve.Evaluate(1.0f - Mathf.Sqrt(disNow / Distance)) * Time.deltaTime * CurveSpeed * 100);
			}

			if (transform.localScale.IsApproximatelyEqual(Target)) {
				_updateAction = null;
			}
		}
	}
}