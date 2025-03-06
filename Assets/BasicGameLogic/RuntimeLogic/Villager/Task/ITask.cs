namespace BasicLogic
{
	public enum TaskType {
		Work,
		Sleep,
		MoveTo,
		Spare,
	}
	
	public abstract class ITask {
		public IVillager AttachedVillager;

		public void SetVillager(IVillager villager) => AttachedVillager = villager;

		public abstract void LogicUpdate();
		public abstract void OnTaskStart();
		public abstract void OnTaskEnd();
	} 
}