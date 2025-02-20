using UnityEngine;

namespace NSFrame 
{
	public abstract class PanelBase : MonoBehaviour {
		public UITypeEnum UIType;
		public int GetTypeNum { get => (int)UIType; }
		public bool IsLoaded;

		public virtual void Start() {
			this.AddToFrame();
		}
		public abstract void OnShow();
		public abstract void OnClose();
	}
}