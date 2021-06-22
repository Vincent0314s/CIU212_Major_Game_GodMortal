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
    public Transform platformParent;
    private Transform[] platforms;
    [SerializeField]
    private Transform currentPlayerPlatform;
    private GameObject currentAttackArea;
    private GameObject currentBindingArea;
    public GameObject creature;
    private List<GameObject> currentCreatures = new List<GameObject>();

    [Space]
    [Header("Healing")]
    public float less50CDTime = 30f;
    public float less25CDTime = 60f;
    public float healingPercentage = 5f;
    private bool isFullHp;

    private float current50CDTime;
    private float current25CDTime;

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
        currentRevocerTime = 0;
        currentReactionTimer = 0;
        reactionTimer = GetRandomTime(reactionTimeRange);
    }

    public override void Idle_Update()
    {
        if (player)
        {
            Debug.Log(reactionTimer);
            if (currentReactionTimer < reactionTimer)
            {
                currentReactionTimer += Time.deltaTime;
            }
            else {
                if (CanUse50Healing() || CanUse25Healing())
                {
                    cbv.anim.Play("Healing");
                }
                switch (decisionMaking.GetCertainPercentageFromList())
                {
                    case "Move":
                        cbv.anim.SetTrigger("Move");
                        break;
                    case "Attack":
                        cbv.anim.Play("Attack");
                        break;
                    case "Summon":
                        if (currentCreatures.Count == 0)
                        {
                            cbv.anim.Play("Summon");
                        }
                        break;
                    case "Binding":
                        if (!currentBindingArea)
                        {
                            cbv.anim.Play("Binding");
                        }
                        break;
                }
            }
        }
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

    public void NormalAttack()
    {
        currentAttackArea = VisualEffectManager.CreateVisualEffect(VisualEffect.LifeBoss_NormalAttack, currentPlayerPlatform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
    }

    public void SummonBindingArea() { 
        currentBindingArea = VisualEffectManager.CreateVisualEffect(VisualEffect.LifeBoss_BindingArea, currentPlayerPlatform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
    }

    public void SummonCreatures() {
        GameObject o1 = Instantiate(creature, transform.position + new Vector3(3, 4.5f, 0), Quaternion.identity);
        GameObject o2 = Instantiate(creature, transform.position + new Vector3(-3, 4.5f, 0), Quaternion.identity);
        currentCreatures.Add(o1);
        currentCreatures.Add(o2);
    }

    private float healingInterval = 1f;
    private float currentInterval;

    private int recoverTime = 3;
    private int currentRevocerTime;

    public void Healing_Update() {
        if ((currentInterval < healingInterval) && currentRevocerTime < recoverTime)
        {
            currentInterval += Time.deltaTime;
        }
        else
        {
            currentRevocerTime += 1;
            cbv.healthSetting.GetHeal(CalculateRecoverPercentage());
        }
    }

    public bool CanUse50Healing() {
        if ((cbv.healthSetting.GetHealthPercentage() < 0.5f && cbv.healthSetting.GetHealthPercentage() > 0.25f && current50CDTime >= less50CDTime)) {
            return true;
        }
        return false;
    }
    public bool CanUse25Healing() {
        if ((cbv.healthSetting.GetHealthPercentage() < 0.25f && current25CDTime >= less25CDTime)) {
            return true;
        }
        return false;
    }

    private float CalculateRecoverPercentage() {
        return healingPercentage / 100;
    }

}
