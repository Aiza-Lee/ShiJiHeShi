using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NSFrame {
	/// <summary>
	/// 使用时方法名务必带上表示参数类型的后缀，如OnUpdateHP_iifs (表示四个参数分别是int,int,float,string)
	/// </summary>
	public static class EventSystem {
		
		private static readonly Dictionary<int, IEventInfo>[] _eventInfos = new Dictionary<int, IEventInfo>[(int)EventType.Default + 1];

		static EventSystem() {
			for (int i = 0; i < _eventInfos.Length; ++i) {
				_eventInfos[i] = new();
			}
		}

		private interface IEventInfo { public void Destroy(); }
		private class EventInfo : IEventInfo {
			public UnityAction handler;
			public void Destroy() { handler = null; PoolSystem.PushObj(this); }
		}
		private class EventInfo<T> : IEventInfo {
			public UnityAction<T> handler;
			public void Destroy(){ handler = null; PoolSystem.PushObj(this); }
		}
		private class EventInfo<T1, T2> : IEventInfo {
			public UnityAction<T1, T2> handler;
			public void Destroy() { handler = null; PoolSystem.PushObj(this); }
		}
		private class EventInfo<T1, T2, T3> : IEventInfo {
			public UnityAction<T1, T2, T3> handler;
			public void Destroy() { handler = null; PoolSystem.PushObj(this); }
		}
		private class EventInfo<T1, T2, T3, T4> : IEventInfo {
			public UnityAction<T1, T2, T3, T4> handler;
			public void Destroy() { handler = null; PoolSystem.PushObj(this); }
		}
		
		public static void AddListener(int eventID, UnityAction action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo).handler += action;
			else {
				PoolSystem.InitObjectPool<EventInfo>();
				EventInfo eventInfo = PoolSystem.PopObj<EventInfo>();
				eventInfo.handler = action;
				_eventInfos[type].Add(eventID, eventInfo);
			}
		}
		public static void AddListener<T>(int eventID, UnityAction<T> action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) {
				(_eventInfos[type][eventID] as EventInfo<T>).handler += action;
			}
			else {
				PoolSystem.InitObjectPool<EventInfo<T>>();
				EventInfo<T> eventInfo = PoolSystem.PopObj<EventInfo<T>>();
				eventInfo.handler = action;
				_eventInfos[type].Add(eventID, eventInfo);
			}
		}
		public static void AddListener<T1, T2>(int eventID, UnityAction<T1, T2> action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) {
				(_eventInfos[type][eventID] as EventInfo<T1, T2>).handler += action;
			}
			else {
				PoolSystem.InitObjectPool<EventInfo<T1, T2>>();
				EventInfo<T1, T2> eventInfo = PoolSystem.PopObj<EventInfo<T1, T2>>();
				eventInfo.handler = action;
				_eventInfos[type].Add(eventID, eventInfo);
			}
		}
		public static void AddListener<T1, T2, T3>(int eventID, UnityAction<T1, T2, T3> action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) {
				(_eventInfos[type][eventID] as EventInfo<T1, T2, T3>).handler += action;
			}
			else {
				PoolSystem.InitObjectPool<EventInfo<T1, T2, T3>>();
				EventInfo<T1, T2, T3> eventInfo = PoolSystem.PopObj<EventInfo<T1, T2, T3>>();
				eventInfo.handler = action;
				_eventInfos[type].Add(eventID, eventInfo);
			}
		}
		public static void AddListener<T1, T2, T3, T4>(int eventID, UnityAction<T1, T2, T3, T4> action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) {
				(_eventInfos[type][eventID] as EventInfo<T1, T2, T3, T4>).handler += action;
			}
			else {
				PoolSystem.InitObjectPool<EventInfo<T1, T2, T3, T4>>();
				EventInfo<T1, T2, T3, T4> eventInfo = PoolSystem.PopObj<EventInfo<T1, T2, T3, T4>>();
				eventInfo.handler = action;
				_eventInfos[type].Add(eventID, eventInfo);
			}
		}
	
		public static void Invoke(int eventID, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo).handler?.Invoke();
		}
		public static void Invoke<T>(int eventID, T arg, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo<T>).handler?.Invoke(arg);
		}
		public static void Invoke<T1, T2>(int eventID, T1 t1, T2 t2, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo<T1, T2>).handler?.Invoke(t1, t2);
		}
		public static void Invoke<T1, T2, T3>(int eventID, T1 t1, T2 t2, T3 t3, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo<T1, T2, T3>).handler?.Invoke(t1, t2, t3);
		}
		public static void Invoke<T1, T2, T3, T4>(int eventID, T1 t1, T2 t2, T3 t3, T4 t4, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo<T1, T2, T3, T4>).handler?.Invoke(t1, t2, t3, t4);
		}

		public static void RemoveListener(int eventID, UnityAction action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo).handler -= action;
		}
		public static void RemoveListener<T>(int eventID, UnityAction<T> action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo<T>).handler -= action;
		}
		public static void RemoveListener<T1, T2>(int eventID, UnityAction<T1, T2> action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo<T1, T2>).handler -= action;
		}
		public static void RemoveListener<T1, T2, T3>(int eventID, UnityAction<T1, T2, T3> action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo<T1, T2, T3>).handler -= action;
		}
		public static void RemoveListener<T1, T2, T3, T4>(int eventID, UnityAction<T1, T2, T3, T4> action, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) 
				(_eventInfos[type][eventID] as EventInfo<T1, T2, T3, T4>).handler -= action;
		}

		public static void RemoveEvent(int eventID, EventType eventType = EventType.Default) {
			int type = (int)eventType;
			if (_eventInfos[type].ContainsKey(eventID)) {
				_eventInfos[type][eventID].Destroy();
				_eventInfos[type].Remove(eventID);
			}
		}
		public static void RemoveAllEvent(EventType eventType = EventType.Default) {
			int type = (int)eventType;
			foreach (int eventID in _eventInfos[type].Keys) 
				_eventInfos[type][eventID].Destroy();
			_eventInfos[type].Clear();
		}
	}
}