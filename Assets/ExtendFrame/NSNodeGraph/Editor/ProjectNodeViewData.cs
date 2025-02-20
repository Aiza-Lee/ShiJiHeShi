using System.Collections.Generic;
using NSFrame;

namespace ExtendFrame {
	public class ProjectNodeViewDataSO : NSNodeViewDataSO {
		public string OutputPortID;
		public string Text;
		public List<NSPair<BasicLogic.Repository, ulong>> Demands;

		public override void GetOutputPort(NSNodeViewBase nodeView, Dictionary<string, NSPort> ports) {
			if (nodeView is ProjectNodeView projectNodeView) {
				ports.Add(OutputPortID, projectNodeView.OutputPort);
			}
		}

		public override void SetNodeView(NSNodeViewBase nodeViewBase) {
			var nodeView = nodeViewBase as ProjectNodeView;
			nodeView.TextTF.value = Text;
			foreach (var demand in Demands) {
				nodeView.AddDemand(new(demand.Key, demand.Value));
			}
		}
	}
}