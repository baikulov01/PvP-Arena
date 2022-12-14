using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardController : MonoBehaviour
{
    static int maxHP=200;

    public int currentHP;

    public Transform leftController;
    public Transform rightController;

    public AudioSource Eattack1;
    public AudioSource Eattack2;
    public AudioSource Eattack3;
    public AudioSource Eattack4;
    public AudioSource Eattack5;
    public AudioSource Eattack6;
    public AudioSource Eattack7;

    public AudioSource Fattack1;
    public AudioSource Fattack2;
    public AudioSource Fattack3;
    public AudioSource Fattack4;

    public AudioSource Schield1;


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
    //public float currentSecondSpellCooldown;

    public float maxAttackSpellCooldown = 1.0f;
    public float maxFirstSpellCooldown = 3.0f;
    //public float maxSecondSpellCooldown;

    public float maxSchieldHP = 100.0f;
    public float currentSchieldHP;

    public bool schieldIsCasting;
    public bool schieldIsActive;

    // Start is called before the first frame update
    void Start()
    {

        currentHP = maxHP;
        healthBar.value = currentHP;

        currentSchieldHP = maxSchieldHP;
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
        //////////////////////////////////////////////
        

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Q))
        {
            if (CastSecondSpell())
            {
                //GameObject schield = GameObject.FindGameObjectWithTag("Schield");
                schieldIsCasting = true;
            }
            
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp(KeyCode.Q))
        {
            GameObject schield = GameObject.FindGameObjectWithTag("Schield");
            //schield.GetComponent<SchieldSpell>().isCasting = false;
            schieldIsCasting = false;
            Destroy(schield);
        }

        ////////////////////////////////////////
        //GameObject schield = GameObject.FindGameObjectWithTag("Schield");

        if (currentSchieldHP < maxSchieldHP && !schieldIsCasting)
        {
            currentSchieldHP += 4*Time.deltaTime;
        }

        if (currentSchieldHP <= 0)
        {
            schieldIsActive = false;
            schieldIsCasting = false;
            currentSchieldHP = 0;
            Destroy(GameObject.FindGameObjectWithTag("Schield"));
        }

        if (currentSchieldHP >= maxSchieldHP * 0.2)
        {
            schieldIsActive = true;
        }

        //Debug.Log(currentSchieldHP);

    }


    public void CastAttackSpell()
    {
        if (attackSpellPrefab.GetComponent<Spell>().isActive)
        {
            int rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    Fattack1.Play();
                    break;
                case 1:
                    Fattack2.Play();
                    break;
                case 2:
                    Fattack3.Play();
                    break;
                case 3:
                    Fattack4.Play();
                    break;
                default:
                    break;
            }

            currentAttackSpellCooldown = maxAttackSpellCooldown;
            Transform coorR = rightController.transform;
            var fireball = Instantiate(attackSpellPrefab, new Vector3(coorR.position.x, coorR.position.y, coorR.position.z), coorR.transform.rotation);
            Physics.IgnoreCollision(fireball.GetComponent<Collider>(),GetComponent<Collider>());
            var schield = GameObject.FindGameObjectWithTag("Schield");
            Physics.IgnoreCollision(fireball.GetComponent<Collider>(), schield.GetComponent<Collider>());


            


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

            int rand = Random.Range(0, 6);
            switch (rand)
            {
                case 0:
                    Eattack1.Play();
                    break;
                case 1:
                    Eattack2.Play();
                    break;
                case 2:
                    Eattack3.Play();
                    break;
                case 3:
                    Eattack4.Play();
                    break;
                case 4:
                    Eattack5.Play();
                    break;
                case 5:
                    Eattack6.Play();
                    break;
                case 6:
                    Eattack7.Play();
                    break;
                default:
                    break;
            }
            
        }
    }

    public bool CastSecondSpell()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (schieldIsActive && !schieldIsCasting)
        {
            var spell = Instantiate(secondSpellPrefab,
                        new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
                        gameObject.transform.rotation, player.transform);
            Schield1.Play();
            Physics.IgnoreCollision(spell.GetComponent<Collider>(), GetComponent<Collider>());
            return true;
        }
        else return false;
    }
}
