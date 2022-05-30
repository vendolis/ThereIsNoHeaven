using UnityEngine;

namespace Gameplay
{
    public class TimedDestroy : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy = 10f;

        private void Start()
        {
            Destroy(gameObject, _timeToDestroy);
        }
    }
}