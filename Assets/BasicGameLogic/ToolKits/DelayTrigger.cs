using System;
using LogicUtilities;
using NSFrame;
using UnityEngine;
using UnityEngine.Events;

namespace BasicLogic
{
	[Serializable]
	public class DelayTrigger : IPooledObject {
		static DelayTrigger() {
			PoolSystem.InitObjectPool<DelayTrigger>();
		}

		[SerializeField] private UnityAction _action;
		[SerializeField] private int _delayTick;
		[SerializeField] private int _tickCount;

		public void DestroyForPool() {
			EventSystem.RemoveListener((int)LogicEvent.Tick, AddTick);
		}
		public void InitForPool() {
			EventSystem.AddListener((int)LogicEvent.Tick, AddTick);
		}

		private void SetTrigger(UnityAction action, int delayTick) {
			if (delayTick <= 0) {
				Debug.LogWarning("Do not need this delay trigger.");
				return;
			}
			_action = action;
			_delayTick = delayTick;
			_tickCount = 0;
		}

		private void AddTick() {
			++_tickCount;
			if (_tickCount == _delayTick) {
				_action?.Invoke();
				PoolSystem.PushObj(this);
			}
		}

		public static void Run(UnityAction action, int delayTick) {
			PoolSystem.PopObj<DelayTrigger>().SetTrigger(action, delayTick);
		}
	}
}