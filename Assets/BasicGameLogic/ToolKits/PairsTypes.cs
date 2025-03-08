using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic 
{
	[System.Serializable] public class RepoList {
		public List<RTPair<float>> RList;
		public int Count => RList.Count;

		[HideInInspector] public bool Full;

		#region Constructor
			public RepoList(bool fillAll = false) {
				RList = new();
				if (fillAll) {
					Full = true;
					for (int i = 0; i < GameManager.RepositorySize; ++i) 
						RList.Add(new((Repository)i, 0));
				}
			}
		#endregion

		public RTPair<float> this[int index] {
			get {
				if (!Full) {
					Debug.LogWarning("Donnot use index when list is not full.");
				}
				return RList[index];
			}
			set => RList[index] = value;
		}

		public RepoList ConvertToFull() {
			if (Full) { return this; }
			Full = true;
			var ori = RList;
			RList = new();
			for (int i = 0; i < GameManager.RepositorySize; ++i) {
				RList.Add(new((Repository)i, 0f));
			}
			if (ori != null) foreach (var rtPair in ori) {
				RList[rtPair.RepoInt].Value = rtPair.Value;
			}
			return this;
		}
	}

	[System.Serializable] public class JobList {
		public List<JTPair<float>> JList;
		public int Count => JList.Count;

		[HideInInspector] public bool Full;

		#region Constructor
			public JobList(bool fillAll = false) {
				JList = new();
				Full = true;
				if (fillAll) {
					for (int i = 0; i < GameManager.JobSize; ++i) 
						JList.Add(new((JobType)i, 0));
				}
			}
		#endregion

		public JTPair<float> this[int index] {
			get {
				if (!Full) {
					Debug.LogWarning("Donnot use index when list is not full.");
				}
				return JList[index];
			}
			set => JList[index] = value;
		}

		public JobList ConvertToFull() {
			if (Full) { return this; }
			Full = true;
			var ori = JList;
			JList = new();
			for (int i = 0; i < GameManager.JobSize; ++i) {
				JList.Add(new((JobType)i, 0f));
			}
			if (ori != null) foreach (var jtPair in ori) {
				JList[jtPair.JobInt].Value = jtPair.Value;
			}
			return this;
		}

	}

	[System.Serializable] public class JTPair<T> {
		public JobType Job;
		public T Value;
		public int JobInt => (int)Job;
		public JTPair(JobType job, T t) {
			Job = job;
			Value = t;
		}
	}

	/// <summary>
	/// Repository -> T
	/// </summary>
	[System.Serializable] public class RTPair<T> {
		public Repository RepositoryType;
		public T Value;
		public int RepoInt => (int)RepositoryType;
		public RTPair(Repository repository, T t) {
			RepositoryType = repository;
			Value = t;
		}
	}

	[System.Serializable] public class KVPair<K, V> {
		public K Key;
		public V Value;
		public KVPair(K key, V value) {
			Key = key;
			Value = value;
		}
	}
}