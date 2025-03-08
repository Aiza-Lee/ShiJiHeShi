using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(fileName = "JobConfig", menuName = "ShiJiHeShi/Config/Job Config")]
	public class JobConfig : ScriptableObject {
		[Header("职业种类")] public JobType JobType;
		[Header("每级配置")] public List<JobLevelConfig> JobLevelConfigs;
	}
}