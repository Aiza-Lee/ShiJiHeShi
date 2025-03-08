using System;
using NSFrame;

namespace BasicLogic
{
	public interface ITask : ISaveable<TaskDataBase>, IPooledObject {
		Type TaskType { get; }
		void SetMissionAndVillager(VillTaskRunner mission, Villager villager);
		void TaskStart();
		void TaskEnd();
	}
}