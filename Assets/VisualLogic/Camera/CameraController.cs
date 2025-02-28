using System;
using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace VisualLogic
{
	public enum CameraSize {
		Focus = 0,
		Normal,
		Panorama,
	}

	public class CameraController : MonoSingleton<CameraController> {

		[Header("Constants")]
		[Tooltip("从0开始从小到大")] public float[] PreDefinedSize = new float[3];
		public AnimationCurve Curve;
		public float CurveSpeed = 1.0f; 
		public float CameraSpeed;

		public SmoothMove SmoothMove { get; private set; }

		private Camera _camera;
		private float _distance;
		private Action _updateAction;


		[Header("Informations")]
		[SerializeField][ReadOnly] private CameraSize _CameraSize;
		/// <summary>
		/// 设置当前视角大小
		/// </summary>
		public CameraSize CameraSize {
			get => _CameraSize;
			set {
				if (_CameraSize == value) return;
				_CameraSize = value;
				_updateAction = DealSize;
				_distance = Mathf.Abs(_camera.orthographicSize - PreDefinedSize[(int)value]);
			}
		}

		protected override void Awake() {
			base.Awake();
			_CameraSize = CameraSize.Normal;
			_camera = GetComponent<Camera>();
			SmoothMove = GetComponent<SmoothMove>();
		}
		private void Update() {
			_updateAction?.Invoke();

			if (Input.GetKey(KeyCode.A)) {
				transform.Translate(new(-Time.deltaTime * 10 * CameraSpeed, 0, 0));
			}
			if (Input.GetKey(KeyCode.D)) {
				transform.Translate(new(Time.deltaTime * 10 * CameraSpeed, 0, 0));
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				CameraSize = CameraSize.Focus;
			}
			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				CameraSize = CameraSize.Normal;
			}
			if (Input.GetKeyDown(KeyCode.Alpha3)) {
				CameraSize = CameraSize.Panorama;
			}
		}


		private void DealSize() {
			float target = PreDefinedSize[(int)CameraSize];
			if (target.IsApproximatelyEqual(_camera.orthographicSize)) {
				_updateAction = null;
				return;
			}
			float cs = _camera.orthographicSize;
			_camera.orthographicSize = cs + (target > cs ? 1 : -1) * Curve.Evaluate(1.0f - Mathf.Abs(cs - target) / _distance) * Time.deltaTime * CurveSpeed * 100;
		}
		
	}
}