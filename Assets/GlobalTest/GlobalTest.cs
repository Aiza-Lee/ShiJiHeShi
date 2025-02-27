using BasicLogic;
using UnityEngine;

public class GlobalTest : MonoBehaviour {
	public Transform Layer;
	private void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			Layer = ILayer.NewLayer(LayerType.Grass, 0).transform;
		}
		if (Input.GetKeyDown(KeyCode.B)) {
			IArch.NewArch(ArchType.Cottage, 0, 0, 0, Layer);
		}
	}
}