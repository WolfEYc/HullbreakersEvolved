using System;
using UnityEngine;

namespace Hullbreakers
{
    public class EnableOnThrust : MonoBehaviour
    {
        GameObject _gameObject;
        Thrust _thrust;
        void Awake()
        {
            _gameObject = gameObject;
            _thrust = GetComponentInParent<Thrust>();

            _thrust.StartedThrusting += () => _gameObject.SetActive(true);
            _thrust.StopThrusting += () => _gameObject.SetActive(false);
        }

        void Start()
        {
            _gameObject.SetActive(_thrust.enabled);
        }
    }
}
