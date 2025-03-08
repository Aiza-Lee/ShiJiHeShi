using UnityEngine;

namespace BasicLogic
{
	[CreateAssetMenu(fileName = "ArchLevelConfig_x", menuName = "ShiJiHeShi/Config/Architecture Config Level")]
	public class ArchLevelConfig : ScriptableObject {
		[Header("等级")] public int Level;
		[Header("容纳人数上限")] public int MaxContain;
		[Header("介绍")][TextArea(5, 30)] public string Introductions;
		[Header("固有产出")] public RepoList InherentProduceVelocities;
		[Header("额外产出/每人")] public RepoList ExtraProduceVelocitiesPerOne;
		[Header("固有消耗")] public RepoList InherentConsumeVelocities;
		[Header("额外消耗/每人")] public RepoList ExtraConsumeVelocitiesPerOne;
		[Header("存储量增量")] public RepoList VolumeAdds;
		[Header("职业经验的增量")] public JobList ExperienceAdds;
	}
}