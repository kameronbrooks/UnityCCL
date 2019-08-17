using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
#endif

namespace CCL
{
    public static class UIHelper
    {
        private static string[] emptyList = new string[0];

        public static Vector2 GetTextCaretPos(InputField field)
        {
            if (!field.isFocused) return Vector2.one;
            if (field.caretPosition < 1) return Vector2.one;

            GameObject ob = GameObject.Find(field.name + " Input Caret");
            Vector2 pos = ((RectTransform)ob.transform).anchoredPosition;
            return pos;
        }

        public static string[] PredictCode(string code, int cursorIndex, CodeHelper codeHelper)
        {
            int i = cursorIndex - 1;
            if (i >= code.Length || i < 0) return emptyList;
            if ("[]()".IndexOf(code[i]) > -1) return emptyList;
            int parenthAccum = 0;
            int bracketAccum = 0;
            while (i >= 0 && (char.IsLetterOrDigit(code[i]) || "[]()._".IndexOf(code[i]) > -1))
            {
                if (code[i] == '(')
                {
                    if (parenthAccum < 1) break;
                    parenthAccum--;
                }
                else if (code[i] == '[')
                {
                    if (bracketAccum < 1) break;
                    bracketAccum--;
                }
                else if (code[i] == ')')
                {
                    parenthAccum++;
                }
                else if (code[i] == ']')
                {
                    if (bracketAccum < 1) break;
                    bracketAccum--;
                }
                i--;
            }
            if (i < -1 || i > code.Length - 1) return emptyList;

            try
            {
                string element = code.Substring(i + 1, cursorIndex - (i + 1));
                codeHelper.code = code;
                return codeHelper.Predict(element);
            }
            catch (System.Exception e)
            {
                return emptyList;
            }
        }

        public static string[] PredictCode(UI.CodeField field, CodeHelper codeHelper)
        {
            if (field.isFocused)
            {
                return PredictCode(field.text, field.caretPosition, codeHelper);
            }
            else
            {
                return emptyList;
            }
        }

        public static void InsertPrediction(UI.CodeField field, string prediction)
        {
            int i = field.caretPosition - 1;
            int cuttoff = 0;
            int caretPosition = field.caretPosition;

            while (i >= 0)
            {
                if (field.text[i] == '.')
                {
                    break;
                }
                if (field.text[i] == prediction[0])
                {
                    cuttoff++;
                    break;
                }
                i--;
                cuttoff++;
            }
            field.text = field.text.Insert(field.caretPosition, prediction.Substring(cuttoff));
            if (!field.isFocused)
            {
                field.Select();
                field.ActivateInputField();
            }
            field.caretPosition = caretPosition + (prediction.Length - cuttoff);
        }

#if UNITY_EDITOR

        public static Vector2 GetTextCaretPos(TextEditor editor)
        {
            return editor.graphicalCursorPos;
        }

        public static string[] PredictCode(TextEditor editor, CodeHelper codeHelper)
        {
            return PredictCode(editor.text, editor.cursorIndex, codeHelper);
        }

#endif
    }
}