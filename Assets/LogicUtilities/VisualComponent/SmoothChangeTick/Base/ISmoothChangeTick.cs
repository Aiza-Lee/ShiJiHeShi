using NSFrame;

namespace LogicUtilities 
{
	public abstract class ISmoothChangeTick<T> : ISmoothChange<T> where T : struct {

		public float TimeSpeed = 1.0f;

		private void OnEnable() {
			EventSystem.AddListener<float>((int)LogicEvent.SpeedChange_f, UpdateTimeSpeed);
		}
		private void OnDisable() {
			EventSystem.RemoveListener<float>((int)LogicEvent.SpeedChange_f, UpdateTimeSpeed);
		}

		#region 事件绑定
			private void UpdateTimeSpeed(float value) {
				TimeSpeed = value;
			}
		#endregion
	}
}