using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Flying : EnemyController
{

    public float bouncingTime = 2.5f;
    private float currentBouncingTime;
    private Character_FlyingMovement cfm;

    public override void Start()
    {
        base.Start();
        cfm = GetComponent<Character_FlyingMovement>();
    }

    Vector3 RandomPatrolPoint() {
        float y = considerDistance - 2.5f;
        return new Vector3(Random.Range(originalPosition.x - considerDistance, originalPosition.x + considerDistance),
                            Random.Range(originalPosition.y - y, originalPosition.y + y),
                            10.5f);
    }


    void LookatTarget(Vector3 targetPos) {
        Vector3 diretion = targetPos - transform.position;
        float angle = Mathf.Atan2(diretion.y,diretion.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,angle);
        //Quaternion angleAxis = Quaternion.AngleAxis(angle,Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation,angleAxis, 20 * Time.deltaTime);
    }

    public override void Move(Vector3 targetPos)
    {
        moveVector = transform.right;
        cfm.SetVelocity(moveVector);
        cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
    }

    public override IEnumerator DetectPlayer()
    {
        while (player == null)
        {
            yield return new WaitForSeconds(1.5f);
            Collider[] colls = Physics.OverlapSphere(transform.position, detectedPlayerRange, playerMask);
            if (colls.Length > 0)
            {
                player = colls[0].transform;
            }
        }
    }

    public override void Idle_Enter()
    {
      
      
    }

    public override void Idle_Update()
    {
        if (player)
        {
            LookatTarget(player.position);
            Move(player.position);
            currentBouncingTime = 0;
        }
        else
        {
            LookatTarget(RandomPatrolPoint());
            Move(RandomPatrolPoint());
        }
    }

    public override void Move_Enter()
    {
        StartCoroutine("DetectPlayer");
    }
    public override void Move_Upate()
    {
        if (player)
        {
            LookatTarget(player.position);
            Move(player.position);
            currentBouncingTime = 0;
            if (IsInAttackRange(player.position))
            {
                cfm.SetSpeed(cfm.flying_AttackSpeed);
            }
            else
            {
                cfm.SetSpeed(cfm.flying_SprintSpeed);
            }

        }
        else {
            cfm.SetSpeed(cfm.MoveSpeed);
            if (currentBouncingTime < bouncingTime)
            {
                currentBouncingTime += Time.deltaTime;
            }
            else
            {
                LookatTarget(RandomPatrolPoint());
                Move(RandomPatrolPoint());
                currentBouncingTime = 0;
            }
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<CharacterBaseValue>().GetHurt(cbv.GetDamageAmountFromAttackType(AttackType.Light),true);
            StopTracinPlayer();
        }
        if (collision.gameObject.layer == 8) {
            StopTracinPlayer();
        }
    }
}
