using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

		public static GameObject InstantiateWithNewMaterial(GameObject prefab, Transform father) {
			var go  = GameObject.Instantiate(prefab, father);
			if (go.TryGetComponent<SpriteRenderer>(out var renderer)) {
				renderer.material = new Material(renderer.material);
			}
			return go;
		}

		public static List<JTPair<int>> ConvertToFull(this List<JTPair<int>> obj) {
			if (obj.Count == GameManager.JobSize) {
				return obj;
			}
			var ori = obj;
			obj = new List<JTPair<int>>();
			for (int i = 0; i < GameManager.JobSize; ++i) {
				obj.Add(new((JobType)i, 0));
			}
			foreach (var jtpair in ori) {
				obj[jtpair.JobInt].Value = jtpair.Value;
			}
			return obj;
		}

		public static bool IsEven(this int value) {
			return value % 2 == 0;
		}
		public static bool IsOdd(this int value) {
			return !value.IsEven();
		}
	}
}