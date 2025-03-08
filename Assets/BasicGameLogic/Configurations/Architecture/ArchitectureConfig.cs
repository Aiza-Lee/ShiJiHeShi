using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(fileName = "ArchConfig", menuName = "ShiJiHeShi/Config/Architecture Config")]
	public class ArchConfig : ScriptableObject {
		[Header("类型")] public ArchType ArchType;
		[Header("名称")] public string Name;
		[Header("大小")] public int Size;
		[Header("建造费用")] public RepoList ConstructCost;
		[Header("拆除返还率")] public float DeconstructRate;
		[Header("修复所需原材料百分比")] public float RepairRate;
		[Header("动画")] public Animator Animator;

		[Header("每级配置")] public List<ArchLevelConfig> LevelConfigs;
	}
}