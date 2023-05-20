using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    public abstract class PooledObject<T> : SerializedMonoBehaviour, ISpawnable where T : PooledObject<T>
    {
        public GameObject GameObject { get; private set; }
        
        public Transform Transform { get; private set; }

        IActionOnDestroyed[] _actionsOnDestroyed;
        ISpawnable _spawnableImplementation;

        [SerializeField] bool persistent;


        protected virtual void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
            _actionsOnDestroyed = GetComponentsInChildren<IActionOnDestroyed>();
            if (!persistent)
            {
                GameStateManager.instance.OnClear += Release;
            }
        }
        
        public virtual void Release()
        {
            if(!GameObject.activeSelf) return;
            TriggerDestroyedActions();
            GenericPool<T>.instance.Release((T)this);
        }

        void TriggerDestroyedActions()
        {
            for (int i = 0; i < _actionsOnDestroyed.Length; i++)
            {
                _actionsOnDestroyed[i].OnDestroyed();
            }
        }

        public void Instantiate(Vector2 pos, float rot = 0, float initialForce = 0)
        {
            if(GameStateManager.instance.state != GameStateManager.GameState.InGame) return;
            GenericPool<T>.instance.Get().Spawn(pos, rot, initialForce);
        }

        public abstract float sizeRadius { get; }

        public virtual void Spawn(Vector2 pos, float rot = 0f, float initialForce = 0f)
        {
            Transform.SetPositionAndRotation(pos, ExtensionMethods.RotationFromAngle2D(rot));
        }
    }
}
