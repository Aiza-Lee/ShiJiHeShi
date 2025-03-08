using System;
using System.Collections.Generic;
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
			JobSize = Enum.GetValues(typeof(JobType)).Length;
			LayerSize = Enum.GetValues(typeof(LayerType)).Length;
		}

		[Header("挂载")]
		public BasicGameConfig GameConfig;


		private SaveInfo _curSaveInfo;
		private GameSaveData _gameSaveData;
		private List<IManager> _managers;

		private void Start() {
			_managers = new() {
				RepositoryManager.Instance,
				TickManager.Instance, 
				WorldManager.Instance
			};
			GameConfig.Activate();
		}
		
		// test:
		private void Update() {	
			if (Input.GetKeyDown(KeyCode.P)) {
				GameExit();
			}
		}

		public void GameStart(SaveInfo saveInfo) {
			_curSaveInfo = saveInfo;
			_gameSaveData = SaveSystem.LoadObject<GameSaveData>(saveInfo);

			_managers.ForEach( (manager) => manager.GameStart(_gameSaveData) );
		}
		public void SaveGame(SaveInfo saveInfo) {
			_managers.ForEach( (manager) => manager.SaveGame(_gameSaveData) );
			SaveSystem.SaveObject(saveInfo, _gameSaveData);
		}
		public void GamePause() {}
		public void GameExit() {
			SaveGame(_curSaveInfo);
			_managers.ForEach( (manager) => manager.GameExit() );
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#endif
			Application.Quit();
		}
		public void GameOver() {}
	}
}