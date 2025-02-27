using System;
using System.Collections.Generic;
using System.Linq;
using NSFrame;
using UnityEngine;

namespace BasicLogic 
{
	/// <summary>
	/// 集中管理全局通用的常量。游戏存档加载入口。管理所有的管理类。
	/// </summary>
	public class GameManager : MonoSingleton<GameManager> {

		public static int RepositorySize { get; private set; }
		public static int ArchSize { get; private set; }
		public static int JobSize { get; private set; }
		public static int LayerSize { get; private set; }
		static GameManager() {
			RepositorySize = Enum.GetValues(typeof(Repository)).Length;
			ArchSize = Enum.GetValues(typeof(ArchType)).Length;
			JobSize = Enum.GetValues(typeof(Job)).Length;
			LayerSize = Enum.GetValues(typeof(LayerType)).Length;
		}


		[Header("挂载")]
		public BasicGameConfig GameConfig;


		private GameSaveData _gameSaveData;
		private List<IManager> _managers;
		private SaveInfo _curSaveInfo;

		private void Start() {
			_managers = new() {
				RepositoryManager.Instance,
				TickManager.Instance, 
				WorldManager.Instance
			};
			// test:
			var saves = SaveSystem.GetAllSaveInfos();
			_curSaveInfo = saves.Last();
			SaveSystem.SaveObject(_curSaveInfo, new GameSaveData());
			GameStart(_curSaveInfo);
		}
		// test:
		private void Update() {	
			if (Input.GetKeyDown(KeyCode.N)) {
				_curSaveInfo = SaveSystem.CreateSaveFile("测试存档01");
				GameStart(_curSaveInfo);
			}
		}

		public void GameStart(SaveInfo saveInfo) {
			_gameSaveData = SaveSystem.LoadObject<GameSaveData>(saveInfo);

			_managers.ForEach( (manager) => manager.GameStart(_gameSaveData) );
		}
		public void SaveGame(SaveInfo saveInfo) {
			_managers.ForEach( (manager) => manager.SaveGame(_gameSaveData) );
			SaveSystem.SaveObject(saveInfo, _gameSaveData);
		}
		public void GamePause() {}
		public void SetGameSpeed(float speed) {}
		public void GameOver() {}
	}
}