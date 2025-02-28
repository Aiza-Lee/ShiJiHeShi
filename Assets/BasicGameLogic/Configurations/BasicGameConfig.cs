using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(menuName = "ShiJiHeShi/Config/BasicGame Config")]
	public class BasicGameConfig : ScriptableObject {

		public List<KVPair<ArchType, GameObject>> ArchPrefabs = new();	
		public List<KVPair<LayerType, GameObject>> LayerPrefabs = new();	
		public List<GameObject> VillagerPrefabs = new();

		[Space][Space]

		[Tooltip("白天的时间")] public int TicksOfDay;
		[Tooltip("夜晚的时间")] public int TicksOfNight;
		[Tooltip("每层长度")] public int MaxContainPerLayer;
		[Tooltip("一共多少层")] public int MaxLayerAmount;

		public int MaxLayerRadius => (MaxLayerAmount - 1) / 2;

		public BasicGameConfig() {
			for (int i = 0; i < GameManager.ArchSize; ++i) ArchPrefabs.Add(new((ArchType)i, null));
			for (int i = 0; i < GameManager.LayerSize; ++i) LayerPrefabs.Add(new((LayerType)i, null));
		}
		
		/// <summary>
		/// 获取建Arch的prefab
		/// </summary>
		/// <param name="archType"></param>
		/// <returns></returns>
		public GameObject GetArchPrefab(ArchType archType) {
			for (var i = 0; i < ArchPrefabs.Count; ++i) {
				if (ArchPrefabs[i].Key == archType) {
					return ArchPrefabs[i].Value;
				}
			}
			Debug.LogWarning($"{archType} has no matched prefab.");
			return null;
		}
		
		/// <summary>
		/// 获取Layer的prefab
		/// </summary>
		/// <param name="layerType"></param>
		/// <returns></returns>
		public GameObject GetLayerPrefab(LayerType layerType) {
			for (var i = 0; i < LayerPrefabs.Count; ++i) {
				if (LayerPrefabs[i].Key == layerType) {
					return LayerPrefabs[i].Value;
				}
			}
			Debug.LogWarning($"{layerType} has no matched prefab.");
			return null;
		}

		/// <summary>
		/// 获取Villager的prefab
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public GameObject GetVillagerPrefab(string ID = "Todo:") {
			return VillagerPrefabs.Count == 0 ? null : VillagerPrefabs[0];
		}
		
	}
}
