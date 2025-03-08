using System.Collections.Generic;
using LogicUtilities;
using NSFrame;
using UnityEngine;
using UnityEngine.U2D;

namespace BasicLogic
{
	[RequireComponent(typeof(SmoothFade), typeof(SpriteRenderer))]
	public abstract class ArchBase : MonoBehaviour, ISaveable<ArchDataBase>, IArchInfo, IPosition, IRepoBuff {

		[Header("挂载")] 
		public ArchConfig Config;
		public List<Light2DBase> Light2Ds;

		#region Config Getter
			public string Name => Config.Name;
			public int Size => Config.Size;
			public int Maxcontain => Config.LevelConfigs[Level].MaxContain;
			public string Introductions => Config.LevelConfigs[Level].Introductions;
			public RepoList InProdVels => Config.LevelConfigs[Level].InherentProduceVelocities;
			public RepoList InConsVels => Config.LevelConfigs[Level].InherentConsumeVelocities;
			public RepoList PerOneProdVels => Config.LevelConfigs[Level].ExtraProduceVelocitiesPerOne;
			public RepoList PerOneConsVels => Config.LevelConfigs[Level].ExtraConsumeVelocitiesPerOne;
			public RepoList VolAdds => Config.LevelConfigs[Level].VolumeAdds;
			public JobList ExperienceAdds => Config.LevelConfigs[Level].ExperienceAdds;
			public RepoList ConstructCost => Config.ConstructCost;
			public float DeconstructRate => Config.DeconstructRate;
			public float Repairate => Config.RepairRate;
			public Animator Animator => Config.Animator;

		#endregion

		[Header("Informations")]

		#region IPosition
			public Position Position { get => new(Layer, Order * 2 + (Layer.IsEven() ? 0 : -1)); }
		#endregion

		#region IArchInfo
			public ArchType ArchType { get; set; }
			public string ID { get; set; }
			public int Layer { get; set; }
			public int Order { get; set; }
			public int Level { get; set; }
			public List<Villager> InArchVillager { get; private set; }
		#endregion

		#region IRepoBuff
			public RepoList ConsBuffs_F { get; private set; } 
			public RepoList ProdBuffs_F { get; private set; }
		#endregion


		public SpriteRenderer SpriteRenderer { get; private set; }
		public SmoothFade SmoothFade { get; private set; }


		private void Awake() {
			SpriteRenderer = GetComponent<SpriteRenderer>();
			SmoothFade = GetComponent<SmoothFade>();
		}
		private void OnEnable() {
			EventSystem.AddListener((int)LogicEvent.Tick, UpdateRepo);
		}
		private void OnDisable() {
			EventSystem.RemoveListener((int)LogicEvent.Tick, UpdateRepo);
		}

		#region 事件绑定

			/// <summary>
			/// 向RepositoryManager提供一次资源更新的数据。成功创造资源的话为村民增加经验
			/// </summary>
			private void UpdateRepo() {
				// 建筑固有
				if (RepositoryManager.Instance.TickConsume_Arch(InConsVels, ConsBuffs_F)) {
					RepositoryManager.Instance.TickProduce_Arch(InProdVels, ProdBuffs_F);
				}
				// 村民产出
				foreach (var villager in InArchVillager) {
					if (RepositoryManager.Instance.TickConsume_Villager(PerOneConsVels, ConsBuffs_F, villager.ConsBuffs_F)) {
						RepositoryManager.Instance.TickProduce_Villager(PerOneProdVels, ProdBuffs_F, villager.ProdBuffs_F);
						villager.AddExperience(ExperienceAdds);
					}
				}
			}

		#endregion
		
		public bool TryAddVillager(Villager villager) {
			if (Maxcontain == InArchVillager.Count) return false;
			InArchVillager.Add(villager);
			return true;
		}
		public bool TryReleaseVillager(Villager villager) {
			return InArchVillager.Remove(villager);
		}
		

		#region ISaveable
			public virtual ArchDataBase GetData() {
				var data = new ArchDataBase() {
					ArchType = ArchType,
					ID = ID,
					Layer = Layer,
					Order = Order,
					Level = Level,
					ProdBuffs = ProdBuffs_F,
					ConsBuffs = ConsBuffs_F,
					InArchVillIDs = new(),
				};
				foreach (var vill in InArchVillager) {
					data.InArchVillIDs.Add(vill.ID);
				}
				return data;
			}
			public virtual void InitData(ArchDataBase data) {
				ArchType = data.ArchType;
				ID = data.ID;
				Layer = data.Layer;
				Order = data.Order;
				Level = data.Level;
				ProdBuffs_F = data.ProdBuffs.ConvertToFull();
				ConsBuffs_F = data.ConsBuffs.ConvertToFull();
				InArchVillager = new();
				foreach (var villID in data.InArchVillIDs) {
					InArchVillager.Add(WorldManager.Instance.FindVillager(villID));
				}
			}
		#endregion
	}
}