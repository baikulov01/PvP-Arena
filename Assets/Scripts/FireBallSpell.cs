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
        castedBy = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ß = " + other.name + " " + castedBy);
        if ( other.tag == "Wall")
        {
            //Debug.Log(true);
            Destroy(gameObject);

        }
        if ( other.tag == "Enemy"  || other.tag == "Player")
        {
            EnemyController wizard = other.GetComponent<EnemyController>();

            wizard.currentHP -= damage;

            Destroy(gameObject);
        }
    }


}
