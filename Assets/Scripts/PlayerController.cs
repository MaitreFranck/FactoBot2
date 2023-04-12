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

    void Start()
    {
        currentJump = maxJump;
        powerBar.UpdatePowerBar();
    }

    
    void Update()
    {
        //player.AddForce(((Input.GetAxis("Horizontal") * -1) * 0.9f), 0, 0);

        if (Input.GetAxisRaw("Horizontal") > 0 && isWalking == false)
        {

            player.gameObject.transform.position = new Vector3(player.position.x + 1, player.position.y, player.position.z);
            isWalking = true;

        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && isWalking == false)
        {

            player.gameObject.transform.position = new Vector3(player.position.x - 1, player.position.y, player.position.z);
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
            }
            canPush = false;
        }

        if (Input.GetButtonUp("Action"))
        {
            canPush = true;
        }






        //player.gameObject.transform.Translate(movement );
        //player.MovePosition(movement * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && canJump && currentJump > 0)
        {
            player.AddForce(0, 650, 0);
            currentJump = currentJump - 1;
            canJump = false;
            powerBar.UpdatePowerBar();
        }
        //Add "game over" screen
    }

    private void OnCollisionEnter(Collision collision)

    {
        Debug.Log("Collision");
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
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
    private void OnTriggerEnter(Collider other)
    {

    }
}
