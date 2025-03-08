namespace LogicUtilities
{
	public enum LogicEvent : int {
		/// <summary>
		/// 游戏逻辑Tick
		/// </summary>
		Tick,

		/// <summary>
		/// tick 倍速改变
		/// </summary>
		SpeedChange_f,

		/// <summary>
		/// 当建筑被创建
		/// </summary>
		ArchConstructed_A,

		/// <summary>
		/// 当layer被创造
		/// </summary>
		LayerConstructed_L,

		/// <summary>
		/// 当Villager被创造
		/// </summary>
		VillagerConstructed_V,

		/// <summary>
		/// 当村民左右移动
		/// </summary>
		VillagerMove_Vi,

		/// <summary>
		/// 当村民跨层 i:跨层之后的层编号
		/// </summary>
		VillagerCross_Vi,

		/// <summary>
		/// 当layerCamera左右移动
		/// </summary>
		LayerCameraMove_v3,

		/// <summary>
		/// 当layerCamera前后移动时触发
		/// </summary>
		LayerCameraFB_b,

		/// <summary>
		/// 白天开始
		/// </summary>
		DayStart, 

		/// <summary>
		/// 夜晚开始
		/// </summary>
		NightStart,
	}
}