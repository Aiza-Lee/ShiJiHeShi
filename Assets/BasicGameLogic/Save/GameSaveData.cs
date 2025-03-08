using System;
using System.Collections.Generic;

namespace BasicLogic 
{
	/// <summary>
	/// 存档村庄状态的类
	/// </summary>
	[Serializable]
	public class GameSaveData {
		public List<LayerDataBase> SavedLayers;
		public List<VillData> SavedVillagers;
		public List<ArchDataBase> SavedArchs;
		public RepoList SavedRepositoryAmounts;

		public GameSaveData() {
			SavedLayers = new();
			SavedVillagers = new();
			SavedArchs = new();
			SavedRepositoryAmounts = new(fillAll: true);
		}
	}
	/*
		Game
		  |---Layers
		  |		|---Archs
		  |
		  |---Villagers
		  |---Repositories
	*/
}