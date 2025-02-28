using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(fileName = "ArchConfigBase", menuName = "ShiJiHeShi/Config/Architecture Config")]
	public class ArchConfigBase : ScriptableObject {
		[Header("类型")] public ArchType ArchType;
		[Header("名称")] public string Name;
		[Header("大小")] public int Size;
		[Header("人数上限")] public int MaxContain;
		[Header("介绍")][TextArea(5, 30)] public List<string> Introductions;
		[Header("固有产出")] public List<RepoList> InherentProduceVelocities;
		[Header("额外产出/每人")] public List<RepoList> ExtraProduceVelocitiesPerOne;
		[Header("固有消耗")] public List<RepoList> InherentConsumeVelocities;
		[Header("额外消耗/每人")] public List<RepoList> ExtraConsumeVelocitiesPerOne;
		[Header("存储量/每级")] public List<RepoList> VolumeAdds;
		[Header("建造费用")] public RepoList ConstructCost;
		[Header("拆除返还率")] public float DeconstructRate;
		[Header("修复所需原材料百分比")] public float RepairRate;

		[Header("动画")] public Animator Animator;
	}
}