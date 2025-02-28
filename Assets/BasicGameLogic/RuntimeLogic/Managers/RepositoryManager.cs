using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace BasicLogic 
{
	/// <summary>
	/// 重叠计算减少消耗规则：相加。
	/// 重叠计算增加产量规则；相加。
	/// </summary>
	public class RepositoryManager : MonoSingleton<RepositoryManager>, IManager {

		[Header("Informations")]
		[SerializeField][ReadOnly] private RepoList _amounts;
		[SerializeField][ReadOnly] private RepoList _globalConsBuffs = new(fillAll: true);
		[SerializeField][ReadOnly] private RepoList _globalProdBuffs = new(fillAll: true);

		public void GameExit() {
			throw new System.NotImplementedException();
		}

		public void GameOver() {
			GamePause();
		}

		public void GamePause() {
		}

		public void GameStart(GameSaveData gameSaveData) {
			_amounts = gameSaveData.SavedRepositoryAmounts;
		}

		public void SaveGame(GameSaveData gameSaveData) {
			
		}

		public bool CheckRequest(RepoList demands) {
			if (demands == null || demands.Count == 0) return true;
			foreach (var demand in demands.RList) {
				if (_amounts[demand.RepoInt].Value < demand.Value) return false;
			}
			return true;
		}

		/// <summary>
		/// 尝试消耗资源，成功消耗后返回true
		/// </summary>
		public bool Consume(RepoList consumes) {
			if (consumes == null || consumes.Count == 0) return true;
			foreach (var consume in consumes.RList) {
				if (_amounts[consume.RepoInt].Value < consume.Value) return false;
			}
			foreach (var consume in consumes.RList) {
				_globalConsBuffs[consume.RepoInt].Value -= consume.Value;
			}
			return true;
		}

		public bool TickConsume(RepoList consVels, RepoList archBuffs, RepoList villagerBuffs) {
			if (consVels == null || consVels.Count == 0) return true;
			int idx;
			foreach (var consume in consVels.RList) {
				idx = consume.RepoInt;
				if (_amounts[idx].Value < consume.Value * (1.0f - archBuffs[idx].Value - villagerBuffs[idx].Value - _globalConsBuffs[idx].Value)) 
					return false; 
			}
			foreach (var consume in consVels.RList) {
				idx = consume.RepoInt;
				_amounts[idx].Value -= consume.Value * (1.0f - archBuffs[idx].Value - villagerBuffs[idx].Value - _globalConsBuffs[idx].Value);
			}
			return true;
		}
		public bool TickConsume(RepoList consVels, RepoList archBuffs) {
			if (consVels == null || consVels.Count == 0) return true;
			int idx;
			foreach (var consume in consVels.RList) {
				idx = consume.RepoInt;
				if (_amounts[idx].Value < consume.Value * (1.0f - archBuffs[idx].Value - _globalConsBuffs[idx].Value)) 
					return false; 
			}
			foreach (var consume in consVels.RList) {
				idx = consume.RepoInt;
				_amounts[idx].Value -= consume.Value * (1.0f - archBuffs[idx].Value - _globalConsBuffs[idx].Value);
			}
			return true;
		}
		public void TickProduce(RepoList produceVels, RepoList archBuffs, RepoList villagerBuffs) {
			int idx;
			foreach (var produce in produceVels.RList) {
				idx = produce.RepoInt;
				_amounts[produce.RepoInt].Value += produce.Value * (1.0f + archBuffs[idx].Value + villagerBuffs[idx].Value + _globalProdBuffs[idx].Value);
			}
		}
		public void TickProduce(RepoList produceVels, RepoList archBuffs) {
			int idx;
			foreach (var produce in produceVels.RList) {
				idx = produce.RepoInt;
				_amounts[produce.RepoInt].Value += produce.Value * (1.0f + archBuffs[idx].Value + _globalProdBuffs[idx].Value);
			}
		}


		private void AddGlobalConsBuff(Repository repository, float buff) {
			_globalConsBuffs[(int)repository].Value += buff;
		}
		private void AddGlobalConsBuff(RTPair<float> rtPair) {
			_globalConsBuffs[rtPair.RepoInt].Value += rtPair.Value;
		}
		private void AddGlobalProdBuff(Repository repository, float buff) {
			_globalProdBuffs[(int)repository].Value += buff;
		}
		private void AddGlobalProdBuff(RTPair<float> rtPair) {
			_globalProdBuffs[rtPair.RepoInt].Value += rtPair.Value;
		}
	}
}