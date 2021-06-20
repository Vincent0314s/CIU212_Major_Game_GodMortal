using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    PlayerController pc;
    public void SetPlayerController(PlayerController _pc) {
        pc = _pc;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<CharacterBaseValue>().GetHurt(pc.pv.GetDamageAmountFromAttackType(AttackType.Projectile),true);
            Destroy(this.gameObject);
        }
    }
}
