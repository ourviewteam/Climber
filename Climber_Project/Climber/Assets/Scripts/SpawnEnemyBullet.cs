using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyBullet : MonoBehaviour
{
    public GameObject Player;
    public Vector3 SpeedBulletScale;
    public float bulletSpeed = 15;
    public Vector3 toScale;
   
    void Start()
    {
        toScale = transform.localScale;
        transform.localScale = Vector3.zero;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
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
