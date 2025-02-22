using NSFrame;
using UnityEngine;

namespace BasicLogic {
	/// <summary>
	/// 将游戏时间(Tick)流逝作为事件"TickAdd"发布
	/// </summary>
	public class GameTimeManager : MonoSingleton<GameTimeManager> {

		[Header("Constants To Be Adjusted")]
		[Range(0.01f, 0.05f)] public float TickTime;


		[Header("Informations")]
		[Range(1.0f, 5.0f)] public float TimeSpeed = 1.0f;

		[SerializeField] ulong _TickSum;
		/// <summary>
		/// TickSum 每次打开游戏会重置，游戏进度不应该依赖这个值
		/// </summary>
		public ulong TickSum {
			get => _TickSum;
			private set {
				while (_TickSum < value) {
					EventSystem.Invoke("TickAdd");
					++_TickSum;
				}
			} 
		}
		[SerializeField] float _TimeSum = 0.0f;
		public float TimeSum { 
			get => _TimeSum;
			private set => _TimeSum = value;	
		}

	  #if UNITY_EDITOR

		[Header("Debug")]
		public int UnityFrameSum = 0;

	  #endif

		float RealTickTime { get => TickTime / TimeSpeed; }

		void Update() {
			while (TimeSum + RealTickTime < Time.time) {
				++TickSum;
				TimeSum += RealTickTime;
			}
			#if UNITY_EDITOR
				++UnityFrameSum;
			#endif
		}
		

	}
}