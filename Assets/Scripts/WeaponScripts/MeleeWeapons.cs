using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapons : MonoBehaviour
{
    private Rigidbody2D _weaponRB;
    [SerializeField] private GameObject _startingPos;
    [SerializeField] private GameObject _maxRange;

    private GameObject _playerOB;
    


    private WeaponStats _weaponStatsScript;




    [SerializeField] private GameObject closestEnemy;

    private EnemyStats _enemyStatsScript;
    private float enemyDistance;
    private Vector2 enemyDirection;


    [SerializeField] private bool _comeBackToPlayer = true;

    [SerializeField] private float _distanceTravelled;
    private Vector2 _lastPosition;
    [SerializeField] private bool _attacking;

    private Animator _weaponAnim;





    private void Awake() {
        _weaponRB = GetComponent<Rigidbody2D>();
        _weaponStatsScript = GetComponent<WeaponStats>();
        _weaponAnim = GetComponent<Animator>();

    }
    private void Update() {
        
        
        
        
        FindClosestEnemy();
        CheckDirectionOfEnemy();
        RotateWeapon();
        if(Input.GetKeyDown(KeyCode.K)){
            _attacking = true; 
            //_weaponAnim.SetTrigger("Attack");          
            Debug.Log("Key pressed");
        }

        if(this.transform.position.x <= _startingPos.transform.position.x){
                _comeBackToPlayer = false;
                //_attacking = false;
            }
        if(this.transform.position.x >= _maxRange.transform.position.x){
                _comeBackToPlayer = true;
            }else{
                
            }
        
    }

    void FixedUpdate(){
        if(_attacking){
            AttackMeleeWeapon();           
            
            
            
        }
    }
    void AttackMeleeWeapon(){
       
        if(_comeBackToPlayer == false){
                transform.position = Vector2.MoveTowards(this.transform.position, _maxRange.transform.position, 5 * Time.deltaTime);
            }else if(_comeBackToPlayer == true){
               transform.position = Vector2.MoveTowards(this.transform.position, _startingPos.transform.position, 5 * Time.deltaTime);
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

    void RotateWeapon(){
        if(_weaponStatsScript.weaponRange >= enemyDistance && closestEnemy !=null){
            this.transform.right = enemyDirection;
        }else{
            this.transform.rotation = Quaternion.identity;
        }
        
    }


    
}
