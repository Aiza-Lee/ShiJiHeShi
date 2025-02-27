using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(fileName = "LayerConfigBase", menuName = "ShiJiHeShi/Config/Layer Config")]
	public class LayerConfigBase : ScriptableObject {
		public LayerType layerType;
		public string Name;
		[TextArea(5, 30)] public string Introductions;
	}
}