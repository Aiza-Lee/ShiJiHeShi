using System;
using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace BasicLogic
{
	public class MoveTask : ITask {
		static MoveTask() {
			PoolSystem.InitObjectPool<MoveTask>();
		}

		private VillTaskRunner _mission;
		private Villager _villager;
		private int _oneMoveTick;
		private int _target;
		private int _count;
		
		private bool _readySetMV;
		private bool _readySetTask;

		public Type TaskType => typeof(MoveTask);

		public void SetMissionAndVillager(VillTaskRunner mission, Villager villager) {
			_mission = mission;
			_villager = villager;
			_readySetMV = true;
		}
		public void SetTask(int oneMoveTick, int target) {
			_oneMoveTick = oneMoveTick;
			_target = target;
			_count = 0;
			_readySetTask = true;
		}

		public void DestroyForPool() {
			_mission = null;
			_villager = null;
		}

		public void InitForPool() {
			_readySetMV = false;
			_readySetTask = false;
		}


		public void TaskEnd() {
			_mission.NextTask();
			PoolSystem.PushObj(this);
		}

		public void TaskStart() {
			if (!_readySetMV || !_readySetTask) {
				Debug.LogError("The task has not been set properly.");
				return ;
			}
			DelayTrigger.Run(DoTask, _oneMoveTick);
		}

		private void DoTask() {
			var movement = _target > 0 ? 1 : -1;
			_villager.Position.Translate(0, movement);
			EventSystem.Invoke<Villager, int>((int)LogicEvent.VillagerMove_Vi, _villager, movement);
			++_count;
			if (_count == Mathf.Abs(_target)) {
				TaskEnd();
				return;
			}
			DelayTrigger.Run(DoTask, _oneMoveTick);
		}

		#region ISaveable
			public TaskDataBase GetData() {
				return new MoveTaskData() {
					TaskTypeFullName = TaskType.FullName,
					Target = _target,
					Count = _count,
					ReadySetMV = _readySetMV,
					ReadySetTask = _readySetTask,
				};
			}

			public void InitData(TaskDataBase saveData) {
				var data = saveData as MoveTaskData;
				_target = data.Target;
				_count = data.Count;
				_readySetMV = data.ReadySetMV;
				_readySetTask = data.ReadySetTask;

				TaskStart();
			}
		#endregion

	}
}