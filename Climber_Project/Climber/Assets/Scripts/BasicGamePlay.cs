using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class BasicGamePlay : MonoBehaviour
{
    [Header("Some intresting variables")]
    public SO_Instrument Instrument;
    public PlayerMuvement PM;
    public Rigidbody2D player_Rigidbody2D;
    public GameObject Ammo;
    public bool PlayerShoted = false;
    public UnityEngine.Transform FirePoint;
    public enemy Enemy;
    public AudioSource shootingAudio;
    public AudioClip[] audioClips = new AudioClip[3];
    public UnityArmatureComponent _armatureComponent = null;
    public ControleUI CUI;
    public GameObject[] enemyList;
    public bool generalState = false;
    private int EnemyCount = 0;
    public int CurrentEnemyKilles = 0;
    private float minYenemyPos = 9000;

    private void Start()
    {
        enemyList = GameObject.FindGameObjectsWithTag("enemy");
        EnemyCount = enemyList.Length;

        for (int i = 0; i < EnemyCount; i++)
        {
            for (int j = i; j < EnemyCount; j++)
            {
                GameObject tmp;
                float enemyPos_i = enemyList[i].transform.position.y;
                float enemyPos_j = enemyList[j].transform.position.y;
                if (enemyPos_i > enemyPos_j)
                {
                    tmp = enemyList[i];
                    enemyList[i] = enemyList[j];
                    enemyList[j] = tmp;
                }
            }
        }

        shootingAudio = GetComponent<AudioSource>();
        player_Rigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMuvement>();
        Ammo = Instrument.instrument_Hock;
        _armatureComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityArmatureComponent>();
    }


    private void FixedUpdate()
    {
        if (!PlayerShoted)
        {
            if (Input.GetMouseButtonDown(0)) //(Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                PlayerShoted = true;
                StartCoroutine(ShootEnemy());
            }
        }
    }

    /// <summary>
    /// This is the IEnumerator to make a player shoots and to analize this shoot
    /// </summary>
    IEnumerator ShootEnemy()
    {
        //Getting the near enemy on the scene. In the EnemyList going from first to last
        if (enemyList[CurrentEnemyKilles] != null)
        {
            Enemy = enemyList[CurrentEnemyKilles].GetComponent<enemy>();
        }

        //For - to instantiate on the scene our bullets
        for (int i = 0; i< Instrument.shoot_Count; i++)
        {
            RunAudio(0);
            Instantiate(Ammo, FirePoint.position,  transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Export fuction to get false if he mised else true.
    public void FireState(bool state)
    {
        if (state == true) generalState = true;
        else generalState = false;

        //if player mised , enemy shoot to the player. Run function ShootPlayer from enemy script.
        //and run the game over function.
        if (generalState)
        {
            //if enemy's heath is <0 then we destroy him and muve the player.
            if (Enemy.health <= 0)
            {
                float playerPosition = player_Rigidbody2D.gameObject.transform.position.x;
                float enemyPosition = Enemy.transform.position.x;
                bool playerFlipping = Enemy.PlayerFlipping;

                Enemy.KillTheEnemy();
                minYenemyPos = 9000;
                _armatureComponent.animation.Play("Walk_pistol");

                if (playerPosition < enemyPosition)
                {
                    if (playerFlipping)
                    {
                        PM.enemyPosition = enemyPosition - 0.15f;
                    }
                    else
                    {
                        PM.enemyPosition = enemyPosition;
                    }
                    PM._state = PlayerMuvement.MuvementState.right;
                }
                else
                {
                    if (playerFlipping)
                    {
                        PM.enemyPosition = enemyPosition + 0.25f;
                    }
                    else
                    {
                        PM.enemyPosition = enemyPosition;
                    }
                    PM._state = PlayerMuvement.MuvementState.left;
                }
            }
            else
            {
                PlayerShoted = false;
            }
            
        }
        else
        {
            Enemy.ShootPlayer();
            PlayerShoted = true;
            //generalState = false;
        }

        if (CurrentEnemyKilles == EnemyCount)
        {
            PlayerShoted = true;
            CUI.CompletedLvl();
        }
    }

    public void AnimateShoting()
    {
        _armatureComponent.animation.Play("shot_pistol");
    }

    public void RunAudio(int index)
    {
        shootingAudio.PlayOneShot(audioClips[index]);
    }
}
