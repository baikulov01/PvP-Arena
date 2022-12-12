using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardController : MonoBehaviour
{
    static int maxHP=100;

    public int currentHP;

    public Transform leftController;
    public Transform rightController;

    public AudioSource Eattack1;
    public AudioSource Eattack2;
    public AudioSource Fattack1;
    public AudioSource Fattack2;
    public AudioSource Fattack3;
    public AudioSource Fattack4;


    //add health bar
    public Slider healthBar;


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

    public bool flag = true;
    public byte flag2 = 0;
    public float currentSoundColdown;
    public float maxSoundColdown = 5.0f;
    public bool soundIsReady = true;
    // Start is called before the first frame update
    void Start()
    {
        currentSoundColdown = maxSoundColdown;
        currentHP = maxHP;
        healthBar.value = currentHP;

        //maxAttackSpellCooldown = attackSpell.cooldown;
        //maxFirstSpellCooldown = firstSpell.cooldown;
        //maxSecondSpellCooldown = secondSpell.cooldown;

        //currentAttackSpellCooldown = maxAttackSpellCooldown;
        //currentFirstSpellCooldown = maxFirstSpellCooldown;
        //currentSecondSpellCooldown = maxSecondSpellCooldown;
        //Debug.Log(transform.position);
        //CastAttackSpell();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHP;
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

        if (!soundIsReady)
        {
            currentSoundColdown -= Time.deltaTime;
        }   
        if (currentSoundColdown <= 0 && !soundIsReady)
        {
            soundIsReady = true;
            currentSoundColdown = maxSoundColdown;
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


    public void CastAttackSpell()
    {
        if (attackSpellPrefab.GetComponent<Spell>().isActive)
        {
            currentAttackSpellCooldown = maxAttackSpellCooldown;
            Transform coorR = leftController.transform;
            var fireball = Instantiate(attackSpellPrefab, new Vector3(coorR.position.x, coorR.position.y, coorR.position.z), coorR.transform.rotation);
            Physics.IgnoreCollision(fireball.GetComponent<Collider>(),GetComponent<Collider>());

            if (soundIsReady)
            {
                if (flag2 == 0)
                {
                    Fattack1.Play();
                }
                else if (flag2 == 1)
                {
                    Fattack2.Play();
                }
                else if (flag2 == 2)
                {
                    Fattack3.Play();
                }
                else
                {
                    Fattack4.Play();
                    flag2 = 0;
                    return;
                }
                flag2++;
                soundIsReady = false;
            }
            


        }
    }
    public void CastFirstSpell()
    {
        if (firstSpellPrefab.GetComponent<Spell>().isActive)
        {
            currentFirstSpellCooldown = maxFirstSpellCooldown;
            Transform coorL = rightController.transform;
            var spell = Instantiate(firstSpellPrefab, new Vector3(coorL.position.x, coorL.position.y, coorL.position.z), coorL.transform.rotation);
            Physics.IgnoreCollision(spell.GetComponent<Collider>(), GetComponent<Collider>());
            if (flag){
                Eattack2.Play();
                flag = false;
            }
            else
            {
                Eattack1.Play();
                flag = true;
            }
            
        }
    }
}
