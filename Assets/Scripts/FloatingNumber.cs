using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingNumber : MonoBehaviour
{
    public TextMeshPro textM;

    void Awake(){
        textM = GetComponent<TextMeshPro>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, 0.9f) * Time.deltaTime);   
    }

    public void ChangeText(float damageTaken){
        textM.text = Mathf.FloorToInt(damageTaken).ToString();
    }
}
