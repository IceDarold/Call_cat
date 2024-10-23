using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;

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

    // ����� ��� ��������� ������� 3x3
    void Start()
    {
        GenerateGrid();
    }

    // ��������� ����� 3x3 � �������������
    void GenerateGrid()
    {
        for (int i = 0; i < callsData.Count; i++)
        {
            GameObject newImage = Instantiate(callPrefab, gridParent);
            Image imgComponent = newImage.GetComponent<Image>();
            imgComponent.sprite = callsData[i].externalImage; // ���������� ������ �� Scriptable Object

            // ��������� ���������� ������� ������� �� ��������
            int index = i;  // ��������� ���������� ��� ���������
            newImage.GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }
    }

    void OnImageClick(int index)
    {
        internalImage.sprite = callsData[index].internalImage;
        internalAudio.clip = callsData[index].internalAudio;
        modalWindow.SetActive(true); // ���������� ��������� ����
        internalAudio.Play();
    }
}
