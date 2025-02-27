using System.Collections.Generic;
using UnityEngine;

namespace BasicLogic 
{
	[System.Serializable] public class RepoList {
		public List<RTPair<float>> List;
		public int Count => List.Count;

		public bool Full { get; private set; }

		public RepoList(bool fillAll = false) {
			List = new();
			if (fillAll) {
				Full = true;
				for (int i = 0; i < GameManager.RepositorySize; ++i) 
					List.Add(new((Repository)i, 0));
			}
		}
		/// <summary>
		/// 从不完整的RepoList中创建完整的RepoList
		/// </summary>
		public RepoList(RepoList other) {
			List = new();
			if (other.Full) {
				for (int i = 0; i < GameManager.RepositorySize; ++i) List.Add(other[i]);
				return;
			}
			Full = true;
			for (int i = 0; i < GameManager.RepositorySize; ++i) 
				List.Add(new((Repository)i, 0));
			for (int i = 0; i < other.List.Count; ++i) {
				Add(other.List[i]);
			}
		}
		public RTPair<float> this[int index] {
			get {
				if (!Full) {
					Debug.LogWarning("Donnot use index when list is not full.");
				}
				return List[index];
			}
			set => List[index] = value;
		}
		public RepoList Add(Repository repository, float val) {
			foreach(var rtpair in List) {
				if (rtpair.RepositoryType == repository) {
					rtpair.Value += val;
					return this;
				}
			}
			List.Add(new(repository, val));
			return this;
		}
		public RepoList Add(RTPair<float> rtPair) {
			return Add(rtPair.RepositoryType, rtPair.Value);
		}
	}
	[System.Serializable] public class JobList {
		public List<JTPair<float>> List;
		public int Count => List.Count;

		public bool Full { get; private set; }

		public JobList(bool fillAll = false) {
			List = new();
			if (fillAll) {
				Full = true;
				for (int i = 0; i < GameManager.RepositorySize; ++i) 
					List.Add(new((Job)i, 0));
			}
		}
		public JTPair<float> this[int index] {
			get {
				if (!Full) {
					Debug.LogWarning("Donnot use index when list is not full.");
				}
				return List[index];
			}
			set => List[index] = value;
		}
		public JobList Add(Job job, float val) {
			foreach(var jtPair in List) {
				if (jtPair.Job == job) {
					jtPair.Value += val;
					return this;
				}
			}
			List.Add(new(job, val));
			return this;
		}
	}
	[System.Serializable] public class JTPair<T> {
		public Job Job;
		public T Value;
		public int JobInt => (int)Job;
		public JTPair(Job job, T t) {
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

	/// <summary>
	/// Level(int) -> T
	/// </summary>
	[System.Serializable] public class LTPair<T> {
		public int Level;
		public T Value;
	}

	/// <summary>
	/// Level(int) -> string
	/// </summary>
	[System.Serializable] public class LStringPair {
		public int Level;
		[TextArea(2, 20)] public string Value;
	}
}