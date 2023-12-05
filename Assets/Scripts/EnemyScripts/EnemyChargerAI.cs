using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargerAI : MonoBehaviour
{
    EnemyStats _enemyStatsScript;
    
    public GameObject target;

    private Animator _spiderAnimator;

    [SerializeField]private float _targetDistance;

    [SerializeField] float _jumpRange;
    [SerializeField] float _chargeSpeed;
    [SerializeField] float _speedHolder;
    [SerializeField] float _chargeCooldown;
    [SerializeField] float _cooldownTime;

    private void Awake() {
        _enemyStatsScript = GetComponent<EnemyStats>();
        target = GameObject.FindGameObjectWithTag("Player");
        _spiderAnimator = GetComponent<Animator>();
        GetTargetDistance();
    }

    private void Start() {
        _chargeCooldown = _cooldownTime;
    }

    private void Update() {
        GetTargetDistance();
        if(_isChargingRoutineOn == false){
            _chargeCooldown -= Time.deltaTime;
        }
        
    }

    private void FixedUpdate() {
        if(_targetDistance >= _jumpRange){
            GetCloseToTarget();
        }else if(_isChargingRoutineOn == false && _targetDistance <= _jumpRange && _chargeCooldown <= 0){
            StartCoroutine(ChargeRoutine());
            _chargeCooldown = _cooldownTime;
        }

        

        if(_isCharging == true){
            ChargeTarget();
        }
    }

    void GetCloseToTarget(){
        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, _enemyStatsScript.randomSpeed * Time.deltaTime);
    }

    void GetTargetDistance(){
        if(target != null){
            _targetDistance = (target.transform.position - this.transform.position).sqrMagnitude;
        }
        
    }

    Vector2 lastTargetPos;

    void ChargeTarget(){
        
        this.transform.position = Vector2.MoveTowards(this.transform.position, lastTargetPos, _chargeSpeed * Time.deltaTime);
    }

    private bool _isCharging;
    private bool _isChargingRoutineOn;
    IEnumerator ChargeRoutine(){
        _isChargingRoutineOn  = true;
        _spiderAnimator.SetBool("Stopped", true);
         
        _speedHolder = _enemyStatsScript.randomSpeed;
        _enemyStatsScript.randomSpeed = 0;
        lastTargetPos = target.transform.position;
        yield return new WaitForSeconds(0.5f);
        _isCharging = true;
        
              
        yield return new WaitForSeconds(1f);
        _enemyStatsScript.randomSpeed = _speedHolder;
        yield return new WaitForSeconds(0.5f);
        _isCharging = false;
        _spiderAnimator.SetBool("Stopped", false);
        _isChargingRoutineOn  = false;

    }

    
}
