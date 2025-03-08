using System.Linq;
using NSFrame;
using UnityEngine;

namespace BasicLogic
{
	// Test:
	public class Test : MonoSingleton<Test> {

		public PredefinedWorldConfig PredefinedWorldConfig;

		private SaveInfo _curSaveInfo;

		private void Start() {
			var saves = SaveSystem.GetAllSaveInfos();
			_curSaveInfo = saves.Last();
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.N)) {
				SaveSystem.SaveObject(_curSaveInfo, PredefinedWorldConfig.GameSaveData);
				GameManager.Instance.GameStart(_curSaveInfo);
			}
			if (Input.GetKeyDown(KeyCode.Q)) {
				var vill = WorldManager.Instance.FindVillager("114514");
				var task = new MoveTask();
				task = PoolSystem.PopObj<MoveTask>();
				vill.TaskRunner.AddTask(task);
				task.SetTask(10, 10);
			}
		}
	}
}