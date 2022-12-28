using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattlePokemon playerPokemon;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattlePokemon enemyPokemon;
    [SerializeField] BattleHUD enemyHud;
    [SerializeField] DialogBox dialogBox;

    BattleState state;
    int currentAction;
    int currentMove;

    private void Start() {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle() {
        playerPokemon.Setup();
        playerHud.SetData(playerPokemon.pokemon);
        enemyPokemon.Setup();
        enemyHud.SetData(enemyPokemon.pokemon);

        dialogBox.SetMoveNames(playerPokemon.pokemon.Moves);

        yield return dialogBox.TypeDialog($"A wild {enemyPokemon.pokemon._base.name} appeared.");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    private void PlayerAction() {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    private void PlayerMove() {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    private void Update() {
        if (state == BattleState.PlayerAction) {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove) {
            HandleMoveSelection();
        }
    }

    private void HandleActionSelection() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (currentAction < 1) {
                currentAction++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (currentAction > 0) {
                currentAction--;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z)) {
            if (currentAction == 0) {
                PlayerMove();
            }
            else {

            }
        }
    }

    private void HandleMoveSelection() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (currentMove < playerPokemon.pokemon.Moves.Count - 1) {
                currentMove++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (currentMove > 0) {
                currentMove--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (currentMove < playerPokemon.pokemon.Moves.Count - 2) {
                currentMove += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (currentMove > 1) {
                currentMove -= 2;
            }
        }

        dialogBox.UpdateMoveSelection(currentMove, playerPokemon.pokemon.Moves[currentMove]);
    }
}
