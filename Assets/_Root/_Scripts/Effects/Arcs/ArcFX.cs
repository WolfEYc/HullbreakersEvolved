using System;
using UnityEngine;
using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    [RequireComponent(typeof(ColoredEffect))]
    public abstract class ArcFX<T> : PooledObject<T>, IColorable where T : ArcFX<T>
    {
        static readonly ExposedProperty DeployerID = "Deployer";
        static readonly ExposedProperty ReceiverID = "Receiver";
        static readonly ExposedProperty ScaleID = "Scale";

        [HideInInspector] public Health receiverTarget;
        public ColoredEffect coloredEffect { get; private set; }

        [HideInInspector] public bool stampOfApproval;

        public Vector2 deployer
        {
            get => coloredEffect.visualEffect.GetVector2(DeployerID);
            set => coloredEffect.visualEffect.SetVector2(DeployerID, value);
        }
        
        public Vector2 receiver
        {
            get => coloredEffect.visualEffect.GetVector2(ReceiverID);
            set => coloredEffect.visualEffect.SetVector2(ReceiverID, value);
        }

        public float scale
        {
            get => coloredEffect.visualEffect.GetFloat(ScaleID);
            set => coloredEffect.visualEffect.SetFloat(ScaleID, value);
        }

        protected override void Awake()
        {
            base.Awake();
            coloredEffect = GetComponent<ColoredEffect>();
        }

        public override float sizeRadius => Vector2.Distance(deployer, receiver);
        public void SetColor(Color color)
        {
            coloredEffect.SetColor(color);
        }

        public void SetRainbow()
        {
            coloredEffect.SetRainbow();
        }
    }
}
