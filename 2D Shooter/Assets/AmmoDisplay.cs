using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    public Shooting shooting;
    Text ammoText;
    // Start is called before the first frame update
    void Start()
    {
        ammoText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int ammo = shooting.getAmmo();
        int maxAmmo = shooting.maxAmmo;
        ammoText.text = ammo + "/" + maxAmmo;
    }
}
