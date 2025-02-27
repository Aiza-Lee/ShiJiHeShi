using System;
using UnityEngine;

namespace BasicLogic 
{
	[Serializable]
	public class VillagerSaveData {
		public string ID;
		public string FirstName, LastName;
		public Vector2 Position;

		public VillagerSaveData(string _firstName, string _lastName, Vector2 _position) {
			ID = Guid.NewGuid().ToString();
			FirstName = _firstName;
			LastName = _lastName;
			Position = _position;
		}
	}
}