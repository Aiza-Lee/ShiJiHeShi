using System.Collections.Generic;

namespace BasicLogic
{
	public interface ILayerInfo {
		int Layer { get; set; }
		LayerType LayerType { get; set; }
		List<Villager> InLayerVills { get; set; }
		List<ArchBase> InLayerArchs { get; set; }
	}
}