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
        private bool isEbabled;
        private  CallDataScriptableObject _dataSource;
        public void Init(CallDataScriptableObject dataSource)
        {
            _dataSource = dataSource;
            mainImage= button.GetComponent<Image>();
            button.onClick.AddListener(OnButtonClick);

            if( dataSource.pointsToOpen == 0 )
            {
                EnableCall();
            }
            else
            {
                mainImage.sprite = null;
                mainImage.color = Color.gray;
                CallIcon.enabled = false;
                pointsNeedText.text = dataSource.pointsToOpen + " бушек";

                isEbabled = false;
            }

        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if(isEbabled)
            {
                OnCall?.Invoke();
            }
            else
            {
                // To Do check points
                EnableCall();
            }
        }

        private void EnableCall()
        {
            mainImage.sprite = _dataSource.externalImage;
            CallIcon.enabled = true;
            mainImage.color = Color.white;
            pointsNeedText.enabled = false;

            isEbabled = true;
        }
    }
}