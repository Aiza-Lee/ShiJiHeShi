using System;
using UnityEngine;

namespace BasicLogic {
	[Serializable]
	public class VillagerData {
		public string FirstName, LastName;
		public string ID;
		public Job Job;
		public Vector2 Position;
	}
}