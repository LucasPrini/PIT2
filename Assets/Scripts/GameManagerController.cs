using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManagerController : MonoBehaviour
{
    //UI References
    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private GameObject _healthBarUIText;
    private TextMeshProUGUI _healthBarUITMP;
    private Slider _healthBarSlider;  


    [SerializeField] private GameObject _waveTextOB;
    private TextMeshProUGUI _waveTextUI; 
    [SerializeField] private GameObject _waveCountDownOB;
    private TextMeshProUGUI _waveCountDownUI;

    [SerializeField] private GameObject _ShopUIOB;



    [SerializeField] private GameObject _materialsTextOB;
    private TextMeshProUGUI _materialsTextUI;


    //end

    //Enemy References
    public float enemyHealthIncrease;
    //

    public float timeRemaining;
    public float lastWaveTime;
    public bool isOutOfTime;

    //Player References
    [SerializeField] private GameObject _playerOB;
    [SerializeField] private PlayerStats _playerStatsScript;
    //end

    //Shop Reference
    [SerializeField] private GameObject _shopManagerOB;
    private ShopManager _shopManagerScript;
    //

    //Pause Menu
    [SerializeField] GameObject _pauseMenuOB;
    [SerializeField] GameObject _deathScreen;
    //end


    //EnemySpawner References
    [SerializeField] private GameObject _gameSpawnerOB;
    private EnemySpawner _enemySpawnerScript;

    //end

    //Objects References
    [SerializeField] private GameObject _materialOB;
    //end


    public int waveNumber;
    public bool gamePaused;

    

    private void Awake() {
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
        _healthBarUITMP = _healthBarUIText.GetComponent<TextMeshProUGUI>();
        _healthBarSlider = _healthBarUI.GetComponent<Slider>();

        _waveTextUI = _waveTextOB.GetComponent<TextMeshProUGUI>();
        _waveCountDownUI = _waveCountDownOB.GetComponent<TextMeshProUGUI>();

        _materialsTextUI = _materialsTextOB.GetComponent<TextMeshProUGUI>();

        _enemySpawnerScript = _gameSpawnerOB.GetComponent<EnemySpawner>();

        _shopManagerScript = _shopManagerOB.GetComponent<ShopManager>();

    }

    private void Start() {
        _deathScreen.SetActive(false);
        _pauseMenuOB.SetActive(false);
        waveNumber = 1;
    }

    private void Update() {
        _healthBarUITMP.text = ("HP   " + _playerStatsScript.playerCurrentHealth.ToString() + "/" + _playerStatsScript.playerMaxHealth.ToString() );
        _healthBarSlider.value = _playerStatsScript.playerCurrentHealth/_playerStatsScript.playerMaxHealth;

        UpdateMaterialsText();

        StartWave();
        ChangeWaveUIText();
        if(timeRemaining <= 0){
            isOutOfTime = true;
            
            OnWaveEnd();
            timeRemaining = 1;

        }

        KillPlayer();

        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1){
            OpenPauseMenu();
        }else if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0){
            ClosePauseMenu();
        }
        
    }

    private void KillPlayer(){
        if(_playerStatsScript.playerCurrentHealth <= 0){
            //Destroy(_playerOB);
            _playerOB.SetActive(false);
            _deathScreen.SetActive(true);
            Time.timeScale = 0;
        }else if(_playerStatsScript.playerCurrentHealth > 0 && gamePaused == false){
            Time.timeScale = 1;
        }
    }

    private void OpenPauseMenu(){
        _pauseMenuOB.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu(){
        _pauseMenuOB.SetActive(false);
        Time.timeScale = 1;
    }

    private void UpdateMaterialsText(){
        _materialsTextUI.text = ("Materials: " + _playerStatsScript.materials.ToString());
    }

    public void StartWave(){
        timeRemaining -= Time.deltaTime;
    }

    private void ChangeWaveUIText(){
        _waveTextUI.text = ("Wave " + waveNumber.ToString());

        _waveCountDownUI.text = (Mathf.Floor(timeRemaining).ToString());
    }

    private void OnWaveEnd(){
        
        if(timeRemaining <= 0){
            Time.timeScale = 0;
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject Enemy in enemies){
                Destroy(Enemy);
            }

            _ShopUIOB.SetActive(true);
            _shopManagerScript.SetShop();
            gamePaused = true;
            
        }
    }

    public void NextWave(){
        waveNumber = waveNumber + 1;
        timeRemaining = lastWaveTime + 5;
        lastWaveTime = timeRemaining;
        enemyHealthIncrease += 10;
        Time.timeScale = 1;
        gamePaused = false;
        _ShopUIOB.SetActive(false);
        _playerStatsScript.playerCurrentHealth = _playerStatsScript.playerMaxHealth;

        _enemySpawnerScript.numberToSpawn += 1;
        _enemySpawnerScript.enemiesPerSpawnMax += 1;


    }


    

    
}
