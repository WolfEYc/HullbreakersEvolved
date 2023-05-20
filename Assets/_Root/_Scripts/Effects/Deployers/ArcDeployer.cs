using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hullbreakers
{
    public class ArcDeployer<T> : MonoBehaviour, IColorable where T : ArcFX<T>
    {
        Dictionary<Collider2D, T> _arcFxs;
        Transform _transform;

        public float size;
        bool _rainbow;
        Color _color;

        void Awake()
        {
            _transform = transform;
            _arcFxs = new Dictionary<Collider2D, T>();
        }

        public void DeactivateArcs()
        {
            var arcsToRemove = _arcFxs.ToArray();

            foreach (var arc in arcsToRemove)
            {
                RemoveArcFX(arc);
            }
        }
        
        public void RemoveArcFX(KeyValuePair<Collider2D, T> arc)
        {
            arc.Value.receiverTarget.Destroyed -= RemoveArcFX;
            arc.Value.stampOfApproval = false;
            arc.Value.Release();
            _arcFxs.Remove(arc.Key);
        }

        public void RemoveArcFX(Collider2D col)
        {
            var arcFX = _arcFxs[col];
            arcFX.stampOfApproval = false;
            arcFX.receiverTarget.Destroyed -= RemoveArcFX;
            arcFX.Release();
            _arcFxs.Remove(col);
        }

        void RemoveArcFX(Health health)
        {
            RemoveArcFX(health.simpleDamageRefs.hitBox);
        }
        
        public void AddArcFX(Collider2D col, Health health)
        {
            T arcFX = GenericPool<T>.instance.Get();

            if (_rainbow)
            {
                arcFX.coloredEffect.SetRainbow();
            }
            else
            {
                arcFX.coloredEffect.SetColor(_color);
            }

            arcFX.scale = size;

            arcFX.stampOfApproval = true;

            arcFX.receiverTarget = health;

            var position = _transform.position;
            arcFX.deployer = position;
            arcFX.receiver = col.ClosestPoint(position);
            
            _arcFxs.Add(col, arcFX);
            
            health.Destroyed += RemoveArcFX;
        }

        public void SetColor(Color color)
        {
            _rainbow = false;
            _color = color;
        }

        public void SetRainbow()
        {
            _rainbow = true;
        }

        public void StampOrAdd(Collider2D col, Health health)
        {
            if (_arcFxs.TryGetValue(col, out T arcFX))
            {
                arcFX.stampOfApproval = true;
            }
            else
            {
                AddArcFX(col, health);
            }
        }

        public void RemoveNonStamped()
        {
            var arcsToRemove = _arcFxs.Where
                (arc => !arc.Value.stampOfApproval).ToArray();

            foreach (var arc in arcsToRemove)
            {
                RemoveArcFX(arc);
            }

            foreach (var arcFX in _arcFxs.Values)
            {
                arcFX.stampOfApproval = false;
            }
        }

        void FixedUpdate()
        {
            Vector2 pos = _transform.position;
            foreach (var arc in _arcFxs)
            {
                arc.Value.deployer = pos;
                arc.Value.receiver = arc.Key.ClosestPoint(pos);
            }
        }
    }
}
