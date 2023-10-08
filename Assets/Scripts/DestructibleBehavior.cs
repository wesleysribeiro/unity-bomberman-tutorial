using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleBehavior : MonoBehaviour
{
    public Sprite[] destructionStages = {};
    public Tile brick = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnCollisionEnter2D(Collision2D other) 
    // {
    //     if(other.gameObject.tag == "Explosion")
    //     {
    //         print("Destroying bbrick");
    //     }
    // }
    // TODO: Animate  explosion
    // IEnumerator AnimateExplosion()
    // {   
    //     // foreach (Sprite destructionStage in destructionStages)
    //     // {
    //     //     brick.sprite = destructionStage;
    //     //     yield return new WaitForSeconds(2f);
    //     // }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Explosion")
        {
            //  TODO: Animate
            // TODO: Destroy only affected blocks 
            Destroy(gameObject);
        }
    }
}
