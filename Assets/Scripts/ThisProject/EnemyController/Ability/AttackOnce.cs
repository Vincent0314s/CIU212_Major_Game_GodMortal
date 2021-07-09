using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnce : MonoBehaviour
{
    public Abilities abilities;
    public float preAttackTime = 2f;

    Renderer rend;
    public Color preAttackColor;
    public Color attackColor;
    public float damage;

    public bool destroyObjectAfterEffecting;

    Collider coll;

    void OnEnable()
    {
        coll = GetComponent<BoxCollider>();
        coll.enabled = false;

        StartCoroutine(PreAttackTimer());
    }

    IEnumerator PreAttackTimer() {
        rend = GetComponent<Renderer>();
        rend.material.color = preAttackColor;
        yield return new WaitForSeconds(preAttackTime);
        rend.material.color = attackColor;
        coll.enabled = true;
        if (destroyObjectAfterEffecting)
        {
            Destroy(this.gameObject, 1f);
        }
        else {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            switch (abilities) {
                case Abilities.LifeBoss_Attack:
                    other.GetComponent<PlayerValue>().GetHurt(damage, true);
                    break;
                case Abilities.LifeBoss_Binding:
                    other.GetComponent<PlayerMovement>().BeingRoot();
                    break;
            }
        }
    }
}
