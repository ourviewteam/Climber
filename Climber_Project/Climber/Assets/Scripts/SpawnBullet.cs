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
    private bool headshot = false;

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
        if (collision.tag == "enemy")
        {
           // BGP.RunAudio(1);
            Instantiate(particleSystem, transform.position, transform.rotation);
            BGP.Enemy.health -= 5;
            BGP.FireState(true);
            Destroy(this.gameObject);
        }
        //else if (collision.tag == "head")
        //{
        //    headshot = true;
        //    BGP.RunAudio(1);
        //    Instantiate(particleSystem, transform.position, transform.rotation);
        //    BGP.Enemy.health -= 10;
        //    BGP.FireState(true);
        //    Destroy(this.gameObject);
        //}
        else if (collision.tag == "block")
        {
           // BGP.RunAudio(2);
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
