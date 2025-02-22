using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace BasicLogic {
	public class ArchConfigBase : ScriptableObject {
		public ArchType ArchType;
		public string Name;
		public NSPair<int, int> Size;
		public List<NSPair<int, string>> Introductions;
		public List<NSPair<int, List<NSPair<Repository, float>>>> ProduceVelocities;
		public List<NSPair<int, List<NSPair<Repository, float>>>> ConsumeVelocities;
		public List<NSPair<int, List<NSPair<Repository, float>>>> VolumeAdds;
	}
}