using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuffEffects : MonoBehaviour
{

    private GameObject _playerOB;
    private PlayerStats _playerStatsScript;

    public int damageBuff;
    public string damageBuffDescription;

    public int attackRateBuff;
    public string attackeRateDescription;

    public int rangeBuff;
    public string rangeBuffDescription;

    public int critChanceBuff;
    public string critChanceBuffDescription;

    private void Awake() {
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
    }


    public void IncreasePlayerStats(int buffID){
        if(buffID == 1){
            _playerStatsScript.playerAttackSpeed = _playerStatsScript.playerAttackSpeed + attackRateBuff;
        }else if(buffID == 2){
            _playerStatsScript.playerDamage = _playerStatsScript.playerDamage + damageBuff;
        }else if(buffID == 3){
            _playerStatsScript.playerRange = _playerStatsScript.playerRange + rangeBuff;
        }else if(buffID == 4){
            _playerStatsScript.playerCritChance = _playerStatsScript.playerCritChance + critChanceBuff;
        }
    }

    

}
