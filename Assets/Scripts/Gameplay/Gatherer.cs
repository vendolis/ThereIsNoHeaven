using UnityEngine;


public class Gatherer : MonoBehaviour
{
    [SerializeField] private LongEventChannelSO _ValuableCollectedEvent;
    [SerializeField] private FlyingValueSetter valueDisplayPrefab;
    
    [SerializeField] private AudioCueEventChannelSO sfxAudioChannel;
    [SerializeField] private AudioCueSO gatherSound;
    [SerializeField] private AudioConfigurationSO audioConfig;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        IValuable valuable = other.GetComponent<IValuable>();
        
        if (valuable != null)
        {
            long _value = valuable.ConsumeValue();
            
            _ValuableCollectedEvent.RaiseEvent(_value);
            
            FlyingValueSetter valueSetter = Instantiate(valueDisplayPrefab, transform.position, Quaternion.identity);
        
            valueSetter.SetText(_value.ToString());

            sfxAudioChannel.RaisePlayEvent(gatherSound, audioConfig);
        }
        
    }
}
