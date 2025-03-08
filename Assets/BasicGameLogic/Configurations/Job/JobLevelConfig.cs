using UnityEngine;

namespace BasicLogic
{
	
	[CreateAssetMenu(fileName = "JobLevelConfig_x", menuName = "ShiJiHeShi/Config/Job Config Level")]
	public class JobLevelConfig : ScriptableObject {
		[Header("等级")] public int Level;
		[Header("升级所需经验")] public float LevelUpDemand;
		[Header("消耗减免的增量")] public float ConsumeBuff;
		[Header("产出增益的增量")] public float ProduceBuff;

	}
}