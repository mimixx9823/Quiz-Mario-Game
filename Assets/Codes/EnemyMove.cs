using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class EnemyMove : MonoBehaviour
//{
//    Rigidbody2D rigid;
//    Animator anim;
//    public int nextMove;
//    SpriteRenderer spriteRenderer;
//    // Start is called before the first frame update
//    void Awake()
//    {
//        rigid = GetComponent<Rigidbody2D>();
//        anim = GetComponent<Animator>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        Invoke("Think",5);
//    }

//    // 1초 50-60실행
//    void FixedUpdate()
//    {
//        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


//        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.3f, rigid.position.y);
//        Debug.DrawRay(frontVec, Vector3.down, new Color(0,1,0));
//        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down,1,  LayerMask.GetMask("Platform"));
//        if(rayHit.collider==null){
//            Turn();
//        }
//    }


//    void Think(){
//        nextMove = Random.Range(-1,2); //ㅊㅚ댓값 랜덤값에서 제외됨
//        float nextThinkTime = Random.Range(2f,5f);
//        Invoke("Think",nextThinkTime);
//        anim.SetInteger("runSpeed", nextMove);
//        if (nextMove != 0)
//            spriteRenderer.flipX  = nextMove ==1;
//    }

//    void Turn()
//    {
//        nextMove *= -1; //방향바꿈
//        spriteRenderer.flipX = nextMove == 1;
//        CancelInvoke();
//        Invoke("Think",3);
//    }
//}


public class EnemyMove : MonoBehaviour
{

    public float movePower = 1f;
    Rigidbody2D rigid;
    Animator anim;
    Vector3 movement;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    int movementFlag = 0;
    bool isDead = false;

    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        //StartCoroutine("ChangeMovement");
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);


        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }

    void FixedUpdate()
    {
        Move();

    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    public void OnDamaged(){
        spriteRenderer.color = new Color(1,1,1,0.4f);

        spriteRenderer.flipY = true;

        boxCollider.enabled = false;

        rigid.AddForce(Vector2.up*5, ForceMode2D.Impulse);

        Invoke("DeActive", 4);
    }

    void DeActive(){
        gameObject.SetActive(false);
    }

    // public void Die()
    // {
    //     StopCoroutine("ChangeMovement");
    //     isDead = true;

    //     SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    //     spriteRenderer.flipY = true;

    //     BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
    //     coll.enabled = false;

    //     Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
    //     Vector2 dieVelocity = new Vector2(0, 30f);
    //     rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

    //     Destroy(gameObject, 5f);
    // }
}

