using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace BasicLogic 
{
	public class VillageManager : MonoSingleton<VillageManager> {

		[Header("Constants To Be Adjusted")]
		[Tooltip("Layer间隔长度")] public float LayerGap = 20;
		[Tooltip("Cell 长度")] public float CellLength = 5;

		[Header("Configurations")]
		public BasicGameConfig BasicGameConfig;

		[Header("Informations")]
		public List<NSPair<Repository, float>> RepositoryVelocity;
		
		public VillageManager() {
			RepositoryVelocity = new();
		}

		public void AddVelocity(Repository repository, float value) {
			for (int i = 0; i < RepositoryVelocity.Count; ++i) {
				var rv_i = RepositoryVelocity[i];
				if (repository == rv_i.Key) {
					RepositoryVelocity[i] = new(rv_i.Key, rv_i.Value + value);
					break;
				}
			}
		}
		

	}
}