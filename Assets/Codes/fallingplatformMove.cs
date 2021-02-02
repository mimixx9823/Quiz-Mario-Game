using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingplatformMove : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator  anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider_plat;
    


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider_plat = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//
    public void Stepped(){
        boxCollider_plat.enabled = false;
        rigid.AddForce(Vector2.down*5, ForceMode2D.Impulse);

        Invoke("DeActive", 3);
        
    }
//
    void DeActive(){
        gameObject.SetActive(false);
    }
}
