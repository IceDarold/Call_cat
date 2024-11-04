using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Call
{
    public class CallObject : MonoBehaviour
    {
        [HideInInspector]public UnityEvent OnCall;

        [SerializeField] private Button button;
        [SerializeField] private Image CallIcon;
        [SerializeField] private TextMeshProUGUI pointsNeedText;
        private Image mainImage;
        private bool isEnabled;
        private  CallDataScriptableObject _dataSource;
        public void Init(CallDataScriptableObject dataSource)
        {
            _dataSource = dataSource;
            mainImage= button.GetComponent<Image>();
            button.onClick.AddListener(OnButtonClick);

            if( dataSource.pointsToOpen == 0  || dataSource.isOpen)
            {
                EnableCall();
            }
            else
            {
                mainImage.sprite = null;
                mainImage.color = Color.gray;
                CallIcon.enabled = false;
                pointsNeedText.text = dataSource.pointsToOpen + " бушек";
                pointsNeedText.enabled = true;
                pointsNeedText.gameObject.SetActive(true);
                isEnabled = false;
            }

        }

        public void CheckPoints(int points)
        {
            button.interactable = points >= _dataSource.pointsToOpen || isEnabled;
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if(isEnabled)
            {
                OnCall?.Invoke();
            }
            else
            {
                int points = DataController.LoadData();
                if( points >= _dataSource.pointsToOpen)
                {
                    EnableCall();
                    DataController.SaveData(points - _dataSource.pointsToOpen);
                    
                }
                
            }
        }

        private void EnableCall()
        {
            mainImage.sprite = _dataSource.externalImage;
            CallIcon.enabled = true;
            mainImage.color = Color.white;
            pointsNeedText.enabled = false;
            _dataSource.isOpen = true;
            isEnabled = true;
        }
    }
}