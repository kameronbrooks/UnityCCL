using System.Collections.Generic;
using UnityEngine;

namespace CCL.UI
{
    public class CodeCompletionList : MonoBehaviour
    {
        [SerializeField]
        private CodeCompletionItem _itemPrefab;

        private List<CodeCompletionItem> _items;

        private int _itemsActive = 0;

        [SerializeField]
        private int _maxItemsShown = 6;

        [SerializeField]
        private UnityEngine.UI.Scrollbar _scrollbar;

        private int _selectedItem = 0;

        private float _width = 100;

        public delegate void Callback();

        public event Callback onSubmit;

        public float height
        {
            get
            {
                return ((RectTransform)transform).sizeDelta.y;
            }
        }

        public List<CodeCompletionItem> items
        {
            get
            {
                if (_items == null) _items = new List<CodeCompletionItem>();
                return _items;
            }
        }

        public int itemsActive
        {
            get
            {
                return _itemsActive;
            }
        }

        public int maxItemsShown
        {
            get
            {
                return _maxItemsShown;
            }

            set
            {
                _maxItemsShown = value;
            }
        }

        public int selectedIndex
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                Select(value);
            }
        }

        public float width
        {
            get
            {
                return ((RectTransform)transform).sizeDelta.x;
            }
        }

        public CodeCompletionItem AddNewItem()
        {
            CodeCompletionItem item = Instantiate<CodeCompletionItem>(_itemPrefab);

            item.transform.SetParent(this.transform, false);
            item.Init(this, items.Count);

            items.Add(item);

            return item;
        }

        public void HideAll()
        {
            for (int i = 0; i < items.Count; i += 1)
            {
                items[i].selected = false;
                items[i].gameObject.SetActive(false);
            }
        }

        public void Select(int index)
        {
            if (index == _selectedItem) return;

            if (index >= _itemsActive) index = index % _itemsActive;
            else if (index < 0) index = index + _itemsActive;

            items[_selectedItem].selected = false;
            _selectedItem = index;
            items[_selectedItem].selected = true;

            AdjustScrollBar();
        }

        public void SetOptions(string[] options)
        {
            int i = 0;
            _width = 150;
            _itemsActive = options.Length;
            for (; i < options.Length; i += 1)
            {
                if (i == items.Count) AddNewItem();

                items[i].gameObject.SetActive(true);
                items[i].selected = false;
                items[i].text = options[i];
                _width = Mathf.Max(((RectTransform)items[i].transform).sizeDelta.x, _width);
            }
            for (; i < items.Count; i += 1)
            {
                items[i].selected = false;
                items[i].gameObject.SetActive(false);
            }
            if (options.Length > 0)
            {
                _selectedItem = 0;
                items[0].selected = true;
            }

            //_scrollbar.numberOfSteps = _itemsActive - (_maxItemsShown - 1);
            _scrollbar.value = 1;
        }

        public void Submit()
        {
            onSubmit?.Invoke();
        }

        /*
public float width
{
   get
   {
       float maxWidth = 0;
       for(int i = 0; i < items.Count; i += 1)
       {
           float itemWidth = items[i].width;
           if (itemWidth > maxWidth) maxWidth = itemWidth;
       }

       return maxWidth;
   }
}
*/

        protected void AdjustScrollBar()
        {
            if (_selectedItem == 0)
            {
                _scrollbar.value = 1.0f;
                return;
            }

            if (_itemsActive <= _maxItemsShown) return;
            float stepSize = 1.0f / (float)(_itemsActive - (_maxItemsShown));
            float maxDist = stepSize * (_maxItemsShown - 1);

            float itemVal = 1 - (_selectedItem * stepSize);
            float difference = itemVal - _scrollbar.value;

            if (difference >= stepSize / 2) _scrollbar.value = itemVal;
            if (difference <= -maxDist) _scrollbar.value = itemVal + maxDist;
        }
    }
}