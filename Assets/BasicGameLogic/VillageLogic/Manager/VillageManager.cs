using System;
using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace BasicLogic 
{
	public class VillageManager : MonoSingleton<VillageManager> {
		[Header("村庄信息")]
		public List<NSPair<Repository, float>> RepositoryVelocity = new();
		[Tooltip("Layer间隔长度")] public float LayerGap;
		[Tooltip("Cell 长度")] public float CellLength;

	}
}