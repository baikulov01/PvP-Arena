using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconsScripts : MonoBehaviour
{
    public Image schieldImg;
    public float schieldFillAmount;

    public Image ElectricityImg;
    public float ElectricityFillAmount;
    // Start is called before the first frame update
    void Start()
    {
        schieldImg.color = new Color(0.615f, 0.47f, 0.91f);
    }

    // Update is called once per frame
    void Update()
    {

        float currSchieldHp = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardController>().currentSchieldHP;
        float maxSchieldHp = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardController>().maxSchieldHP;

        schieldImg.fillAmount = currSchieldHp / maxSchieldHp;
        //Debug.Log(currSchieldHp);

        if (currSchieldHp>=0.2* maxSchieldHp)
        {
            schieldImg.color = new Color(0.615f, 0.47f, 0.91f);
        }
        else
        {
            schieldImg.color = new Color(1f, 1f, 1f);
        }
        //////////////////////////////
        float currElectr = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardController>().currentFirstSpellCooldown;
        float maxElectr = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardController>().maxFirstSpellCooldown;
        ElectricityImg.fillAmount = 1- currElectr / maxElectr;
    }
}
