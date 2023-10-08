using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.MPE;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject up = null;
    GameObject down = null;
    GameObject right = null;
    GameObject left = null;
    GameObject deadPlayer = null;
    public Sprite []deadPlayerAnimations= {};

    public GameObject bomb = null;

    public float speed = 4f;
    public uint bombsLeft = 5;
    private bool overDroppedBomb = false;

    private bool dead = false;

    void Start()
    {
        up = GameObject.FindWithTag("PlayerUp");
        down = GameObject.FindWithTag("PlayerDown");
        right = GameObject.FindWithTag("PlayerRight");
        left = GameObject.FindWithTag("PlayerLeft");
        deadPlayer = GameObject.FindWithTag("PlayerDead");
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
            return;

        float verticalMovement = Input.GetAxis("Vertical");
        if(verticalMovement > 0)
        {
            up.GetComponent<SpriteRenderer>().enabled = true;
            down.GetComponent<SpriteRenderer>().enabled = false;
            right.GetComponent<SpriteRenderer>().enabled = false;
            left.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if(verticalMovement < 0)
        {
            up.GetComponent<SpriteRenderer>().enabled = false;
            down.GetComponent<SpriteRenderer>().enabled = true;
            right.GetComponent<SpriteRenderer>().enabled = false;
            left.GetComponent<SpriteRenderer>().enabled = false;
        }

        float horizontalMovement = Input.GetAxis("Horizontal");

        if(horizontalMovement > 0)
        {
            up.GetComponent<SpriteRenderer>().enabled = false;
            down.GetComponent<SpriteRenderer>().enabled = false;
            right.GetComponent<SpriteRenderer>().enabled = true;
            left.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if(horizontalMovement < 0)
        {
            up.GetComponent<SpriteRenderer>().enabled = false;
            down.GetComponent<SpriteRenderer>().enabled = false;
            right.GetComponent<SpriteRenderer>().enabled = false;
            left.GetComponent<SpriteRenderer>().enabled = true;
        }

        if(Input.GetKeyDown("space") && canDropBomb())
        {
            print("Dropping bomb here!");
            Instantiate(bomb, transform.position - new Vector3(0, 0.3f, 0), Quaternion.identity);
            bombsLeft--;
        }

        transform.Translate(horizontalMovement * speed * Time.deltaTime, verticalMovement * speed * Time.deltaTime, 0);
    }

    // private void OnCollisionEnter2D(Collision2D other) 
    // {
    //     if(other.gameObject.tag == "Explosion")
    //     {
    //         dead = true;
    //         up.GetComponent<SpriteRenderer>().enabled = false;
    //         down.GetComponent<SpriteRenderer>().enabled = false;
    //         right.GetComponent<SpriteRenderer>().enabled = false;
    //         left.GetComponent<SpriteRenderer>().enabled = false;
    //         deadPlayer.GetComponent<SpriteRenderer>().enabled = true;
    //         print("x.x  player has died");
    //         StartCoroutine(AnimateExplosion());
    //     }
    // }

    
    IEnumerator AnimateExplosion()
    {
        SpriteRenderer deadSprite = deadPlayer.GetComponent<SpriteRenderer>();

        foreach (Sprite deadAnimation in deadPlayerAnimations)
        {
            deadSprite.sprite = deadAnimation;
            yield return new WaitForSeconds(0.3f);
        }
        Time.timeScale = 0;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        string otherTag = other.gameObject.tag;
        if(otherTag == "Bomb")
        {
            overDroppedBomb = true;
        }
        else if(otherTag == "Explosion")
        {

            dead = true;
            up.GetComponent<SpriteRenderer>().enabled = false;
            down.GetComponent<SpriteRenderer>().enabled = false;
            right.GetComponent<SpriteRenderer>().enabled = false;
            left.GetComponent<SpriteRenderer>().enabled = false;
            deadPlayer.GetComponent<SpriteRenderer>().enabled = true;
            print("x.x  player has died");
            StartCoroutine(AnimateExplosion());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Bomb")
        {
            overDroppedBomb = false;
        }
    }

    private bool canDropBomb()
    {
        return bombsLeft > 0 && !overDroppedBomb;
    }

    public void notifyBombExploded()
    {
        bombsLeft++;
    }
}
