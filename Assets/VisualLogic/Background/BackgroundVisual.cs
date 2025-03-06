using System.Collections.Generic;
using LogicUtilities;
using NSFrame;
using UnityEngine;

namespace VisualLogic
{
	public class BackgroundVisual : MonoSingleton<BackgroundVisual> {
		[Header("挂载")]
		public Camera BackgroundCamera;
		public List<GameObject> Backgrounds;

		[Header("Constant")]
		public List<float> MoveVelocities;

		private void OnEnable() {
			EventSystem.AddListener<Vector3>((int)LogicEvent.LayerCameraMove_v3, OnCameraMove);
		}
		private void OnDisable() {
			EventSystem.RemoveListener<Vector3>((int)LogicEvent.LayerCameraMove_v3, OnCameraMove);
		}

		private void OnCameraMove(Vector3 movement) {
			for (int i = 0; i < Backgrounds.Count; ++i) {
				Backgrounds[i].transform.Translate(-movement * MoveVelocities[i]);
			}
		}
	}
}