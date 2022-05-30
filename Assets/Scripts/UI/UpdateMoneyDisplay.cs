using System;
using TMPro;
using UnityEngine;

    public class UpdateMoneyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI debtText;
        
        [Header("Listenig to")]
        [SerializeField] private LongEventChannelSO debtChanged;

        private void Start()
        {
            debtChanged.OnEventRaised += UpdateMoneyText;
        }

        private void UpdateMoneyText(long arg0)
        {
            debtText.text = arg0.ToString();
        }
    }
