using System;
using NSFrame;
using UnityEditor;
using UnityEngine;

namespace VisualLogic {

	public enum CameraSize {
		Focus = 0,
		Normal,
		Panorama,
	}

	public class CameraController : MonoSingleton<CameraController> {
		[Header("Constants")]
		[Tooltip("从0开始从小到大")] public float[] PreDefinedSize = new float[3];
		public AnimationCurve Curve;


		private Camera _attachedCamera;
		private float _distance;


		[Header("Informations")]
		[SerializeField][ReadOnly] private CameraSize _CameraSize;
		public CameraSize CameraSize {
			get => _CameraSize;
			set {
				if (_CameraSize == value) return;
				_CameraSize = value;
				_updateAction = DealSize;
				_distance = Mathf.Abs(_attachedCamera.orthographicSize - PreDefinedSize[(int)value]);
			}
		}
		private Action _updateAction;


		private void DealSize() {
			float target = PreDefinedSize[(int)CameraSize];
			if (target.IsApproximatelyEqual(_attachedCamera.orthographicSize)) {
				_updateAction = null;
				return;
			}
			float cs = _attachedCamera.orthographicSize;
			_attachedCamera.orthographicSize = cs + (target > cs ? 1 : -1) * Curve.Evaluate(1.0f - Mathf.Abs(cs - target) / _distance) * Time.deltaTime * 100;
		}

		protected override void Awake() {
			base.Awake();
			_CameraSize = CameraSize.Normal;
			_attachedCamera = GetComponent<Camera>();
		}
		private void Update() {
			_updateAction?.Invoke();
		}

	}
}