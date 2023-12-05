using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    PlayerStats _playerStatsScript;
    
    private GameObject currentTarget;

    public Transform[] weaponPosition;
    public Transform weaponpos1;
    public Transform weaponpos2;

    public GameObject weaponPrefab1;

    public bool fireRateRoutineOn = false;
    public bool _bypassFireRate = false;

    public Vector2 weaponRotation;
    public Vector2 enemyDirection;

    public GameObject[] playerWeapons;
    public bool[] _weaponsActive;
    private int _weaponID;
    public int weaponSlot = 1;
    
    // Start is called before the first frame update
    void Awake(){
        _playerStatsScript = GetComponent<PlayerStats>();
        
    }
    void Start()
    {
        _weaponID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirectionOfEnemy();
        //currentTarget = _playerStatsScript.closestEnemy;

        if(fireRateRoutineOn == false && _playerStatsScript.playerRange >= _playerStatsScript.enemyDistance ){
            //StartCoroutine(FireRateRoutine());
        }

        //weapons activation
        if(_weaponsActive[0] == true){
            _weaponID = 0;
            EquipWeapon(playerWeapons[0], weaponPosition[0]);
            
        }
        if(_weaponsActive[1] == true){
            _weaponID = 1;
            EquipWeapon(playerWeapons[1], weaponPosition[1]);
        }
        if(_weaponsActive[2] == true){
            _weaponID = 2;
            EquipWeapon(playerWeapons[2], weaponPosition[2]);
        }
        if(_weaponsActive[3] == true){
            _weaponID = 3;
            EquipWeapon(playerWeapons[3], weaponPosition[3]);
        }
        /*if(_weaponsActive[4] == true){
            _weaponID = 4;
            EquipWeapon(playerWeapons[4], weaponPosition[4]);
        }*/

        //end
        
    }

    void FixedUpdate(){
        if(fireRateRoutineOn){
            //Instantiate(weaponPrefab1, weaponpos1.position, Quaternion.identity);
            
        }
        
        //weaponTransform.transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, 1);
    }

    void CheckDirectionOfEnemy(){
        if(_playerStatsScript.closestEnemy != null){
            enemyDirection = (_playerStatsScript.closestEnemy.transform.position - this.transform.position).normalized;
        }
        
    }

    void EquipWeapon(GameObject weapon, Transform weaponPosition){
        GameObject newWeaponOne;
        newWeaponOne = Instantiate(playerWeapons[_weaponID], weaponPosition.transform.position, Quaternion.identity);
        newWeaponOne.transform.parent = this.gameObject.transform;
        _weaponsActive[_weaponID] = false;
        
    }

    IEnumerator FireRateRoutine(){
        fireRateRoutineOn = true;
        Instantiate(weaponPrefab1, weaponpos1.position, Quaternion.identity);
        
            yield return new WaitForSeconds(2);
            _bypassFireRate = false;
        
        
        fireRateRoutineOn = false;
        
    }
}
