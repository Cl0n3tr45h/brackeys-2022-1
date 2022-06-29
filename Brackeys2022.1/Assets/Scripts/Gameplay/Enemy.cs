using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum EnemyState
{
    IDLE,
    SHOOT,
    TELEPORT
};
public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public int Health;

    public bool RealEnemy;

    public float ShootInterval;

    public int TargetShootCount;
    private int currentShootCount;

    public GameObject BulletPrefab;
    private bulletMovement bulletMovement;
    public Transform BulletSpawn;
    public int BulletSpeed;
    public int BulletLifeTime;

    public Animator Animator;
    private EnemyState state;

    private Collider2D coll;

    public LayerMask CamLayer;

    public LayerMask ShootLayer;

    private PlaneShift planeShift;

    private bool waitForShoot;

    public TeleportCollider[] TeleportColliders;

    public UnityEvent OnEnemyDeath;

    private SpriteRenderer[] sprites;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletMovement = BulletPrefab.GetComponent<bulletMovement>();
        Player = GameObject.FindWithTag("Player");
        state = EnemyState.IDLE;
        coll = GetComponentInChildren<Collider2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        TeleportColliders = FindObjectsOfType<TeleportCollider>();
        Animator = GetComponentInChildren<Animator>();
        
        if (OnEnemyDeath == null)
        {
            OnEnemyDeath = new UnityEvent();
        }
    }

    private void OnEnable()
    {
        
        planeShift = GameObject.Find("GameManager").GetComponent<PlaneShift>();
        planeShift.OnShiftToReal?.AddListener(SetLayerMaskReal);
        planeShift.OnShiftToImaginary?.AddListener(SetLayerMaskImaginary);
        
        GameLoop.SubscribeToEnemyEvent(OnEnemyDeath);
        RealEnemy = GameLoop.NextEnemyReal;
    }

    // Update is called once per frame
    void Update()
    {
        if (RealEnemy == PlaneShift.InReal)
        {
            coll.isTrigger = false;
            Animator.StopPlayback();
            Shift(false);
            
            LookAtPlayer();
            switch (state)
            {
                case EnemyState.IDLE:
                    IdleBehaviour();
                    break;
                case EnemyState.SHOOT:
                    ShootBehaviour();
                    FindObjectOfType<AudioManager>().Play("EnemyShoot");
                    break;
                case EnemyState.TELEPORT:
                    TeleportBehaviour();
                    FindObjectOfType<AudioManager>().Play("EnemyTeleport");
                    break;
                default:
                   IdleBehaviour();
                   break;
            }
        }
        else
        {
            Shift(true);
            coll.isTrigger = true;
        }
    }

    public void Shift(bool shifted)
    {
        if(shifted)
        {
            Animator.StartPlayback();
            foreach (var sprite in sprites)
            {
                sprite.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }
        else
        {
            foreach (var sprite in sprites)
            {
                sprite.color = Color.white;
            }
        }
    }
    
    public void LookAtPlayer()
    {
        if (Player.transform.position.x > this.transform.position.x)
        {
            //scale = 1

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        }
        else
        {
            //scale = -1
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }
    }
    
    public void IdleBehaviour()
    {
        if (!waitForShoot)
        {
            StartCoroutine(WaitToShoot());
        }
        
    }

    public IEnumerator WaitToShoot()
    {
        waitForShoot = true;
        yield return new WaitForSeconds(ShootInterval);
        if (currentShootCount > 0 && InCameraView())
            state = EnemyState.SHOOT;
        else
            state = EnemyState.TELEPORT;
    }

    public void ShootBehaviour()
    {
        waitForShoot = false;
        //Get player pos
        var direction = -(transform.position - Player.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position,
            direction,
            bulletMovement.LifeTime, ShootLayer, Mathf.NegativeInfinity, Mathf.Infinity);
        if (hit)
        {
            if (hit.collider.gameObject.layer == Player.layer)
            {
                //fuckn shoot
                GameObject Bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity);
                var bullet = Bullet.GetComponent<bulletMovement>();
                    bullet.Direction = direction;
                    bullet.IsReal = RealEnemy;
                currentShootCount--;
                state = EnemyState.IDLE;
            }
        }
        else
        {
            state = EnemyState.TELEPORT;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if(bulletMovement)
            Gizmos.DrawRay(transform.position, -(transform.position - Player.transform.position).normalized * bulletMovement.LifeTime);
    }

    public void TeleportBehaviour()
    {
        
        waitForShoot = false;
        List<TeleportCollider> FreeColliders = new List<TeleportCollider>();
        foreach (var teleportCollider in TeleportColliders)
        {
            if(teleportCollider.IsFree)
                FreeColliders.Add(teleportCollider);
        }
        if (FreeColliders.Count >= 1)
        {
            var collider = FreeColliders[Random.Range(0, FreeColliders.Count - 1)];
            if (collider.IsFree)
            {
                this.transform.SetPositionAndRotation(collider.gameObject.transform.position, Quaternion.identity);
            }
        }

        currentShootCount = TargetShootCount;
        
        state = EnemyState.IDLE;
    }

    public void SetLayerMaskReal()
    { 
        ShootLayer = LayerMask.GetMask("Player", "Real");
    }

    public void SetLayerMaskImaginary()
    {
            
        ShootLayer = LayerMask.GetMask("Player", "Imaginary");
    }
    public void Damage(int _damage)
    {
    Health -= _damage;
    if (Health <= 0)
      KillEnemy();
    }

    public void KillEnemy()
    {
        OnEnemyDeath.Invoke();
        
        planeShift.OnShiftToImaginary?.RemoveListener(SetLayerMaskImaginary);
        planeShift.OnShiftToReal?.RemoveListener(SetLayerMaskReal);
        
        GameLoop.UnSubscribeToEnemyEvent(OnEnemyDeath);
        //Do stuff for combos points loot etc
        Destroy(this.gameObject);
        FindObjectOfType<AudioManager>().Play("EnemyDie");
    }

    public bool InCameraView()
    {
        Collider2D[] resultcolls = new Collider2D[1];
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.layerMask = CamLayer;
        contactFilter2D.useTriggers = true;
        var colliderExists = Physics2D.OverlapCollider(coll, contactFilter2D, resultcolls);
        if (resultcolls[0] != null)
          return true;
        return false;
    }
}
