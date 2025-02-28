using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(fileName = "LayerConfigBase", menuName = "ShiJiHeShi/Config/Layer Config")]
	public class LayerConfigBase : ScriptableObject {
		[Header("类型")] public LayerType layerType;
		[Header("名称")] public string Name;
		[Header("介绍")][TextArea(5, 30)] public string Introductions;
	}
}