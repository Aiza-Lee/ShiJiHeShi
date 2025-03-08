using System.Collections.Generic;
using LogicUtilities;
using UnityEngine;

namespace BasicLogic 
{
	[RequireComponent(typeof(SmoothMoveTick), typeof(SmoothScaleTick), typeof(SpriteRenderer))]
	public class Villager : MonoBehaviour, ISaveable<VillData>, IVillInfo, IRepoBuff, IVillJob, IPosition {
		
		[Header("挂载")]
		public VillagerConfig Config;

		#region Config Getter
			public Animator Animator => Config.Animator;

			public JobConfig GetJobConfig(JobType jobType) {
				return GameManager.Instance.GameConfig.GetJobConfig(jobType);
			}

		#endregion

		#region IVillInfo
			public string ID { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public Position Position { get; set; }
			public VillTaskRunner TaskRunner { get; set; }
			public int GetJobLevel(JobType jobType) { return JobLevel_F[(int)jobType].Value; }
		#endregion

		#region IVillRepo
			public RepoList ConsBuffs_F { get; set; }
			public RepoList ProdBuffs_F { get; set; }
		#endregion

		#region IVillJob
			public JobList JobExp_F { get; set; }
			public List<JTPair<int>> JobLevel_F { get; set; }
			public void AddExperience(JobList jobExpsToAdd) {
				foreach (var JT in jobExpsToAdd.JList) {

					JobExp_F[JT.JobInt].Value += JT.Value;

					var jobConfig = GetJobConfig(JT.Job);
					var level = GetJobLevel(JT.Job);

					if (jobConfig.JobLevelConfigs.Count - 1 > level) {

						var levelConfig = jobConfig.JobLevelConfigs[level];

						if (JobExp_F[JT.JobInt].Value >= levelConfig.LevelUpDemand) {
							JobExp_F[JT.JobInt].Value -= levelConfig.LevelUpDemand;
							++JobLevel_F[JT.JobInt].Value;
							ConsBuffs_F[JT.JobInt].Value += levelConfig.ConsumeBuff;
							ProdBuffs_F[JT.JobInt].Value += levelConfig.ProduceBuff;
						}

					}
				}
			}

		#endregion

		#region ISaveable
			public VillData GetData() {
				return new VillData {
					ID = ID,
					FirstName = FirstName,
					LastName = LastName,
					JobLevel = JobLevel_F,
					JobExperiences = JobExp_F,
					ConsumeBuffs = ConsBuffs_F,
					ProduceBuffs = ProdBuffs_F,
					Position = Position,
					TaskRunnerData = TaskRunner.GetData()
				};
			}
			public void InitData(VillData data) {
				ID = data.ID;
				FirstName = data.FirstName;
				LastName = data.LastName;
				JobLevel_F = data.JobLevel.ConvertToFull();
				JobExp_F = data.JobExperiences.ConvertToFull();
				ConsBuffs_F = data.ConsumeBuffs.ConvertToFull();
				ProdBuffs_F = data.ProduceBuffs.ConvertToFull();
				Position = data.Position;
				TaskRunner = new(this);
				TaskRunner.InitData(data.TaskRunnerData);
			}
		#endregion


		public SmoothMoveTick SmoothMoveTick { get; private set; }
		public SmoothScaleTick SmoothScaleTick { get; private set; }
		public SpriteRenderer SpriteRenderer { get; private set; }

		private void Awake() {
			SmoothMoveTick = GetComponent<SmoothMoveTick>();
			SmoothScaleTick = GetComponent<SmoothScaleTick>();
			SpriteRenderer = GetComponent<SpriteRenderer>();
		}

	}
	
	
}