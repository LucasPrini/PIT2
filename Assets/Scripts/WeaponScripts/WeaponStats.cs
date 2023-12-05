using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private GameObject _playerOB;
    private PlayerStats _playerStatsScript;

    public string weaponName;
    public string weaponType;

    public Sprite weaponImage;    
    public float weaponBaseDamage;
    public float weaponDamage;
    public float weaponBaseCritChance;
    public float weaponCritChance;
    public float weaponBaseCritDamage;
    public float weaponCritDamage;
    public float weaponBaseAttackSpeed;
    public float weaponAttackSpeed;
    public float weaponBaseRange;
    public float weaponRange;
    public int weaponPierceCount;

    public int weaponPrice;
    

    private void Awake() {
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
    }

    private void Update() {
        if(Input.GetKeyDown("space")){
            UpdateWeaponStats();
        }

        weaponDamage = weaponBaseDamage * 1+(_playerStatsScript.playerDamage/100);
        weaponAttackSpeed = weaponBaseAttackSpeed * 1+(_playerStatsScript.playerAttackSpeed/100);
        weaponRange = weaponBaseRange * 1+(_playerStatsScript.playerRange/100);
        weaponCritChance = weaponBaseCritChance * 1+(_playerStatsScript.playerCritChance/100);
    }

    public void UpdateWeaponStats(){
        //weaponDamage = weaponBaseDamage * 1+(_playerStatsScript.playerDamage/100);
        //weaponAttackSpeed = weaponBaseAttackSpeed * 1+(_playerStatsScript.playerAttackSpeed/100);
        //weaponBaseDamage += weaponBaseDamage * (_playerStatsScript.playerDamage/100);
    }
}
