namespace BasicLogic 
{
	public interface IManager {
		void GameStart(GameSaveData gameSaveData);
		void SaveGame(GameSaveData gameSaveData);
		void GamePause();
		void GameExit();
		void GameOver();
	}
}

