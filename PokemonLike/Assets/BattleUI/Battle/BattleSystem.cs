using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattlePokemon playerPokemon;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattlePokemon enemyPokemon;
    [SerializeField] BattleHUD enemyHud;

    private void Start() {
        SetupBattle();
    }

    public void SetupBattle() {
        playerPokemon.Setup();
        playerHud.SetData(playerPokemon.pokemon);
        enemyPokemon.Setup();
        enemyHud.SetData(enemyPokemon.pokemon);
    }
}
