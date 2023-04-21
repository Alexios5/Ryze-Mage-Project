using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSpell : MonoBehaviour
{

    public ParticleSystem currentParticleSystem;
    private float SecondsToDeactivate;

    // Start is called before the first frame update
    void OnEnable()
    {
        
        SecondsToDeactivate = currentParticleSystem.main.duration;
        StartCoroutine(DeactivateAfterSeconds());
    }
    IEnumerator DeactivateAfterSeconds()
    {
        yield return new WaitForSeconds(SecondsToDeactivate);
        
        this.gameObject.SetActive(false);
    }
    
}
