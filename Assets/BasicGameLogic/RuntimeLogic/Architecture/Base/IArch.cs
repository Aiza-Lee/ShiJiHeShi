using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace BasicLogic
{
	public enum ArchType {
		/* 居住 */
		Cottage, 
		/* 原始生产 */
		Farm,
		LumberMill,
		Quarry,
		Mine,
		Fishery,
		Well,
		Windmill,
		Ochard,
		/* 存储 */
		Warehouse,
		/* 精炼 */
		Blacksmith,
		Workshop, 
		/* 装饰 */
		Garden,
		Fountain,
		Statue,
		/* 特殊 */
		Ruins,
	}
	public abstract class IArch : MonoBehaviour {

	  #region ArchitectureData
		public int Level {
			get => _archData.Level;
		}
		public int Layer {
			get => _archData.Layer;
		}
		public int Order {
			get => _archData.Order;
		}
		/// <summary>
		/// 类似于 GUID 的存在
		/// </summary>
		public string ID {
			get => _archData.ID;
		}
	  #endregion
		[SerializeField] protected ArchData _archData;
	  
	  #region ArchitectureConfig
		public string Name {
			get => ArchConfig.Name;
		}
		public ArchType ArchType {
			get => ArchConfig.ArchType;
		}
		/// <summary>
		/// 建筑的尺寸 Layers * Orders，默认以右下角为上面 Layer Order 所在的点
		/// </summary>
		public NSPair<int, int> Size {
			get => ArchConfig.Size;
		}
		
		/// <summary>
		/// Level -> Introduction
		/// </summary>
		public List<NSPair<int, string>> Introductions {
			get => ArchConfig.Introductions;
		}
		/// <summary>
		/// Level -> List<Resitory, ProduceVelocity>
		/// </summary>
		public List<NSPair<int, List<NSPair<Repository, float>>>> ProduceVelocities {
			get => ArchConfig.ProduceVelocities;
		}
		/// <summary>
		/// Level -> List<Resitory, ConsumeVelocity>
		/// </summary>
		public List<NSPair<int, List<NSPair<Repository, float>>>> ConsumeVelocities {
			get => ArchConfig.ConsumeVelocities;
		}
		/// <summary>
		/// Level -> <Repository, VolumeAdd>
		/// </summary>
		public List<NSPair<int, List<NSPair<Repository, float>>>> VolumeAdds {
			get => ArchConfig.VolumeAdds;
		}
	  #endregion
	  	public ArchConfigBase ArchConfig;

		/// <summary>
		/// 建筑在世界中的坐标
		/// </summary>
		public Vector3 CenterPosition {
			get {
				float tmpX = 0;
				if (Layer % 2 == 0) tmpX += VillageManager.Instance.CellLength * Order;
				else tmpX += VillageManager.Instance.CellLength * (Order - 0.5f);

				float tmpZ = VillageManager.Instance.LayerGap * Layer;

				return new(tmpX, 0.0f, tmpZ);
			}
		}

		public abstract void ConstructAt();
		public abstract void OnConstruct();
		public abstract void OnDeconstruct();
	}
}