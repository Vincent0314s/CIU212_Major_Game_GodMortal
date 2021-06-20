using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_LifeBoss : EnemyController
{
    [Space]
    [Header("FixedTeleporationPoint")]
    public Transform[] teleportPoints;

    [Space]
    [Header("NewValues")]
    public float IdleTime = 1.5f;
    public Transform platformParent;
    private Transform[] platforms;
    [SerializeField]
    private Transform currentPlayerPlatform;
    private GameObject currentDOTArea;

    [Space]
    [Header("Percentage")]
    public PercentageManager decisionMaking = new PercentageManager();

    public override void Start()
    {
        base.Start();
        platforms = new Transform[platformParent.childCount];
        decisionMaking.Initialization();
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = platformParent.GetChild(i);
        }
    }

    private void Update()
    {
        DeTectPlayerCurrentPlatform();
    }


    public override IEnumerator DetectPlayer()
    {
        while (player == null)
        {
            yield return new WaitForSeconds(IdleTime);
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
        if (player)
        {
            switch (decisionMaking.GetCertainPercentageFromList())
            {
                case "Move":
                    cbv.anim.SetTrigger("Move");
                    break;
                case "Attack":
                    cbv.anim.Play("Attack");
                    break;
                case "Summon":
                    cbv.anim.Play("Summon");
                    break;
                case "Binding":
                    cbv.anim.Play("Binding");
                    break;
            }
        }

    }

    public override void Move_Enter()
    {

    }
    public override void Move_Upate()
    {
    
    }

    void DeTectPlayerCurrentPlatform()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].GetComponent<PlatformToRecordPlayer>().isStaying)
            {
                currentPlayerPlatform = platforms[i];
                break;
            }
        }
    }

    public void SummoonDOTArea()
    {
        currentDOTArea = VisualEffectManager.CreateVisualEffect(VisualEffect.DealthBoss_DotArea, currentPlayerPlatform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
    }

}
