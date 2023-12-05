using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private GameObject _playerOB;
    private PlayerWeapons _playerWeaponsScript;
    private PlayerStats _playerStatsScript;

    private Vector2 enemyDirection;

    
    void Awake(){
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _playerWeaponsScript = _playerOB.GetComponent<PlayerWeapons>();
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
    }
    void Update(){
        CheckDirectionOfEnemy();
        this.transform.right = _playerWeaponsScript.enemyDirection;
        
    }


    void CheckDirectionOfEnemy(){
        
        if(_playerStatsScript.closestEnemy != null){
            enemyDirection = (_playerStatsScript.closestEnemy.transform.position - this.transform.position).normalized;
        }
    }
}
