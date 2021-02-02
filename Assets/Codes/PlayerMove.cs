using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D boxCollider;
    public GameManager gameManager;
    public GameObject warning;
 

    public float maxSpeed;
    public float jumpPower;
    //for double jump 
    public int jumpCount = 2;
    public bool isGround = false;
    public bool isNPC = false;

    //for NPC
    public GameObject npcObject;
    public NpcManager npcManager;
    
    
//
    public bool leftMove = false;
    public bool rightMove  = false; //발판움직임 위한 변수
    bool jumping = false;   //
    bool jumpState = false; //
    bool playerState = false;
    bool sideState = false;
    int sideFlat = 0;
    float sidePower = 0.12f;
    float sideSpeed = 0;
//



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        jumpCount = 0;
       
    }

    void Update()
    {
        
        //jump
        if(isGround){
            if(jumpCount>0){
                if (Input.GetButtonDown("Jump"))
                {
                    if (isNPC)
                    {
                        if (npcManager.isAnswer)    
                            return;
                        npcManager.Quiz(npcObject);
                        if(!npcManager.isQuiz)
                            isNPC = false;

                    }
                    else
                    {
                        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                        SoundManager.instance.PlaySE("jump");
                        anim.SetBool("isJumping", true);
                        jumpCount--;
                    }
                    ///////////////////
                    
                 }
            }

        }//if(isGround)
    


        if (!npcManager.isQuiz)
        {
            //stop speed
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }

            //direction sprite
            if (Input.GetButtonDown("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }

            //if(rigid.velocity.normalized.x ==0) /
            if (Mathf.Abs(rigid.velocity.x) < 0.3)
                anim.SetBool("isRunning", false);
            else
                anim.SetBool("isRunning", true);
        }//if(!npcManaver.isQuiz)

        //////////////
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!npcManager.isQuiz)
        {
            //moving speed
            float h = Input.GetAxisRaw("Horizontal");

            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            //max speed
            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

            if (rigid.velocity.x < maxSpeed * (-1))
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

            //landing platform
            if (rigid.velocity.y < 0)
            {
                Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
                RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));
                if (rayHit.collider != null)
                {
                    if (rayHit.distance < 0.5f)
                        anim.SetBool("isJumping", false);
                }
            }
        }//if (!npcManager.isQuiz)
    }


    private void OnCollisionEnter2D(Collision2D collision)  
    {
        
        if(collision.gameObject.tag=="ground" || collision.gameObject.tag=="falling platform"){
            isGround = true;
            jumpCount = 2;
        }
//
        if(collision.gameObject.tag=="falling platform"){
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y){
                //stepped
                OnStep(collision.transform);
            }
        }
//
        if(collision.gameObject.tag == "obstacle")
        {
            OnDamaged(collision.transform.position);
        }
        else if(collision.gameObject.tag == "enemy")
        {
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y){
                //attack
                OnAttack(collision.transform);
            }
            else
                 OnDamaged(collision.transform.position);
            
        }
    }


    void OnStep(Transform fallingPlatform){
        fallingplatformMove fMove = fallingPlatform.GetComponent<fallingplatformMove>();
        fMove.Stepped();
    }
    
    void OnAttack(Transform enemy){

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();

        SoundManager.instance.PlaySE("kick");
    }
    void OnDamaged(Vector2 targetPos)
    {
        //health down
        gameManager.LifeDown();
        // 레이어 바꾸기
        gameObject.layer = 12;
        
        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x >0 ?1:-1;
        rigid.AddForce(new Vector2(dirc,1)*7, ForceMode2D.Impulse);

        //animation
        anim.SetTrigger("doHit");

        SoundManager.instance.PlaySE("damaged");

        Invoke("OffDamaged", 3);
    }

    void OffDamaged()
    {
        // 레이어 바꾸기
        gameObject.layer = 11;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie(){
        spriteRenderer.color = new Color(1,1,1,0.4f);

        spriteRenderer.flipY = true;

        boxCollider.enabled = false;

        SoundManager.instance.PlayDeathBGM();

        rigid.AddForce(Vector2.up*5, ForceMode2D.Impulse);
    }

    public void VelocityZero(){
        rigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            //point
            gameManager.fruitPoint += 100;

            //sound
            SoundManager.instance.PlaySE("getfruits");

            //과일 없애기
            collision.gameObject.SetActive(false);
        }

        if(collision.gameObject.tag == "npc")
        {
           
            npcObject = collision.gameObject;
            if(!npcObject.GetComponent<ObjData>().isclear)
                isNPC = true;
            
        }

        if(collision.gameObject.tag == "Finish"){
            if (gameManager.quizPoint == 0) //퀴즈 다 풀었을 
            {
                gameManager.NextStage();
                SoundManager.instance.PlaySE("nextstage");
            }
            else        //퀴즈 남아있을 때
            {
                Text text = warning.GetComponent<Text>();
                text.text = string.Format("다음 스테이지로 가려면 퀴즈를 모두 풀어야 해요!");
                SoundManager.instance.PlaySE("damaged");
                warning.SetActive(true);
                Invoke("ActWarning", 3f);
            }

        }

        if(collision.gameObject.tag == "gameclear")
        {
            if (gameManager.quizPoint == 0) //퀴즈 다 풀었을 
            {
                gameManager.GameClear();
            }
            else        //퀴즈 남아있을 때
            {
                Text text = warning.GetComponent<Text>();
                text.text = string.Format("퀴즈가 아직 남아있어요!");
                SoundManager.instance.PlaySE("damaged");
                warning.SetActive(true);
                Invoke("ActWarning", 3f);
            }
        }

        // if(collision.gameObject.tag == "enemy" &&  !collision.isTrigger && rigid.velocity.y < -2f)
        // {
        //     //kill
        //     EnemyMove enemy = collision.gameObject.GetComponent<EnemyMove>();
        //     enemy.Die();

        //     //bounce
        //     Vector2 killVelocity = new Vector2(0, 25f);
        //     rigid.AddForce(killVelocity, ForceMode2D.Impulse);


        // } 
    }

    void ActWarning()
    {
        
        warning.SetActive(false);
    }



}