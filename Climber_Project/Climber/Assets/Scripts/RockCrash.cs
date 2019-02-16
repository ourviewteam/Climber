using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCrash : MonoBehaviour
{
    public bool isEnter = false;
    public float currentTime = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            isEnter = true;
        }
    }

    private void Update()
    {
        if (isEnter)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                isEnter = false;
                Destroy(this.gameObject);
            }
        }    
    }
}
