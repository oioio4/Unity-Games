using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePokemon : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerPokemon;

    public Pokemon pokemon { get; set; }

    public void Setup() {
        pokemon = new Pokemon(_base, level);
        if (isPlayerPokemon) {
            GetComponent<Image>().sprite = pokemon._base.BackSprite;
        }
        else {
            GetComponent<Image>().sprite = pokemon._base.FrontSprite;
        }
    }
}
