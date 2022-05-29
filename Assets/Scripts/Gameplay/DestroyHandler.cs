using System;
using UnityEngine;

public class DestroyHandler : MonoBehaviour
{

    [SerializeField] private ParticleSystem explosionParticle;
    
    [SerializeField] private AudioCueEventChannelSO sfxAudioChannel;
    [SerializeField] private AudioCueSO explosionSound;
    [SerializeField] private AudioConfigurationSO _audioConfig;

    
    private IDestroyable destroyAble; 
    
    private void Start()
    {
        destroyAble = GetComponent<IDestroyable>();
        if (destroyAble != null)
            destroyAble.OnDie += HandleDestruction;
    }

    private void OnDestroy()
    {
        if (destroyAble != null)
            destroyAble.OnDie -= HandleDestruction;
    }

    void HandleDestruction()
    {
        Instantiate(explosionParticle, transform.position, transform.rotation);
        sfxAudioChannel.RaisePlayEvent(explosionSound, _audioConfig);
    }
}
