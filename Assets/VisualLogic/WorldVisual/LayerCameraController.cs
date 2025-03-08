using System;
using System.Collections;
using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace VisualLogic
{
	public enum CameraSize {
		Focus = 0,
		Normal,
		Panorama,
		Biggest,
	}

	public class LayerCameraController : MonoSingleton<LayerCameraController> {

		[Header("Constants")]
		[Tooltip("从0开始从小到大")] public float[] PresetSizes = new float[3];
		public AnimationCurve Curve;
		public float CurveSpeed = 1.0f; 
		public float CameraSpeed;
		public float ForbidMoveTime = 0.25f;

		public Vector3 VirtualPosition { get; private set; }

		/* 处理相机平滑放缩功能 */
		private Camera _camera;
		private float _distance;
		private Action _updateAction;

		/*  相机前后移动 */
		private bool _forbidMove = false;


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
				_distance = Mathf.Abs(_camera.orthographicSize - PresetSizes[(int)value]);
			}
		}

		protected override void Awake() {
			base.Awake();
			_CameraSize = CameraSize.Normal;
			_camera = GetComponent<Camera>();
			VirtualPosition = _camera.transform.position;
		}
		private void Update() {
			_updateAction?.Invoke();

			if (!_forbidMove) {
				if (Input.GetKeyDown(KeyCode.W)) { 
					EventSystem.Invoke<bool>((int)LogicEvent.LayerCameraFB_b, true); 
					StartCoroutine(ForbidMove());
				}
				if (Input.GetKeyDown(KeyCode.S)) { 
					EventSystem.Invoke<bool>((int)LogicEvent.LayerCameraFB_b, false); 
					StartCoroutine(ForbidMove());
				}
			}
			

			/* 控制相机左右移动 */
			if (!_forbidMove) {
				if (Input.GetKey(KeyCode.A)) {
					var movement = new Vector3(-Time.deltaTime * CameraSpeed, 0, 0);
					VirtualPosition += movement;
					EventSystem.Invoke((int)LogicEvent.LayerCameraMove_v3, movement);
				}
				if (Input.GetKey(KeyCode.D)) {
					var movement = new Vector3(Time.deltaTime * CameraSpeed, 0, 0);
					VirtualPosition += movement;
					EventSystem.Invoke((int)LogicEvent.LayerCameraMove_v3, movement);
				}
			}
			
			
			/* 控制相机视角大小 */
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				CameraSize = CameraSize.Focus;
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				CameraSize = CameraSize.Normal;
			} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				CameraSize = CameraSize.Panorama;
			} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
				CameraSize = CameraSize.Biggest;
			}
		}

		IEnumerator ForbidMove() {
			_forbidMove = true;
			yield return new WaitForSecondsRealtime(ForbidMoveTime);
			_forbidMove = false;
		}


		/// <summary>
		/// 处理相机平滑放缩视角
		/// </summary>
		private void DealSize() {
			float target = PresetSizes[(int)CameraSize];
			if (target.IsApproximatelyEqual(_camera.orthographicSize)) {
				_updateAction = null;
				return;
			}
			float cs = _camera.orthographicSize;
			_camera.orthographicSize = cs + (target > cs ? 1 : -1) * Curve.Evaluate(1.0f - Mathf.Abs(cs - target) / _distance) * Time.deltaTime * CurveSpeed;
		}
		
	}
}