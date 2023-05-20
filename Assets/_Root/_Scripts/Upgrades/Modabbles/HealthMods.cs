using UnityEngine;

namespace Hullbreakers
{
    public class HealthMods : MonoBehaviour
    {
        [field: SerializeField] public MultiplyableInt maxHp { get; private set; }
        [field: SerializeField] public Multiplyable damageReceptionScalar { get; private set; }
        [field: SerializeField] public Multiplyable healingReceptionScalar { get; private set; }
        [field: SerializeField] public Multiplyable stunReceptionScalar { get; private set; }
        [field: SerializeField] public Multiplyable crashDmgReceptionScalar { get; private set; }

        void Awake()
        {
            maxHp.RefreshValue();
            damageReceptionScalar.RefreshValue();
            healingReceptionScalar.RefreshValue();
            stunReceptionScalar.RefreshValue();
            crashDmgReceptionScalar.RefreshValue();
        }
    }
}
