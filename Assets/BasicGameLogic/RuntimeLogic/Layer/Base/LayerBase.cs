using System;
using System.Collections.Generic;
using System.Linq;
using LogicUtilities;
using UnityEngine;

namespace BasicLogic
{
	public abstract class LayerBase : MonoBehaviour, ISaveable<LayerDataBase>, ILayerInfo {

		[Header("挂载")]
		public LayerConfig Config;

		#region Config Getter
		
			public string Name => Config.Name;
			public string Introductions => Config.Introductions;

		#endregion
		

		#region ILayerInfo
			public int Layer { get; set; }
			public LayerType LayerType { get; set; }

			public List<Villager> InLayerVills { get; set; }
			public List<ArchBase> InLayerArchs { get; set; }
		#endregion

		public SmoothMove SmoothMove { get; private set; }
		public SmoothScale SmoothScale { get; private set; }
		public List<SmoothFade> SmoothFades { get; private set; }
		public List<SpriteRenderer> SpriteRenderers { get; private set; }

		private void Awake() {

			InLayerArchs = new();
			InLayerArchs = new();

			SmoothMove = GetComponent<SmoothMove>();
			SmoothScale = GetComponent<SmoothScale>();
			// 在还没有加入建筑的时候获得以下两个列表
			SmoothFades = GetComponentsInChildren<SmoothFade>().ToList();
			SpriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
		}

		public void SetAlpha(float value, Action callBack = null) {
			value = Mathf.Clamp01(value);
			SmoothFades.ForEach( (smoothFade) => smoothFade.SetTarget(value, 0, callBack));
			InLayerArchs.ForEach( (arch) => arch.SmoothFade.SetTarget(value) );
		}
		public void SetAlphaDirect(float value) {
			value = Mathf.Clamp01(value);
			SmoothFades.ForEach( (smoothFade) => smoothFade.DirectlySet(value));
			InLayerArchs.ForEach( (arch) => arch.SmoothFade.DirectlySet(value) );
		}

		#region ISaveable
			public virtual LayerDataBase GetData() {
				return new LayerDataBase() {
					LayerType = LayerType,
					Layer = Layer,
				};
			}

			public virtual void InitData(LayerDataBase data) {
				LayerType = data.LayerType;
				Layer = data.Layer;
			}
		#endregion


	}
}