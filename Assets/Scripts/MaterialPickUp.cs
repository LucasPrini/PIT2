using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPickUp : MonoBehaviour
{
    public float speed;
    public bool isPicked = false;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _playerOB;
    private PlayerStats _playerStatsScript;
    private float _playerDistance;
    
    private void Awake() {
        _playerOB = GameObject.FindGameObjectWithTag("Player");
        _playerStatsScript = _playerOB.GetComponent<PlayerStats>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance();
        if(_playerStatsScript.playerPickUpRange >= _playerDistance){
            isPicked = true;
        }
    }

    void FixedUpdate(){
        if(isPicked){
            MaterialCollected();
        }
    }

    void CheckPlayerDistance(){
        _playerDistance = Vector2.Distance(_playerOB.transform.position, this.transform.position);
    }



    public void MaterialCollected(){
        
        this.transform.position = Vector2.MoveTowards(this.transform.position, _playerOB.transform.position, speed * Time.deltaTime);
        /*if(this.transform.position == _playerOB.transform.position){
            
            Destroy(this.gameObject);
        }*/
    }

    
}
