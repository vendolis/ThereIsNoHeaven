using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroy : MonoBehaviour
{
    public bool OnlyDeactivate;
	
    void OnEnable()
    {
        StartCoroutine("CheckIfAlive");
    }
	
    IEnumerator CheckIfAlive ()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            if(!GetComponent<ParticleSystem>().IsAlive(true))
            {
                if(OnlyDeactivate)
                {
                    this.gameObject.SetActive(false);
                }
                else
                    GameObject.Destroy(this.gameObject);
                break;
            }
        }
    }
}