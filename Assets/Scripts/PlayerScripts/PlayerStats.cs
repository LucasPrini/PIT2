using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Collider2D playerRangeCollider;
    [SerializeField] private GameObject[] _enemiesInRange;

    public GameObject closestEnemy = null;

    public float enemyDistance;

    [SerializeField] private GameObject _healthBarOB;
    [SerializeField] private GameObject _healthBarFill;

    

    public float playerMaxHealth;
    public float playerCurrentHealth;
    [SerializeField] private float _playerHealthPercent;
    public float playerDamage;
    public float playerRange;
    public float playerPickUpRange;
    public float playerAttackSpeed;
    public float playerCritChance;
    public float materials = 0;

    private float _iFrameScalingFactor;
    private float _incomingDamage;

    //Triggers
    private bool _isIFrameRoutine = false;
    //end


    private void Awake() {
        _healthBarFill = _healthBarOB.transform.GetChild(1).gameObject;
    }

    void Start() {  
        playerCurrentHealth = playerMaxHealth;
        
    }


    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        ChangeHealthBarUI();

        
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

    void ChangeHealthBarUI(){
        _playerHealthPercent = playerCurrentHealth/playerMaxHealth;
        _healthBarFill.transform.localScale = new Vector2(_playerHealthPercent, 1);
    }
    public void PlayerTakeDamage(float damage){

        if(_isIFrameRoutine == false){
            playerCurrentHealth = playerCurrentHealth - damage;
            _incomingDamage = damage;
            _iFrameScalingFactor = 0.4f * (_incomingDamage/playerMaxHealth) * (15/100);
            StartCoroutine(IFrame());
        }
        
    }

    

    IEnumerator IFrame(){
        _isIFrameRoutine = true;
        yield return new WaitForSeconds(0.4f);
        _isIFrameRoutine = false;
    }


    public void  AddToMaterials(GameObject materialOB){
        int randomDrop;
        randomDrop = Random.Range(1,4);
        materials = materials + randomDrop;
        Destroy(materialOB);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "MaterialDrop"){
            
            AddToMaterials(other.gameObject);
        }

        if(other.gameObject.tag == "LifePickUp"){
            if(playerCurrentHealth < playerMaxHealth){
                playerCurrentHealth += 1;
            }
            
            Destroy(other.gameObject);
        }

    }



    


    

    

    
    
}
