using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMove2 : MonoBehaviour
{

    public int moveFlag = 1;
    public float moveSpeed = 20;
    float movePower = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SawMoving");
    }

    private void LateUpdate(){
        Move();
    }
    void Move(){
        Vector3 moveVelocity = Vector3.zero;

        if(this.moveFlag ==1){
            moveVelocity = new Vector3(0,-movePower,0);

        }
        else
        {
            moveVelocity = new Vector3(0,movePower,0);
        }
        transform.position +=moveVelocity *moveSpeed*Time.deltaTime;
    }
    IEnumerator SawMoving(){
        if(moveFlag==1){
            moveFlag=2;
        }
        else{
            moveFlag =1;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine("SawMoving");
    }
    // Update is called once per frame
   
}
