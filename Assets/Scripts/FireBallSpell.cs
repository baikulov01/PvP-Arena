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

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Debug.Log(true);
            Destroy(gameObject);

        }
        if (other.tag == "Enemy")
        {
            WizardController wizard = other.GetComponent<WizardController>();

            wizard.currentHP -= damage;

            Destroy(gameObject);
        }
    }


}
