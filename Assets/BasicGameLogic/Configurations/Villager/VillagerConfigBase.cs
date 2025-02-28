using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(fileName = "VillagerConfigBase", menuName = "ShiJiHeShi/Config/Villager Config")]
	public class VillagerConfigBase : ScriptableObject {
		[Header("经验获取速度")] public JobList JobExperienceGain;
		[Header("升级经验要求")] public JobList LevelUpDemands;
		[Header("产出增益/每级")] public JobList JobBuff;

		[Header("动画")] public Animator Animator;
	}
}