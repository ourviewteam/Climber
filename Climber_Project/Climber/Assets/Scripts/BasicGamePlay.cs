using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGamePlay : MonoBehaviour
{
    [Header("Range variables")]
    const float max_Range = 0.50f;
    const float min_Range = -0.15f;

    [Header("Speed variables")]
    public float verticalImpulse = 5f;
    public float muvementSpeed = 10f;
    public float speed_ToAim = 200f;

    [Header("Other")]
    public Rigidbody2D player_Rigidbody2D;
    public GameObject AimLine;
    public bool PlayerShoted = false;
    public bool changeDirection = false;
    public bool IsFacingRight = true;

    RaycastHit2D hit;

    private void Start()
    {
        player_Rigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Debug.Log(transform.rotation);

        if (Input.GetMouseButtonDown(0))
        {
            PlayerShoted = CheckTheAim();
            

        }

        // run script to animate the aim
        if (!PlayerShoted)
        {
            AimLine.SetActive(true);
            
            if (changeDirection)
            {
                transform.Rotate(0, 0, min_Range * speed_ToAim * 3 * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, max_Range * speed_ToAim * Time.deltaTime);
                
            }

            if (transform.rotation.z >= max_Range)
            {
                changeDirection = true;
            }
            else if (transform.rotation.z <= min_Range)
            {
                changeDirection = false;
            }
        }
        else
        {
            Transform playerPosition = player_Rigidbody2D.transform;
            AimLine.SetActive(false);

            if (((playerPosition.position.x - hit.collider.transform.position.x) <= 0) && (IsFacingRight))
            {
                player_Rigidbody2D.transform.Translate(transform.right * muvementSpeed * Time.deltaTime);
                //
            }
            else if (((playerPosition.position.x - hit.collider.transform.position.x) >= 0) && (!IsFacingRight))
            {
                player_Rigidbody2D.transform.Translate(-transform.right * muvementSpeed * Time.deltaTime);
                //transform.rotation = new Quaternion(0, 0, -0.15f, -1);
            }
            else
            {
                playerPosition.localScale = new Vector3(-playerPosition.localScale.x, playerPosition.localScale.y, playerPosition.localScale.z);
                IsFacingRight = !IsFacingRight;
                if (IsFacingRight)
                {
                    transform.rotation = new Quaternion(0, 0, -0.15f, 1);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, -0.15f, -1);
                }
                PlayerShoted = false;
            }
        }
    }

    public bool CheckTheAim ()
    {
        if (IsFacingRight)
        {
            hit = Physics2D.Raycast(transform.position, transform.right, 1000f);
            Debug.DrawLine(transform.position, hit.point, Color.blue, 0);
        } else
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, 1000f);
            Debug.DrawLine(transform.position, hit.point, Color.blue, 0);
        }

        PlayerShoted = true;


        if ((hit.collider != null) && (hit.collider.tag == "Rock"))
        {
            player_Rigidbody2D.AddForce(new Vector2(0, verticalImpulse), ForceMode2D.Impulse);
            return true;
        }
        else
        {
            return false;
        }
    }


}
