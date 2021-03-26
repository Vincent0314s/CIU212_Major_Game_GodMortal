using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class EnemyController : MonoBehaviour
{
    public enum Direction { 
        Right,
        Left
    }

    private Direction dir;
    public EnemySequence enemySequence;

    [Space(10)]
    [Header("Detect Player")]
    public Color circleColor = Color.white;
    public float detectedPlayerRange = 1f;
    [Tooltip("This is for optimization")]
    [Range(0,2)]
    public float detectPlayerBySec = 0.5f;
    public LayerMask playerMask;

    [Space(10)]
    [Header("AI_DetectPlatform")]
    public float reactToJumpPlatformTimer = 1f;
    private float currentTimeStayInJumpArea;
    public bool isInJumpArea;
    public Action JumpEvent;


    [Space(10)]
    [Header("AI_Reaction")]
    public Vector2 firstTimeReactionTimeRange = new Vector2(1.5f,3f);
    public Vector2 reactionTimeRange = new Vector2(0.5f,2f);

    private float reactionTimer;
    private float currentReactionTimer;

    [Space(10)]
    [Header("AI_Movement")]
    public float stoppedDistance = 2.5f;

    public string firstAttackAnimName = "Attack01";

    private Transform player;
    [SerializeField]
    private Transform platform;
    private Vector3 moveVector;

    public CharacterMovement cm { get; private set; }
    public CharacterBaseValue cbv { get; private set; }

    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cbv = GetComponent<CharacterBaseValue>();
        player = null;
        StartCoroutine("DetectPlayer");
    }

    IEnumerator DetectPlayer() {
        while (player == null) {
            yield return new WaitForSeconds(detectPlayerBySec);
            Collider[] colls = Physics.OverlapSphere(transform.position, detectedPlayerRange, playerMask);
            if (colls.Length > 0)
            {
                player = colls[0].transform;
                cm.FacingRight(false);
            }
        }
    }

    public void Idle_Enter() {
        currentReactionTimer = 0;

        if (player == null)
        {
            reactionTimer = GetRandomTime(firstTimeReactionTimeRange);
        }
        else {
            reactionTimer = GetRandomTime(reactionTimeRange);
        }
    }

    public void Idle_Update() {
        if (player)
        {
            if (currentReactionTimer < reactionTimer)
            {
                currentReactionTimer += Time.deltaTime;
            }
            else {
                if (IsCloseToPlayer())
                {
                    Attack();
                }
                else
                {
                    TracingPlayer();
                }
            }
        }
    }

    public void Running() {
        if (IsCloseToPlayer())
        {
            Attack();
        }
        else
        {
            if (!isInJumpArea)
            {
                TracingPlayer();
            }
            else { 
                
            }
        }
    }

    public void TracingPlayer() {
        if (player.position.x < transform.position.x)
        {
            Move(Direction.Left);
        }
        else if (player.position.x > transform.position.x)
        {
            Move(Direction.Right);
        }
    }

    IEnumerator MoveBack() {
        if (player.position.x < transform.position.x)
        {
            Move(Direction.Right);
        }
        else if (player.position.x > transform.position.x)
        {
            Move(Direction.Left);
        }
        yield return new WaitForSeconds(0.15f);
        cm.Jump();
    }

    private void MoveBackAndJump() {
        isInJumpArea = true;
        StartCoroutine("MoveBack");
        
    }


    public void Move(Direction _dir) {
        switch (_dir)
        {
            case Direction.Right:
                moveVector = Vector3.right;
                cm.SetVelocity(moveVector);
                break;
            case Direction.Left:
                moveVector = Vector3.left;
                cm.SetVelocity(moveVector);
                break;
        }
        cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
    }

    public bool IsCloseToPlayer() {
        float disBetween = Vector3.Distance(player.position, transform.position);
        return disBetween < stoppedDistance;
    }

    public void Attack() {
        cbv.anim.Play(firstAttackAnimName);
    }

    public float GetRandomTime(float _min, float _max) {
        return UnityEngine.Random.Range(_min,_max);
    }

    public float GetRandomTime(Vector2 _range)
    {
        return UnityEngine.Random.Range(_range.x, _range.y);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = circleColor;
        Handles.DrawWireDisc(transform.position,transform.forward,detectedPlayerRange);
        GUI.color = Color.white;
        Handles.Label(transform.position, detectedPlayerRange.ToString("f1"));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JumpTrigger") {
            JumpEvent = cm.Jump;
            JumpEvent?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "JumpTrigger") {
            if (currentTimeStayInJumpArea < reactToJumpPlatformTimer)
            {
                currentTimeStayInJumpArea += Time.deltaTime;
            }
            else {
                if (!isInJumpArea) {
                    JumpEvent = MoveBackAndJump;
                    JumpEvent?.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "JumpTrigger") {
            currentTimeStayInJumpArea = 0;
            isInJumpArea = false;
            JumpEvent = null;
        }
    }
}
