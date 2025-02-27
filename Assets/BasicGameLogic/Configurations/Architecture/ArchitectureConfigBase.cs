using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(fileName = "ArchConfigBase", menuName = "ShiJiHeShi/Config/Architecture Config")]
	public class ArchConfigBase : ScriptableObject {
		/// <summary>
		/// 建筑的类型
		/// </summary>
		public ArchType ArchType;
		/// <summary>
		/// 建筑名称
		/// </summary>
		public string Name;
		/// <summary>
		/// 建筑的长度
		/// </summary>
		public int Size;
		/// <summary>
		/// 最多容纳的工人数
		/// </summary>
		public int MaxContain;
		/// <summary>
		/// 对每一级建筑的介绍
		/// </summary>
		[TextArea(5, 30)] public List<string> Introductions;
		/// <summary>
		/// 每一级建筑固有的产出
		/// </summary>
		public List<RepoList> InherentProduceVelocities;
		/// <summary>
		/// 每一级建筑中每个人的基础产出
		/// </summary>
		public List<RepoList> ExtraProduceVelocitiesPerOne;
		/// <summary>
		/// 每一级建筑固有的消耗
		/// </summary>
		public List<RepoList> InherentConsumeVelocities;
		/// <summary>
		/// 每一级建筑中每个人的基础消耗
		/// </summary>
		public List<RepoList> ExtraConsumeVelocitiesPerOne;
		/// <summary>
		/// 每一级存储空间的增量
		/// </summary>
		public List<RepoList> VolumeAdds;
		/// <summary>
		/// 建筑材料
		/// </summary>
		public RepoList ConstructCost;
		/// <summary>
		/// 拆除返还材料倍率
		/// </summary>
		public float DeconstructRate;
		/// <summary>
		/// 损坏后修复所需材料倍率
		/// </summary>
		public float RepairRate;

		/// <summary>
		/// 动画
		/// </summary>
		public Animator Animator;
	}
}