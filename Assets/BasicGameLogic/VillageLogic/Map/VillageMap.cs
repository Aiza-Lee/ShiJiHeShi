using System.Collections.Generic;
using NSFrame;

namespace BasicLogic
{
	/// <summary>
	/// 完成寻路功能
	/// </summary>
	public class VillageMap : MonoSingleton<VillageMap> {
		public float CellSize;

		private List<IArchitecture> _map;
		private int _maxLayer, _minLayer, _maxOrder, _minOrder;
	}
}