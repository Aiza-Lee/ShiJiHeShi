using UnityEngine;

namespace BasicLogic 
{
	public abstract class IVillager : MonoBehaviour {
		[Header("挂载")]
		public VillagerConfigBase Config;

		public JobList JobExperienceGain => Config.JobExperienceGain;
		public JobList LevelUpDemands => Config.LevelUpDemands;
		public JobList JobBuff => Config.JobBuff;
		public Animator Animator => Config.Animator;

		[Header("Informations")]
		[SerializeField][ReadOnly] protected VillagerSaveData _saveData;
		[SerializeField][ReadOnly] protected RepoList _consBuffs = new(fillAll: true);
		[SerializeField][ReadOnly] protected RepoList _prodBuffs = new(fillAll: true);

		public string FirstName => _saveData.FirstName;
		public string LastName => _saveData.LastName;
		public Vector2 Position => _saveData.Position;
		
		public RepoList ConsBuffs => _consBuffs;
		public RepoList ProdBuffs => _prodBuffs;
	}
	
	
}