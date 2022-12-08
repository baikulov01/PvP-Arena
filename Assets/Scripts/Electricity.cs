using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : Spell
{
    public float moveSpeed = 0;
    public int damage = 15;
    public float lifeTime = 0.1f;
    public bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Debug.Log(true);
            Destroy(gameObject);
        }
        //����������� ������� ����� ��������� ������� �����
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && flag)
        {
            Debug.Log(true);
            //other.hp-=damage;
            //������ �� � ������� ������;
            flag = false;
            //����� �� ������ ���� ���� ���, �� ���������� ������ �������

        }
    }

}
