using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IValuable))]
public class SpriteRendererObjectByValue : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> sprites;
    private IValuable myIValuable;

    private void Awake()
    {
        myIValuable = GetComponent<IValuable>();
    }

    private void Start()
    {
        
        Instantiate(GetSpriteRendererByValue(), transform.position, Quaternion.identity, transform);
    }

    private SpriteRenderer GetSpriteRendererByValue()
    {
        
        long value = myIValuable.GetValue();
        
        long valueRange = myIValuable.GetMaxValue() - myIValuable.GetMinValue();

        long section = valueRange / sprites.Count;
        int index = (int) (value / section);

        if(index >= sprites.Count)
        {
            index = sprites.Count - 1;
        }
        
        return sprites[index];
    }
}