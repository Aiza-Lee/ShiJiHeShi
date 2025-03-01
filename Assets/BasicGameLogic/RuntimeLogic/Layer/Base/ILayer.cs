using System.Collections.Generic;
using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace BasicLogic
{
	public abstract class ILayer : MonoBehaviour {

		[Header("挂载")]
		public LayerConfigBase Config;
		
		[Header("Informations")]
		[SerializeField][ReadOnly] protected LayerSaveData _saveData;
		[SerializeField][ReadOnly] protected List<IArch> _inLayerArches;

		public int Layer => _saveData.Layer;
		public LayerType LayerType => _saveData.LayerType;

		public List<IArch> InLayerArches => _inLayerArches;

		public SmoothMove SmoothMove { get; private set; }
		public SmoothScale SmoothScale { get; private set; }
		public LerpMove LerpMove { get; private set; }
		public SpriteRenderer SpriteRenderer { get; private set; }

		private void Awake() {
			SmoothMove = GetComponent<SmoothMove>();
			SmoothScale = GetComponent<SmoothScale>();
			LerpMove = GetComponent<LerpMove>();
			SpriteRenderer = GetComponent<SpriteRenderer>();
		}

		/// <summary>
		/// 从全局游戏配置中实例化Layer的预制体
		/// </summary>
		/// <returns>预制体上的ILayer脚本。</returns>
		public static ILayer LoadLayerGO(LayerSaveData layerSaveData, Transform father = null) {
			var prefab = GameManager.Instance.GameConfig.GetLayerPrefab(layerSaveData.LayerType);
			var go = GameObject.Instantiate(prefab, father);
			if (!go.TryGetComponent<ILayer>(out var layer)) {
				Debug.LogError($"Cannot get component ILayer from perfab of type {layerSaveData.LayerType}");
				return null;
			}
			go.name = layer.Config.Name;
			layer._saveData = layerSaveData;
			layerSaveData.InLayerArches?.ForEach( (archData) => layer._inLayerArches.Add(IArch.LoadArchGO(archData, layer.transform)) );

			layer.OnConstruct();
			return layer;
		}

		public static ILayer NewLayer(LayerType layerType, int layerCount, Transform father = null) {
			var prefab = GameManager.Instance.GameConfig.GetLayerPrefab(layerType);
			var go = GameObject.Instantiate(prefab, father);
			if (!go.TryGetComponent<ILayer>(out var layer)) {
				Debug.LogError($"Cannot get component ILayer from perfab of type {layerType}");
				return null;
			}
			go.name = layer.Config.Name;
			layer._saveData = new(layerType, layerCount);
			
			layer.OnConstruct();
			return layer;
		}

		protected void OnConstruct() {
			EventSystem.Invoke<ILayer>("LayerCTOR", this);
		}

	}
}