using System.Collections.Generic;

namespace BasicLogic
{
	public enum Job {
		Farmer,
		Timberjack
	}
	public interface IVillager {
		string ID { get; }
		string FirstName { get; set; }
		string LastName { get; set; }
		Job Job { get; set; }
		List<IVillagerTask> VillagerTasksToDo { get; set; }
	}
}