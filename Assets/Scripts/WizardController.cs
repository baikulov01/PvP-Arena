using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    static int maxHP=100;

    public int currentHP;

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

    public float maxAttackSpellCooldown;
    public float maxFirstSpellCooldown;
    public float maxSecondSpellCooldown;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);

        //maxAttackSpellCooldown = attackSpell.cooldown;
        maxAttackSpellCooldown = 1.0f;
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


        if (OVRInput.Get(OVRInput.Button.One) || Input.GetMouseButton(0))
        {
            if (currentAttackSpellCooldown <= 0)
            {
                CastAttackSpell();
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
            Transform coor = gameObject.transform;
            Instantiate(attackSpellPrefab, new Vector3(coor.position.x, coor.position.y, coor.position.z), gameObject.transform.rotation);
        }
    }
}
