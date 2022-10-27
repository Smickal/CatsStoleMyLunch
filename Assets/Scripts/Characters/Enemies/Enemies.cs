using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;

    public bool isEnemyBos = false;

    [SerializeField] float enemyScale = 1f;

    [Header("Attributes")]
    [SerializeField] public float walkingSpeed;
    [SerializeField] float sandalSpeedTreshold;
    [SerializeField] float attackPower = 1f;
    [SerializeField] float knockBackPower = 5f;
    


    [Header("RayCast Attributes")]
    Vector2 rayDir = Vector2.right;
    [SerializeField] float rayDistanceToGround;
    [SerializeField] float rayDistanceToWall = 1f;
    [SerializeField] float rayDistanceToPlayer = 10f;
    float distanceToPlayer;


    [Header("UI")]
    [SerializeField] Transform tmpText;
    [SerializeField] public TMP_Text behaviourText;
    [SerializeField] Image ExclamationMark;
    [SerializeField] GameObject hpBar;
 
    [Header("LayerMask")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] LayerMask cameraColliderMask;
    [SerializeField] LayerMask attColliderMask;

    [Header("Other Serilizeable")]
    [SerializeField] Transform eye;

    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isRight = true;
    [HideInInspector] public bool isAttacking = false;

    private bool isJumping = false;
    Animator anim;
    EnemyStateManager stateManager;

    [SerializeField] float internalJumpTimer = 0.2f;
    float timer;

    [SerializeField] BoxCollider2D attCollider;
    BoxCollider2D catCol;

    //popUp Container
    [SerializeField] GameObject damagePopUpText;
    [SerializeField] GameObject popUpContainer;
    Rigidbody2D rb;

    //knocback Attributes
    Transform currentKnockbackPoint;
    Transform[] knockbackPlace; 
    [SerializeField] knockBackPoints points;

    HealthScript healthScript;
    bool isHit = false;
    public bool isJumpingIsland = false;
    Vector3 jumpingDirection;



    private void Awake()
    {
        
        player = FindObjectOfType<Player>();

        anim = GetComponent<Animator>();
        healthScript = GetComponent<HealthScript>();
        stateManager = GetComponent<EnemyStateManager>();
        rb = GetComponent<Rigidbody2D>();
        catCol = GetComponent<BoxCollider2D>();

        knockbackPlace = points.knocbackPoints;

        if(!isEnemyBos)
            hpBar.SetActive(false);
    }

    private void Start()
    {      
        HideExclamationMark();
    }
    // Update is called once per frame
    void Update()
    {
       if(!isJumpingIsland)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            Move();
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
            Jump();
        }

        if(RaycastToGround() && isJumping && timer >= internalJumpTimer)
        {
            //Debug.Log("MEEE");
            anim.SetTrigger("CatLanding");
            isJumping = false;
            timer = 0f;
            stateManager.SwitchState(stateManager.GetAttackState(), "SetToAttack");
        }

        if (isJumping) timer += Time.deltaTime;

        RaycastToWall();
        ChangeDirection();
    }


    public void ChangeToJump(bool temp, Vector3 targetPosition)
    {
        isJumpingIsland = temp;
        jumpingDirection = targetPosition;
        anim.SetTrigger("SetToPounce");
    }

    public void Jump()
    {
        transform.position = Vector2.MoveTowards(transform.position, jumpingDirection, walkingSpeed * Time.deltaTime * 2f);
    }

    public void SetToJumpAnimation()
    {
        anim.SetTrigger("SetToPounce");
    }


    public void ChangeDirection()
    {
        if (isRight)
        {
            transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale);
            tmpText.localScale = new Vector3(enemyScale, enemyScale, enemyScale);
            rayDir = Vector2.right;

        }
        if(!isRight)
        {
            transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale);
            tmpText.localScale = new Vector3(-enemyScale, enemyScale, enemyScale);
            rayDir = Vector2.left;

        }
    }

    void Move()
    {
        if(isMoving && isRight)
            transform.Translate(transform.right * walkingSpeed * Time.deltaTime, Space.World);     

        if(isMoving && !isRight)
            transform.Translate(transform.right* -1 * walkingSpeed * Time.deltaTime, Space.World);
    }


    public  bool RaycastToGround()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit =  Physics2D.Raycast(collider.bounds.center,
                                                        Vector2.down,
                                                        collider.bounds.extents.y + rayDistanceToGround,
                                                        groundLayerMask);
        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + rayDistanceToGround), Color.red);
        //Debug.Log(raycastHit.collider);  
        return raycastHit.collider != null;
    }

    public bool RaycastToWall()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, 
                                                        rayDir, 
                                                        collider.bounds.extents.y + rayDistanceToWall,
                                                        wallLayerMask);
        Debug.DrawRay(collider.bounds.center, rayDir * (collider.bounds.extents.y + rayDistanceToWall), Color.green);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    public bool RayCastToPlayer()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D raycastHit = Physics2D.Raycast(eye.transform.position, rayDir, collider.bounds.extents.y + rayDistanceToPlayer, ~cameraColliderMask & ~attColliderMask);
        //Debug.Log(raycastHit.collider);
        Debug.DrawRay(collider.bounds.center + (Vector3.one * 0.2f), rayDir * (collider.bounds.extents.y + rayDistanceToPlayer), Color.grey);
        if (raycastHit.collider)
        {
            distanceToPlayer = Mathf.Abs(raycastHit.collider.transform.position.x - transform.position.x);
            //Debug.Log(distanceToPlayer);
            //Debug.Log(raycastHit.collider.gameObject.name);
            if (raycastHit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public float GetDistanceToPlayer()
    {
        return distanceToPlayer;
    }




    private void OnDrawGizmos()
    {
        Bounds  boundCol = GetComponent<BoxCollider2D>().bounds;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(boundCol.center, boundCol.center + ((Vector3)rayDir * rayDistanceToWall));
        Gizmos.DrawLine(boundCol.center, boundCol.center + (Vector3.down * rayDistanceToGround));
    }


    public void HideExclamationMark()
    {
        ExclamationMark.gameObject.SetActive(false);
    }


    public void EnableExclamationMark()
    {
        ExclamationMark.gameObject.SetActive(true);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
       if (col.gameObject.tag == "Sandal" && col.gameObject.GetComponent<Sandal>().GetCurrentVelocity() >= sandalSpeedTreshold)
       {
            float sandalDamage = col.gameObject.GetComponent<Sandal>().GetDamage();
            if(!isEnemyBos)
                hpBar.SetActive(true);


            PlayerComingFrom(player.GetDir());

            knocBackEffect();

            FindObjectOfType<AudioManager>().PlaySound("SandalHit_SFX");
            FindObjectOfType<AudioManager>().PlaySound("Cat_Damaged_SFX");

            GameObject popUpText = Instantiate(damagePopUpText, transform.position, Quaternion.identity, popUpContainer.transform) as GameObject;
            popUpText.GetComponent<DamagePopUp>().Setup(this.gameObject, sandalDamage);



            anim.SetTrigger("Damaged");

            isHit = true;
            healthScript.TakeDamage(sandalDamage);

            if (healthScript.GetCurrentHP() <= 0)
            {
                if(!isEnemyBos)
                    hpBar.SetActive(false);
                FindObjectOfType<CountEnemy>().AddEnemyCount();
                catCol.enabled = false;
            }


            if(stateManager.GetCurrentState() != stateManager.GetAttackState())
            {
                SetToAttackState();
            }
       }
    }

    public void SetToAttackState()
    {
        stateManager.SwitchState(stateManager.GetAttackState(), "SetToAttack");
    }


    public bool GetIsHit()
    {
        return isHit;
    }

    public void SetCatPounce()
    {
        isJumping = true;
    }

    public void SetCatStopPounce()
    {
        isJumping = false;
    }

    public bool GetCatPounce()
    {
        return isJumping;
    }

    public void Attack()
    {
        FindObjectOfType<AudioManager>().PlaySound("CatAttack_SFX");
        if (player == null) return;
        if (attCollider.IsTouching(player.GetComponent<CapsuleCollider2D>()))
        {
            //Debug.Log("Hit Player");

            player.TakeDamage(attackPower);
            player.EnemyComingFrom(isRight);
            player.knocBackEffect();


        }
            
    }

    public void MoveTowardsPlayer()
    {
        anim.SetTrigger("SetToAttack");
        isMoving = true;
    }
    
    void knocBackEffect()
    {
        Vector2 directionToKnockback = (Vector2)(currentKnockbackPoint.position - transform.position);
        rb.AddForce(directionToKnockback * knockBackPower, ForceMode2D.Impulse);
    }



    public void PlayerComingFrom(bool temp)
    {
        if (temp)
        {
            if (transform.localScale.x > 0f)
                currentKnockbackPoint = knockbackPlace[0];
            else
                currentKnockbackPoint = knockbackPlace[1];
        }
        else
        {
            if (transform.localScale.x < 0f)
                currentKnockbackPoint = knockbackPlace[0];
            else
                currentKnockbackPoint = knockbackPlace[1];
        }

    }

    public void PlayEnemyAttackSFX()
    {
        FindObjectOfType<AudioManager>().PlaySound("CatAttack_SFX");
    }
}
