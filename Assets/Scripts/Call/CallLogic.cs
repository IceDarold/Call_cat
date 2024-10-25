using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
using Assets.Scripts.Call;

public class CallLogic : MonoBehaviour
{
    [Header("Image and Video Data")]
    public List<CallDataScriptableObject> callsData;  // ������ �� Scriptable Object

    [Header("UI Elements")]
    public Transform gridParent;   // ������������ ������ ��� �����
    public GameObject modalWindow; // ���� ������
    public Image internalImage; // �������� ������
    public AudioSource internalAudio; // �������� ������
    public GameObject callPrefab;

    private List<CallObject> callObjects = new List<CallObject>();

    // ����� ��� ��������� ������� 3x3
    void Start()
    {
        GenerateGrid();

        Debug.Log(DataController.OnPointsCountChanged);
        DataController.OnPointsCountChanged.AddListener(PointsCountChanged);
    }

    // ��������� ����� 3x3 � �������������
    void GenerateGrid()
    {
        int currentPoints = DataController.LoadData();
        for (int i = 0; i < callsData.Count; i++)
        {
            GameObject newImage = Instantiate(callPrefab, gridParent);
            var obj = newImage.GetComponent<CallObject>();
            obj.Init(callsData[i]);

            // ��������� ���������� ������� ������� �� ��������
            int index = i;  // ��������� ���������� ��� ���������
            obj.OnCall.AddListener(() => OnImageClick(index));

            callObjects.Add(obj);
            
            obj.CheckPoints(currentPoints);
        }
    }

    void OnImageClick(int index)
    {
        internalImage.sprite = callsData[index].internalImage;
        internalAudio.clip = callsData[index].internalAudio;
        modalWindow.SetActive(true); // ���������� ��������� ����
        internalAudio.Play();
    }

    private void PointsCountChanged(int points)
    {
        foreach (var obj in callObjects)
        {
            obj.CheckPoints(points);
        }
    }


    private void OnDestroy()
    {
        foreach (var obj in callObjects)
        {
            obj.OnCall.RemoveAllListeners();
        }

        DataController.OnPointsCountChanged.RemoveListener(PointsCountChanged);
    }
}
