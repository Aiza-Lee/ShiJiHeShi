using System.Collections.Generic;
using NSFrame;
using UnityEngine.Events;

namespace BasicLogic {
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
	
	public interface IVillagerTask {
		UnityAction Task { get; set; }
		UnityAction OnTaskDown { get; set; }
		List<NSPair<Repository, ulong>> Demands { get; set; }
	}
}