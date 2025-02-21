using System.Collections.Generic;
using NSFrame;
using UnityEngine;
using UnityEngine.Events;

namespace BasicLogic
{
	public enum ArchitectureTag {
		Room
	}
	public abstract class IArchitecture {
		public ArchitectureTag ArchitectureTag;
		public int Level;
		public string Name;
		/// <summary>
		/// Level -> Introduction
		/// </summary>
		public List<NSPair<int, string>> Introductions;
		/// <summary>
		/// Level -> List<Resitory, ProduceVelocity>
		/// </summary>
		public List<NSPair<int, List<NSPair<Repository, float>>>> ProduceVelocities;
		/// <summary>
		/// Level -> List<Resitory, ConsumeVelocity>
		/// </summary>
		public List<NSPair<int, List<NSPair<Repository, float>>>> ConsumeVelocities;

		public int Layer;
		public int Order;
		/// <summary>
		/// 建筑的尺寸 Layers * Orders，默认以右下角为上面 Layer Order 所在的点
		/// </summary>
		public NSPair<int, int> Size;
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

		public UnityAction OnConstruct;
		public UnityAction OnDeconstruct;

		public abstract void ConstructAt(int layer, int order);
	}
}