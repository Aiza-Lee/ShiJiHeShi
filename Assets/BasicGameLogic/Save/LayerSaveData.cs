using System;
using System.Collections.Generic;

namespace BasicLogic
{
	[Serializable]
	public class LayerSaveData {
		public string ID;
		public LayerType LayerType;
		public int Layer;
		public List<ArchSaveData> InLayerArches;
		
		public LayerSaveData(LayerType _layerType, int _layer) {
			ID = Guid.NewGuid().ToString();
			InLayerArches = new();
			LayerType = _layerType;
			Layer = _layer;
		}
	}
}