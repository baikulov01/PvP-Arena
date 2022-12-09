using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : Spell
{
    public float moveSpeed = 0;
    public new int damage = 30;
    public float lifeTime = 1f;
    public bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 2.0f;
    }

    //Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            //Debug.Log(true);
            Destroy(gameObject);
        }
        //����������� ������� ����� ��������� ������� �����
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            return;
        }
        if ((other.tag == "Enemy") && flag)
        {
            flag = false;
        }
        if (other.tag == "Enemy")
        {
            WizardController wizard = other.GetComponent<WizardController>();

            wizard.currentHP -= damage;

        }
    }

}
