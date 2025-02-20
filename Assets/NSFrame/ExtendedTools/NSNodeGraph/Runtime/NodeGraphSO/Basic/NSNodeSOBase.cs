using System.Collections.Generic;
using UnityEngine;

namespace NSFrame
{
	public abstract class NSNodeSOBase : ScriptableObject {
		public string NodeName;
		public string NodeSOType;
		public List<NSNodeSOBase> PreviousNodes;

	}
}