using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //references
    public float MoveSpeed = 10;
    //public Transform Ball;
    public GameObject Ball;
    public Transform Arms;
    public Transform PosOverHead;
    public Transform PosDribble;
    public Transform Target;
    
    //variables
    private bool InBallInHands = true;
    private bool IsBallFlying = false;
    private float T=0;
    
    void Update()
    {
        Ball = GameObject.FindWithTag("Ball");
        //walk
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + direction);

        // ball in hands
        if (InBallInHands)
        {
            //hold over head
            if (Input.GetKey(KeyCode.Space))
            {
                Ball.transform.position = PosOverHead.position;
                Arms.localEulerAngles = Vector3.right * 180;

                //look towards the target

                transform.LookAt(Target.parent.position);
            }

            //dribbling
           
            else
            {
                Ball.transform.position = PosDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time *5));
                Arms.localEulerAngles = Vector3.right * 0;
            }

             //throw ball
            if (Input.GetKeyUp(KeyCode.Space))
            {
                InBallInHands = false;
                IsBallFlying = true;
                T = 0;
                Destroy(Ball, 2);
            }
        }

        //ball in the air
        if (IsBallFlying)
        {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t01 = T / duration;

            //move to target
            Vector3 A = PosOverHead.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            //move in arc

            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            Ball.transform.position = pos + arc;

            //moment when ball arrives at the target
            if (t01 >= 1)
            {
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!InBallInHands && !IsBallFlying)
        {
            if (Input.GetKey(KeyCode.E))
            {
                InBallInHands = true;
                StartCoroutine(delay());
                
            }
           
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds (0.5f);
        Ball.GetComponent<Rigidbody>().isKinematic = true;
    }
    

}
