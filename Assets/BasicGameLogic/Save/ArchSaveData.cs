using System;

namespace BasicLogic 
{
	[Serializable]
	public class ArchSaveData {
		public string ID;
		public int Layer;
		public int Order;
		public int Level;
		public ArchType ArchType;

		public ArchSaveData(ArchType _archType, int _layer, int _order, int _level = 0) {
			ID = Guid.NewGuid().ToString();
			Layer = _layer;
			ArchType = _archType;
			Order = _order;
			Level = _level;
		}
	}
}