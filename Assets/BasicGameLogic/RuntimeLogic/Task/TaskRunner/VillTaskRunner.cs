using System;
using System.Collections.Generic;
using System.Linq;
using NSFrame;

namespace BasicLogic
{
	public class VillTaskRunner : ISaveable<TaskRunnerData> {
		private readonly Villager _villager;
		private readonly Queue<ITask> _tasks;
		private ITask _curTask;
		private bool _working;

		public VillTaskRunner(Villager villager) {
			_villager = villager;
			_tasks = new();
		}

		public void AddTask(ITask task) {
			_tasks.Append(task);
			task.SetMissionAndVillager(this, _villager);
		}

		public void StartMission() {
			if (_working) {
				return;
			}
			NextTask();
		}

		/// <summary>
		/// 切换下一个任务。1、由Task执行完毕后调用。  2、由任务开始时由该Mission调用
		/// </summary>
		public void NextTask() {
			if (!_tasks.TryDequeue(out _curTask)) {
				_working = false;
				return;
			}
			_working = true;
			_curTask.TaskStart();
		}
		#region ISaveable
			public TaskRunnerData GetData() {
				var data = new TaskRunnerData {
					Working = _working,
					Tasks = new()
				};
				foreach (var task in _tasks) {
					data.Tasks.Add(task.GetData());
				}
				return data;
			}

			public void InitData(TaskRunnerData data) {
				_working = data.Working;
				_tasks.Clear();
				if (data.Tasks != null) foreach (var taskData in data.Tasks) {
					var task = PoolSystem.PopObj(Type.GetType(taskData.TaskTypeFullName)) as ITask;
					task.InitData(taskData);
					_tasks.Append(task);
				}
			}
		#endregion
	}
}