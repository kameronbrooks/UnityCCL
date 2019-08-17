using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CCL.UI
{
    public class CodeField : InputField
    {
        [SerializeField]
        private CodeCompletionPopup _codeCompletionPopup;

        private CCL.CodeHelper _codeHelper;

        private System.Type _contextType;

        [SerializeField]
        private int _lastCursorPos;

        public new int caretPosition
        {
            get
            {
                if (isFocused) return base.caretPosition;
                else return _lastCursorPos;
            }
            set
            {
                base.caretPosition = value;
                _lastCursorPos = value;
            }
        }

        public Type contextType
        {
            get
            {
                return _contextType;
            }

            set
            {
                _contextType = value;
                codeHelper.contextType = _contextType;
            }
        }

        public RectTransform rectTransform
        {
            get
            {
                return (RectTransform)transform;
            }
        }

        private CCL.CodeHelper codeHelper
        {
            get
            {
                if (_codeHelper == null)
                {
                    _codeHelper = new CCL.CodeHelper();
                    _codeHelper.assembly = CCL.Core.Assembly.main;
                }
                return _codeHelper;
            }
        }

        public new void ActivateInputField()
        {
            StartCoroutine(OnFocusNextFrame());
        }

        public Vector2 GetCaretScreenPosition()
        {
            if (isFocused)
            {
                var chars = this.cachedInputTextGenerator.characters;
                if (chars.Count < 1 || chars.Count < caretPosition) return Vector2.zero;
                return chars[caretPosition - 1].cursorPos;
            }
            else
            {
                return Vector2.zero;
            }
        }

        public Vector2 GetCaretScreenPosition(float yOffset = 30)
        {
            if (isFocused)
            {
                var chars = this.cachedInputTextGenerator.characters;
                if (chars.Count < 1 || chars.Count < caretPosition) return Vector2.zero;
                return chars[caretPosition - 1].cursorPos + new Vector2(0, yOffset);
            }
            else
            {
                return Vector2.zero;
            }
        }

        public Vector2 GetCursorPos()
        {
            return Vector2.zero;
        }

        public void OnTextUpdate(string text)
        {
            codeHelper.code = this.text;
            codeHelper.FindVariables(this.text);
            string[] predictions = CCL.UIHelper.PredictCode(this, codeHelper);

            if (predictions == null || predictions.Length < 1)
            {
                _codeCompletionPopup.Close();
            }
            else
            {
                _codeCompletionPopup.Open(predictions, GetCaretScreenPosition(-30));
            }
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (_codeCompletionPopup.isOpen)
            {
                if (
                    Input.GetKeyDown(KeyCode.UpArrow) ||
                    Input.GetKeyDown(KeyCode.DownArrow) ||
                    Input.GetKeyDown(KeyCode.Return) ||
                    Input.GetKeyDown(KeyCode.Tab)
                    )
                {
                    return;
                }
            }

            base.OnUpdateSelected(eventData);

            _lastCursorPos = this.caretPosition;
        }

        protected new void Awake()
        {
            base.Awake();
            onValueChanged.AddListener(OnTextUpdate);
        }

        protected IEnumerator OnFocusNextFrame()
        {
            int pos = _lastCursorPos;
            yield return 0;
            Debug.Log("OnFocus: " + pos);
            this.MoveTextEnd(false);
            if (text.Length >= pos)
            {
                caretPosition = pos;
            }
        }
    }
}