using System;

namespace BasicLogic
{
	[Serializable]
	public class MoveTaskData : TaskDataBase {
		public int OneMoveTick;
		public int Target;
		public int Count;

		public bool ReadySetMV;
		public bool ReadySetTask;
	}
}