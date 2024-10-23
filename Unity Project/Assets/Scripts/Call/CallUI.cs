using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallUI : MonoBehaviour
{
    [SerializeField] GameObject CallWindow;
    [SerializeField] GameObject MainWindow;

    public void Reset()
    {
        CallWindow.SetActive(false);
        MainWindow.SetActive(true);
    }
}
