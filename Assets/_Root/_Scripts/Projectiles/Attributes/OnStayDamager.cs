using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hullbreakers
{
    public class OnStayDamager : MonoBehaviour
    {
        public Dictionary<Collider2D, IDamageable> inRange { get; private set; }

        static readonly WaitForSeconds WaitASec = new(1f);

        IDamager[] _damagers;
        Rigidbody2D _rb;

        void Awake()
        {
            inRange = new Dictionary<Collider2D, IDamageable>();
            _damagers = GetComponentsInChildren<IDamager>();
            _rb = GetComponent<Rigidbody2D>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.TryGetComponent(out IDamageable damageable)) return;
            inRange[col] = damageable;
        }

        void OnTriggerExit2D(Collider2D col)
        {
            inRange.Remove(col);
        }

        void OnEnable()
        {
            StartCoroutine(DamageRoutine());
        }

        void OnDisable()
        {
            StopCoroutine(DamageRoutine());
        }

        IEnumerator DamageRoutine()
        {
            while (true)
            {
                var selfpos = _rb.position;
                var arr = inRange.ToArray();
                foreach (var damageable in arr)
                {
                    var pos = damageable.Key.ClosestPoint(selfpos);
                    var dir = pos - selfpos;

                    for (int j = 0; j < _damagers.Length; j++)
                    {
                        _damagers[j].InflictDamage(damageable.Value, pos, dir);
                    }
                }

                yield return WaitASec;
            }
        }
    }
}
