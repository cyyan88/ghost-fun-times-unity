﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CompletePlayerController : MonoBehaviour {


   

    //antidote
    private bool immune;

  
  //movement
    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public float maxpos;
    public float minpos;
   
   //candy count
    public float candyCount;

    private float candyCollected;


    //dialogue
    public Text dialogue;
    public string dialogueOne;
    public string dialogueTwo;
    public string dialogueThree;
    public string dialogueFour;

    private int dialogueNum;

    private int nextUpdate = 1;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
        candyCollected = 0;
        immune = false;
        dialogueNum = 0;

    }

    void Update()
    {
        if(Time.time>=nextUpdate)
        {
            
            if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("right") || Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("left"))
        {
            dialogueNum+=1;   
            switch (dialogueNum)
            {
            case 1:
            dialogue.text = dialogueOne;
            break;
            case 2:
            dialogue.text = dialogueTwo;
            break;
            case 3:
            dialogue.text = dialogueThree;
            break;
            case 4:
            dialogue.text = dialogueFour;
            break;
            }         
        }
        
        nextUpdate=Mathf.FloorToInt(Time.time)+2;

        }

        

    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        
        //Store the current horizontal input in the float moveHorizontal.
         float moveHorizontal = Input.GetAxis ("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis ("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);


        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce (movement * speed);

    


        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minpos, maxpos);
        pos.y = Mathf.Clamp(pos.y, minpos, maxpos);
        transform.position = pos;

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bad Candy")){
            
        Destroy(other.gameObject); 

           if (!immune)
           {
                Destroy(gameObject);
                SceneManager.LoadScene(7);
           }
           else
               immune = false;
           
        }
        if(other.gameObject.CompareTag("Candy")){
            Destroy(other.gameObject); 
            candyCollected += 1;
        }
        if (candyCollected == candyCount){
            
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

        }
        if(other.gameObject.CompareTag("Immune")){
            Destroy(other.gameObject); 
            immune = true;
        }
    }


}
