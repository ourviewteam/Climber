using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMuvement : MonoBehaviour
{
    public MuvementState _state = MuvementState.stop;
    public Rigidbody2D playerRigidbody;
    public float enemyPosition;
    public BasicGamePlay BGP;
    public GameObject[] Floor;
    public int i = 0;

    public float muveSpeed = 5f;
    public float floorMuveSpeed = 5f;
    public enum MuvementState
    {
        right,
        left,
        stop,
        muveNext
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        BGP = GameObject.FindGameObjectWithTag("AIM").GetComponent<BasicGamePlay>();
        Floor = GameObject.FindGameObjectsWithTag("floor");

        for (i = 0; i < Floor.Length; i++)
        {
            for (int j = i; j < Floor.Length; j++)
            {
                GameObject tmp;
                float floorPos_i = Floor[i].transform.position.y;
                float floorPos_j = Floor[j].transform.position.y;
                if (floorPos_i > floorPos_j)
                {
                    tmp = Floor[i];
                    Floor[i] = Floor[j];
                    Floor[j] = tmp;
                }
            }
        }
        i = 0;
    }

    private void FixedUpdate()
    {
        if (_state != MuvementState.stop) {   

            if (_state == MuvementState.right)
            {
                if (transform.position.x < enemyPosition)
                {
                    playerRigidbody.velocity = transform.right * muveSpeed;
                }
                else
                {
                    if (!BGP.Enemy.PlayerFlipping)
                    {
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        _state = MuvementState.muveNext;
                    }
                    else
                    {
                        BGP.AnimateShoting();
                        _state = MuvementState.stop;
                        BGP.PlayerShoted = false;
                    }
                }
            }
            else if (_state == MuvementState.left)
            {
                if (transform.position.x > enemyPosition)
                {
                    playerRigidbody.velocity = -transform.right * muveSpeed;
                }
                else
                {
                    if (!BGP.Enemy.PlayerFlipping)
                    {
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        _state = MuvementState.muveNext;
                    }
                    else
                    {
                        BGP.AnimateShoting();
                        _state = MuvementState.stop;
                        BGP.PlayerShoted = false;
                    }
                }
            }

            else if (_state == MuvementState.muveNext)
            {
                if (transform.localScale.x < 0)
                {
                    if (transform.position.x > (Floor[i].transform.position.x))
                        playerRigidbody.velocity = -transform.right * floorMuveSpeed;
                    else
                    {
                        BGP.AnimateShoting();
                        _state = MuvementState.stop;
                        BGP.PlayerShoted = false;
                        i++;
                    }
                }
                else
                {
                    if (transform.position.x < (Floor[i].transform.position.x))
                        playerRigidbody.velocity = transform.right * floorMuveSpeed;
                    else
                    {
                        BGP.AnimateShoting();
                        _state = MuvementState.stop;
                        BGP.PlayerShoted = false;
                        i++;
                    }
                }
            }
        }        
    }
}
