# 3D Cube Escape

A 3D take on Rusty Lake's *Cube Escape: Seasons*: the same puzzles, but in a first-person room you
walk around in. Pick a season's cube on the level select, solve the room, and escaping it unlocks
the next season: Spring 1964, Summer 1971, Autumn 1971, Winter 1981.

**Unity 6.3** (6000.3.5f2), built-in render pipeline with Post Processing. Scenes in build order:
`StartMenu`, `LevelSelection`, `SpringLevel`, `SummerLevel`, `FallLevel`, `WinterLevel`. Every
season is the same apartment, changed: Summer is a moonlit night, Autumn has red trees and blood,
Winter has snow outside and the window boarded up.

## Controls

| Action | Key |
|---|---|
| Move / look | WASD / mouse |
| Pick something up, or use the selected item on something (it lights up when you're close enough) | Left click |
| Select a hotbar item | 1-0 or mouse wheel (press the same number again to put it away) |
| Switch to the 1-bit camera | Q |
| Pause | Esc or E |

## Walkthroughs (spoilers)

Each room ends the same way: fill the frame above the fireplace with its four pieces (Spring has no
pieces), then put the season's cube in the hollow behind it.

### Spring 1964

1. Collect the **matches** (coffee table), the **spoon** (kitchen drawer), the **firewood** (kitchen
   cabinet), the **pot** (cabinet left of the stove) and the **bird food**.
2. Feed the crow in the bedroom. It lays an **egg**.
3. Use the pot on the kitchen tap, then take the **pot of water** from the sink.
4. Put it on the stove, turn the red knob, light the burner with the matches, drop the egg in, and
   once it's boiled fish it out with the spoon.
5. Put the **boiled egg** in the pink egg cup by the sink and tap it six times with the spoon: a
   **black cube**.
6. Stack the wood in the fireplace and light it.
7. Put the cube behind the painting above the fireplace.

### Summer 1971

1. Take the **telescope** (coffee table), the **matches** (drawer of the end table by the clock),
   the **screwdriver** (drawer of the end table in the room behind the door), the **cassette**
   (mantelpiece) and the **plug** (on top of the radio).
2. Use the telescope on the window: **1487** is scratched into the moon, backwards.
3. Switch on the light by the kitchen door: a **key** hangs from the living-room light.
4. The key opens the padlock on the safe by the fireplace; dial 1487 for a **shrimp**.
5. Unstick the oven door with the screwdriver, turn the red knob, light the oven with the matches,
   put the shrimp in. When it's done, look inside: a lake, and the **black cube**.
6. Play the cassette in the radio and pull out the chewed **tape**. Throw it in the fireplace and
   burn it.
7. Photo pieces: under the clock, under the end table, buried in the flowerpot (dig three times),
   and under the floor lamp once you've fitted the plug to its cord and tightened it with the
   screwdriver.

### Autumn 1971

1. Take the **knife** from the end table's drawer.
2. Photo pieces: behind the notes on the cork board, behind the kitchen tap, inside Harvey (cut him
   open, then reach in three times), and from the eye in the ceiling (click it six times until it
   shuts).
3. Switch on the TV. Each channel is a clue to the next: **3, 6, 1, 2, 4, 5, 3, 2, 5, 4**. A wrong
   channel gives static and the trail starts again from 3. The last channel shows the **black
   cube**. The finished photo is dated 12-10-1971.

### Winter 1981

1. Take the **matches** (end table drawer) and the **hatchet** (by the fireplace).
2. Hack the four boards off the window: **firewood**, and a blueprint piece.
3. The radio's clock reads **13:10**: click the hands of the wall clock to set it to 1:10, and it
   opens on the **blue cube**.
4. Blueprint pieces: the window, behind the old photographs on the cork board, in the oven, and
   taped to the fuel can by the fireplace.
5. Stack the firewood in the fireplace and light it; the cube won't go in while the room is
   freezing.

## How the puzzles are built

Everything is configured in the inspector:

- **`Pickup`**: an item in the room. `itemButton` is its hotbar icon (a prefab in
  `Assets/Assets/Prefabs/Pickups`), and the icon's name in lower case is the item's id
  (`"Matches"` becomes `matches`). World models for pickups are the `...Model` prefabs next to them.
- **`PuzzleInteractable`**: a list of **steps** done in order. Each step can need an item selected
  (`item`), use it up or not, need **flags** set earlier (`needs`), take several clicks, set a
  flag, hand over a new item (`giveItem`), and run `onDone` (switch objects on or off, start a
  `DelayedStep`, open a `SwingOpen`, end the room). Each step has hint lines for the wrong item,
  "not yet", each click, and done.
- **`UseItemTrigger`**: use an item as often as you like, e.g. the telescope at the window.
- **`CodeWheel`** + **`CodeLock`**: combinations. Each wheel steps through its values when
  clicked, shown as a digit (the safe) or by turning (the clock hands); the lock opens when they
  all match its answer, optionally only once some flags are set (the padlock).
- **`ChannelTV`** + **`ChannelButton`**: a TV whose channels each show the next clue on a route.
- **`PeekView`**: a camera you look through for a few seconds (the telescope).
- **`SwingOpen`**: swings a door open (the oven, the safe).
- **`Interactable`**: something just worth a look; its `description` is shown when clicked.
- **`DelayedStep`**: sets a flag after a few seconds (the egg boiling, the shrimp cooking).
- **`RoomEnding`**: glitches and fades out, unlocks the next season (saved in PlayerPrefs) and goes
  back to the level select.
- **`HintText`**: the line of text above the hotbar; any script can call `HintText.Show("...")`.
- **`SceneMusic`**: which theme the AudioManager plays in a scene.

Flags reset every time a room loads; unlocked seasons are kept between sessions.

## Notes

- Five models were `.blend` files, which Unity only imports with Blender installed. They're now
  `.fbx` (exported with Unity's own Blender-to-FBX script, under the same GUIDs, so the prefabs that
  use them didn't change).
- Models with an outline highlight need Read/Write enabled in their import settings (QuickOutline
  reads their vertices).
- Three plastic flowers in the living-room vase (part of the apartment kit) have no mesh in the
  repo, so they don't show.
- Not built from the original: the optional extras (achievements, the secret ending's jars,
  blender and juices).
