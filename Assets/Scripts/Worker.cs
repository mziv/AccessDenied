using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    private GameObject playerGO;
    private Transform player;
    public bool locateTarget = false;
    private Animator anim;

    enum workerState { Idle, Patrol };
    enum AnimState { Idle, Walk, Run };
    private workerState state = workerState.Idle;

    //chase states
    public float attachDistance = 3f;
    public float closesenseDistance = 6f;
    public float chaseDistance = 20f;
    public float workerSpeed = 0.1f;
    public float workerChaseSpeed = 0.2f;
    public float visionAngle = 30f;

    //patrol states
    public int idleTimerRange = 5;
    private float idleTimer = 0f;
    private bool idleDurationProduced = false;
    private int IdleDuration = 0;
    private bool patrolling = false;
    public float patrolDuration = 7f;
    private float patrolTimer = 0f;
    private GameObject currentTarget;
    public GameObject landmark1;
    public GameObject landmark2;
    
    bool playerAlive = true;

    /// <summary>
    float gravity = 20f;
    float verticalSpeed = 0f;
    CharacterController controller;
    private CollisionFlags WorkerCollisionFlags;
    /// </summary>
    
    //Keep track of whether sound effect has played
    bool hasPlayed;

    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.transform;
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        WorkerCollisionFlags = controller.collisionFlags;
        currentTarget = landmark2;
    }


    void Update()
    {
        if (Time.timeScale == 0) return;

        FindPlayer();
        applyGravity();
        Vector3 movement = new Vector3(0, verticalSpeed, 0) + Vector3.zero;
        movement *= Time.deltaTime;
        controller.Move(movement);

        //get player direction relative to the worker and get the angle between player and worker
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        //Debug.DrawRay(transform.position + transform.up * 0.5f, direction, Color.green);
        //if all conditions satisfy the worker will chase the player
        if (PlayerInRange(angle) && CanSeePlayer(direction))
        {
            FollowAndAttack(direction, angle);
        }
        //Patrol
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        //reset sound effect bool
        hasPlayed = false;

        System.Random rnd = new System.Random();

        //while not idling, produce idleDuration
        if (!idleDurationProduced)
        {
            IdleDuration = rnd.Next(0, idleTimerRange);
            idleDurationProduced = true;
            idleTimer = 0;
            state = workerState.Idle;
            //Debug.Log(IdleDuration);
        }

        if (!patrolling)
        {
            patrolTimer = 0;
        }

        switch (state)
        {
            case workerState.Idle:
               //Debug.Log("Idle");
                if (idleDurationProduced)
                {
                    idleTimer += Time.deltaTime;
                    
                    if (idleTimer > IdleDuration)
                    {
                        state = workerState.Patrol;
                        patrolling = true;
                        RandomTarget();
                    }
                }
                setAnimationState(AnimState.Idle);
                break;
            case workerState.Patrol:
                
                WorkerMove();
                patrolTimer += Time.deltaTime;
                
                if(patrolTimer > patrolDuration)
                {
                    patrolling = false;
                    state = workerState.Idle;
                    idleDurationProduced = false;
                    setAnimationState(AnimState.Idle);
                   
                }
                break;
        }
            
    }

    void RandomTarget()
    {
        System.Random rnd = new System.Random();
        int targetNum = rnd.Next(0, 2);
        if (targetNum == 0)
        {
            currentTarget = landmark1;
        }
        else
        {
            currentTarget = landmark2;
        }
    }

    void WorkerMove()
    {
        Vector3 direction = currentTarget.transform.position - this.transform.position;
        direction.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        if(Vector3.Distance(this.transform.position, currentTarget.transform.position) > 1f)
        {
            this.transform.Translate(0, 0, workerSpeed * Time.deltaTime);
            setAnimationState(AnimState.Walk);
        }
        else if(currentTarget == landmark1)
        {
            currentTarget = landmark2;
        }
        else if(currentTarget == landmark2)
        {
            currentTarget = landmark1;
        }
    }

    bool PlayerInRange(float angle)
    {
        return (((Vector3.Distance(player.position, this.transform.position) < chaseDistance && angle < visionAngle) ||
            Vector3.Distance(player.position, this.transform.position) < closesenseDistance) && playerAlive);
    }

    bool CanSeePlayer(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, chaseDistance))
        {
            if (hit.transform == player.transform)
            {
                return true;
            }
        }
        return false;
    }

    void FollowAndAttack(Vector3 direction, float angle)
    {
        //anim.SetBool("isIdle", false);
        //play sound effect if you haven't already
        PlaySound();

        direction.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        //chase
        if (direction.magnitude > attachDistance)
        {
            this.transform.Translate(0, 0, workerChaseSpeed * Time.deltaTime);
            setAnimationState(AnimState.Run);
        }
        //stop to attack
        else
        {
            KillPlayer();
            setAnimationState(AnimState.Idle);
        }
    }

    void PlaySound()
    {
        if (!hasPlayed)
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            hasPlayed = true;
        }
    }


    ////
    void applyGravity()
    {
        if (isGrounded())
        {
            verticalSpeed = 0f;
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
    }

    bool isGrounded()
    {
        return (WorkerCollisionFlags == CollisionFlags.CollidedBelow);
    }

 
    bool attackPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, attachDistance + 0.5f);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.tag == "Player")
            {              
                return true;
            }
        }
        return false;
    }
    
    void KillPlayer()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        Animator anim = playerGO.GetComponent<PlayerControl>().GetComponent<Animator>();
        anim.SetBool("die", true);
        playerGO.GetComponent<PlayerControl>().enabled = false;
        FindObjectOfType<GameManager>().EndGame();
    }

    void setAnimationState(AnimState state)
    {
        switch (state)
        {
            case AnimState.Idle:
                anim.SetBool("idle", true);
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                break;
            case AnimState.Run:
                anim.SetBool("idle", false);
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
                break;
            case AnimState.Walk:
                anim.SetBool("idle", false);
                anim.SetBool("walk", true);
                anim.SetBool("run", false);
                break;
        }
    }

    void FindPlayer()
    {
        //locateTarget set to true everytime player switches bodies so worker can auto update player body it follow
        if (locateTarget)
        {
            playerGO = GameObject.FindGameObjectWithTag("Player");
            player = playerGO.transform;
            locateTarget = false;
        }
    }

}
