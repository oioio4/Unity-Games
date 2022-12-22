using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HealthBar hpBar;

    public void SetData(Pokemon pokemon) {
        nameText.text = pokemon._base.name;
        levelText.text = "Lvl " + pokemon.level;
        hpBar.SetHP((float) pokemon.HP / pokemon.Health);
    }
}
