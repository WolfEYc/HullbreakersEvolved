using UnityEngine;

namespace Hullbreakers
{
    public class WeaponMods : MonoBehaviour
    {
        [field: SerializeField] public Multiplyable damage { get; private set; }
        [field: SerializeField] public Multiplyable speed { get; private set; }
        [field: SerializeField] public Multiplyable knockback { get; private set; }
        [field: SerializeField] public Multiplyable size { get; private set; }
        [field: SerializeField] public Multiplyable spread { get; private set; }
        [field: SerializeField] public Multiplyable ttl { get; private set; }
        [field: SerializeField] public Multiplyable shotsPerMinute { get; private set; }
        [field: SerializeField] public MultiplyableInt pierce { get; private set; }
        [field: SerializeField] public MultiplyableInt count { get; private set; }
        [field: SerializeField] public Multiplyable stunDuration { get; private set; }
        [field: SerializeField] public Multiplyable dmgRecepIncOnTarget { get; private set; }
        [field: SerializeField] public Multiplyable lifeSteal { get; private set; }

        public LayerMask layerMask { get; private set; }
        
        void Awake()
        {
            damage.RefreshValue();
            speed.RefreshValue();
            knockback.RefreshValue();
            size.RefreshValue();
            spread.RefreshValue();
            ttl.RefreshValue();
            shotsPerMinute.RefreshValue();
            pierce.RefreshValue();
            count.RefreshValue();
            stunDuration.RefreshValue();
            dmgRecepIncOnTarget.RefreshValue();
            lifeSteal.RefreshValue();
            layerMask = Physics2D.GetLayerCollisionMask(gameObject.layer);
        }
        
        
    }
}
