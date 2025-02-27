using System.Collections.Generic;
using NSFrame;

namespace BasicLogic 
{
	public class WorldManager : MonoSingleton<WorldManager>, IManager {

		public List<ILayer> Layers;
		public List<IVillager> Villagers;


		public void GameExit()
		{
			throw new System.NotImplementedException();
		}

		public void GameOver()
		{
			throw new System.NotImplementedException();
		}

		public void GamePause()
		{
			throw new System.NotImplementedException();
		}

		public void GameStart(GameSaveData gameSaveData) {
			Layers = new();
			gameSaveData.SavedLayers.ForEach( (layerData) =>  {
				var layer = ILayer.LoadLayerGO(layerData);
				Layers.Add(layer);
			} );
		}

		public void SaveGame(GameSaveData gameSaveData) {
			throw new System.NotImplementedException();
		}
	}
}