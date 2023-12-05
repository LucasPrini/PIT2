using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _destroyTIme;
    private float _time;

    
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(this.gameObject, _destroyTIme);
    }

    // Update is called once per frame
    void Update()
    {
        _time =+ Time.deltaTime;
        if(_time >= _destroyTIme){
            DestroyObject(this.gameObject);
        }
    }

    
}
