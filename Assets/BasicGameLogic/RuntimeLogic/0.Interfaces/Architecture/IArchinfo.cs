using System.Collections.Generic;

namespace BasicLogic
{
	public interface IArchInfo {
		ArchType ArchType { get; }
		string ID { get; }
		int Layer { get; }
		int Order { get; }
		int Level { get; }
		List<Villager> InArchVillager { get; }

	}
}