using System;
using System.Collections.Generic;
using LogicUtilities;

namespace BasicLogic 
{
	[Serializable]
	public class VillData {
		public string ID;
		public string FirstName, LastName;
		public List<JTPair<int>> JobLevel;
		public JobList JobExperiences;
		public RepoList ConsumeBuffs;
		public RepoList ProduceBuffs;
		public Position Position;
		public TaskRunnerData TaskRunnerData;
	}
}