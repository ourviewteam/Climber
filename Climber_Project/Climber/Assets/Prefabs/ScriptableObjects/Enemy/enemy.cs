using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 10;
    public GameObject Ammo;
    public bool isFlipped = true;

    private void Start()
    {
        
        if (isFlipped)
        {
            transform.Rotate(0, 180, 0);
        }
    }
    public void ShootPlayer()
    {
        Instantiate(Ammo, transform.position, transform.rotation);
    }
    public void KillTheEnemy()
    {
            Debug.Log("WIN, next stage!");
            Destroy(this.gameObject);
    }
}
