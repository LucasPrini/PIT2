using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDamager : MonoBehaviour
{
    EnemyStats _enemyStatsScript;
    public GameObject floatingNumber;
    [SerializeField] GameObject _bloodExplosion;
    [SerializeField] GameObject _MaterialDropOB;
    [SerializeField] GameObject _lifePickUp;
    [SerializeField] GameObject _burningIconOB;
    [SerializeField] GameObject _chillIconOB;

    private GameObject _playerOB;
    private PlayerStats _playerStatsScript;
    private GameObject _gameSpawnerOB;
    private EnemySpawner _enemySpawnerScript;
    

    //Collider References
    private Collider2D _enemyDamageCollider;

    //end

    //Triggers
    [SerializeField] private bool _isAttackRateRoutineOn = false;
    public bool canSpread;
    public bool alreadyBurning;
    public bool alreadyChilled;
    [SerializeField] public float incomingSecondaryDamage;

    private int dropChance;
    

    //end
    

    void Awake(){
        _enemyStatsScript = GetComponent<EnemyStats>();
        _enemyDamageCollider = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Collider2D>();
        _gameSpawnerOB = GameObject.FindGameObjectWithTag("GameSpawner");
        _enemySpawnerScript = _gameSpawnerOB.GetComponent<EnemySpawner>();
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        dropChance = Random.Range(0,100);
        _burningIconOB.SetActive(false);
        _chillIconOB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isAttackRateRoutineOn = false){
            StartCoroutine(AttackRateRoutine());
        }

        if(Input.GetKeyDown(KeyCode.K)){
            StartCoroutine(BurningRoutine(1));
        }
    }

    public void EnemyTakeDamage(float incomingDamage){
        //Debug.Log("tookDamage");
        
        
        _enemyStatsScript.enemyCurrentHealth = _enemyStatsScript.enemyCurrentHealth - incomingDamage;
        GameObject Fnumber = Instantiate(floatingNumber, this.transform.position, Quaternion.identity);
        Fnumber.GetComponent<FloatingNumber>().ChangeText(incomingDamage);
        if(_enemyStatsScript.enemyCurrentHealth <= 0){
            Instantiate(_bloodExplosion, this.transform.position, Quaternion.identity);
            Instantiate(_MaterialDropOB, this.transform.position, Quaternion.identity);
            _enemySpawnerScript.enemiesOnScreen = _enemySpawnerScript.enemiesOnScreen - 1;
            Destroy(this.gameObject);
        }
    }

    private void EnemyTakeSecondaryDamage(float incomingSecondaryDamage){

            _enemyStatsScript.enemyCurrentHealth = _enemyStatsScript.enemyCurrentHealth - incomingSecondaryDamage;
            GameObject Fnumber = Instantiate(floatingNumber, this.transform.position, Quaternion.identity);
            Fnumber.GetComponent<FloatingNumber>().ChangeText(incomingSecondaryDamage);
            if(_enemyStatsScript.enemyCurrentHealth <= 0){

                Instantiate(_bloodExplosion, this.transform.position, Quaternion.identity);
                if(dropChance <= 20){
                    Instantiate(_lifePickUp, this.transform.position, Quaternion.identity);
                }
                Instantiate(_MaterialDropOB, this.transform.position, Quaternion.identity);
                _enemySpawnerScript.enemiesOnScreen = _enemySpawnerScript.enemiesOnScreen - 1;
                Destroy(this.gameObject);
        }
        
    }

    

    public void ApplyBurn(float damage){
        if(alreadyBurning == false){
            StartCoroutine(BurningRoutine(damage));
        }
        
    }

    public void ApplyChill(){
        if(alreadyChilled == false){
            StartCoroutine(ChillRoutine());
        }
        
    }

    IEnumerator ChillRoutine(){
        alreadyChilled = true;
        _chillIconOB.SetActive(true);
        float speedHolder;
        speedHolder = _enemyStatsScript.randomSpeed;
        _enemyStatsScript.randomSpeed = _enemyStatsScript.randomSpeed * 0.6f;
        yield return new WaitForSeconds(1f);
        _enemyStatsScript.randomSpeed = speedHolder;
        _chillIconOB.SetActive(false);
        alreadyChilled = false;
    }

    IEnumerator BurningRoutine(float damage){
        alreadyBurning = true;
        _burningIconOB.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        EnemyTakeSecondaryDamage(damage);
        yield return new WaitForSeconds(0.5f);
        EnemyTakeSecondaryDamage(damage);
        yield return new WaitForSeconds(0.5f);
        EnemyTakeSecondaryDamage(damage);
        _burningIconOB.SetActive(false);
        alreadyBurning = false;
    }

    private void DisableDamageCollider(){
        _enemyDamageCollider.enabled = false;
    }

    private void EnableDamageCollider(){
        _enemyDamageCollider.enabled = true;
    }

    
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "PlayerDamage"){
            RangedProjectile _rangedProjScript = other.gameObject.GetComponent<RangedProjectile>();
            if(_rangedProjScript.canPierce == false || _rangedProjScript.pierceEnabled == false){
                Destroy(other.gameObject);
            }else{
                _rangedProjScript.pierceCount++;
            }
            
            
        }

        /*if(other.gameObject.tag == "Player" && other.gameObject != null){
            PlayerStats _playerStatsScript = other.gameObject.GetComponent<PlayerStats>();
            _playerStatsScript.PlayerTakeDamage(_enemyStatsScript.enemyBaseDamage);
        }*/
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && other.gameObject != null){
            PlayerStats _playerStatsScript = other.gameObject.GetComponent<PlayerStats>();
            _playerStatsScript.PlayerTakeDamage(_enemyStatsScript.enemyBaseDamage);
            
        }

        /*if(other.gameObject.tag == "EnemyDamageCollider"){

            
            if(true){
                EnemyDamager enemyDamagerScript = other.gameObject.GetComponentInParent<EnemyDamager>();
                if(enemyDamagerScript.alreadyBurning == false){
                    
                    enemyDamagerScript.ApplyBurn(incomingSecondaryDamage);
                    
                }
            }
            Debug.Log("Aplied");
            
            
            
            
        }*/
    }


    //EnemyAttackRate Routine

    IEnumerator AttackRateRoutine(){
        _isAttackRateRoutineOn = true;
        
        DisableDamageCollider();
        yield return new WaitForSeconds(0.5f);
        EnableDamageCollider();
        
        _isAttackRateRoutineOn = false;
    }

    //end


}
