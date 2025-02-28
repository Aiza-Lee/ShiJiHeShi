using System;
using NSFrame;
using UnityEngine;

namespace LogicUtilities
{
	public class LerpMove : MonoBehaviour {

		[Range(0.0f, 1.0f)] public float LerpSpeed;

		private Action _updateAction;

		private Vector3 _Target;
		public Vector3 Target {
			get => _Target;
			set {
				_updateAction = DealPos;
				_Target = value;
			}
		}

		private void Update() {
			_updateAction?.Invoke();
		}

		public void Translate(Vector3 movement) {
			Debug.Log($"LayerMove:{movement}");
			Target = transform.position + movement;
		}

		private void DealPos() {
			transform.position = Vector3.Lerp(transform.position, Target, LerpSpeed);
			if (transform.position.IsApproximatelyEqual(Target)) {
				_updateAction = null;
			}
		}
	}
}