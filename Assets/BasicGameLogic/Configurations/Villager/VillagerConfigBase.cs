using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic {
	[CreateAssetMenu(fileName = "VillagerConfigBase", menuName = "ShiJiHeShi/Config/Villager Config")]
	public class VillagerConfigBase : ScriptableObject {
		/// <summary>
		/// 每Tick做对应工作的经验增加量
		/// </summary>
		public JobList JobExperienceGain;
		/// <summary>
		/// 提升等级的经验要求
		/// </summary>
		public JobList LevelUpDemands;
		/// <summary>
		/// 每一级对工作产出的增益
		/// </summary>
		public JobList JobBuff;

		/// <summary>
		/// 动画
		/// </summary>
		public Animator Animator;
	}
}