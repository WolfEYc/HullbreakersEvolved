using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class CrosshairManager : Singleton<CrosshairManager>
    {
        [Tooltip("All crosshairs must be of equal dimensions, hotspot is configured to center of first texture")]
        [SerializeField] Texture2D[] cursorTextures;
        [SerializeField] float delay;

        Vector2 _hotspot;
        WaitForSeconds _waitforFrame;
        int _currentFrame;

        protected override void Awake()
        {
            base.Awake();
            _waitforFrame = new WaitForSeconds(delay);
            _hotspot = new Vector2(cursorTextures[0].width / 2f, cursorTextures[0].height / 2f);
        }

        void Start()
        {
            StartCoroutine(AnimateTextureRoutine());
        }

        IEnumerator AnimateTextureRoutine()
        {
            while (true)
            {
                Cursor.SetCursor(cursorTextures[_currentFrame], _hotspot, CursorMode.ForceSoftware);
                _currentFrame = (_currentFrame + 1) % cursorTextures.Length;
                yield return _waitforFrame;
            }
        }
    }
}
