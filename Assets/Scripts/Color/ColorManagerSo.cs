
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorManager", menuName = "Game/Color Manager")]
public class ColorManagerSO:DescriptionBaseSO
{
    [SerializeField] private List<Color> colorPallet;

    public List<Color> GetColorList()
    {
        return colorPallet;
    }

    public Color GetColor(uint index)
    {
        if(colorPallet.Count > index)
            return colorPallet[(int)index];

        return Color.white;
    }
    
}
