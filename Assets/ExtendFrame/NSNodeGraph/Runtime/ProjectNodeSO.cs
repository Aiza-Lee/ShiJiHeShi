using System.Collections.Generic;
using BasicLogic;
using NSFrame;
using UnityEngine;

namespace ExtendFrame {
	public class ProjectNodeSO : NSNodeSOBase {
		public List<NSNodeSOBase> NextNodes;
		public string Tag;
		[TextArea(5, 30)] public string Text;
		public List<NSPair<Repository, ulong>> Demands;
		public List<NSPair<ArchType, int>> Unlocks, Buffs;

	}
}