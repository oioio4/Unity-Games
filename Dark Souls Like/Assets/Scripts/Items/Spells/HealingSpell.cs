using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(AnimatorHandler animatorHandler, PlayerStats playerStats) {
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
            //Destroy(instantiatedWarmUpSpellFX, 0.5f);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
        }

        public override void SuccessfullyCastSpell(AnimatorHandler animatorHandler, PlayerStats playerStats) {
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            //Destroy(instantiatedSpellFX, 0.3f);
            playerStats.HealPlayer(healAmount);
        }
    }
}
