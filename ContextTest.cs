using System.Collections.Generic;
using UnityEngine;

public class ContextTest : MonoBehaviour
{
    private List<int> _list;

    public List<int> list
    {
        get
        {
            return _list;
        }
    }

    public int versionCode
    {
        get
        {
            return 1;
        }
    }

    public List<int> GetList()
    {
        return new List<int>();
    }

    public void Test()
    {
        Debug.Log("Hello!");
    }
}