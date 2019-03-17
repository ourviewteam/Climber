using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorOn : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collider2D thisCollider = this.gameObject.GetComponent<Collider2D>();
            thisCollider.isTrigger = false;
        }
    }
}
