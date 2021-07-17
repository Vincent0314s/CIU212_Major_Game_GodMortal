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
    public float healingPercentage = 0.05f;
    [SerializeField]
    private bool canUseHealing50;
    [SerializeField]
    private bool canUseHealing25;
    [SerializeField]
    private float current50CDTime;
    [SerializeField]
    private float current25CDTime;

    private BossValue_LifeBoss bvl;

    [Space]
    [Header("Percentage")]
    public PercentageManager decisionMaking = new PercentageManager();

    public override void Start()
    {
        base.Start();
        platforms = new Transform[platformParent.childCount];
        decisionMaking.Initialization();
        canUseHealing50 = true;
        canUseHealing25 = true;
        bvl = GetComponent<BossValue_LifeBoss>();
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = platformParent.GetChild(i);
        }
    }

    private void Update()
    {
        DeTectPlayerCurrentPlatform();
        if (!canUseHealing50)
        {
            if (current50CDTime < less50CDTime)
            {
                current50CDTime += Time.deltaTime;
            }
            else
            {
                canUseHealing50 = true;
            }
        }
        else {
            current50CDTime = 0;
        }
        if (!canUseHealing25)
        {
            if (current25CDTime < less25CDTime)
            {
                current25CDTime += Time.deltaTime;
            }
            else
            {
                canUseHealing25 = true;
            }
        }
        else
        {
            current25CDTime = 0;
        }
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
        currentReactionTimer = 0;

        reactionTimer = GetRandomTime(reactionTimeRange);
    }

    public override void Idle_Update()
    {
        if (player)
        {
            FacingPlayer();
            if (CanUse50Healing() || CanUse25Healing())
            {
                cbv.anim.Play("Healing");
            }
            else {
                if (currentReactionTimer < reactionTimer)
                {
                    currentReactionTimer += Time.deltaTime;
                }
                else
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

    public void Healing_Update() {
        if (canUseHealing50) {
            if (cbv.healthSetting.GetHealthPercentage() < 1)
            {
                if (currentInterval < healingInterval)
                {
                    currentInterval += Time.deltaTime;
                }
                else
                {
                    currentInterval = 0;
                    cbv.healthSetting.GetHeal(CalculateRecoverPercentage());
                    bvl.UpdateHealthBar();
                }
            }
            else
            {
                cbv.anim.Play("Idle");
            }
        } else if (canUseHealing25) {
            if (cbv.healthSetting.GetHealthPercentage() < 0.5f)
            {
                if (currentInterval < healingInterval)
                {
                    currentInterval += Time.deltaTime;
                }
                else
                {
                    currentInterval = 0;
                    cbv.healthSetting.GetHeal(CalculateRecoverPercentage());
                    bvl.UpdateHealthBar();
                }
            }
            else
            {
                cbv.anim.Play("Idle");
            }
        }
    
    }

    public void Healing_Exit() {
        if (canUseHealing50)
        {
            canUseHealing50 = false;
        }
        else {
            if (canUseHealing25) {
                canUseHealing25 = false;
            }
        }
    }

    public bool CanUse50Healing() {
        if ((cbv.healthSetting.GetHealthPercentage() <= 0.5f) && canUseHealing50) {
            return true;
        }
        return false;
    }
    public bool CanUse25Healing() {
        if ((cbv.healthSetting.GetHealthPercentage() <= 0.25f && !canUseHealing50 && canUseHealing25)) {
            return true;
        }
        return false;
    }

    private float CalculateRecoverPercentage() {
        return 100 * healingPercentage;
    }

}
