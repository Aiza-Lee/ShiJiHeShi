using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace BasicLogic
{
	/// <summary>
	/// 完成寻路功能
	/// </summary>
	public class VillageMap : MonoSingleton<VillageMap> {
		
		[Header("Constants To Be Adjusted")]
		[Tooltip("单元格边长")] public float CellSize;

		[Header("Information")]
		[SerializeField] private List<IArch> _map;
		[SerializeField] private int _maxLayer, _minLayer, _maxOrder, _minOrder;

		VillageMap() {
			_map = new();
		}

		public void AddToMap(IArch arch) {
			_map.Add(arch);
			_maxLayer = Mathf.Max(_maxLayer, arch.Layer);
			_minLayer = Mathf.Min(_minLayer, arch.Layer);
			_maxOrder = Mathf.Max(_maxOrder, arch.Order);
			_minOrder = Mathf.Min(_minOrder, arch.Order);
		}
	}
}