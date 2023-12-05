using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    private Rigidbody2D _projectileRB;
    [SerializeField] private GameObject _spreadColliderOB;
    public float damage;
    public int pierceCount;
    public int pierceMax;
    public bool canPierce;

    public GameObject enemy;
    private Vector2 _enemyDirection;
    public float speed;
    public bool pierceEnabled;
    

    [SerializeField] private bool _canApplyBurn;
    [SerializeField] private bool _canApplyChill;

    EnemyStats _enemyStatsScript;
    //public bool bypassFireRate = false;

    private void Awake() {
        _projectileRB = GetComponent<Rigidbody2D>();
    }
    

    private void Start() {
        if(enemy != null){
            _enemyStatsScript = enemy.GetComponent<EnemyStats>();
        }
        
        _enemyDirection = (enemy.transform.position - this.transform.position);
        
    }

    private void Update() {

        if(pierceCount <= pierceMax){
            canPierce = true;
        }else{
            canPierce = false;
        }

        if(_enemyStatsScript != null){
            if(_enemyStatsScript.enemyCurrentHealth <= damage){
                _enemyStatsScript.alreadyGonnaDie = true;
                
            }
        }
        
    }
    
    void FixedUpdate(){
        ProjTraveling();
    }

    void ProjTraveling(){
        if(true){
            
            //this.transform.position = Vector2.MoveTowards(this.transform.position, enemy.transform.position, speed * Time.deltaTime);
            _projectileRB.velocity = new Vector2(_enemyDirection.x, _enemyDirection.y).normalized * speed;


        }else{
            //Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            GameObject spreadCollider = Instantiate(_spreadColliderOB, this.transform.position, Quaternion.identity);
            SpreadCollider _spreadColliderScript = spreadCollider.GetComponent<SpreadCollider>();
            EnemyDamager target = other.gameObject.GetComponent<EnemyDamager>();
            target.EnemyTakeDamage(damage);
            if(_canApplyBurn){
                _spreadColliderScript.canApplyBurn = true;
                _spreadColliderScript.spreadDamage = damage/3;
                //spreadCollider.GetComponent<SpreadCollider>().spreadDamage = damage/3;                
            }else if(_canApplyChill){
                
                _spreadColliderScript.canApplyChill = true;
            }
            
            
            //other.gameObject.GetComponent<EnemyDamager>().EnemyTakeDamage(damage);
            /*if(_canApplyBurn){
                target.ApplyBurn(damage);
                target.canSpread = true;
            }*/
        }
    }
}
