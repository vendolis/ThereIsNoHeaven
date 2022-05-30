
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class FlyingTextAnimation : MonoBehaviour
{
    [SerializeField] private Color color_i,color_f;
    [SerializeField] private Vector3 initialOffset,finalOffset;
    [SerializeField] private float fadeDuration;
    
    [SerializeField] private TMPro.TextMeshProUGUI textMesh;
    
    private float fadeStartTime;
    
    

    // Start is called before the first frame update
    void Start()
    {
        fadeStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float progress = (Time.time-fadeStartTime)/fadeDuration;
        if(progress <= 1)
        {
            //lerp factor is from 0 to 1, so we use (FadeExitTime-Time.time)/fadeDuration
            transform.localPosition = Vector3.Lerp(initialOffset, finalOffset, progress);
            textMesh.color = Color.Lerp(color_i, color_f, progress);
        }
        else Destroy(transform.parent.gameObject);
    }
}
