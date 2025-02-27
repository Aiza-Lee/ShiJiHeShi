using NSFrame;
using UnityEngine;

namespace BasicLogic 
{
	/// <summary>
	/// 将游戏时间(Tick)流逝作为事件"Tick"发布
	/// </summary>
	public class TickManager : MonoSingleton<TickManager>, IManager {

		[Header("Constants")]
		[Range(0.01f, 0.05f)] public float TickTime;


		[Header("Informations")]
		[Range(0.5f, 5.0f)] public float Speed = 1.0f;
		[SerializeField] private bool Pause = true;

		[SerializeField] private ulong _TickSum;
		/// <summary>
		/// TickSum 每次打开游戏会重置，游戏进度不应该依赖这个值
		/// </summary>
		public ulong TickSum {
			get => _TickSum;
			private set {
				if (Pause) return;
				while (_TickSum < value) {
					EventSystem.Invoke("Tick");
					++_TickSum;
				}
			} 
		}


		[SerializeField] private float _TimeSum = 0.0f;
		public float TimeSum { 
			get => _TimeSum;
			private set => _TimeSum = value;	
		}


	  #if UNITY_EDITOR

		[Header("Debug")]
		public int UnityFrameSum = 0;

	  #endif

		float RealTickTime { get => TickTime / Speed; }

		void Update() {
			while (TimeSum + RealTickTime < Time.time) {
				++TickSum;
				TimeSum += RealTickTime;
			}
			#if UNITY_EDITOR
				++UnityFrameSum;
			#endif
		}

		public void GamePause() {
			Pause = true;
		}

		public void GameStart(GameSaveData _) {
			Pause = false;
		}

		public void SaveGame(GameSaveData _) {
			return;
		}
		
		public void GameExit()
		{
			throw new System.NotImplementedException();
		}

		public void GameOver()
		{
			throw new System.NotImplementedException();
		}
	}
}