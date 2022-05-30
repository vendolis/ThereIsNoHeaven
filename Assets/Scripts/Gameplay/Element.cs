using Unity.Collections;
using UnityEngine;
using System;
using Random = System.Random;

namespace Gameplay
{
    public class Element : MonoBehaviour, IValuable
    {

        [SerializeField] private long _minValue = 1111;
        [SerializeField] private long _maxValue = 9999999;
        
        [SerializeField] [ReadOnly] private long _value;


        
        public long GetMinValue()
        {
            return _minValue;
        }
        
        public long GetMaxValue()
        {
            return _maxValue;
        }
        
        private void Awake()
        {
            Random random = new Random();
            _value = (random.NextLong(_minValue, _maxValue) + random.NextLong(_minValue, _maxValue)) / 2L ;
        }

        public long GetValue()
        {
            return _value;
        }

        public long ConsumeValue()
        {
            GetComponent<Health>()?.Kill();
            return _value;
        }


    }
}