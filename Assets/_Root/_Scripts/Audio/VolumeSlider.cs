using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(UISlider))]
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] VolumeSetter volumeSetter;
        UISlider _uiSlider;

        void Awake()
        {
            _uiSlider = GetComponent<UISlider>();
            _uiSlider.OnValueChanged.AddListener(volumeSetter.SetVolume);
        }

        void OnEnable()
        {
            _uiSlider.SetValue(volumeSetter.GetVolume(), false);
        }
    }
}
