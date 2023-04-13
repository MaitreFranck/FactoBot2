using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public new Transform camera;
    private bool canJump = true;
    public PowerBarManager powerBar;
    public float maxJump = 5;
    public float currentJump;
    private bool isWalking = false;
    private bool canPush = true;
    private bool onTapis = false;
    private bool goingLeft;
    private float t;
    private float speed = 10000;
    private float lastMove;
    

    void Start()
    {
        currentJump = maxJump;
        powerBar.UpdatePowerBar();
    }


    void Update()
    {
        //player.AddForce(((Input.GetAxis("Horizontal") * -1) * 0.9f), 0, 0);
        t += Time.deltaTime * speed;

        if (Input.GetAxisRaw("Horizontal") > 0 && isWalking == false)
        {
            if (CheckMove(Vector2.right) == true)
            {
                //player.gameObject.transform.position = new Vector3(player.position.x + 1 * Time.deltaTime * 50, player.position.y, player.position.z);

                transform.position = Vector3.Lerp(player.gameObject.transform.position, new Vector3(player.position.x + 1, player.position.y, player.position.z), t);
                isWalking = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && isWalking == false)
        {

            transform.position = Vector3.Lerp(player.gameObject.transform.position, new Vector3(player.position.x - 1, player.position.y, player.position.z), t);
            isWalking = true;


        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            isWalking = false;
        }



        if (Input.GetButtonDown("Action") && canPush)
        {
            foreach (var p in GameManager.Instance.pistons)
            {
                Debug.Log(p);
                GameObject bras = p.transform.GetChild(0).gameObject;
                Vector3 posBras = bras.transform.position;
                bras.transform.position = new Vector3(posBras.x + 1, posBras.y, posBras.z);

            }
            canPush = false;
        }

        if (Input.GetButtonUp("Action"))
        {
            canPush = true;
        }


        if (onTapis)
        {
            //player.gameObject.transform.position = new Vector3(player.position.x + 1 * Time.deltaTime * 50, player.position.y, player.position.z);
            if (Time.time - lastMove > 1f)
            {
                lastMove = Time.time;
                if (goingLeft)
                {
                    transform.position = Vector3.Lerp(player.gameObject.transform.position, new Vector3(player.position.x + 1, player.position.y, player.position.z), t);
                }
                else
                {
                    transform.position = Vector3.Lerp(player.gameObject.transform.position, new Vector3(player.position.x - 1, player.position.y, player.position.z), t);
                }                
            }
        }



        //player.gameObject.transform.Translate(movement );
        //player.MovePosition(movement * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && canJump && currentJump > 0)
        {
            player.AddForce(0, 750, 0);
            currentJump = currentJump - 1;
            canJump = false;
            powerBar.UpdatePowerBar();
        }
        //Add "game over" screen
    }

    private bool CheckMove(Vector2 direction)
    {
        //RaycastHit2D hit = Physics2D.Raycast(player.position, direction, 2f);
        //RaycastHit hit2 = Physics2D.Raycast(player.position, Vector3.right, 2f);
        //Debug.Log(hit.collider);
        //Debug.DrawRay(player.position, direction, Color.blue);
        //if (hit.collider != null)
        //{
        //return false;
        //}
        return true;
    }



    private void OnCollisionEnter(Collision collision)

    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }

        if (collision.gameObject.tag == "TapisRoulant")
        {
            canJump = true;
            onTapis = true;
            if (collision.gameObject.GetComponent<TapisRegisterer>().left)
            {
                goingLeft = true;
            }
            else
            {
                goingLeft = false;
            }

        }

        if (collision.gameObject.tag == "Wall")
        {
            player.velocity = Vector3.zero;
        }
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(player.gameObject);
        }
        if (collision.gameObject.tag == "Battery")
        {
            Destroy(collision.gameObject);
            currentJump = maxJump;
            powerBar.UpdatePowerBar();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        onTapis = false;
    }

}
