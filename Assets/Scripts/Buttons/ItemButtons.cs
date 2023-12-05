using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButtons : MonoBehaviour
{
    [SerializeField] GameObject _playerOB;
    [SerializeField] PlayerWeapons _playerWeaponsScript;
    [SerializeField] private PlayerStats _playerStatsScript;
    [SerializeField] private WeaponStats _weaponStatsScript;

    [SerializeField] GameObject _gameManagerOB;
    private GameManagerController _gameManagerScript;

    private GameObject _shopManagerOB;
    private ShopManager _shopManagerScript;
    private ShopBuffEffects _ShopBuffScript;
    public int shopItemID;
    public bool isWeapon;
    public bool isBuff;
    public int buffID;
    public int itemPrice;
    TextMeshProUGUI _thisButtonText;

    private void Awake() {
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _playerWeaponsScript = _playerOB.GetComponent<PlayerWeapons>();
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();

        _gameManagerOB = GameObject.FindGameObjectWithTag("GameManager");
        _gameManagerScript = _gameManagerOB.GetComponent<GameManagerController>();

        _shopManagerOB = GameObject.FindGameObjectWithTag("ShopManager");
        _shopManagerScript = _shopManagerOB.GetComponent<ShopManager>();
        _ShopBuffScript = _shopManagerOB.GetComponent<ShopBuffEffects>();
        _thisButtonText = GetComponentInChildren<TextMeshProUGUI>();

        


    }

    private void Update() {
        UpdateButtons();
    }
    
    public void BuyWeaponOnClick(){

        if(isWeapon && _playerWeaponsScript.weaponSlot < 5 && _playerStatsScript.materials >=_shopManagerScript.shopWeaponPrice ){            
            _playerWeaponsScript.playerWeapons[_playerWeaponsScript.weaponSlot] = _shopManagerScript.weaponsToBuy[shopItemID];
            _playerWeaponsScript._weaponsActive[_playerWeaponsScript.weaponSlot] = true;
            _playerWeaponsScript.weaponSlot++;
            int thisWeaponPrice = _shopManagerScript.shopWeaponPrice;
            //_playerStatsScript.materials -= thisWeaponPrice;
            _playerStatsScript.materials -= itemPrice;
            this.transform.parent.gameObject.SetActive(false); 
        }else if(isBuff && _playerStatsScript.materials >= _shopManagerScript.buffsPrices){
            _ShopBuffScript.IncreasePlayerStats(buffID);
            this.transform.parent.gameObject.SetActive(false);
            _playerStatsScript.materials -= _shopManagerScript.buffsPrices;
        }

        

        




        
            
            
        
        
    }

    void UpdateButtons(){
        if((isWeapon && itemPrice > _playerStatsScript.materials) || isBuff && itemPrice > _playerStatsScript.materials){
            _thisButtonText.color = Color.red;
        }else{
            _thisButtonText.color = Color.green;
        }
    }

    public void GivePlayerWeapon(){

    }

    public void GoToNextWave(){
        _gameManagerScript.NextWave();
    }

    


}
