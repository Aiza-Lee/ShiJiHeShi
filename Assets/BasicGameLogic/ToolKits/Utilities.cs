using System.Collections.Generic;
using System.Linq;
using System.Security;
using UnityEditor.ShaderKeywordFilter;

namespace BasicLogic
{
	public static class Utilities {
		public static List<Dictionary<Repository, float>> ToListDict(this List<List<RTPair<float>>> obj) {
			var res = new List<Dictionary<Repository, float>>();
			foreach (var list in obj) {
				res.Add(new());
				foreach (var rfpair in list) {
					res.Last().Add(rfpair.RepositoryType, rfpair.Value);
				}
			}
			return res;
		}
		public static Dictionary<Repository, float> ToDict(this List<RTPair<float>> obj) {
			var res = new Dictionary<Repository, float>();
			foreach (var rfpair in obj) {
				res.Add(rfpair.RepositoryType, rfpair.Value);
			}
			return res;
		}

		public static List<RepoList> ToFullFill(this List<RepoList> obj) {
			var res = new List<RepoList>();
			foreach (var repoList in obj) {
				res.Add(new(repoList));
			}
			return res;
		}
	}
}