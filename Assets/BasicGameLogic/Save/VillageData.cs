using System;
using System.Collections.Generic;
using NSFrame;

namespace BasicLogic {
	/// <summary>
	/// 存档村庄状态的类
	/// </summary>
	[Serializable]
	public class VillageData {
		public List<ArchData> ArchitectureDatas;
		public List<VillagerData> VillagerDatas;
		public List<NSPair<Repository, float>> RepositoryAmounts;
	}
}