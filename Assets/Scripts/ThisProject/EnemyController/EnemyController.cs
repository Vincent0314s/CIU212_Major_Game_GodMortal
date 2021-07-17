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
    public float detectedPlayerRange = 6f;
    [Tooltip("This is for optimization")]
    [Range(0,2)]
    public float detectPlayerBySec = 0.5f;
    public LayerMask playerMask;


    [Space(10)]
    [Header("AI_Reaction")]
    public Vector2 reactionTimeRange = new Vector2(0.5f,2f);

    protected float reactionTimer;
    protected float currentReactionTimer;

    [Space(10)]
    [Header("AI_Movement")]
    public float stoppedDistance = 2.5f;
    public float attackRangeDistance = 5f;
    public float considerDistance = 5f;
    protected Vector3 originalPosition;

    //protected Transform platformToBeconfined;
    //public float platformDetectedDis = 3.5f;
    //public LayerMask platformMask;

    public string firstAttackAnimName = "Attack01";

    [SerializeField]
    public Transform player { get; protected set; }
    protected Vector3 moveVector;

    public Character_GroundMovement cm { get; private set; }
    public CharacterBaseValue cbv { get; private set; }

    public virtual void Start()
    {
        cm = GetComponent<Character_GroundMovement>();
        cbv = GetComponent<CharacterBaseValue>();
        player = null;
        originalPosition = transform.position;
    }

   

    //void DetectConfinedPlatform() {
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit,platformDetectedDis, platformMask)) {
    //        platformToBeconfined = hit.transform;
    //    }
    //}

    public virtual IEnumerator DetectPlayer() {
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

    protected void FacingPlayer()
    {
        if (player.position.x > transform.position.x) {
            cm.FacingRight(true);
        } else if (player.position.x < transform.position.x) {
            cm.FacingRight(false);
        }
    }

    public virtual void Idle_Enter() {
        currentReactionTimer = 0;
        StartCoroutine("DetectPlayer");

        reactionTimer = GetRandomTime(reactionTimeRange);
    }

    public virtual void Idle_Update() { 
    
    }

    public virtual void Move_Enter() {

    }

    public virtual void Move_Upate() { 
    
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
        dir = _dir;
        cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
    }

    public virtual void Move(Vector3 targetPos) {
        //float disbetOriginalPos = Vector3.Distance(transform.position, targetPos);
        //if (disbetOriginalPos > stoppedDistance)
        //{
        //    if (targetPos.x < transform.position.x)
        //    {
        //        Move(Direction.Left);
        //    }
        //    else if (targetPos.x > transform.position.x)
        //    {
        //        Move(Direction.Right);
        //    }
        //}

        if (targetPos.x < transform.position.x)
        {
            Move(Direction.Left);
        }
        else if (targetPos.x > transform.position.x)
        {
            Move(Direction.Right);
        }
    }



    public bool IsCloseToTarget(Vector3 targetPos) {
        float disBetween = Vector3.Distance(targetPos, transform.position);
        return disBetween < stoppedDistance;
    }

    public bool IsInAttackRange(Vector3 targetPos) {
        float disBetween = Vector3.Distance(targetPos, transform.position);
        return disBetween < attackRangeDistance; 
    }

    public bool IsCloseToConsiderRange(Vector3 targetPos) {
        float disBetween = Vector3.Distance(targetPos, transform.position);
        return disBetween < considerDistance;
    }

    public bool IsTargetOutOfRange(Vector3 targetPos)
    {
        float disBetween = Vector3.Distance(targetPos, transform.position);
        return disBetween > detectedPlayerRange;
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = circleColor;
        Handles.DrawWireDisc(transform.position,transform.forward,detectedPlayerRange);
        //GUI.color = Color.white;
        //Handles.Label(transform.position, detectedPlayerRange.ToString("f1"));
        
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, stoppedDistance);

        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, attackRangeDistance);

        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, considerDistance);
    }
#endif

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawLine(transform.position,transform.position + Vector3.down * platformDetectedDis);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlatformCollider")
        {
            StopTracinPlayer();
        }
    }

    public void Dead() {
        transform.parent = null;
        cbv.rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject,5f);
    }

    public void ResetPosition() {
        if (originalPosition != Vector3.zero) { 
            transform.position = originalPosition;
        }
    }
}
