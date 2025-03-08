namespace BasicLogic
{
	public interface ISaveable<T> {
		T GetData();
		void InitData(T data);
	}
}