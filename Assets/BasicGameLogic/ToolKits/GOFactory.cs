using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace BasicLogic
{
	public static class GOFactory {
		public static Villager LoadVill(VillData data, Transform father) {
			var prefab = GameManager.Instance.GameConfig.GetVillagerPrefab();
			var go = Utilities.InstantiateWithNewMaterial(prefab, father);
			if (!go.TryGetComponent<Villager>(out var vill)) {
				Debug.LogError($"Cannot get component IVillager from perfab of Villager.");
				return null;
			}
			vill.InitData(data);

			EventSystem.Invoke<Villager>((int)LogicEvent.VillagerConstructed_V, vill);

			return vill;
		}

		/// <summary>
		/// 请确保 data 传入的时 ArchDataBase 的派生类型
		/// </summary>
		/// <param name="data"></param>
		/// <param name="father"></param>
		/// <returns></returns>
		public static ArchBase LoadArch(ArchDataBase data, Transform father) {
			var prefab = GameManager.Instance.GameConfig.GetArchPrefab(data.ArchType);
			var go = Utilities.InstantiateWithNewMaterial(prefab, father);
			if (!go.TryGetComponent(BasicGameConfig.ArchClasses[data.ArchType], out var comp)) {
				Debug.LogError($"Cannot get component IArch from perfab of type {data.ArchType}");
				return null;
			}

			var arch = comp as ArchBase;
			arch.InitData(data);
			go.name = arch.Name;

			EventSystem.Invoke<ArchBase>((int)LogicEvent.ArchConstructed_A, arch);

			return arch;
		}

		/// <summary>
		/// 请确保 data 传入的时 LayerDataBase 的派生类型
		/// </summary>
		/// <param name="data"></param>
		/// <param name="father"></param>
		/// <returns></returns>
		public static LayerBase LoadLayer(LayerDataBase data, Transform father) {
			var prefab = GameManager.Instance.GameConfig.GetLayerPrefab(data.LayerType);
			var go = Utilities.InstantiateWithNewMaterial(prefab, father);
			if (!go.TryGetComponent(BasicGameConfig.LayerClasses[data.LayerType], out var comp)) {
				Debug.LogError($"Cannot get component IArch from perfab of type {data.LayerType}");
				return null;
			}

			var layer = comp as LayerBase;
			layer.InitData(data);
			go.name = layer.Name;

			EventSystem.Invoke<LayerBase>((int)LogicEvent.LayerConstructed_L, layer);

			return layer;
		}

	}
}