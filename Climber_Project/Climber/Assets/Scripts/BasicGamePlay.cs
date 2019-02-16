using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGamePlay : MonoBehaviour
{
    [Header("Range variables")]
  //  const float max_Range = 0.50f;
  //  const float min_Range = -0.15f;

    [Header("Speed variables")]
    public float verticalImpulse = 5f;
    public float muvementSpeed = 10f;
    public int FoultShootCount = 0;
   // public float speed_ToAim = 200f;

    [Header("Other")]
    public SO_Instrument Instrument;
    public Rigidbody2D player_Rigidbody2D;
    public GameObject AimLine;
    public bool PlayerShoted = false;
    public bool changeDirection = false;
    public bool IsFacingRight = true;

    RaycastHit2D hit;

    private void Start()
    {
        player_Rigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(Instrument.aim_Distance, transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {
        if (FoultShootCount == Instrument.shoot_Count)
        {
            GameOver();
        }
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
                transform.Rotate(0, 0, Instrument.min_Angle_Range * Instrument.rangeMovingspeed * 3 * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, Instrument.max_Angle_Range * Instrument.rangeMovingspeed * Time.deltaTime);
                
            }

            if (transform.rotation.z >= Instrument.max_Angle_Range)
            {
                changeDirection = true;
            }
            else if (transform.rotation.z <= Instrument.min_Angle_Range)
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
            }
            else if (((playerPosition.position.x - hit.collider.transform.position.x) >= 0) && (!IsFacingRight))
            {
                player_Rigidbody2D.transform.Translate(-transform.right * muvementSpeed * Time.deltaTime);
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
            FoultShootCount++;
            return false;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Is Over");
    }


}
