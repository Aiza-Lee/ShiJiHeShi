using System.Linq;
using NSFrame;
using UnityEngine;

namespace BasicLogic
{
	public class Test : MonoSingleton<Test> {

		public PredefinedWorldConfig PredefinedWorldConfig;

		private SaveInfo _curSaveInfo;

		private void Start() {
			var saves = SaveSystem.GetAllSaveInfos();
			_curSaveInfo = saves.Last();
			SaveSystem.SaveObject(_curSaveInfo, PredefinedWorldConfig.GameSaveData);
		}

		private void Update() {
			if (Input.GetKeyDown( KeyCode.S)) {
				GameManager.Instance.GameStart(_curSaveInfo);
			}
		}
	}
}