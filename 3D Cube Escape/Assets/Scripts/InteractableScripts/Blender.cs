using System;
using UnityEngine;

// The blender: put something in (water comes from the tap), switch it on, pour it into a
// cup. Works through any of its recipes, as often as you like, in any order.
public class Blender : Interactable
{
    [Serializable]
    public class Recipe
    {
        public string item;            // what goes in, e.g. "mushroom"
        public GameObject juice;       // the hotbar icon it comes out as, e.g. "mushroom juice"
        public Color color = Color.white;
    }

    [SerializeField] private Recipe[] recipes = new Recipe[0];
    [SerializeField] private string cup = "cup";
    [Tooltip("Shown while something's in the jug; tinted with the recipe's colour once blended.")]
    [SerializeField] private Renderer contents;
    [SerializeField] private string sound;

    private Recipe loaded;
    private bool blended;

    public override void InteractionEvent() {
        if (inventory == null) inventory = FindFirstObjectByType<Inventory>();
        string held = inventory.SelectedItem;

        if (loaded == null) {
            var recipe = Array.Find(recipes, r => r.item == held);
            if (recipe == null) {
                Say(held != null ? "That won't go in the blender." : description);
                return;
            }
            loaded = recipe;
            inventory.DestroyCur();
            Show(new Color(0.75f, 0.85f, 0.95f, 1f));
            Say("You put the " + held + " in the blender and top it up with water from the tap.");
            return;
        }

        if (!blended) {
            if (held != null) {
                Say("There's already something in the blender. Switch it on.");
                return;
            }
            blended = true;
            Show(loaded.color);
            if (!string.IsNullOrEmpty(sound) && AudioManager.instance != null) AudioManager.instance.Play(sound);
            Say("Bzzzzzz. The " + loaded.item + " whirls round and round.");
            return;
        }

        string juice = Inventory.IdOf(loaded.juice);
        if (held != cup) {
            Say("The blender is full of " + juice + ". You'll need a cup to pour it into.");
            return;
        }
        inventory.ReplaceCur(loaded.juice);
        Say("You pour out a cup of " + juice + ".");
        loaded = null;
        blended = false;
        if (contents != null) contents.gameObject.SetActive(false);
    }

    private void Show(Color color) {
        if (contents == null) return;
        contents.gameObject.SetActive(true);
        contents.material.color = color;
    }
}
