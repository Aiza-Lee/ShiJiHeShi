namespace LogicUtilities
{
	public enum LogicEvent : int {
		/// <summary>
		/// 游戏逻辑Tick
		/// </summary>
		Tick,

		/// <summary>
		/// 当建筑被创建
		/// </summary>
		ArchConstructed_A,

		/// <summary>
		/// 当layer被创造
		/// </summary>
		LayerConstructed_L,

		/// <summary>
		/// 当layerCamera左右移动
		/// </summary>
		LayerCameraMove_v3,

		/// <summary>
		/// 当layerCamera前后移动时触发
		/// </summary>
		LayerCameraFB_b,
	}
}