using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Muve : MonoBehaviour
{
    public float minX, maxX;
    public float minY, maxY;

    public Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Camera cam = Camera.main;
        float cam_height = 2 * cam.orthographicSize;
        float cam_width = cam_height * cam.aspect;
        minX = -3 + (cam_width / 2);
        maxX = 3 - (cam_width / 2);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.localScale.x > 0) transform.position = new Vector3(Mathf.Clamp(player.position.x + 1.5f, minX, maxX), Mathf.Clamp(player.position.y, minY, maxY), -10);
        else transform.position = new Vector3(Mathf.Clamp(player.position.x-1.5f, minX, maxX), Mathf.Clamp(player.position.y, minY, maxY), -10);
    }
}
