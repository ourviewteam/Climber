using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMuvement : MonoBehaviour
{
    public MuvementState _state = MuvementState.stop;
    public Rigidbody2D playerRigidbody;
    public float enemyPosition;
    public BasicGamePlay BGP;

    public float muveSpeed = 3f;
    public enum MuvementState
    {
        right,
        left,
        stop
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        BGP = GameObject.FindGameObjectWithTag("AIM").GetComponent<BasicGamePlay>();
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
                    Quaternion newStartPosition = BGP.startRotation;
                    newStartPosition.w = -newStartPosition.w;
                    BGP.transform.rotation = newStartPosition;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    _state = MuvementState.stop;
                    BGP.PlayerShoted = false;
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
                    Quaternion newStartPosition = BGP.startRotation;
                    //newStartPosition.w = -newStartPosition.w;
                    BGP.transform.rotation = newStartPosition;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    _state = MuvementState.stop;
                    BGP.PlayerShoted = false;
                }
            }
        }
    }
}
