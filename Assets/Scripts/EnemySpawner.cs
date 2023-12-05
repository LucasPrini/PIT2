using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _mapAreaOB;
    

    [SerializeField]  private GameObject _packSpawnOB;
    private Collider2D _mapAreaCol;
    [SerializeField] private Collider2D _closeSpawnArea;
    [SerializeField] private Collider2D _cantSpawnArea;
    [SerializeField] private Vector2 _canSpawnClosePositionsMax;
    [SerializeField] private Vector2 _canSpawnClosePositionsMin;
    [SerializeField] private float _canSpawnClosePositionsX;
    [SerializeField] private float _canSpawnClosePositionsY;
    [SerializeField] private bool _canSpawn;

    [SerializeField] private Vector2 _randomMapPos;
    

    private float _randomMapPosX;
    private float _randomMapPosY;

    [SerializeField] GameObject[] _enemyOB;
    [SerializeField] private GameObject _spawnWarningOB;

    public int enemiesPerSpawn;
    private GameObject[] _spawnWarningHolder = new GameObject[10];
    
    public int numberToSpawn;
    public int enemiesOnScreen;
    public int maxEnemiesSpawn;

    public int enemiesPerSpawnMax;
    
    private Vector2 _spawnPos;

    [SerializeField] private int _count;
    public bool isSpawnRoutineOn = false;

    void Awake(){
        _mapAreaCol = _mapAreaOB.GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RandomSpawn();
        if(isSpawnRoutineOn == false && (enemiesOnScreen <= maxEnemiesSpawn) && _canSpawn){
            StartCoroutine(SpawnRoutine());
        }

        //SpawnCloseToPlayer();

        if(Input.GetKeyDown("space")){
            
            
        }

        
        
    }

    /*void SpawnCloseToPlayer(){
        _canSpawnClosePositionsMax = new Vector2((_closeSpawnArea.bounds.max.x - _cantSpawnArea.bounds.max.x), (_closeSpawnArea.bounds.max.y - _cantSpawnArea.bounds.max.y));
        _canSpawnClosePositionsMin = new Vector2((_closeSpawnArea.bounds.min.x - _cantSpawnArea.bounds.min.x), (_closeSpawnArea.bounds.min.y - _cantSpawnArea.bounds.min.y));

        _canSpawnClosePositionsX = Random.Range(_canSpawnClosePositionsMin.x, _canSpawnClosePositionsMax.x);
        _canSpawnClosePositionsY = Random.Range(_canSpawnClosePositionsMin.y, _canSpawnClosePositionsMax.y);
        Vector2 _testpos = new Vector2(_canSpawnClosePositionsX, _canSpawnClosePositionsY);

        if(Input.GetKeyDown("space")){
            Instantiate(_enemyOB, _testpos, Quaternion.identity);
        }
        
    }*/


    void RandomSpawnPos(){
        

        
            _randomMapPosX = Random.Range(_mapAreaCol.bounds.min.x, _mapAreaCol.bounds.max.x);
            _randomMapPosY = Random.Range(_mapAreaCol.bounds.min.y, _mapAreaCol.bounds.max.y);

            _spawnPos = new Vector2(_randomMapPosX, _randomMapPosY);

            

            _count = _count + 1;

        
        

        _randomMapPos.x = _randomMapPosX;
        _randomMapPos.y = _randomMapPosY;
    }

    void SpawnEnemyWarning(int i){
        _spawnWarningHolder[i] = Instantiate(_spawnWarningOB, _spawnPos, Quaternion.identity) as GameObject;
    }
    void SpawnEnemy(Vector2 spawnPos){
        int randomizeEnemy;
        
        Instantiate(_enemyOB[Random.Range(0, _enemyOB.Length)], spawnPos, Quaternion.identity);
        enemiesOnScreen = enemiesOnScreen + 1;
    }

    private Vector2[] positionHolder = new Vector2[10];
    public float spawnChance;
    IEnumerator SpawnRoutine(){
        isSpawnRoutineOn = true;
        float typeOfSpawn = Random.Range(0,100);


        if(typeOfSpawn <= spawnChance){
            RandomSpawnPos();
            GameObject packSpawn = Instantiate(_packSpawnOB, _spawnPos, Quaternion.identity);
            for(int i = 0; i < 5; i++){
                
                Vector2 testpos = packSpawn.transform.GetChild(i).transform.position;                
                Instantiate(_enemyOB[0], testpos, Quaternion.identity);
            }

        }else{

            enemiesPerSpawn = Random.Range(2, enemiesPerSpawnMax + 1);
            
            for(int i = 0; i <= enemiesPerSpawn; i++){
            RandomSpawnPos();            
            positionHolder[i] = _spawnPos;          
            SpawnEnemyWarning(i);
                        
            
                     
            
            }
            yield return new WaitForSeconds(1.2f);
            

            for(int i = 0; i <= enemiesPerSpawn; i++){
                Destroy(_spawnWarningHolder[i]);
                SpawnEnemy(positionHolder[i]);
            }
        }
        
        yield return new WaitForSeconds(4);
        isSpawnRoutineOn = false;
        
        
        



    }
}
