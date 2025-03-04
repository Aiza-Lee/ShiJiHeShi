using System.Collections.Generic;
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
			EventSystem.AddListener<Vector3>("CM", OnCameraMove);
		}
		private void OnDisable() {
			EventSystem.RemoveListener<Vector3>("CM", OnCameraMove);
		}

		private void OnCameraMove(Vector3 movement) {
			for (int i = 0; i < Backgrounds.Count; ++i) {
				Backgrounds[i].transform.Translate(-movement * MoveVelocities[i]);
			}
		}
	}
}