using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataController : MonoBehaviour
{
    [HideInInspector] public static UnityEvent<int> OnPointsCountChanged = new UnityEvent<int>();

    [SerializeField] private DataScriptableObject dataScriptableObject;

    private static DataController instance;



    private void Awake()
    {
        instance = this;
    }

    

    public static void SaveData(int points)
    {
        instance.dataScriptableObject.points = points;
        OnPointsCountChanged?.Invoke(points);
    }

    public static int LoadData()
    {
        return instance.dataScriptableObject.points;
    }


}
