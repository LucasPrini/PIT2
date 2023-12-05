/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWandProj : MonoBehaviour
{
    public float damage;

    public GameObject enemy;
    public float speed;

    EnemyStats _enemyStatsScript;
    //public bool bypassFireRate = false;
    

    private void Start() {
        _enemyStatsScript = enemy.GetComponent<EnemyStats>();
    }

    private void Update() {
        if(_enemyStatsScript.enemyCurrentHealth <= damage){
                _enemyStatsScript.alreadyGonnaDie = true;
                Debug.Log("diee");
            }
    }
    
    void FixedUpdate(){
        ProjTraveling();
    }

    void ProjTraveling(){
        if(enemy != null){
            
            this.transform.position = Vector2.MoveTowards(this.transform.position, enemy.transform.position, speed * Time.deltaTime);

        }else{
            Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyDamager>().EnemyTakeDamage(damage);
        }
    }
}*/
