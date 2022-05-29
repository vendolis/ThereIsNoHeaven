using Cinemachine;
using UnityEngine;

public class AssignFollowTarget : MonoBehaviour
{
    [Header("Listens On")]
    [SerializeField] private TransformEventChannelSO playerInstanciatedChannel;
    
    private void Awake()
    {
        playerInstanciatedChannel.OnEventRaised += SetFollowTarget;
    }

    private void OnDestroy()
    {
        playerInstanciatedChannel.OnEventRaised -= SetFollowTarget;
    }

    private void SetFollowTarget(Transform arg0)
    {
        GetComponent<CinemachineVirtualCameraBase>().Follow = arg0;
    }
    
}
