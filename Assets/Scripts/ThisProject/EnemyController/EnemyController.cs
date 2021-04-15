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

    [Space(10)]
    [Header("Detect Player")]
    public Color circleColor = Color.white;
    public float detectedPlayerRange = 1f;
    [Tooltip("This is for optimization")]
    [Range(0,2)]
    public float detectPlayerBySec = 0.5f;
    public LayerMask playerMask;


    [Space(10)]
    [Header("AI_Reaction")]
    public Vector2 reactionTimeRange = new Vector2(0.5f,2f);

    private float reactionTimer;
    private float currentReactionTimer;

    [Space(10)]
    [Header("AI_Movement")]
    public float stoppedDistance = 2.5f;
    private Transform platformToBeconfined;
    private Vector3 originalPosition;
    public float platformDetectedDis;
    public LayerMask platformMask;

    public string firstAttackAnimName = "Attack01";

    private Transform player;
    private Transform platform;
    private Vector3 moveVector;

    public CharacterMovement cm { get; private set; }
    public CharacterBaseValue cbv { get; private set; }

    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cbv = GetComponent<CharacterBaseValue>();
        player = null;
        DetectConfinedPlatform();
        originalPosition = transform.position;
    }

    void DetectConfinedPlatform() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,platformDetectedDis, platformMask)) {
            platformToBeconfined = hit.transform;
        }
    }

    IEnumerator DetectPlayer() {
        while (player == null) {
            yield return new WaitForSeconds(detectPlayerBySec);
            Collider[] colls = Physics.OverlapSphere(transform.position, detectedPlayerRange, playerMask);
            if (colls.Length > 0)
            {
                player = colls[0].transform;
                FacingPlayer();
            }
        }
    }

    void FacingPlayer()
    {
        if (player.position.x > transform.position.x) {
            cm.FacingRight(true);
        } else if (player.position.x < transform.position.x) {
            cm.FacingRight(false);
        }
    }

    public void Idle_Enter() {
        currentReactionTimer = 0;
        StartCoroutine("DetectPlayer");

        reactionTimer = GetRandomTime(reactionTimeRange);
    }

    public void Idle_Update() {
        if (player)
        {
            if (currentReactionTimer < reactionTimer)
            {
                currentReactionTimer += Time.deltaTime;
            }
            else
            {
                if (IsCloseToTarget(player.position))
                {
                    Attack();
                }
                else
                {
                    TracingPlayer();
                }
            }
        }
        else {
            if (!IsCloseToTarget(originalPosition)) { 
                Move(originalPosition);
            }
        }
    }

    public void Running() {
        if (player)
        {
            if (IsCloseToTarget(player.position))
            {
                Attack();
            }
        }
        else {
            if (IsCloseToTarget(originalPosition))
            {
                cm.StopMoving();
            }
            else {
                Move(originalPosition);
            }
        }
    }

    public void TracingPlayer() {
        Move(player.position);
    }

    public void StopTracinPlayer() {
        cm.StopMoving();
        player = null;
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
    public void Move(Vector3 targetPos) {
        float disbetOriginalPos = Vector3.Distance(transform.position, targetPos);
        if (disbetOriginalPos > 0.2f)
        {
            if (targetPos.x < transform.position.x)
            {
                Move(Direction.Left);
            }
            else if (targetPos.x > transform.position.x)
            {
                Move(Direction.Right);
            }
        }
    }

    public bool IsCloseToTarget(Vector3 targetPos) {
        float disBetween = Vector3.Distance(targetPos, transform.position);
        return disBetween < stoppedDistance;
    }

    public void Attack() {
        Move(player.position);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position,transform.position + Vector3.down * platformDetectedDis);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlatformCollider") {
            StopTracinPlayer();
        }
    }

    public void DisableCollision() {
        cbv.rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject,5f);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "JumpTrigger") {
    //        if (currentTimeStayInJumpArea < reactToJumpPlatformTimer)
    //        {
    //            currentTimeStayInJumpArea += Time.deltaTime;
    //        }
    //        else {
    //            if (!isInJumpArea) {
    //                JumpEvent = MoveBackAndJump;
    //                JumpEvent?.Invoke();
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "JumpTrigger") {
    //        currentTimeStayInJumpArea = 0;
    //        isInJumpArea = false;
    //        JumpEvent = null;
    //    }
    //}


}
