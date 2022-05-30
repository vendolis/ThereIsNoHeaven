using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have one long argument.
/// Example: An money change event, where the long is the new money value.
/// </summary>

[CreateAssetMenu(menuName = "Events/Long Event Channel")]
public class LongEventChannelSO : DescriptionBaseSO
{
    public UnityAction<long> OnEventRaised;
	
    public void RaiseEvent(long value)
    {
        OnEventRaised?.Invoke(value);
    }
}