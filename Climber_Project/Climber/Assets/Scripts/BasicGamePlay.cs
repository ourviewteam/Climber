using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGamePlay : MonoBehaviour
{
    
    [Header("Speed variables")]
    public float muvementSpeed = 5f;
    public float AmmoSpeed = 20f;
    public int FoultShootCount = 0;

    [Header("Other")]
    public SO_Instrument Instrument;
    public PlayerMuvement PM;
    public Rigidbody2D player_Rigidbody2D;
    public SpriteRenderer Gun_body;
    public GameObject AimLine;
    public GameObject Ammo;
    public bool PlayerShoted = false;
    public bool changeDirection = false;
    public bool IsFacingRight = true;
    public Transform FirePoint;
    public enemy Enemy;
    public Quaternion startRotation;

    public bool generalState = false;

    private void Start()
    {
        player_Rigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMuvement>();
        AimLine.transform.localScale = Instrument.gun_scale;
        startRotation = transform.localRotation;
        Gun_body.sprite = Instrument.instrument_Body;
        Ammo = Instrument.instrument_Hock;
    }


    private void FixedUpdate()
    {
        
        // run script to animate the aim
        if (!PlayerShoted)
        {
            StartAimAnimation();

            if (Input.GetMouseButtonDown(0))
            {
                PlayerShoted = true;
                Shoot();
            }
        }
    }

    public void StartAimAnimation()
    {
        AimLine.SetActive(true);
        if (changeDirection)
        {
            transform.Rotate(0, 0, Instrument.min_Angle_Range * Instrument.rangeMovingspeed * 3);
            if (transform.rotation.z <= Instrument.min_Angle_Range)
            {
                changeDirection = false;
            }
        }
        else
        {
            transform.Rotate(0, 0, Instrument.max_Angle_Range * Instrument.rangeMovingspeed);
            if (transform.rotation.z >= Instrument.max_Angle_Range)
            {
                changeDirection = true;
            }
        }

        
        
    }

    public void Shoot()
    { 
        AimLine.SetActive(false);
        StartCoroutine (ShootEnemy());
    }

    public void GameOver()
    {
        Debug.Log("Game Is Over");
    }

    IEnumerator ShootEnemy()
    {
        for (int i = 0; i< Instrument.shoot_Count; i++)
        {
            Instantiate(Ammo, FirePoint.position,  transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }

        Enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<enemy>();
        if (!generalState)
        {
            Enemy.ShootPlayer();
            //
            PlayerShoted = false;
        }
        else
        {
            if (Enemy.health <= 0)
            {
                if (player_Rigidbody2D.gameObject.transform.position.x < Enemy.transform.position.x)
                {
                    PM.enemyPosition = Enemy.transform.position.x;
                    PM._state = PlayerMuvement.MuvementState.right;  
                }
                else
                {
                    PM.enemyPosition = Enemy.transform.position.x;
                    PM._state = PlayerMuvement.MuvementState.left;
                }
                Enemy.KillTheEnemy();
                
            }

            generalState = false;
            PlayerShoted = false;
        }
    }

    public void FireState(bool state)
    {
        if (state == true) generalState = true;
    }
}
