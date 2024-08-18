using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoScript : MonoBehaviour
{
    [SerializeField] private GameObject weapon_generic;
    [SerializeField] private TMP_Text ammoText;

    [SerializeField] private weapon_generic weapongenericscript;

    // Start is called before the first frame update
    void Awake()
    {
        //weapongenericscript = weapon_generic.GetComponent<weapon_generic>();
        ammoText = GetComponent<TMP_Text>();
        ammoText.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = weapongenericscript.currentClipSize.ToString();
    }
}
