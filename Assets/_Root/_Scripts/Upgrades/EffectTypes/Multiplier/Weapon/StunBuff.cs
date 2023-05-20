using System;
using UnityEngine;

namespace Hullbreakers
{
	[Serializable]
	public class StunBuff : MultiplierEffect
	{
		public override void Do(GameObject context)
		{ 
			context.GetComponentInChildren<WeaponMods>().stunDuration.AddToHierarchy(buff);
		}

		public override void Undo(GameObject context)
		{
			context.GetComponentInChildren<WeaponMods>().stunDuration.RemoveFromHierarchy(buff);
		}
	}
}