using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace NSFrame
{
	public class SimpleNodeView : NSNodeViewBase {
		public NSPort OutputPort { get; set; }

		public override void DesignExtensionContainer() {
			return;
		}
		public override void DesignOutputContainer() {
			OutputPort = this.CreatePort("out", Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(int));
			outputContainer.Add(OutputPort);
		}

		public override NSNodeViewDataSO GetEmptyNodeViewDataSO() {
			return IOUtility.CreateSO<SimpleNodeViewDataSO>();
		}
		public override void SetViewDataSO(NSNodeViewDataSO nodeViewDataSO) {

			if (nodeViewDataSO is not SimpleNodeViewDataSO) {
				Debug.LogError("NS: NodeViewDataSO type wrong.");
				return;
			}
			var dataSO = nodeViewDataSO as SimpleNodeViewDataSO;

			dataSO.NodeViewName = NodeViewName;
			dataSO.NodeViewType = NodeViewType.FullName;
			dataSO.InputPortID = InputPort.ID;
			dataSO.OutputPortID = OutputPort.ID;
			dataSO.Position = GetPosition().position;

			if (GroupView == null) {
				dataSO.name = $"Node__{NodeViewName}";
			}
			else {
				dataSO.name = $"NodeInGroup__{GroupView.title}__{NodeViewName}";
			}
		}

		public override NSNodeSOBase GetEmptyNodeSO() {
			return IOUtility.CreateSO<SimpleNodeSO>();
		}
		public override void SetNodeSO(NSNodeSOBase nodeSOBase, NSGraphView graphView, Dictionary<string, NSNodeSOBase> nodeSOs) {

			if (nodeSOBase is not SimpleNodeSO) {
				Debug.LogError("NS: NodeSO type wrong.");
				return;
			}
			var nodeSO = nodeSOBase as SimpleNodeSO;

			nodeSO.NodeName = NodeViewName;
			nodeSO.NodeSOType = NodeViewType.FullName;

			nodeSO.NextNodes = new();
			nodeSO.PreviousNodes = new();
			// TODO: 从整个图中寻找会有性能浪费
			foreach (var edge in graphView.edges) {
				if (edge.output.node == this) {
					nodeSO.NextNodes.Add(nodeSOs[(edge.input.node as NSNodeViewBase).ID]);
				}
				else if (edge.input.node == this) {
					nodeSO.PreviousNodes.Add(nodeSOs[(edge.output.node as NSNodeViewBase).ID]);
				}
			}

			if (GroupView == null) {
				nodeSO.name = $"Node__{NodeViewName}";
			}
			else {
				nodeSO.name = $"NodeInGroup__{GroupView.title}__{NodeViewName}";
			}
		}
	}
}