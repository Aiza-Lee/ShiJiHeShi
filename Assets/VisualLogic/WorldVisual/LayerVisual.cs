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

		private const float DEFAULT_LAYER_SCALE = 1f;
		private const float DEFAULT_LAYER_Y = -1f;
		private const float LOGIC_VISUAL_RATE = 1f;

		[Header("挂载")]
		public LayerCameraController LayerCamera;

		[Header("Cosntants")]
		[Tooltip("层间距")][Range(0f, 10f)] public float LayerGap;
		[Tooltip("")][Range(0f, 5f)] public float ArchGap;
		[Tooltip("相机距离")][Range(2f, 10f)] public float CameraDistance; 
		[Tooltip("每层Y坐标偏移量")][Range(0f, 0.3f)] public float YOffset;
		public int LayersToShow;

		[Header("Informations")]
		[SerializeField][ReadOnly] private int _curLayer = 0;
		[SerializeField][ReadOnly] private List<LayerBase> _layers;
		[SerializeField][ReadOnly] private int _curMaxLayer = -1;
		[SerializeField][ReadOnly] private int _curMinLayer = 0;

		private List<float> _slope;
		private int RADIUS => GameManager.Instance.GameConfig.MaxLayerRadius;
		private int LAYER_MAX => GameManager.Instance.GameConfig.MaxLayerAmount;

		private void Start() {

			_slope = new();
			for (int i = -LAYER_MAX; i < LAYER_MAX; ++i) {
				_slope.Add(CameraDistance / (CameraDistance + i * LayerGap));
			}

			_layers = new();
			for (int i = 0; i < LAYER_MAX; ++i)
				_layers.Add(null); 
		}
		private void OnEnable() {
			EventSystem.AddListener<LayerBase>((int)LogicEvent.LayerConstructed_L, OnLayerCreate);
			EventSystem.AddListener<ArchBase>((int)LogicEvent.ArchConstructed_A, OnArchCreate);
			EventSystem.AddListener<Villager>((int)LogicEvent.VillagerConstructed_V, OnVillCreate);
			EventSystem.AddListener<Vector3>((int)LogicEvent.LayerCameraMove_v3, OnCameraMove);
			EventSystem.AddListener<bool>((int)LogicEvent.LayerCameraFB_b, MoveForBackward);
			EventSystem.AddListener<Villager, int>((int)LogicEvent.VillagerMove_Vi, OnVillMove);
		}
		private void OnDisable() {
			EventSystem.RemoveListener<LayerBase>((int)LogicEvent.LayerConstructed_L, OnLayerCreate);
			EventSystem.RemoveListener<ArchBase>((int)LogicEvent.ArchConstructed_A, OnArchCreate);
			EventSystem.RemoveListener<Villager>((int)LogicEvent.VillagerConstructed_V, OnVillCreate);
			EventSystem.RemoveListener<Vector3>((int)LogicEvent.LayerCameraMove_v3, OnCameraMove);
			EventSystem.RemoveListener<bool>((int)LogicEvent.LayerCameraFB_b, MoveForBackward);
			EventSystem.RemoveListener<Villager, int>((int)LogicEvent.VillagerMove_Vi, OnVillMove);
		}

		#region 事件绑定
			private void OnArchCreate(ArchBase arch) {
				int sortingLayerID = SortingLayer.NameToID("m_Layer" + (RADIUS + arch.Layer));
				arch.SpriteRenderer.sortingLayerID = sortingLayerID;
				foreach (var light in arch.Light2Ds) {
					(light as Light2D).SetLayers(sortingLayerID);
				}
				arch.transform.Translate(LOGIC_VISUAL_RATE * arch.Position.Coord, 0, 0);
			}

			private void OnLayerCreate(LayerBase layer) {
				layer.SpriteRenderers.ForEach( (spriteRenderer) => spriteRenderer.sortingLayerName = "m_Layer" + (RADIUS + layer.Layer) );
				_layers[layer.Layer + RADIUS] = layer;
				_curMaxLayer = Mathf.Max(_curMaxLayer, layer.Layer);
				_curMinLayer = Mathf.Min(_curMinLayer , layer.Layer);
				if (layer.Layer < _curLayer || layer.Layer >= _curLayer + LayersToShow) {
					layer.gameObject.SetActive(false);
				} else {
					SetSacleAndPos(layer, directly: true);
				}
			}

			private void OnVillCreate(Villager villager) {
				int sortingLayerID = SortingLayer.NameToID("m_Layer" + (RADIUS + villager.Position.Layer));
				villager.SpriteRenderer.sortingLayerID = sortingLayerID;
				// villager.transform.Translate()
			}

			private void OnCameraMove(Vector3 movement) {
				for (int i = _curMinLayer; i <= _curMaxLayer; ++i) {
					var slope = Slope(_layers[i + RADIUS].Layer);
					_layers[i + RADIUS].SmoothMove.Translate(movement * (-slope), 1);
				}
			}
		
			private void MoveForBackward(bool forward) {
				var lastCurLayer = _curLayer;
				if (forward) {
					if (_curLayer + 1 <= _curMaxLayer) {
						++_curLayer;
						LayerDispear(_layers[lastCurLayer + RADIUS]);
						if (lastCurLayer + LayersToShow <= _curMaxLayer) {
							LayerEmerge(_layers[lastCurLayer + LayersToShow + RADIUS]);
						}
					}
				} else {
					if (_curLayer - 1 >= _curMinLayer) {
						--_curLayer;
						LayerEmerge(_layers[lastCurLayer - 1 + RADIUS]);
						if (lastCurLayer + LayersToShow - 1 <= _curMaxLayer) {
							LayerDispear(_layers[lastCurLayer + LayersToShow - 1 + RADIUS]);
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
		
			private void OnVillMove(Villager villager, int movement) {
				villager.SmoothMoveTick.Translate(new(movement * LOGIC_VISUAL_RATE, 0, 0));
			}
		#endregion

		#region Utilities

			private void LayerEmerge(LayerBase layer) {
				layer.SetAlphaDirect(0f);
				layer.gameObject.SetActive(true);
				layer.SetAlpha(1f, () => layer.SetAlphaDirect(1f));
				SetSacleAndPos(layer, directly: true);
			}
			private void LayerDispear(LayerBase layer) {
				layer.SetAlpha(0f, () => {
					layer.gameObject.SetActive(false);
					layer.SetAlphaDirect(0f);
				});
			}

			private void SetSacleAndPos(LayerBase layer, bool directly = false) {
				float slope = Slope(layer.Layer);
				var targetPosition = new Vector3((-slope) * LayerCamera.VirtualPosition.x, DEFAULT_LAYER_Y + (-slope) * YOffset * 10);
				var targetScale = new Vector3(DEFAULT_LAYER_SCALE * slope, DEFAULT_LAYER_SCALE * slope, 1);
				if (directly) {
					layer.SmoothMove.DirectlySet(targetPosition);
					layer.SmoothScale.DirectlySet(targetScale);
				} else {
					layer.SmoothMove.SetTarget(targetPosition);
					layer.SmoothScale.SetTarget(targetScale);
				}
			}
			private float Slope(int n) {
				return _slope[n - _curLayer + LAYER_MAX];
			}

		#endregion
	}
}