using System;
using System.Collections.Generic;
using BasicLogic;
using LogicUtilities;
using NSFrame;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

namespace VisualLogic 
{
	/// <summary>
	/// 管理 2D 视角下建筑的放缩问题
	/// 管理摄像机的移动
	/// </summary>
	public partial class LayerVisual : MonoSingleton<LayerVisual> {

		private const float DEFAULT_LAYER_SCALE = 5.0f;
		private const float DEFAULT_LAYER_Y = -1.0f;

		[Header("挂载")]
		public CameraController LayerCamera;

		[Header("Cosntants")]
		[Range(0.0f, 10f)] public float LayerGap;
		[Range(0.0f, 5f)] public float ArchGap;
		[Range(2.0f, 10.0f)] public float CameraDistance; 
		[Range(0.0f, 0.3f)] public float YOffset;
		public int LayersToShow;

		[Header("Informations")]
		[SerializeField][ReadOnly] private int _curLayer = 0;
		[SerializeField][ReadOnly] private List<ILayer> _layers;
		[SerializeField][ReadOnly] private int _curMaxLayer = 0;
		[SerializeField][ReadOnly] private int _curMinLayer = 0;

		private Vector3 _cameraLastPos;
		private List<float> _slope;
		private int RADIUS => GameManager.Instance.GameConfig.MaxLayerRadius;
		private int LAYER_MAX => GameManager.Instance.GameConfig.MaxLayerAmount;

		private void Start() {
			_cameraLastPos = LayerCamera.transform.position;

			_slope = new();
			for (int i = 0; i < LAYER_MAX; ++i) {
				_slope.Add(CameraDistance / (CameraDistance + i * LayerGap));
			}

			_layers = new();
			for (int i = 0; i < LAYER_MAX; ++i)
				_layers.Add(null); 
		}
		private void OnEnable() {
			EventSystem.AddListener<ILayer>((int)LogicEvent.LayerConstructed_L, OnLayerCreate);
			EventSystem.AddListener<IArch>((int)LogicEvent.ArchConstructed_A, OnArchCreate);
			EventSystem.AddListener<Vector3>((int)LogicEvent.CameraMove_v3, OnCameraMove);
		}
		private void OnDisable() {
			EventSystem.RemoveListener<ILayer>((int)LogicEvent.LayerConstructed_L, OnLayerCreate);
			EventSystem.RemoveListener<IArch>((int)LogicEvent.ArchConstructed_A, OnArchCreate);
			EventSystem.RemoveListener<Vector3>((int)LogicEvent.CameraMove_v3, OnCameraMove);
		}

		private void Update() {
			if (!_cameraLastPos.IsApproximatelyEqual(LayerCamera.transform.position)) {
				// OnCameraMove(movement);
				EventSystem.Invoke<Vector3>((int)LogicEvent.CameraMove_v3, LayerCamera.transform.position - _cameraLastPos);
				_cameraLastPos = LayerCamera.transform.position;
			}
			if (Input.GetKeyDown(KeyCode.W)) { MoveForBackward(forward: true); }
			if (Input.GetKeyDown(KeyCode.S)) { MoveForBackward(forward: false); }
		}

		private void OnArchCreate(IArch arch) {
			int sortingLayerID = SortingLayer.NameToID("m_Layer" + (RADIUS + arch.Layer));
			arch.SpriteRenderer.sortingLayerID = sortingLayerID;
			foreach (var light in arch.Light2Ds) {
				(light as Light2D).SetLayers(sortingLayerID);
			}
			arch.transform.Translate(ArchGap * arch.Order, 0, 0);
		}

		private void OnLayerCreate(ILayer layer) {
			layer.SpriteRenderer.sortingLayerName = "m_Layer" + (RADIUS + layer.Layer);
			_layers[layer.Layer + RADIUS] = layer;
			_curMaxLayer = Mathf.Max(_curMaxLayer, layer.Layer);
			_curMinLayer = Mathf.Min(_curMinLayer , layer.Layer);
			if (layer.Layer < _curLayer || layer.Layer >= _curLayer + LayersToShow) {
				layer.gameObject.SetActive(false);
			} else {
				SetSacleAndPos(layer, directly: true);
			}
		}

		private void OnCameraMove(Vector3 movement) {
			for (int i = _curLayer + 1; i <= _curMaxLayer; ++i) {
				var slope = Slope(_layers[i + RADIUS].Layer);
				// _layers[i + RADIUS].SmoothMove.Translate(movement * (1.0f - slope));
				_layers[i + RADIUS].transform.Translate(movement * (1.0f - slope));

				if (i - _curLayer + 1 == LayersToShow) 
					break;
			}
		}

		private void MoveForBackward(bool forward) {
			var lastCurLayer = _curLayer;
			if (forward) {
				if (_curLayer + 1 <= _curMaxLayer) {
					++_curLayer;
					_layers[lastCurLayer + RADIUS].gameObject.SetActive(false);
					if (lastCurLayer + LayersToShow <= _curMaxLayer) {
						var layerToShow = _layers[lastCurLayer + LayersToShow + RADIUS];
						layerToShow.gameObject.SetActive(true);
						SetSacleAndPos(layerToShow, directly: true);
					}
				}
			} else {
				if (_curLayer - 1 >= _curMinLayer) {
					--_curLayer;
					var layerToShow = _layers[lastCurLayer - 1 + RADIUS];
					layerToShow.gameObject.SetActive(true);
					SetSacleAndPos(layerToShow, directly: true);
					if (lastCurLayer + LayersToShow - 1 <= _curMaxLayer) {
						var layerToHide = _layers[lastCurLayer + LayersToShow - 1 + RADIUS];
						layerToHide.gameObject.SetActive(false);
					}
				}
			}
			if (lastCurLayer != _curLayer)
				for (int i = _curLayer; i <= _curMaxLayer; ++i) {
					SetSacleAndPos(_layers[i + RADIUS]);
					if (i - _curLayer + 1 == LayersToShow) 
						break;
				}
		}


		#region Utilities

			private void SetSacleAndPos(ILayer layer, bool directly = false) {
				float slope = Slope(layer.Layer);
				var targetPosition = new Vector3((1.0f - slope) * LayerCamera.transform.position.x, DEFAULT_LAYER_Y + (1.0f - slope) * YOffset * 10);
				var targetScale = new Vector3(DEFAULT_LAYER_SCALE * slope, DEFAULT_LAYER_SCALE * slope, 1);
				if (directly) {
					layer.SmoothMove.DirectlySet(targetPosition);
					layer.SmoothScale.DirectlySet(targetScale);
				} else {
					layer.SmoothMove.Target = targetPosition;
					layer.SmoothScale.Target = targetScale;
				}
			}
			/// <summary>
			/// 传入需要计算的Layer编号（比_curLayer大的）
			/// </summary>
			private float Slope(int n) {
				return _slope[n - _curLayer];
			}

		#endregion
	}
}