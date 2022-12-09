using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public float cooldown;
    public bool isActive = true;
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            WizardController wizard = other.GetComponent<WizardController>();

            wizard.currentHP -= damage;

            //Destroy(gameObject);
        }
    }


}
