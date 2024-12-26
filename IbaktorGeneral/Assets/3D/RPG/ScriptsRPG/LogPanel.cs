using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogPanel : MonoBehaviour
{
    protected static LogPanel current;

    public Text GeneralText;

    void Awake()
    {
        current = this;
    }

    public static void Write(string message)
    {
        current.GeneralText.text = message;
    }

}
