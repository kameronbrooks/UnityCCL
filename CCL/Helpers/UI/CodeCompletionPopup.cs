using UnityEngine;

namespace CCL.UI
{
    public class CodeCompletionPopup : MonoBehaviour
    {
        public static float eventDuplicationThreshold = 0.2f;

        private bool _isOpen = false;

        private float _lastAutoCompleteTime = 0;

        [SerializeField]
        private CodeCompletionList _list;

        [SerializeField]
        private float _maxHeight = 300;

        private string[] _options;

        [SerializeField]
        private CodeField _parent;

        private bool _updateOnNextFrame = false;

        public bool isOpen
        {
            get
            {
                return _isOpen;
            }
        }

        public RectTransform rectTransform
        {
            get
            {
                return (RectTransform)transform;
            }
        }

        public void Close()
        {
            _list.onSubmit -= SendChoice;
            _isOpen = false;
            gameObject.SetActive(false);
        }

        public void Open(string[] options, Vector2 position)
        {
            _isOpen = true;
            _options = options;
            _list.onSubmit += SendChoice;
            _list.SetOptions(options);
            _list.selectedIndex = 0;
            gameObject.SetActive(true);
            rectTransform.anchoredPosition = position;
            _updateOnNextFrame = true;
        }

        public void SendChoice()
        {
            if (Time.realtimeSinceStartup - _lastAutoCompleteTime < eventDuplicationThreshold) return;
            UIHelper.InsertPrediction(_parent, _options[_list.selectedIndex]);
            _lastAutoCompleteTime = Time.realtimeSinceStartup;

            Close();
        }

        public void Update()
        {
            if (_updateOnNextFrame)
            {
                UpdateSize();
            }
            if (_options == null || _options.Length < 1) return;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Tab))
            {
                SendChoice();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _list.selectedIndex += 1;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _list.selectedIndex -= 1;
            }
        }

        protected void UpdateSize()
        {
            Vector2 rectSize = ((RectTransform)transform).sizeDelta;
            rectSize.x = _list.width + 18;

            int items = Mathf.Min(_list.maxItemsShown, _list.itemsActive);
            rectSize.y = (30 * items);
            ((RectTransform)transform).sizeDelta = rectSize;
        }
    }
}