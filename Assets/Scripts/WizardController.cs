using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    static int maxHP=100;

    public int currentHP;

    public Transform leftController;
    public Transform rightController;

    //add health bar
    public HealthBar healthBar;


    //public Spell attackSpell;
    //public Spell firstSpell;
    //public Spell secondSpell;

    public GameObject attackSpellPrefab;
    public GameObject firstSpellPrefab;
    public GameObject secondSpellPrefab;

    public float currentAttackSpellCooldown;
    public float currentFirstSpellCooldown;
    public float currentSecondSpellCooldown;

    public float maxAttackSpellCooldown = 1.0f;
    public float maxFirstSpellCooldown = 2.0f;
    public float maxSecondSpellCooldown;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);

        //maxAttackSpellCooldown = attackSpell.cooldown;
        //maxFirstSpellCooldown = firstSpell.cooldown;
        //maxSecondSpellCooldown = secondSpell.cooldown;

        //currentAttackSpellCooldown = maxAttackSpellCooldown;
        //currentFirstSpellCooldown = maxFirstSpellCooldown;
        //currentSecondSpellCooldown = maxSecondSpellCooldown;
        Debug.Log(transform.position);
        CastAttackSpell();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAttackSpellCooldown > 0.0f)
        {
            currentAttackSpellCooldown -= Time.deltaTime;
            attackSpellPrefab.GetComponent<Spell>().isActive = false;
        } 
        else attackSpellPrefab.GetComponent<Spell>().isActive = true; 


        if (OVRInput.Get(OVRInput.RawButton.A) || Input.GetMouseButton(0))
        {
            if (currentAttackSpellCooldown <= 0)
            {
                CastAttackSpell();
            }
        }


        if (currentFirstSpellCooldown > 0.0f)
        {
            currentFirstSpellCooldown -= Time.deltaTime;
            firstSpellPrefab.GetComponent<Spell>().isActive = false;
        }
        else firstSpellPrefab.GetComponent<Spell>().isActive = true;


        if (OVRInput.Get(OVRInput.RawButton.B) || Input.GetMouseButton(1))
        {
            if (currentFirstSpellCooldown <= 0)
            {
                CastFirstSpell();
            }
        }


    }

    //нужно добавить: когда мы получаем урон, обновлять health bar 
    //healthBar.SetHealth(currentHP);

    public void CastAttackSpell()
    {
        if (attackSpellPrefab.GetComponent<Spell>().isActive)
        {
            currentAttackSpellCooldown = maxAttackSpellCooldown;
            Transform coorR = leftController.transform;
            //Transform coorR = gameObject.transform;
            Instantiate(attackSpellPrefab, new Vector3(coorR.position.x, coorR.position.y, coorR.position.z), coorR.transform.rotation);
        }
    }
    public void CastFirstSpell()
    {
        if (firstSpellPrefab.GetComponent<Spell>().isActive)
        {
            currentFirstSpellCooldown = maxFirstSpellCooldown;
            Transform coorL = rightController.transform;
            Instantiate(firstSpellPrefab, new Vector3(coorL.position.x, coorL.position.y, coorL.position.z), coorL.transform.rotation);
        }
    }
}
