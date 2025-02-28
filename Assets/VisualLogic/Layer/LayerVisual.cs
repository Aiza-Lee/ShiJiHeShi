using System;
using System.Collections.Generic;
using BasicLogic;
using LogicUtilities;
using NSFrame;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

namespace VisualLogic 
{
	/// <summary>
	/// 管理 2D 视角下建筑的放缩问题
	/// 管理摄像机的移动
	/// </summary>
	public partial class LayerVisual : MonoSingleton<LayerVisual> {

		[Header("挂载")]
		public CameraController MainCamera;

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
		private int _radius => GameManager.Instance.GameConfig.MaxLayerRadius;
		private int _layerMax => GameManager.Instance.GameConfig.MaxLayerAmount;

		public void Forward() {}
		public void Backward() {}

		private void Start() {
			EventSystem.AddListener<ILayer>("LayerCTOR", OnLayerCreate);
			EventSystem.AddListener<IArch>("CTOR", OnArchCreate);
			
			_cameraLastPos = MainCamera.transform.position;

			_slope = new();
			for (int i = 0; i < _layerMax; ++i) {
				_slope.Add(CameraDistance / (CameraDistance + i * LayerGap));
			}

			_layers = new();
			for (int i = 0; i < _layerMax; ++i)
				_layers.Add(null); 
		}

		private void Update() {
			if (!_cameraLastPos.IsApproximatelyEqual(MainCamera.transform.position)) {
				OnCameraMove(MainCamera.transform.position - _cameraLastPos);
				_cameraLastPos = MainCamera.transform.position;
			}
		}

		private void OnArchCreate(IArch arch) {
			int sortingLayerID = SortingLayer.NameToID("m_Layer" + (_radius + arch.Layer));
			arch.SpriteRenderer.sortingLayerID = sortingLayerID;
			foreach (var light in arch.Light2Ds) {
				(light as Light2D).SetLayers(sortingLayerID);
			}
			arch.transform.Translate(ArchGap * arch.Order, 0, 0);
		}

		private void OnLayerCreate(ILayer layer) {
			layer.SpriteRenderer.sortingLayerName = "m_Layer" + (_radius + layer.Layer);
			_layers[layer.Layer + _radius] = layer;
			_curMaxLayer = Mathf.Max(_curMaxLayer, layer.Layer);
			_curMinLayer = Mathf.Min(_curMinLayer , layer.Layer);
			if (layer.Layer < _curLayer || layer.Layer >= _curLayer + LayersToShow) {
				layer.gameObject.SetActive(false);
			} else {
				SetSacleAndPos(layer);
			}
		}

		private void OnCameraMove(Vector3 movement) {
			for (int i = _curLayer + 1; i <= Mathf.Min(_curMaxLayer, _layerMax); ++i) {
				var slope = Slope(_layers[i + _radius].Layer - _curLayer);
				_layers[i + _radius].LerpMove.Translate(movement * (1.0f - slope));

				if (i - _curLayer + 1 == LayersToShow) 
					break;
			}
		}


		#region Utilities

			private void SetSacleAndPos(ILayer layer) {
				float slope = Slope(layer.Layer - _curLayer);
				layer.transform.localScale *= slope;
				layer.transform.Translate(new(0, (1.0f - slope) * YOffset * 10));
			}
			private float Slope(int n) {
				return _slope[n];
			}

		#endregion
	}
}