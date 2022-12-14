using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public float damage = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            WizardController player = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardController>();
            if (player.schieldIsCasting)
            {
                player.currentSchieldHP -= damage;
            }
            else
            {
                player.currentHP -= 20;
            }
        }
        
    }
}
