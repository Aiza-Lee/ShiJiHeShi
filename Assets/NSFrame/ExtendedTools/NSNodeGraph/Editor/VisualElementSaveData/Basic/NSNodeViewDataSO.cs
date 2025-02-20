using System.Collections.Generic;
using UnityEngine;

namespace NSFrame
{
	public abstract class NSNodeViewDataSO : ScriptableObject {
		public string NodeViewName;
		public string NodeViewType;
		public Vector2 Position;
		public string InputPortID;
		public abstract void GetOutputPort(NSNodeViewBase nodeView, Dictionary<string, NSPort> ports);
		public abstract void SetNodeView(NSNodeViewBase nodeViewBase);
		public void GetInputPort(NSNodeViewBase nodeView, Dictionary<string, NSPort> ports) {
			ports.Add(InputPortID, nodeView.InputPort);
		}
	}
}