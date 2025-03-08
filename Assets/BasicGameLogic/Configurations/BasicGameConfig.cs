using System;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(menuName = "ShiJiHeShi/Config/BasicGame Config")]
	public class BasicGameConfig : ScriptableObject {

		public static Dictionary<ArchType, Type> ArchClasses = new() {
			{ArchType.Cottage, typeof(CottageArch)},
		};
		public static Dictionary<LayerType, Type> LayerClasses = new() {
			{LayerType.Grass, typeof(GrassLayer)},
			{LayerType.Snow, typeof(SnowLayer)},
		};



		public List<KVPair<ArchType, GameObject>> ArchPrefabs = new();	
		public List<KVPair<LayerType, GameObject>> LayerPrefabs = new();	
		public List<GameObject> VillagerPrefabs = new();
		public List<KVPair<JobType, JobConfig>> JobConfigs = new();

		[Space][Space]

		[Tooltip("白天的时间")] public int TicksOfDay;
		[Tooltip("夜晚的时间")] public int TicksOfNight;
		[Tooltip("每层可容纳建筑数量")] public int MaxContainPerLayer;
		[Tooltip("一共多少层")] public int MaxLayerAmount;

		public int MaxLayerRadius => (MaxLayerAmount - 1) / 2;


		private readonly Dictionary<ArchType, GameObject> _archPrefabs = new();
		private readonly Dictionary<LayerType, GameObject> _layerPrefabs = new();
		private readonly Dictionary<JobType, JobConfig> _jobConfigs = new();


		// note: 由GameManager激活该对象内部的字典以便于使用
		public void Activate() {
			foreach (var item in ArchPrefabs) _archPrefabs.Add(item.Key, item.Value);
			foreach (var item in LayerPrefabs) _layerPrefabs.Add(item.Key, item.Value);
			foreach (var item in JobConfigs) _jobConfigs.Add(item.Key, item.Value);
		}
		
		/// <summary>
		/// 获取建Arch的prefab
		/// </summary>
		/// <param name="archType"></param>
		/// <returns></returns>
		public GameObject GetArchPrefab(ArchType archType) {
			if (!_archPrefabs.TryGetValue(archType, out var prefab)) {
				Debug.LogError($"{archType} has no matched prefab.");
				return null;
			}
			return prefab;
		}
		
		/// <summary>
		/// 获取Layer的prefab
		/// </summary>
		/// <param name="layerType"></param>
		/// <returns></returns>
		public GameObject GetLayerPrefab(LayerType layerType) {
			if (!_layerPrefabs.TryGetValue(layerType, out var prefab)) {
				Debug.LogError($"{layerType} has no matched prefab.");
				return null;
			}
			return prefab;
		}

		/// <summary>
		/// 获取对应职业的配置文件
		/// </summary>
		/// <param name="jobType"></param>
		/// <returns></returns>
		public JobConfig GetJobConfig(JobType jobType) {
			if (!_jobConfigs.TryGetValue(jobType, out var config)) {
				Debug.LogError($"{jobType} has no matched config.");
				return null;
			}
			return config;
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
