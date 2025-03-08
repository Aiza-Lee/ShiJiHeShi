using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace BasicLogic 
{
	/// <summary>
	/// 重叠计算减少消耗规则：1.0 - \sigma buffs
	/// 重叠计算增加产量规则；1.0 + \sigma buffs
	/// </summary>
	public class RepositoryManager : MonoSingleton<RepositoryManager>, IManager {

		[Header("Informations")]
		[SerializeField][ReadOnly] private RepoList _amounts;
		[SerializeField][ReadOnly] private RepoList _globalConsBuffs_F = new(fillAll: true);
		[SerializeField][ReadOnly] private RepoList _globalProdBuffs_F = new(fillAll: true);

		public void GameExit() { }

		public void GameOver() { }

		public void GamePause() { }

		public void GameStart(GameSaveData gameSaveData) {
			_amounts = gameSaveData.SavedRepositoryAmounts;
		}

		public void SaveGame(GameSaveData gameSaveData) { }

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
				_globalConsBuffs_F[consume.RepoInt].Value -= consume.Value;
			}
			return true;
		}

		public bool TickConsume_Villager(RepoList consVels, RepoList archBuffs_F, RepoList villagerBuffs_F) {
			if (consVels == null || consVels.Count == 0) return true;
			int idx;
			foreach (var consume in consVels.RList) {
				idx = consume.RepoInt;
				if (_amounts[idx].Value < consume.Value * (1.0f - archBuffs_F[idx].Value - villagerBuffs_F[idx].Value - _globalConsBuffs_F[idx].Value)) 
					return false; 
			}
			foreach (var consume in consVels.RList) {
				idx = consume.RepoInt;
				_amounts[idx].Value -= consume.Value * (1.0f - archBuffs_F[idx].Value - villagerBuffs_F[idx].Value - _globalConsBuffs_F[idx].Value);
			}
			return true;
		}
		public bool TickConsume_Arch(RepoList consVels, RepoList archBuffs_F) {
			if (consVels == null || consVels.Count == 0) return true;
			int idx;
			foreach (var consume in consVels.RList) {
				idx = consume.RepoInt;
				if (_amounts[idx].Value < consume.Value * (1.0f - archBuffs_F[idx].Value - _globalConsBuffs_F[idx].Value)) 
					return false; 
			}
			foreach (var consume in consVels.RList) {
				idx = consume.RepoInt;
				_amounts[idx].Value -= consume.Value * (1.0f - archBuffs_F[idx].Value - _globalConsBuffs_F[idx].Value);
			}
			return true;
		}
		public void TickProduce_Villager(RepoList produceVels, RepoList archBuffs_F, RepoList villagerBuffs_F) {
			int idx;
			foreach (var produce in produceVels.RList) {
				idx = produce.RepoInt;
				_amounts[produce.RepoInt].Value += produce.Value * (1.0f + archBuffs_F[idx].Value + villagerBuffs_F[idx].Value + _globalProdBuffs_F[idx].Value);
			}
		}
		public void TickProduce_Arch(RepoList produceVels, RepoList archBuffs_F) {
			int idx;
			foreach (var produce in produceVels.RList) {
				idx = produce.RepoInt;
				_amounts[produce.RepoInt].Value += produce.Value * (1.0f + archBuffs_F[idx].Value + _globalProdBuffs_F[idx].Value);
			}
		}


		private void AddGlobalConsBuff(Repository repository, float buff) {
			_globalConsBuffs_F[(int)repository].Value += buff;
		}
		private void AddGlobalConsBuff(RTPair<float> rtPair) {
			_globalConsBuffs_F[rtPair.RepoInt].Value += rtPair.Value;
		}
		private void AddGlobalProdBuff(Repository repository, float buff) {
			_globalProdBuffs_F[(int)repository].Value += buff;
		}
		private void AddGlobalProdBuff(RTPair<float> rtPair) {
			_globalProdBuffs_F[rtPair.RepoInt].Value += rtPair.Value;
		}
	}
}