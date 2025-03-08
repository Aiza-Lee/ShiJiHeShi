using System;
using System.Collections.Generic;

namespace BasicLogic
{
	[Serializable]
	public class TaskRunnerData {
		public bool Working;
		public List<TaskDataBase> Tasks;
	}
}