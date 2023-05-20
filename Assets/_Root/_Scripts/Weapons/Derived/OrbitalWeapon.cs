using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    public class OrbitalWeapon : Weapon, IColorable
    {
        List<DamageOrb> _orbs;
        ContactFilter2D _contactFilter;
        Color _color;
        bool _rainbow;

        protected override void Awake()
        {
            base.Awake();
            _orbs = new List<DamageOrb>();
            mods.count.OnValueChanged += ReallocateOrbs;
            mods.size.OnValueChanged += SetOrbSize;
            mods.spread.OnValueChanged += PlaceOrbsInRadial;
            OnStoppedShooting += CeaseOrbMaddess;
            OnStartedShooting += BeginOrbMaddess;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ReallocateOrbs();
            SetOrbSize();
            PlaceOrbsInRadial();
        }

        void BeginOrbMaddess()
        {
            for (int i = 0; i < _orbs.Count; i++)
            {
                _orbs[i].GameObject.SetActive(true);
            }
        }

        void CeaseOrbMaddess()
        {
            for (int i = 0; i < _orbs.Count; i++)
            {
                _orbs[i].GameObject.SetActive(false);
            }
        }

        protected override void Start()
        {
            base.Start();
            _contactFilter.useLayerMask = true;
            _contactFilter.layerMask = mods.layerMask;
        }

        void ReallocateOrbs()
        {
            ReleaseOrbs();

            for (int i = 0; i < mods.count.value; i++)
            {
                DamageOrb damageOrb = DamageOrbPool.instance.Get();

                damageOrb.GameObject.layer = GameObject.layer;
                damageOrb.weapon = this;
                damageOrb.Transform.parent = Transform;

                if (_rainbow)
                {
                    damageOrb.SetRainbow();
                }
                else
                {
                    damageOrb.SetColor(_color);
                }
                
                
                _orbs.Add(damageOrb);
            }
            
            SetOrbSize();
            PlaceOrbsInRadial();
            CeaseOrbMaddess();
        }

        void SetOrbSize()
        {
            for (int i = 0; i < _orbs.Count; i++)
            {
                _orbs[i].Transform.localScale = new Vector3(mods.size.value, mods.size.value);
            }
        }
        
        void PlaceOrbsInRadial()
        {
            for (int i = 0; i < _orbs.Count; i++)
            {
                _orbs[i].Transform.localPosition =
                    ExtensionMethods.PosInCircle(mods.spread.value, i, _orbs.Count);
            }
        }

        public override void Shoot()
        {
            for (int i = 0; i < _orbs.Count; i++)
            {
                Physics2D.OverlapCircle(
                    _orbs[i].Transform.position,
                    _orbs[i].sizeRadius,
                    _contactFilter,
                    Explosion.Hits);
                
                _orbs[i].DamageHits();
            }
        }

        public void SetColor(Color color)
        {
            _color = color;
            _rainbow = false;

            for(int i = 0; i < _orbs.Count; i++)
            {
                _orbs[i].SetColor(color);
            }
        }

        public void SetRainbow()
        {
            _rainbow = true;
            for(int i = 0; i < _orbs.Count; i++)
            {
                _orbs[i].SetRainbow();
            }
        }

        void ReleaseOrbs()
        {
            for (int i = 0; i < _orbs.Count; i++)
            {
                _orbs[i].Transform.parent = DamageOrbPool.instance.Transform;
                _orbs[i].Release();
            }
            _orbs.Clear();
        }

        public override void OnDestroyed()
        {
            base.OnDestroyed();
            ReleaseOrbs();
        }
    }
}
