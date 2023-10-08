using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public GameObject explosion = null;
    readonly uint explodeTimerSeconds = 1;
    readonly float explosionTimerSeconds = 1.3f;
    private PlayerBehavior pb = null;
    // Start is called before the first frame update
    void Start()
    {
        pb = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();
        // print("Starting BombBehavior");
        StartCoroutine(Explode());
        // print("End of Start()");
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explodeTimerSeconds);
        Destroy(gameObject);
        if(pb)
        {
            pb.notifyBombExploded();
        }
        // Horizontal explosion
        GameObject horExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(horExplosion, explosionTimerSeconds);
        // Vertical explosion
        GameObject vertExplosion = Instantiate(explosion, transform.position, Quaternion.AngleAxis(90, Vector3.forward));
        Destroy(vertExplosion, explosionTimerSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(10f, 2f * Time.deltaTime, 0);
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    // }

    private void OnTriggerExit2D(Collider2D other) {
        GetComponent<Collider2D>().isTrigger = false;
    }
}
