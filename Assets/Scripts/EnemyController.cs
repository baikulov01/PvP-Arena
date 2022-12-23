using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHP = 200;

    public int currentHP;


    //public AudioSource Eattack1;
    //public AudioSource Eattack2;
    //public AudioSource Fattack1;
    //public AudioSource Fattack2;
    //public AudioSource Fattack3;
    //public AudioSource Fattack4;

    //add health bar
    public Slider healthBar;

    private bool isDead;
    public GameManagerScript gameManager;


    //public Spell attackSpell;
    //public Spell firstSpell;
    //public Spell secondSpell;

    //public GameObject attackSpellPrefab;
    //public GameObject firstSpellPrefab;
    //public GameObject secondSpellPrefab;

    //public float currentAttackSpellCooldown;
    //public float currentFirstSpellCooldown;
    //public float currentSecondSpellCooldown;

    //public float maxAttackSpellCooldown = 1.0f;
    //public float maxFirstSpellCooldown = 3.0f;
    //public float maxSecondSpellCooldown;


    // Start is called before the first frame update
    void Start()
    {
        maxHP = 200;
        currentHP = maxHP;
        healthBar.value = currentHP;

        //maxAttackSpellCooldown = attackSpell.cooldown;
        //maxFirstSpellCooldown = firstSpell.cooldown;
        //maxSecondSpellCooldown = secondSpell.cooldown;

        //currentAttackSpellCooldown = maxAttackSpellCooldown;
        //currentFirstSpellCooldown = maxFirstSpellCooldown;
        //currentSecondSpellCooldown = maxSecondSpellCooldown;

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHP;

        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), GetComponent<Collider>());
        if (currentHP <= 0)
        {
            isDead = true;
            gameManager.gameOver();
            Time.timeScale = 0;
        }


        //if (currentAttackSpellCooldown > 0.0f)
        //{
        //    currentAttackSpellCooldown -= Time.deltaTime;
        //    attackSpellPrefab.GetComponent<Spell>().isActive = false;
        //}
        //else attackSpellPrefab.GetComponent<Spell>().isActive = true;


        //if (currentAttackSpellCooldown <= 0)
        //{
        //    //CastAttackSpell();
        //}

        //if (currentFirstSpellCooldown > 0.0f)
        //{
        //    currentFirstSpellCooldown -= Time.deltaTime;
        //    firstSpellPrefab.GetComponent<Spell>().isActive = false;
        //}
        //else firstSpellPrefab.GetComponent<Spell>().isActive = true;

        //if (currentFirstSpellCooldown <= 0)
        //{
        //    CastFirstSpell();
        //}



    }

    //нужно добавить: когда мы получаем урон, обновлять health bar 
    //healthBar.SetHealth(currentHP);

    //public void CastAttackSpell()
    //{
    //    if (attackSpellPrefab.GetComponent<Spell>().isActive)
    //    {
    //        currentAttackSpellCooldown = maxAttackSpellCooldown;
    //        Transform coorR = gameObject.transform;
    //        var fireball= Instantiate(attackSpellPrefab, new Vector3(coorR.position.x, coorR.position.y, coorR.position.z), coorR.transform.rotation);
    //        Physics.IgnoreCollision(fireball.GetComponent<Collider>(), GetComponent<Collider>());

    //    }
    //}
    //public void CastFirstSpell()
    //{
    //    if (firstSpellPrefab.GetComponent<Spell>().isActive)
    //    {
    //        currentFirstSpellCooldown = maxFirstSpellCooldown;
    //        Transform coorL = gameObject.transform;
    //        var spell = Instantiate(firstSpellPrefab, new Vector3(coorL.position.x, coorL.position.y, coorL.position.z), coorL.transform.rotation);
    //        Physics.IgnoreCollision(spell.GetComponent<Collider>(), GetComponent<Collider>());

    //    }
    //}
}
