using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollButton : MonoBehaviour
{

    private GameObject _shopManagerOB;
    private ShopManager _shopManagerScript;

    private void Awake() {
        _shopManagerOB = GameObject.FindGameObjectWithTag("ShopManager");
        _shopManagerScript = _shopManagerOB.GetComponent<ShopManager>();
    }
    public void RerollShop(){
        _shopManagerScript.SetShop();
    }
}
