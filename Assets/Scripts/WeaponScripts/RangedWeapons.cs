using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapons : MonoBehaviour
{
    //Script References
    private GameObject _playerOB;
    private PlayerStats _playerStatsScript;

    private WeaponStats _weaponStatsScript;

    //end

    //Variables
    [SerializeField] private GameObject closestEnemy;
    private EnemyStats _enemyStatsScript;
    private float enemyDistance;
    private Vector2 enemyDirection;
    //end

    //Object Allocations
    [SerializeField] GameObject _weaponProjectile;
    [SerializeField] GameObject _shootingPos;
    


    //end

    //Weapon Stats Variables

    public string weaponName;
    public float weaponBaseDamage;
    
    public float weaponRange;
    public float weaponAttackSpeed;

    public float weaponCritChance;
    public float weaponCritDamage;

    private bool _canShoot;

    //

    //Triggers
    [SerializeField] private bool _isShootingRoutineOn = false;
    public bool bypassFireRate = false;
    //end
    void Awake(){
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
        _weaponStatsScript = GetComponent<WeaponStats>();
    }

    void Start(){
        //weaponAttackSpeed = weaponAttackSpeed * _playerStatsScript.attackSpeed;
        //_weaponStatsScript.weaponAttackSpeed = _weaponStatsScript.weaponAttackSpeed * _playerStatsScript.playerattackSpeed;
        

    }

    private void Update() {
        FindClosestEnemy();
        CheckDirectionOfEnemy();
        RotateWeapon();
        

        if(closestEnemy != null){
            _enemyStatsScript = closestEnemy.GetComponent<EnemyStats>();
        }

        

        
    }

    void FixedUpdate(){
        if(_isShootingRoutineOn == false && _weaponStatsScript.weaponRange >= enemyDistance && closestEnemy != null){
            if(_enemyStatsScript.alreadyGonnaDie == false){
                StartCoroutine(ShootingRoutine());
            }
            

        }
    }

    void RotateWeapon(){
        if(weaponRange >= enemyDistance && closestEnemy !=null){
            this.transform.right = enemyDirection;
        }else{
            this.transform.rotation = Quaternion.identity;
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

        
        GameObject weaponProj = Instantiate(_weaponProjectile, _shootingPos.transform.position, Quaternion.identity);
        RangedProjectile _rangedProjectileScript = weaponProj.GetComponent<RangedProjectile>();
        weaponProj.transform.right = enemyDirection;
        //iceProj.GetComponent<IceWandProj>().enemy = _playerStatsScript.closestEnemy;
        //_rangedProjectileScript.damage = _weaponStatsScript.weaponBaseDamage * (1 + _playerStatsScript.playerDamage/100);
        _rangedProjectileScript.damage = _weaponStatsScript.weaponDamage;
        _rangedProjectileScript.enemy = closestEnemy;
        
            
        
        
        
        
        
    }

    IEnumerator ShootingRoutine(){
        
        _isShootingRoutineOn = true;
        

        
        ShootRanged();

        
        
        yield return new WaitForSeconds(_weaponStatsScript.weaponAttackSpeed);
        bypassFireRate = false;
        
        
        _isShootingRoutineOn = false;
    }

    
}
