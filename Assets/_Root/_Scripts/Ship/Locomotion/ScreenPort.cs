using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    public class ScreenPort : MonoBehaviour
    {
        public event Action OnTp;

        Transform _transform;

        bool OutLeft()
        {
            return _transform.position.x < MainCam.instance.bottomLeft.x;
        }

        bool OutRight()
        {
            return _transform.position.x > MainCam.instance.topRight.x;
        }
        
        bool OutBottom()
        {
            return _transform.position.y < MainCam.instance.bottomLeft.y;
        }

        bool OutTop()
        {
            return _transform.position.y > MainCam.instance.topRight.y;
        }

        void TpRight()
        {
            _transform.position = new Vector2(MainCam.instance.topRight.x, _transform.position.y);
        }
        
        void TpLeft()
        {
            _transform.position = new Vector2(MainCam.instance.bottomLeft.x, _transform.position.y);
        }
        
        void TpTop()
        {
            _transform.position = new Vector2(_transform.position.x, MainCam.instance.topRight.y);
        }

        void TpBottom()
        {
            _transform.position = new Vector2(_transform.position.x, MainCam.instance.bottomLeft.y);
        }

        Dictionary<Func<bool>, Action> _actions;

        void Awake()
        {
            _transform = transform;
            
            _actions = new Dictionary<Func<bool>, Action>
            {
                { OutLeft, TpRight },
                { OutRight, TpLeft },
                { OutTop, TpBottom },
                { OutBottom, TpTop }
            };
        }
        
        void FixedUpdate()
        {
            foreach (var pair in _actions)
            {
                if (!pair.Key()) continue;
                
                pair.Value();
                OnTp?.Invoke();
                return;
            }
        }
        
    }
}
