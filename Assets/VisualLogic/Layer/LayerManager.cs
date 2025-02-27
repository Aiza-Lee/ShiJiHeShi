using System;
using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace BasicLogic {
	/// <summary>
	/// 管理 2D 视角下建筑的放缩问题
	/// 管理摄像机的移动
	/// </summary>
	public partial class LayerVisual : MonoSingleton<LayerVisual> {
		[Header("挂载")]
		public Transform MainCamera;

		[Header("Cosntants To Be Adjusted")]
		[Range(0.1f, 1.2f)] public float ScaleRate;

		[Header("Informations")]
		[ReadOnly] int _curLayer;
		[ReadOnly] List<Transform> Layers;

		private Vector3 _lastPos;

		public void Forward() {}
		public void Backward() {}
	}
}