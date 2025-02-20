using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NSFrame
{
	public class TextNodeView : NSNodeViewBase {
		public NSPort OutputPort;
		public string Text;
		public TextField TextTF; 
		public override void DesignExtensionContainer() {
			VisualElement customDataContainer = new();
			customDataContainer.AddClasses("nsframe-nodeview__custom-data-container");

			Foldout textFoldout = GraphViewUtility.CreateFoldOut("Text");

			TextTF = GraphViewUtility.CreateTextField(Text, null, evt => {
				Text = evt.newValue;
			});
			TextTF.AddClasses(
				"nsframe-nodeview__textfield",
				"nsframe-nodeview__quote-textfield"
			);
			textFoldout.Add(TextTF);

			customDataContainer.Add(textFoldout);
			extensionContainer.Add(customDataContainer);
		}

		public override void DesignOutputContainer() {
			OutputPort = this.CreatePort("out", Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(int));
			outputContainer.Add(OutputPort);
		}

		public override NSNodeSOBase GetEmptyNodeSO() {
			return IOUtility.CreateSO<TextNodeSO>();
		}

		public override NSNodeViewDataSO GetEmptyNodeViewDataSO() {
			return IOUtility.CreateSO<TextNodeViewDataSO>();
		}

		public override void SetNodeSO(NSNodeSOBase nodeSOBase, NSGraphView graphView, Dictionary<string, NSNodeSOBase> nodeSOs) {
			if (nodeSOBase is not TextNodeSO) {
				Debug.LogError("NS: NodeSO type wrong.");
				return;
			}

			var nodeSO = nodeSOBase as TextNodeSO;

			nodeSO.NodeName = NodeViewName;
			nodeSO.NodeSOType = NodeViewType.FullName;
			nodeSO.Text = Text;
			if (GroupView == null) {
				nodeSO.name = $"Node__{NodeViewName}";
			}
			else {
				nodeSO.name = $"NodeInGroup__{GroupView.title}__{NodeViewName}";
			}

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
		}

		public override void SetViewDataSO(NSNodeViewDataSO nodeViewDataSO) {
			if (nodeViewDataSO is not TextNodeViewDataSO) {
				Debug.LogError("NS: NodeViewDataSO type wrong.");
				return;
			}

			var viewDataSO = nodeViewDataSO as TextNodeViewDataSO;

			viewDataSO.NodeViewName = NodeViewName;
			viewDataSO.NodeViewType = NodeViewType.FullName;
			viewDataSO.InputPortID = InputPort.ID;
			viewDataSO.OutputPortID = OutputPort.ID;
			viewDataSO.Position = GetPosition().position;
			viewDataSO.Text = Text;

			if (GroupView == null) {
				viewDataSO.name = $"Node__{NodeViewName}";
			}
			else {
				viewDataSO.name = $"NodeInGroup__{GroupView.title}__{NodeViewName}";
			}
		}
	}
}