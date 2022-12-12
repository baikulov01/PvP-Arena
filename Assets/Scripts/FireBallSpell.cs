using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpell : Spell
{
    public float moveSpeed = 0.25f;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if ( other.tag == "Wall")
        {
            //Debug.Log(true);
            Destroy(gameObject);

        }
        if ( other.tag == "Enemy" )
        {
            EnemyController wizard = other.GetComponent<EnemyController>();

            wizard.currentHP -= damage;
            Destroy(gameObject);
        } else if ( other.tag == "Player")
        {
            WizardController wizard = other.GetComponent<WizardController>();

            wizard.currentHP -= damage;

            Destroy(gameObject);
        }



    }


}
