using System.Collections.Generic;
using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace BasicLogic 
{
	public class WorldManager : MonoSingleton<WorldManager>, IManager {

		private readonly Dictionary<string, ArchBase> _id_Archs = new();
		private readonly Dictionary<string, Villager> _id_Vills = new();
		private readonly Dictionary<int, LayerBase> _layerID_Layers = new();


		private void OnEnable() {
			EventSystem.AddListener<LayerBase>((int)LogicEvent.LayerConstructed_L, (layer) => _layerID_Layers.Add(layer.Layer, layer));
			EventSystem.AddListener<ArchBase>((int)LogicEvent.ArchConstructed_A, (arch) => _id_Archs.Add(arch.ID, arch));
			EventSystem.AddListener<Villager>((int)LogicEvent.VillagerConstructed_V, (vill) => _id_Vills.Add(vill.ID, vill));

			// todo: deconstuct 事件绑定
		}


		/// <summary>
		/// 根据 ID 找到对应的IArch对象
		/// </summary>
		public ArchBase FindArch(string ID) {
			if (!_id_Archs.TryGetValue(ID, out var arch)) {
				Debug.LogWarning($"ID:{ID}, doesnot have a related architecture");
			}
			return arch;
		}
		public Villager FindVillager(string ID) {
			if (!_id_Vills.TryGetValue(ID, out var vill)) {
				Debug.LogWarning($"ID:{ID}, doesnot have a related villager");
			}
			return vill;
		}
		public LayerBase FindLayer(int layerID) {
			if (!_layerID_Layers.TryGetValue(layerID, out var layer)) {
				Debug.LogWarning($"ID:{layerID}, doesnot have a related layer");
			}
			return layer;
		}

		public List<Position> GetRoute(Position start, Position end) {
			if (end.RoadUp()) {
				Debug.LogWarning("Undefined Behaviour when end point is not Arch");
				return null;
			}

			// 一般的最短路
			var res = new List<Position>();
			Position cur = start;
			while (cur != end) {
				if (cur.Layer == end.Layer) {
					res.Add(end);
					break;
				}

				var distance = cur.Distance(end);
				var direction = cur.Coord == end.Coord ? RandomDirection() : (cur.Coord > end.Coord ? -1 : 1);
				if (cur.Layer < end.Layer) {
					if (cur.RoadUp()) {
						cur += (0, 1);
					} else {
						cur += (direction, 0);
					}
				} else {
					if (cur.RoadDown()) {
						cur += (0, -1);
					} else {
						cur += (direction, 0);
					}
				}

				res.Add(cur);
			}

			// todo: 随机化
			if (res.Count == 1) {
				return res;
			}



			return res;
		}


		public void GameExit() { }

		public void GameOver() { }

		public void GamePause() { }

		public void GameStart(GameSaveData gameSaveData) {

			gameSaveData.SavedLayers.ForEach( (data) =>  {
				var layer = GOFactory.LoadLayer(data, null);
			} );
			
			// Debug.Log(gameSaveData.SavedArchs.Count);

			gameSaveData.SavedArchs.ForEach( (data) => {
				var arch = GOFactory.LoadArch(data, _layerID_Layers[data.Layer].transform);
			} );

			gameSaveData.SavedVillagers.ForEach( (data) => {
				var villager = GOFactory.LoadVill(data, _layerID_Layers[data.Position.Layer].transform);
			} );
		}

		public void SaveGame(GameSaveData gameSaveData) { }


		#region Utlities

			private int RandomDirection() {
				return (Random.Range(0, 2) == 0) ? 1 : -1;
			}
			
		#endregion

	}
}