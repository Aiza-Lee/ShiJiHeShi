using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace BasicLogic {
	[CreateAssetMenu(menuName = "ShiJiHeShi/BasicGameConfig")]
	public class BasicGameConfig : ScriptableObject {
		/// <summary>
		/// 为每一种建筑选择 prefab
		/// </summary>
		public List<NSPair<ArchType, GameObject>> ArchPrefabs;	

		/// <summary>
		/// 根据建筑类型获得建筑的预制体
		/// </summary>
		/// <param name="archType">建筑类型</param>
		public GameObject GetPrefab(ArchType archType) {
			for (int i = 0; i < ArchPrefabs.Count; ++i) {
				if (ArchPrefabs[i].Key == archType) {
					return ArchPrefabs[i].Value;
				}
			}
			return null;
		}
	}
}
