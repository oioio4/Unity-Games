# 3D Cube Escape

A 3D take on Rusty Lake's *Cube Escape: Seasons*: the same puzzles, but in a first-person room you
walk around in. Pick a season's cube on the level select, solve the room, and escaping it unlocks
the next season.

**Unity 6.3** (6000.3.5f2), built-in render pipeline with Post Processing. Scenes in build order:
`StartMenu`, `LevelSelection`, `SpringLevel`.

## Controls

| Action | Key |
|---|---|
| Move / look | WASD / mouse |
| Pick something up, or use the selected item on something (it lights up when you're close enough) | Left click |
| Select a hotbar item | 1-0 or mouse wheel (press the same number again to put it away) |
| Switch to the 1-bit camera | Q |
| Pause | Esc or E |

## Spring (spoilers)

1. Collect the **matches** (coffee table), the **spoon** (kitchen drawer), the **firewood** (kitchen
   cabinet), the **pot** (cabinet left of the stove) and the **bird food**.
2. Feed the crow in the bedroom. It lays an **egg**.
3. Use the pot on the kitchen tap, then take the **pot of water** from the sink.
4. Put it on the stove, turn the red knob, light the burner with the matches, drop the egg in, and
   once it's boiled fish it out with the spoon.
5. Put the **boiled egg** in the pink egg cup by the sink and tap it six times with the spoon: a
   **black cube**.
6. Stack the wood in the fireplace and light it.
7. Put the cube behind the painting above the fireplace. Spring ends and Summer unlocks.

## How the puzzles are built

Everything is configured in the inspector, so new seasons need no new scripts:

- **`Pickup`**: an item in the room. `itemButton` is its hotbar icon (a prefab in
  `Assets/Assets/Prefabs/Pickups`), and the icon's name in lower case is the item's id
  (`"Matches"` becomes `matches`).
- **`PuzzleInteractable`**: a list of **steps** done in order. Each step can need an item selected
  (`item`), use it up or not, need **flags** set earlier (`needs`), take several clicks (the egg
  takes 6), set a flag, hand over a new item (`giveItem`), and run `onDone` (switch objects on or
  off, start a `DelayedStep`, end the room). Each step has hint lines for the wrong item, "not yet",
  each click, and done.
- **`Interactable`**: something just worth a look; its `description` is shown when clicked.
- **`DelayedStep`**: sets a flag after a few seconds (the egg boiling).
- **`RoomEnding`**: glitches and fades out, unlocks the next season (saved in PlayerPrefs) and goes
  back to the level select.
- **`HintText`**: the line of text above the hotbar; any script can call `HintText.Show("...")`.

Flags reset every time a room loads; unlocked seasons are kept between sessions.

## Notes

- Five models were `.blend` files, which Unity only imports with Blender installed. They're now
  `.fbx` (exported with Unity's own Blender-to-FBX script, under the same GUIDs, so the prefabs that
  use them didn't change).
- Three plastic flowers in the living-room vase (part of the apartment kit) have no mesh in the
  repo, so they don't show.
- Summer, Fall and Winter aren't built yet; their cubes say so when clicked.
