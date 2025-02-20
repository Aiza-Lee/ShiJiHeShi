using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace ExtendFrame {
	public class ProjectNodeSO : NSNodeSOBase {
		public List<NSNodeSOBase> NextNodes;
		[TextArea(5, 30)] public string Text;
		public List<NSPair<BasicLogic.Repository, ulong>> Demands;

	}
}