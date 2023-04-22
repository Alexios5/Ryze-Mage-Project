using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace MoreMountains.CorgiEngine
{
    public class FindEnemy : MonoBehaviour
    {
        public ParticleSystem currentParticle;
        private float SecondsToDeactivate;
        private void OnEnable()
        {
            
            this.gameObject.MMGetComponentNoAlloc<DamageOnTouch>().enabled = false;
            currentParticle.gameObject.SetActive(false);
            SecondsToDeactivate = currentParticle.main.duration;
            StartCoroutine(DeactivateAfterSeconds());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 13 )
            {
                this.gameObject.transform.position = collision.gameObject.transform.position;
                StopAllCoroutines();
                StartCoroutine(ActivateParticle());
               
            }
            
        }
        IEnumerator ActivateParticle()
        {
            yield return new WaitForSeconds(0.2f);
            currentParticle.gameObject.SetActive(true);
            this.gameObject.MMGetComponentNoAlloc<DamageOnTouch>().enabled = true;

        }
        IEnumerator DeactivateAfterSeconds()
        {
            yield return new WaitForSeconds(SecondsToDeactivate);

            this.gameObject.SetActive(false);
        }

    }
}
