using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove2 : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D boxCollider;


    public int moveFlag = 2;
    public float moveSpeed = 15;
    float movePower = 0.14f;
    // Start is called before the first frame update


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    void Start()
    {
        StartCoroutine("EMove");
    }

    private void LateUpdate()
    {
        Move();
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (this.moveFlag == 1)
        {
            moveVelocity = new Vector3(movePower, 0, 0);
            spriteRenderer.flipX = true;
        }
        else
        {
            moveVelocity = new Vector3(-movePower, 0, 0);
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }
    IEnumerator EMove()
    {
        if (moveFlag == 1)
        {
            moveFlag = 2;
        }
        else
        {
            moveFlag = 1;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine("EMove");
    }

    public void OnDamaged()
    {

        boxCollider.enabled = false;
        anim.SetBool("Ghost_ishit", true);
        //yield new WaitForSeconds(2f);
        boxCollider.enabled = true;
        anim.SetBool("Ghost_ishit", false);
    }
}