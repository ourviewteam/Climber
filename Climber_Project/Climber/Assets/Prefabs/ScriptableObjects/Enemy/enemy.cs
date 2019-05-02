using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class enemy : MonoBehaviour
{
    public int health = 10;
    public GameObject Ammo;
    public bool isFlipped = true;
    public bool PlayerFlipping = false;
    public Rigidbody2D enemyRG;
    public Vector2 directionToDie;
    public UnityEngine.Transform enemyFirePoint;
    public UnityArmatureComponent EnemyArmature;
    public GameObject handToHide;
    public GameObject handToView;
    public GameObject player;
    public AudioSource audio;
    public ControleUI CUI;
    public BasicGamePlay BGP;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        BGP = GameObject.FindGameObjectWithTag("AIM").GetComponent<BasicGamePlay>();
        EnemyArmature = GetComponent<UnityArmatureComponent>();
        enemyRG = GetComponent<Rigidbody2D>();
        if (isFlipped)
        {
            directionToDie = Vector2.left;
            transform.Rotate(0, 180, 0);
        } else
        {
            directionToDie = Vector2.right;
        }
    }
    public void ShootPlayer()
    {
        EnemyArmature.animation.Play("newAnimation");
        handToHide.SetActive(false);
        handToView.SetActive(true);

        if (isFlipped)
        {
            handToView.transform.Rotate(0, 180, 0);
            handToView.transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector3 _currentDirection = new Vector3(0, 1, 1);
        Vector2 _direction = player.transform.position - handToView.transform.position;
        _direction.Normalize();

        StartCoroutine(lerpToPlayer(_currentDirection, _direction));
    }

    public void KillTheEnemy()
    {
        BGP.CurrentEnemyKilles++;
        if (!PlayerFlipping)
        {
            enemyRG.AddForce(directionToDie * 10f, ForceMode2D.Impulse);
            StartCoroutine(destroyEnemy());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator destroyEnemy()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }
    IEnumerator ShowEnd()
    {
        yield return new WaitForSeconds(.3f);
        CUI.EndGame();
    }
    IEnumerator lerpToPlayer(Vector3 currentDirection, Vector2 direction)
    {
        for (int i = 0; i < 10; i++)
        {
            currentDirection = Vector2.Lerp(currentDirection, direction, Time.deltaTime * 1f);
            handToView.transform.up = currentDirection;
            yield return new WaitForSeconds(.02f);
        }
        audio.Play();
        Instantiate(Ammo, enemyFirePoint.position, enemyFirePoint.rotation);
        StartCoroutine(ShowEnd());
    }
}
