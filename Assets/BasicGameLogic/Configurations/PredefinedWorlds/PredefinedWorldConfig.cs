using UnityEngine;

namespace BasicLogic
{
	[CreateAssetMenu(fileName = "PredefinedWorldConfig", menuName = "ShiJiHeShi/Config/Predefined World Config")]
	public class PredefinedWorldConfig : ScriptableObject {
		public string PresetName;
		public GameSaveData GameSaveData;
	}
}

