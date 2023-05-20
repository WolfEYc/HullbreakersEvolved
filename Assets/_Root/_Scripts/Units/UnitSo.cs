using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    [CreateAssetMenu(order = 3, fileName = "NewUnit", menuName = "Unit")]
    public class UnitSo : SerializedScriptableObject
    {
        [field: SerializeField] public int price { get; private set; }
        [field: SerializeField] public GameObject prefab { get; private set; }
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public Color color { get; private set; }
        [field: SerializeField] public string description { get; private set; }

        [SerializeReference] List<Upgrade> top;
        [SerializeReference] List<Upgrade> mid;
        [SerializeReference] List<Upgrade> bot;

        public List<Upgrade> Top => top;
        public List<Upgrade> Mid => mid;
        public List<Upgrade> Bot => bot;

        public bool purchased { get => price == 0 || PlayerPrefs.GetInt(name, 0) == 1; set => PlayerPrefs.SetInt(name, value ? 1 : 0); }
    }
}
