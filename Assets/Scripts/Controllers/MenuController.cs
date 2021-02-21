using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class MenuController
    {
        private Canvas _canvas;
        private Text _text;
        private Button _button;

        public Button Button => _button;
        
        public MenuController(Text textPrefab, Button buttonPrefab)
        {
            Initialize(textPrefab, buttonPrefab);
        }
        
        private void Initialize(Text text, Button button)
        {
            _canvas = new GameObject("Canvas").
                AddComponent<CanvasRenderer>()
                .gameObject.AddComponent<GraphicRaycaster>()
                .gameObject.AddComponent<CanvasScaler>()
                .gameObject.GetComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = _canvas.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.matchWidthOrHeight = 0.5f;
            
            _text = Object.Instantiate(text,
                new Vector2(_canvas.pixelRect.height / 2, _canvas.pixelRect.width / 2),
                Quaternion.identity, _canvas.transform);

            _button = Object.Instantiate(button,
                new Vector2(_canvas.pixelRect.height / 2, _canvas.pixelRect.width / 2- _text.rectTransform.rect.height * 2),
                Quaternion.identity, _canvas.transform);
            Close();
        }
        
        public void View(string text)
        {
            _canvas.gameObject.SetActive(true);
            _text.text = text;
        }

        public void Close()
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}