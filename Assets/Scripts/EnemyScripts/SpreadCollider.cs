using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadCollider : MonoBehaviour
{
   // private EnemyDamager _enemyDamagerScript;
   public float spreadDamage;
   public bool canApplyBurn;
   public bool canApplyChill;

   private float lifeTime;

    private void Awake() {
        //_enemyDamagerScript = GetComponentInParent<EnemyDamager>();
    }

    private void Start() {
        lifeTime = 0.4f;
    }

    private void Update() {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0){
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            if(canApplyBurn){
                EnemyDamager enemyDamagerScript = other.gameObject.GetComponent<EnemyDamager>();
                enemyDamagerScript.ApplyBurn(spreadDamage);
                Destroy(this.gameObject);
            }else if(canApplyChill){
                //EnemyStats enemyStatsScript = other.gameObject.GetComponent<EnemyStats>();
                EnemyDamager enemyDamagerScript = other.gameObject.GetComponent<EnemyDamager>();
                enemyDamagerScript.ApplyChill();
                //enemyStatsScript.ApplyChill();
            }
            
        }
    }
}
