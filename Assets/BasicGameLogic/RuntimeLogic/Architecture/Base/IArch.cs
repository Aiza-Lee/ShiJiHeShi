using System.Collections.Generic;
using LogicUtilities;
using NSFrame;
using UnityEngine;
using UnityEngine.U2D;

namespace BasicLogic
{
	public abstract class IArch : MonoBehaviour {

		[Header("挂载")] 
		public ArchConfigBase Config;
		public List<Light2DBase> Light2Ds;

		#region Config Getter
			public string Name => Config.Name;
			public int Size => Config.Size;
			public int Maxcontain => Config.MaxContain;
			public List<string> Introductions => Config.Introductions;
			private List<RepoList> _InprodVels;
			public List<RepoList> InProdVels {
				get => _InprodVels ??= Config.InherentProduceVelocities.ToFullFill();
				private set => _InprodVels = value;
			}
			private List<RepoList> _InConsVels;
			public List<RepoList> InConsVels {
				get => _InConsVels ??= Config.InherentConsumeVelocities.ToFullFill();
				private set => _InConsVels = value;
			}
			private List<RepoList> _PerOneProdVels;
			public List<RepoList> PerOneProdVels {
				get => _PerOneProdVels ??= Config.ExtraProduceVelocitiesPerOne.ToFullFill();
				private set => _PerOneProdVels = value;
			}
			private List<RepoList> _PerOneConsVels;
			public List<RepoList> PerOneConsVels {
				get => _PerOneConsVels ??= Config.ExtraConsumeVelocitiesPerOne.ToFullFill();
				private set => _PerOneConsVels = value;
			}
			public List<RepoList> VolAdds => Config.VolumeAdds;
			public RepoList ConstructCost => Config.ConstructCost;
			public float DeconstructRate => Config.DeconstructRate;
			public float Repairate => Config.RepairRate;
			public Animator Animator => Config.Animator;
		#endregion

		[Header("Informations")]
		[SerializeField][ReadOnly] protected ArchSaveData _saveData;
		[SerializeField][ReadOnly] protected List<IVillager> _insideVillagers = new();
		[SerializeField][ReadOnly] protected RepoList _prodBuffs = new(fillAll: true);
		[SerializeField][ReadOnly] protected RepoList _consBuffs = new(fillAll: true);

		#region _saveData Getter
			public int Layer => _saveData.Layer;
			public int Order => _saveData.Order;
			public int Level => _saveData.Level;
			public ArchType ArchType => _saveData.ArchType;
			public int ArchTypeInt => (int)ArchType;
		#endregion

		public List<IVillager> InsideVillagers => _insideVillagers;
		public int InsiderCount => _insideVillagers.Count;
		public RepoList ProdBuffs => _prodBuffs;
		public RepoList ConsBuffs => _consBuffs;

		public SpriteRenderer SpriteRenderer { get; private set; }

		private void Awake() {
			SpriteRenderer = GetComponent<SpriteRenderer>();
		}

		/// <summary>
		/// 从全局游戏配置中实例化Arch的预制体，触发CTOR事件，参数为IArch类型
		/// </summary>
		/// <param name="archSaveData"></param>
		/// <param name="father">预制体上的IArch脚本。</param>
		/// <returns></returns>
		public static IArch LoadArchGO(ArchSaveData archSaveData, Transform father) {
			var prefab = GameManager.Instance.GameConfig.GetArchPrefab(archSaveData.ArchType);
			var go = GameObject.Instantiate(prefab, father);
			if (!go.TryGetComponent<IArch>(out var arch)) {
				Debug.LogError($"Cannot get component ILayer from perfab of type {archSaveData.ArchType}");
				return null;
			}
			go.name = arch.Config.Name;
			arch._saveData = archSaveData;

			arch.OnConstruct();
			return arch;
		}

		public static IArch NewArch(ArchType archType, int layer, int order, int level, Transform father) {
			var prefab = GameManager.Instance.GameConfig.GetArchPrefab(archType);
			var go = GameObject.Instantiate(prefab, father);
			if (!go.TryGetComponent<IArch>(out var arch)) {
				Debug.LogError($"Cannot get component ILayer from perfab of type {archType}");
				return null;
			}
			go.name = arch.Config.Name;
			arch._saveData = new(archType, layer, order, level);

			arch.OnConstruct();
			return arch;
		}

		/// <summary>
		/// 向RepositoryManager提供一次资源更新的数据
		/// </summary>
		private void UpdateRepo() {
			if (RepositoryManager.Instance.TickConsume(InConsVels[Level], _consBuffs)) {
				RepositoryManager.Instance.TickProduce(InProdVels[Level], _prodBuffs);
			}
			foreach (var villager in _insideVillagers) {
				if (RepositoryManager.Instance.TickConsume(PerOneConsVels[Level], _consBuffs, villager.ConsBuffs)) {
					RepositoryManager.Instance.TickProduce(PerOneProdVels[Level], _prodBuffs, villager.ProdBuffs);
				}
			}
		}
		protected virtual void OnConstruct() {
			EventSystem.Invoke<IArch>("CTOR", this);
			EventSystem.AddListener("Tick", UpdateRepo);
		}
		protected virtual void OnDeconstruct() {
			EventSystem.RemoveListener("Tick", UpdateRepo);
		}
	}
}