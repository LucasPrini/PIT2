using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private GameObject _gameManagerOB;
    private GameManagerController _gameManagerScript;

    public float enemyMaxHealth;
    public float enemyCurrentHealth;
    public float enemyBaseHealth;

    public bool alreadyGonnaDie = false;
    public float[] enemySpeed = new float[2];
    public float randomSpeed;

    public float enemyBaseDamage;
    

    private void Awake() {
        _gameManagerOB = GameObject.FindGameObjectWithTag("GameManager");
        _gameManagerScript = _gameManagerOB.GetComponent<GameManagerController>();
    }
    
    void Start()
    {
       enemyMaxHealth = enemyBaseHealth + _gameManagerScript.enemyHealthIncrease;
       enemyCurrentHealth = enemyMaxHealth;
       randomSpeed = Random.Range(enemySpeed[0], enemySpeed[1]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
