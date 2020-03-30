using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public bool shouldDamage = true;
    public float damage = 1f;
    public float timeBetweenDamages = 0.4f;

    private void OnTriggerEnter(Collider other)
    {
        Actor act = other.GetComponent<Actor>();
        if (act)
        {
            Damageable dam = other.GetComponent<Damageable>();
            if (dam && act.affiliation == 1)
            {
                StartCoroutine(DamageTilExitCo(dam));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Actor act = other.GetComponent<Actor>();
        if (act)
        {
            Damageable dam = other.GetComponent<Damageable>();
            if (dam && act.affiliation == 1)
            {
                StopAllCoroutines();
            }
        }
    }

    IEnumerator DamageTilExitCo(Damageable dam)
    {
        float t = 0;
        while (shouldDamage)
        {
            t -= Time.deltaTime;

            if (t <= 0)
            {
                dam.InflictDamage(damage, false, gameObject);
                t = timeBetweenDamages;
            }

            yield return new WaitForFixedUpdate();
        }
    }

}


