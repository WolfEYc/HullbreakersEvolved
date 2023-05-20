using System;
using UnityEngine;

namespace Hullbreakers
{
    public class PhysicsMods : MonoBehaviour
    {
        [field: SerializeField] public Multiplyable friction { get; private set; }
        [field: SerializeField] public Multiplyable accel { get; private set; }
        [field: SerializeField] public Multiplyable maxVel { get; private set; }
        [field: SerializeField] public Multiplyable rotationSpeed { get; private set; }
        [field: SerializeField] public Multiplyable crashDmg { get; private set; }

        void Awake()
        {
            friction.RefreshValue();
            accel.RefreshValue();
            rotationSpeed.RefreshValue();
            maxVel.RefreshValue();
            crashDmg.RefreshValue();
        }
    }
}
