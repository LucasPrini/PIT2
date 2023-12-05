using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkerAI : MonoBehaviour
{
    EnemyStats _enemyStatsScript;
    [SerializeField] private GameObject _target;
    public GameObject target;

    void Awake(){
        _enemyStatsScript = GetComponent<EnemyStats>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        ChaseTarget();
    }

    void ChaseTarget(){
        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, _enemyStatsScript.randomSpeed * Time.deltaTime);
    }
}
