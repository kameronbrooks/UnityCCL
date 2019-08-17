using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class MobileConsole : MonoBehaviour
{
    private static MobileConsole _instance;

    private InputField _text;

    public static MobileConsole instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<MobileConsole>();
            return _instance;
        }
    }

    public InputField text
    {
        get
        {
            if (_text == null) _text = GetComponent<InputField>();
            return _text;
        }
    }

    public static void Clear()
    {
        instance.text.text = "";
    }

    public static void Log(string log)
    {
        instance.text.text += log + "\n";
    }
}