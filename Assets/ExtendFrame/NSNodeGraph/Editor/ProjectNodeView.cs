using System.Collections.Generic;
using NSFrame;
using UnityEngine.UIElements;
using BasicLogic;
using System.Linq;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ExtendFrame {
	public class ProjectNodeView : NSNodeViewBase {
		public NSPort OutputPort;
		public string Text;
		public List<NSPairRef<Repository, ulong>> Demands = new();
		public TextField TextTF;

		private VisualElement _demandFoldout;
		private static readonly List<string> _enumNames;

		static ProjectNodeView() {
			_enumNames = Enum.GetNames(typeof(Repository)).ToList();
		}

		private void AddNewDemand() {
			AddDemand(new(Repository.Villager, 0));
		}
		public void AddDemand(NSPairRef<Repository, ulong> demand) {
			Demands.Add(demand);

			var demandContainer = new VisualElement();
			demandContainer.style.flexDirection = FlexDirection.Row;

			/* ENUM DROP DOWN */
			var enumDropDown = new DropdownField("", _enumNames, demand.Key.GetHashCode()) {
				value = demand.Key.ToString()
			};
			enumDropDown.RegisterValueChangedCallback( evt => {
				demand.Key = (Repository)System.Enum.Parse(typeof(Repository), evt.newValue);
			} );
			demandContainer.Add(enumDropDown);

			/* NUMBER FIELD */
			var numField = GraphViewUtility.CreateTextField(demand.Value.ToString(), null, evt => {
				demand.Value = ulong.Parse(evt.newValue);
			});
			demandContainer.Add(numField);

			/* DELETE BUTTON */
			var deleteButton = GraphViewUtility.CreateButton("X", () => {
				Demands.Remove(demand);
				_demandFoldout.Remove(demandContainer);
			});
			deleteButton.style.marginLeft = StyleKeyword.Auto;
			demandContainer.Add(deleteButton);
			
			_demandFoldout.Add(demandContainer);
		}

		public override void DesignExtensionContainer() {
			var customDataContainer = new VisualElement();

			/* TEXT FOLDOUT */
			var textFoldout = GraphViewUtility.CreateFoldOut("Text");
			TextTF = GraphViewUtility.CreateTextField(Text, null, evt => {
				Text = evt.newValue;
			});
			TextTF.AddClasses(
				"nsframe-nodeview__textfield",
				"nsframe-nodeview__quote-textfield"
			);
			textFoldout.Add(TextTF);
			customDataContainer.Add(textFoldout);

			/* DEMAND FOLDOUT */
			_demandFoldout = GraphViewUtility.CreateFoldOut("Demand");
			var addButton = GraphViewUtility.CreateButton("Add", AddNewDemand);
			_demandFoldout.Add(addButton);
			customDataContainer.Add(_demandFoldout);

			extensionContainer.Add(customDataContainer);
		}

		public override void DesignOutputContainer() {
			OutputPort = this.CreatePort("out", Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(int));
			outputContainer.Add(OutputPort);
		}

		public override NSNodeSOBase GetEmptyNodeSO() {
			return IOUtility.CreateSO<ProjectNodeSO>();
		}

		public override NSNodeViewDataSO GetEmptyNodeViewDataSO() {
			return IOUtility.CreateSO<ProjectNodeViewDataSO>();
		}

		protected override void SetAdditionalNodeSO(NSNodeSOBase nodeSOBase, NSGraphView graphView, Dictionary<string, NSNodeSOBase> nodeSOs) {
			if (nodeSOBase is not ProjectNodeSO) {
				Debug.LogError("NS: NodeSO type wrong.");
				return;
			}
			var nodeSO = nodeSOBase as ProjectNodeSO;

			nodeSO.NodeSOType = NodeViewType.FullName;
			nodeSO.Text = Text;
			nodeSO.Demands = new();
			foreach (var demand in Demands) {
				nodeSO.Demands.Add(new NSPair<Repository, ulong>(demand.Key, demand.Value));
			}
			nodeSO.NextNodes = new();
			foreach (var edge in graphView.edges) {
				if (edge.output.node == this) {
					nodeSO.NextNodes.Add(nodeSOs[(edge.input.node as NSNodeViewBase).ID]);
				}
			}
		}

		protected override void SetAdditionalViewDataSO(NSNodeViewDataSO nodeViewDataSO) {
			if (nodeViewDataSO is not ProjectNodeViewDataSO) {
				Debug.LogError("NS: NodeViewDataSO type wrong.");
				return;
			}
			var viewDataSO = nodeViewDataSO as ProjectNodeViewDataSO;

			viewDataSO.NodeViewType = NodeViewType.FullName;
			viewDataSO.OutputPortID = OutputPort.ID;
			viewDataSO.Text = Text;
			viewDataSO.Demands = new();
			foreach (var demand in Demands) {
				viewDataSO.Demands.Add(demand.ToNSPair());
			}
		}
	}
}