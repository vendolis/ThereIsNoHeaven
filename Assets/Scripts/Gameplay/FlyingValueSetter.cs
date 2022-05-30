using TMPro;
using UnityEngine;


public class FlyingValueSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    
    public void SetText(string text)
    {
        textField.text = text;
    }
}
