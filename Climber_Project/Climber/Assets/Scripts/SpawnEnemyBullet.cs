using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyBullet : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 20f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Debug.Log("YOU Are dead!");
        }
    }
}
