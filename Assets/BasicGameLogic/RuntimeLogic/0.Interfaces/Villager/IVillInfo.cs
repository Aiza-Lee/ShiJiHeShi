namespace BasicLogic
{
	public interface IVillInfo {
		string ID { get; }
		string FirstName { get; }
		string LastName { get; }

		VillTaskRunner TaskRunner { get; }
		int GetJobLevel(JobType jobType);
	}
}