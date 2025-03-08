using System;
using System.Collections.Generic;

namespace BasicLogic 
{
	[Serializable]
	public class ArchDataBase {
		public ArchType ArchType;
		public string ID;
		public int Layer;
		public int Order;
		public int Level;
		public RepoList ProdBuffs;
		public RepoList ConsBuffs;
		public List<String> InArchVillIDs;
	}
}