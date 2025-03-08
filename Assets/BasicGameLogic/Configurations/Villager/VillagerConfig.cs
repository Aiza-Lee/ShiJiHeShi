using UnityEngine;

namespace BasicLogic 
{
	[CreateAssetMenu(fileName = "VillagerConfig", menuName = "ShiJiHeShi/Config/Villager Config")]
	public class VillagerConfig : ScriptableObject {
		[Header("动画")] public Animator Animator;
	}
}