using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_DealthBoss : EnemyController
{
    private EnemyMovement_Flying ef;
    public Transform platformParent;
    private Transform[] platforms;
    private Transform currentPlayerPlatform;
    private GameObject currentDOTArea;

    [Space]
    [Header("Percentage")]
    public PercentageManager rangeMedium = new PercentageManager();
    public override void Start()
    {
        base.Start();
        ef = GetComponent<EnemyMovement_Flying>();
        platforms = new Transform[platformParent.childCount];
        rangeMedium.Initialization();
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = platformParent.GetChild(i);
        }
    }

    private void Update()
    {
        DeTectPlayerCurrentPlatform();
    }

    public override void Move(Vector3 targetPos)
    {

        if (player.position.x > transform.position.x)
        {
            ef.FacingRight(true);
        }
        else if (player.position.x < transform.position.x)
        {
            ef.FacingRight(false);
        }
        moveVector = player.position - transform.position;
        moveVector.Normalize();
        ef.SetVelocity(moveVector);
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
        StartCoroutine("DetectPlayer");
    }

    public override void Idle_Update()
    {
        if (player) {
            Move(player.position);
            if (IsInAttackRange(player.position))
            {
                Attack();
                StopTracinPlayer();
            }
        }
      
    }

    public override void Move_Enter()
    {
        
    }
    public override void Move_Upate()
    {
        if (player)
        {
            Move(player.position);
            if (IsInAttackRange(player.position)) {
                Attack();
                StopTracinPlayer();
            } else if (IsCloseToTarget(player.position) && !IsInAttackRange(player.position))
            {
                if (rangeMedium.GetCertainPercentageFromList() == "Debuff")
                {
                    PlayerMovement pm = player.GetComponent<PlayerMovement>();
                    if (!pm.isBeingDebuff)
                    {
                        cbv.anim.Play("Debuff");
                    }
                }
                else {
                    if (!currentDOTArea && currentPlayerPlatform)
                    {
                        cbv.anim.Play("DOT");
                    }
                }
            } else if (IsCloseToConsiderRange(player.position) && !IsCloseToTarget(player.position)) {
                cbv.anim.Play("Telegraphed");
            }
        }
       
    }

    void DeTectPlayerCurrentPlatform() {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].GetComponent<PlatformToRecordPlayer>().isStaying) {
                currentPlayerPlatform = platforms[i];
                break;
            }
        }
    }

    public void SummoonDOTArea() {
        currentDOTArea = VisualEffectManager.CreateVisualEffect(VisualEffect.DealthBoss_DotArea, currentPlayerPlatform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
    }

    public void BreakLinkofPlatform() {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].GetComponent<HingeJoint>()) {
                platforms[i].GetComponent<HingeJoint>().breakForce = 0;
                platforms[i].GetComponent<HingeJoint>().breakTorque = 0;
            }
        }
    }
}
