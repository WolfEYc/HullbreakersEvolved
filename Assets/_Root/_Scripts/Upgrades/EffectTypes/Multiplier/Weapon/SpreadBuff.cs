using System;
using UnityEngine;

namespace Hullbreakers
{
	[Serializable]
	public class SpreadBuff : MultiplierEffect
	{
		public override void Do(GameObject context)
		{ 
			context.GetComponentInChildren<WeaponMods>().spread.AddToHierarchy(buff);
		}

		public override void Undo(GameObject context)
		{
			context.GetComponentInChildren<WeaponMods>().spread.RemoveFromHierarchy(buff);
		}
	}
}