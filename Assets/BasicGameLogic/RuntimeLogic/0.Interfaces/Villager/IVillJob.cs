using System.Collections.Generic;

namespace BasicLogic
{
	public interface IVillJob {
		JobList JobExp_F { get; }
		List<JTPair<int>> JobLevel_F { get; }
		public void AddExperience(JobList jobExpsToAdd);
	}
}