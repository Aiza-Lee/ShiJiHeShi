using System.Collections.Generic;
using NSFrame;
using UnityEngine.Events;

namespace BasicLogic
{
	public interface IVillagerTask {
		UnityAction Task { get; set; }
		UnityAction OnTaskDown { get; set; }
		List<NSPair<Repository, ulong>> Demands { get; set; }
	}
}