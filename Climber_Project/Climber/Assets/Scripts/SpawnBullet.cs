using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public BasicGamePlay BGP;
    public Transform Player;
    public enemy Enemy;
    public GameObject particleSystem;
    public Vector3 SpeedBulletScale;
    public Vector3 toScale; 

    private void Awake()
    {
        BGP = GameObject.FindGameObjectWithTag("AIM").GetComponent<BasicGamePlay>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.Rotate(0, 0, Random.Range(-4, 4));
        toScale = transform.localScale;
        transform.localScale = Vector3.zero;

        if (Player.localScale.x >= 0)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
        } 
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * bulletSpeed;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("enemy"))
        {
            Enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<enemy>();
            Instantiate(particleSystem, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Enemy.health -= 5;
            BGP.FireState(true);
        }
        else if (collision.gameObject.CompareTag("head"))
        {
            Enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<enemy>();
            Instantiate(particleSystem, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Enemy.health -= 10;
            Debug.Log("This is a HEADSHOT");
            BGP.FireState(true);
        }
        else if ((collision != null) && (!collision.gameObject.CompareTag("gun")) && (!collision.gameObject.CompareTag("block")))
        {
            Destroy(this.gameObject);
            BGP.FireState(false);
        }
    }

    private void FixedUpdate()
    {
        if (transform.localScale.x < toScale.x)
        {
            transform.localScale += SpeedBulletScale * Time.deltaTime;
        }
    }
}
