using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchieldSpell : Spell
{
    //public float maxSchieldHP=60.0f;
    //public float currentSchieldHP = 60.0f;

    //public bool isCasting;

    // Start is called before the first frame update
    void Start()
    {
        //currentSchieldHP = 60.0f;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentSchieldHP < maxSchieldHP && !isCasting)
        //{
        //    currentSchieldHP += Time.deltaTime;
        //}

        //if (currentSchieldHP<=0)
        //{
        //    isActive = false;
        //    isCasting = false;
        //    currentSchieldHP = 0;
        //}

        //if (currentSchieldHP>=maxSchieldHP/2)
        //{
        //    isActive = true;
        //}

        //Debug.Log(currentSchieldHP);
    }

}
