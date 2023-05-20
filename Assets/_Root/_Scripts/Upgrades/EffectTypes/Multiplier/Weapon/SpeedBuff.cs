using System;
using UnityEngine;

namespace Hullbreakers
{
	[Serializable]
	public class SpeedBuff : MultiplierEffect
	{
		public override void Do(GameObject context)
		{ 
			context.GetComponentInChildren<WeaponMods>().speed.AddToHierarchy(buff);
		}

		public override void Undo(GameObject context)
		{
			context.GetComponentInChildren<WeaponMods>().speed.RemoveFromHierarchy(buff);
		}
	}
}