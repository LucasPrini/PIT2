using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    
    public GameObject[] weaponsToBuy;
    private int _shopItemID;
    public WeaponStats[] _weaponStatsScript;
    [SerializeField] private ShopBuffEffects _shopBuffsScript;
    private GameObject _playerOB;
    private PlayerStats _playerStatsScript;
    private PlayerWeapons _playerWeaponsScript;

    //Shop Objects References
    [SerializeField] private GameObject _shopItemOne;

    [SerializeField] private GameObject[] _itemsOB;
    [SerializeField] private TextMeshProUGUI _materialsText;

    [SerializeField] private Button[] _buyButtons;
    [SerializeField] private TextMeshProUGUI[] _weaponNameTexts;
    [SerializeField] private Image[] _weaponImages;
    [SerializeField] private TextMeshProUGUI[] _damageTexts;
    [SerializeField] private TextMeshProUGUI[] _attackRateTexts;
    [SerializeField] private TextMeshProUGUI[] _CritChanceTexts;
    [SerializeField] private TextMeshProUGUI[] _RangeTexts;
    [SerializeField] private TextMeshProUGUI[] _buffTexts;


    

    [SerializeField] private GameObject[] _weaponImagesOB;
    [SerializeField] private GameObject[] _weaponTextsOB;
    [SerializeField] private GameObject[] _weaponStatsOB;
    [SerializeField] private GameObject[] _buffTextsOB;
    

    //

    public int buffsPrices;
    public int shopWeaponPrice;

    private void Awake() {
       _shopBuffsScript = GetComponent<ShopBuffEffects>(); 
       _playerOB = GameObject.FindGameObjectWithTag("Player");
       _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
       _playerWeaponsScript = _playerOB.GetComponent<PlayerWeapons>();
    }

    private void Start() {
        
    }

    private void Update() {
        UpdateMaterials();
        if(Input.GetKeyDown("space")){
            SetShop();
        }
    }

    private int RandomItemType;
    private int RandomBuff;


    void UpdateMaterials(){
        _materialsText.text = ("Materials: " + _playerStatsScript.materials);
    }

    
    public void SetShop(){

        RandomItemType = Random.Range(1,3);

        
        for(int i = 0; i < 4; i++){
            RandomItemType = Random.Range(1,3);
            _itemsOB[i].SetActive(true);
            
            if(RandomItemType == 1 && _playerWeaponsScript.weaponSlot < 4){

                _weaponImagesOB[i].SetActive(true);
                _weaponTextsOB[i].SetActive(true);
                _weaponTextsOB[i].SetActive(true);
                _weaponStatsOB[i].SetActive(true);
                _buffTextsOB[i].SetActive(false);
                

                _weaponStatsScript[i] = weaponsToBuy[i].GetComponent<WeaponStats>();
                TextMeshProUGUI _buyButtonText = _buyButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                _buyButtonText.text = ("Price: " + _weaponStatsScript[i].weaponPrice);

                ItemButtons buyButtonScript = _buyButtons[i].GetComponent<ItemButtons>();
                shopWeaponPrice = _weaponStatsScript[i].weaponPrice;
                if(_playerStatsScript.materials >= _weaponStatsScript[i].weaponPrice){
                    _buyButtonText.color = Color.green;
                }else{
                    _buyButtonText.color = Color.red;
                }
                buyButtonScript.shopItemID = i;
                buyButtonScript.itemPrice = _weaponStatsScript[i].weaponPrice;
                buyButtonScript.isWeapon = true;
                buyButtonScript.isBuff = false;
                //shopItemID = i;
                _damageTexts[i].text = ("Damage: " + _weaponStatsScript[i].weaponBaseDamage.ToString() + "("+_weaponStatsScript[i].weaponType+")");
                _weaponNameTexts[i].text = (_weaponStatsScript[i].weaponName);
                _attackRateTexts[i].text = ("Attack Rate: " + _weaponStatsScript[i].weaponBaseAttackSpeed);
                _weaponImages[i].sprite = (_weaponStatsScript[i].weaponImage);
                _CritChanceTexts[i].text = ("Crit Chance: " + _weaponStatsScript[i].weaponBaseCritDamage +"x" +"(" +_weaponStatsScript[i].weaponBaseCritChance + "%" + ")" );
                _RangeTexts[i].text = ("Range: " + _weaponStatsScript[i].weaponBaseRange);
            }else{
                _weaponImagesOB[i].SetActive(false);
                _weaponTextsOB[i].SetActive(false);
                _weaponTextsOB[i].SetActive(false);
                _weaponStatsOB[i].SetActive(false);
                _buffTextsOB[i].SetActive(true);
                TextMeshProUGUI _buyButtonText = _buyButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                ItemButtons buyButtonScript = _buyButtons[i].GetComponent<ItemButtons>();
                buyButtonScript.shopItemID = i;
                buyButtonScript.isWeapon = false;
                buyButtonScript.isBuff = true;
                
                RandomBuff = Random.Range(0, 5);
                _buyButtonText.text = ("Price: " + buffsPrices);
                buyButtonScript.itemPrice = buffsPrices;

                if(_playerStatsScript.materials >= buffsPrices){
                    _buyButtonText.color = Color.green;
                }else{
                    _buyButtonText.color = Color.red;
                }
                
                
                if(RandomBuff == 1){
                    _buffTexts[i].text = _shopBuffsScript.attackRateBuff.ToString() + "%" +" " + _shopBuffsScript.attackeRateDescription;
                    
                    buyButtonScript.buffID = 1;
                }else if(RandomBuff == 2){
                    _buffTexts[i].text = _shopBuffsScript.damageBuff.ToString() + "%" +" " + _shopBuffsScript.damageBuffDescription;
                    buyButtonScript.buffID = 2;
                }else if(RandomBuff == 3){
                    _buffTexts[i].text = _shopBuffsScript.rangeBuff.ToString() + "%" +" " + _shopBuffsScript.rangeBuffDescription;
                    buyButtonScript.buffID = 3;
                }else if(RandomBuff == 4){
                    _buffTexts[i].text = _shopBuffsScript.critChanceBuff.ToString() + "%" +" " + _shopBuffsScript.critChanceBuffDescription;
                    buyButtonScript.buffID = 4;
                }
                

            }
            
        }
    }
}
