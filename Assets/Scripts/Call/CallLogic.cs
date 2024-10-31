using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
using TMPro;
using Assets.Scripts.Call;


public class CallLogic : MonoBehaviour
{
    [Header("Image and Video Data")]
    public List<CallDataScriptableObject> callsData;

    [Header("UI Elements")]
    public Transform gridParent;   
    public GameObject modalWindow; 
    public Image internalImage;
    public AudioSource internalAudio;
    public GameObject callPrefab;
    public DataScriptableObject data;
    public Sprite lock_image;
    public TextMeshProUGUI priceTextPrefab;


    private List<CallObject> callObjects = new List<CallObject>();

    //                             3x3

    void Start()
    {
        GenerateGrid();

        Debug.Log(DataController.OnPointsCountChanged);
        DataController.OnPointsCountChanged.AddListener(PointsCountChanged);
    }

    void GenerateGrid()
    {
        int currentPoints = DataController.LoadData();
        for (int i = 0; i < callsData.Count; i++)
        {
            GameObject newImage = Instantiate(callPrefab, gridParent);
            var obj = newImage.GetComponent<CallObject>();
            obj.Init(callsData[i]);

            //                                                 
            int index = i;                               
            obj.OnCall.AddListener(() => OnImageClick(index));

            callObjects.Add(obj);
            
            obj.CheckPoints(currentPoints);
        }
    }

    void OnImageClick(int index,Image image=null, GameObject priceText=null)
    {
        bool isOpen = callsData[index].isOpen;
        if (isOpen)
        {
            Debug.Log(callsData.Count.ToString() + index.ToString());
            internalImage.sprite = callsData[index].internalImage;
            internalAudio.clip = callsData[index].internalAudio;
            modalWindow.SetActive(true);
            internalAudio.Play();
        }
        /*else
        {
            Debug.Log(callsData.Count.ToString() + index.ToString());
            if (data.points >= callsData[index].pointsToOpen)
            {
                data.points -= callsData[index].pointsToOpen;
                callsData[index].isOpen = true;
                Destroy(priceText);
                image.sprite = callsData[index].externalImage;
                image.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
            }
        }*/
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

