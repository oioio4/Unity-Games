using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public PokemonBase _base { get; set; }
    public int level { get; set; }

    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public Pokemon(PokemonBase pBase, int pLevel) {
        _base = pBase;
        level = pLevel;
        HP = Health;

        Moves = new List<Move>();
        foreach (var move in _base.LearnableMoves) {
            if (move.Level <= level) {
                Moves.Add(new Move(move.Base));
            }

            if (Moves.Count >= 4) {
                break;
            }
        }
    }

    public int Health {
        get { return (int) ((_base.HP * level) / 100f) + 10; }
    }
    public int Attack {
        get { return (int) ((_base.Attack * level) / 100f) + 5; }
    }
    public int Defense {
        get { return (int) ((_base.Defense * level) / 100f) + 5; }
    }
    public int SpAttack {
        get { return (int) ((_base.SpAttack * level) / 100f) + 5; }
    }
    public int SpDefense {
        get { return (int) ((_base.SpDefense * level) / 100f) + 5; }
    }
    public int Speed {
        get { return (int) ((_base.Speed * level) / 100f) + 5; }
    }
}
