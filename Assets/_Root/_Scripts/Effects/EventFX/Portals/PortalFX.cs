using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public abstract class PortalFX<T> : EventFX<T> where T : PortalFX<T>
    {
        [SerializeField] protected float spawnTime;

        [field: SerializeField] public float spawnForce { get; private set; }

        protected abstract IEnumerator SpawnEncounter(Encounter encounter);

        public void Spawn(Vector2 pos, Encounter encounter)
        {
            base.Spawn(pos, 0f, 1f);
            StartCoroutine(SpawnEncounter(encounter));
        }
    }
}
