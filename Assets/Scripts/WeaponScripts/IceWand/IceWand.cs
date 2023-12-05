/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWand : MonoBehaviour
{
    //Script References
    private GameObject _playerOB;
    private PlayerStats _playerStatsScript;

    //end

    //Variables
    [SerializeField] private GameObject closestEnemy;
    private EnemyStats _enemyStatsScript;
    private float enemyDistance;
    private Vector2 enemyDirection;
    //end

    //Object Allocations
    [SerializeField] GameObject _iceWandProj;
    [SerializeField] GameObject _ShootingPos;
    [SerializeField] private float _weaponBaseDamage;
    public float weaponRange;


    //end

    //Weapon Stats Variables
    [SerializeField] private float _attackSpeed;
    private bool _canShoot;

    //

    //Triggers
    [SerializeField] private bool _isShootingRoutineOn = false;
    public bool bypassFireRate = false;
    //end
    void Awake(){
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
    }

    void Start(){
        _attackSpeed = _attackSpeed * _playerStatsScript.attackSpeed;
    }

    private void Update() {
        FindClosestEnemy();
        CheckDirectionOfEnemy();
        this.transform.right = enemyDirection;

        if(closestEnemy != null){
            _enemyStatsScript = closestEnemy.GetComponent<EnemyStats>();
        }

        

        
    }

    void FixedUpdate(){
        if(_isShootingRoutineOn == false && weaponRange >= enemyDistance && closestEnemy != null){
            if(_enemyStatsScript.alreadyGonnaDie == false){
                StartCoroutine(ShootingRoutine());
            }
            

        }
    }

    void FindClosestEnemy(){
        float distanceToClosestEnemy = Mathf.Infinity;
        //GameObject closestEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject currentEnemy in allEnemies){
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position). sqrMagnitude;
            if(distanceToEnemy < distanceToClosestEnemy){
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;

                enemyDistance = distanceToClosestEnemy;

            }
        }
       
    }

    void CheckDirectionOfEnemy(){
        
        if(closestEnemy != null){
            enemyDirection = (closestEnemy.transform.position - this.transform.position).normalized;
        }
    }

    void ShootRanged(){

        
        GameObject iceProj = Instantiate(_iceWandProj, _ShootingPos.transform.position, Quaternion.identity);
        IceWandProj _iceWandProjScript = iceProj.GetComponent<IceWandProj>();
        //iceProj.GetComponent<IceWandProj>().enemy = _playerStatsScript.closestEnemy;
        _iceWandProjScript.damage = _weaponBaseDamage * _playerStatsScript.playerDamage;
        _iceWandProjScript.enemy = closestEnemy;
        if(_iceWandProjScript.damage >= _enemyStatsScript.enemyCurrentHealth){
            
        }
            
        
        
        
        
        
    }

    IEnumerator ShootingRoutine(){
        
        _isShootingRoutineOn = true;
        

        
        ShootRanged();

        
        
        yield return new WaitForSeconds(_attackSpeed);
        bypassFireRate = false;
        
        
        _isShootingRoutineOn = false;
    }

    
}*/
