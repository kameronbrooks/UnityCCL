using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CCL.UI
{
    public class CodeCompletionItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _background;

        private int _index;

        [SerializeField]
        private LayoutElement _layoutElem;

        [SerializeField]
        private CodeCompletionList _list;

        [SerializeField]
        private Color _normalColor;

        private bool _selected;

        [SerializeField]
        private Color _selectedColor;

        [SerializeField]
        private Text _textElement;

        public int index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }

        public RectTransform rectTransform
        {
            get
            {
                return (RectTransform)transform;
            }
        }

        public bool selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                _background.color = (value) ? _selectedColor : _normalColor;
            }
        }

        public string text
        {
            get
            {
                return _textElement.text;
            }
            set
            {
                _textElement.text = value;
                _layoutElem.preferredWidth = _textElement.preferredWidth + 10;
                Vector2 vec = rectTransform.sizeDelta;
                vec.x = _layoutElem.preferredWidth;
                rectTransform.sizeDelta = vec;
            }
        }

        public void Init(CodeCompletionList parent, int index)
        {
            _list = parent;
            _index = index;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_selected)
            {
                _list.Submit();
            }
            else
            {
                _list.Select(_index);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}