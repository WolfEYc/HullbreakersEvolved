using System;
using UnityEngine;

namespace Hullbreakers
{
	[Serializable]
	public class TTLBuff : MultiplierEffect
	{
		public override void Do(GameObject context)
		{ 
			context.GetComponentInChildren<WeaponMods>().ttl.AddToHierarchy(buff);
		}

		public override void Undo(GameObject context)
		{
			context.GetComponentInChildren<WeaponMods>().ttl.RemoveFromHierarchy(buff);
		}
	}
}