using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_DealthBoss : EnemyController
{
    private Character_FlyingMovement cfm;
    private ObstacleDetection od;

    [Space]
    [Header("NewValues")]
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
        cfm = GetComponent<Character_FlyingMovement>();
        od = GetComponent<ObstacleDetection>();
        platforms = new Transform[platformParent.childCount];
        rangeMedium.Initialization();
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = platformParent.GetChild(i);
        }

        player = GameAssetManager.i.currentPlayer.transform;
    }

    private void Update()
    {
        DeTectPlayerCurrentPlatform();
    }

    public override void Move(Vector3 targetPos)
    {
        if (targetPos.x > transform.position.x)
        {
            cfm.FacingRight(true);
        }
        else if (targetPos.x < transform.position.x)
        {
            cfm.FacingRight(false);
        }
        moveVector = targetPos - transform.position;
        moveVector.Normalize();
        cfm.SetVelocity(moveVector);
        cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
    }

    //public override IEnumerator DetectPlayer()
    //{
    //    while (player == null)
    //    {
    //        yield return new WaitForSeconds(IdleTime);
    //        Collider[] colls = Physics.OverlapSphere(transform.position, detectedPlayerRange, playerMask);
    //        if (colls.Length > 0)
    //        {
    //            player = colls[0].transform;
    //        }
    //    }
    //}

    public override void Idle_Enter()
    {
        player = GameAssetManager.i.currentPlayer.transform;
    }

    public override void Idle_Update()
    {
        if (player)
        {
            Move(player.position);
            if (IsInAttackRange(player.position))
            {
                Attack();
                StopTracinPlayer();
            }
            //if (od.isBeingBlocked)
            //{
            //    if (moveVector.y > 0)
            //    {
            //        Move(new Vector3(player.position.x, player.position.y + 5f, 6));
            //    }
            //    else
            //    {
            //        Move(new Vector3(player.position.x, player.position.y, 6));
            //    }
            //}
            //else {
            //    Move(player.position);
            //    if (IsInAttackRange(player.position))
            //    {
            //        Attack();
            //        StopTracinPlayer();
            //    }
            //}
        }
    }

    public override void Move_Enter()
    {
        currentReactionTimer = 0;
        reactionTimer = GetRandomTime(reactionTimeRange);
    }

    public override void Move_Upate()
    {
        if (player)
        {
            //Move(player.position);
            //if (IsInAttackRange(player.position))
            //{
            //    Attack();
            //    StopTracinPlayer();
            //}
            //if (currentReactionTimer < reactionTimer)
            //{
            //    currentReactionTimer += Time.deltaTime;

            //}
            //else {
            //    if (IsCloseToTarget(player.position) && !IsInAttackRange(player.position))
            //    {
            //        if (rangeMedium.GetCertainPercentageFromList() == "Debuff")
            //        {
            //            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            //            if (!pm.isBeingDebuff)
            //            {
            //                cbv.anim.Play("Debuff");
            //            }
            //        }
            //        else
            //        {
            //            if (!currentDOTArea && currentPlayerPlatform)
            //            {
            //                cbv.anim.Play("DOT");
            //            }
            //        }
            //    }
            //    else if (IsCloseToConsiderRange(player.position) && !IsCloseToTarget(player.position))
            //    {
            //        cbv.anim.Play("Telegraphed");
            //    }
            //}


            if (od.isBeingBlocked)
            {
                if (moveVector.y > 0)
                {
                    Move(new Vector3(player.position.x, player.position.y + 5f, 6));
                }
                else
                {
                    Move(new Vector3(player.position.x, player.position.y, 6));
                }
            }
            else
            {
                Move(player.position);
              
                if (currentReactionTimer < reactionTimer)
                {
                    currentReactionTimer += Time.deltaTime;

                }
                else
                {
                    if (IsInAttackRange(player.position))
                    {
                        Attack();
                        StopTracinPlayer();
                    }
                    else if (IsCloseToTarget(player.position) && !IsInAttackRange(player.position))
                    {
                        if (rangeMedium.GetCertainPercentageFromList() == "Debuff")
                        {
                            PlayerMovement pm = player.GetComponent<PlayerMovement>();
                            if (!pm.isBeingDebuff)
                            {
                                cbv.anim.Play("Debuff");
                            }
                        }
                        else
                        {
                            if (!currentDOTArea && currentPlayerPlatform)
                            {
                                cbv.anim.Play("DOT");
                            }
                        }
                    }
                    else if (IsCloseToConsiderRange(player.position) && !IsCloseToTarget(player.position))
                    {
                        cbv.anim.Play("Telegraphed");
                    }
                }
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
        currentDOTArea = VisualEffectManager.CreateVisualEffect(VisualEffect.DeathBoss_DotArea, currentPlayerPlatform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
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
