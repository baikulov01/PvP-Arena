using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public float cooldown;
    public bool isActive = true;
    public int damage;
    public string castedBy;
    
    // Start is called before the first frame update
    void Start()
    {
        castedBy = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ß = " + other.name + " " + other.tag);
        if (other.tag == "Player" || other.tag == "Enemy" )
        {
            EnemyController wizard = other.GetComponent<EnemyController>();

            wizard.currentHP -= damage;

            //Destroy(gameObject);
        }
    }


}
