# 3D Cube Escape

A 3D take on Rusty Lake's *Cube Escape: Seasons*: the same puzzles, but in a first-person room you
walk around in. Pick a season's cube on the level select, solve the room, and escaping it unlocks
the next season: Spring 1964, Summer 1971, Autumn 1971, Winter 1981. Like the original, there's a
secret ending: Winter's clock takes you back to the earlier seasons, and what you do there changes
the future.

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

Each room ends the same way: fill the frame above the fireplace with its four pieces, then put the
season's cube in the hollow behind it.

### Spring 1964

1. Collect the **matches** (coffee table), the **spoon** (kitchen drawer), the **firewood** (kitchen
   cabinet), the **pot** (cabinet left of the stove) and the **bird food**.
2. Feed the crow in the bedroom. It lays an **egg**.
3. Use the pot on the kitchen tap, then take the **pot of water** from the sink.
4. Put it on the stove, turn the red knob, light the burner with the matches, drop the egg in, and
   once it's boiled fish it out with the spoon.
5. Put the **boiled egg** in the pink egg cup by the sink and tap it six times with the spoon: a
   **black cube**, and a photo piece.
6. The other photo pieces: knock on the window three times; switch on the light by the kitchen door
   (a piece is caught in the ceiling light it turns on, in the room behind the door); look behind
   the radio.
7. Stack the wood in the fireplace and light it.
8. Put the four pieces in the frame above the fireplace, then the cube behind it.

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

### The secret ending

Each earlier season's clock has stopped: Spring at **9:05**, Summer at **3:55**, Autumn at **11:25**.
In Winter, set the wall clock to one of those times (its hands still turn after 1:10) and click its
face: you're back in that season, and a few things are there that weren't before. Click that
room's clock face to go forward to Winter again. What you do in the past is remembered (saved
between sessions) and Winter changes to match; Winter itself starts over each time you come back.

1. **Spring, 9:05.** A **key** and a **coat hanger** have fallen out of the clock.
   - Put the hanger on the radio as an aerial: FM 105.0, and Harvey whistles along.
   - Feed Harvey, then use the key on the padlock of his cage. He flies off out of the window.
   - The safe by the fireplace opens with Summer's **1487**: a **seed**. Dig the flower up with the
     spoon and plant the seed. (The safe and the flowerpot work on an ordinary visit too.)
   - Light the candle.
2. **Summer, 3:55.** A **wrench** on the kitchen counter: loosen the joint of the water pipe on the
   kitchen wall. Light the candle.
3. **Autumn, 11:25.** **Matches** in the drawer of the end table in the room behind the door: light
   what's left of the candle.
4. **Winter.**
   - The candle has burned all the way down. Chip the wax away with the hatchet: **Prozac**.
   - The seed is a cactus now: pick its **fruit**. Under the leaking pipe: mould, and a **mushroom**.
   - Turn the tuning knob on the radio to FM 105.0 and take the boards off the window: Harvey flies
     in with a **flower**.
   - Take the **cup** by the sink. For each of the four: put it in the blender, switch it on, pour
     it into the cup, and pour that into its tank by the north wall. The note on the cork board
     says which: flower one dot, cactus fruit two, mushroom three, pills four.
   - Pull the switch beside the tanks.
5. **Spring, 9:05, again.** The phone is ringing. Answer it, then go to the woman by the window.

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
  clicked, shown as a digit (the safe, the radio's station) or by turning (the clock hands); the
  lock opens when they all match its answer, optionally only once some flags are set (the
  padlock). `keepTurning` leaves the wheels working after it opens (Winter's clock).
- **`ChannelTV`** + **`ChannelButton`**: a TV whose channels each show the next clue on a route.
- **`PeekView`**: a camera you look through for a few seconds (the telescope).
- **`SwingOpen`**: swings a door open (the oven, the safe).
- **`Interactable`**: something just worth a look; its `description` is shown when clicked.
- **`DelayedStep`**: sets a flag after a few seconds (the egg boiling, the shrimp cooking).
- **`RoomEnding`**: glitches and fades out, unlocks the next season (saved in PlayerPrefs) and goes
  back to the level select.
- **`HintText`**: the line of text above the hotbar; any script can call `HintText.Show("...")`.
- **`SceneMusic`**: which theme the AudioManager plays in a scene.
- **`TimeTravel`**: a clock face. In Winter it reads two `CodeWheel`s as a time and loads the season
  that time belongs to; in an earlier season, while visiting, it goes back to Winter.
- **`Past`**: `Past.Travel(scene)` loads a season as a visit from Winter (`Past.Visiting`), and
  `Remember("seed")` (from a step's `onDone`) saves something done in the past.
- **`Echo`**: shows and hides objects when a room starts, or when `Apply()` is called, if things
  were remembered from earlier seasons, flags are set, or only while visiting: the cactus, the
  burned-down candle, Harvey coming back, the ringing phone.
- **`Blender`**: put an item in, switch it on, pour it into a cup. Any of its recipes, any order.

Flags reset every time a room loads; unlocked seasons and what you did in the past are kept between
sessions (PlayerPrefs `unlocked_<Season>`, `past_<thing>`).

## Notes

- Five models were `.blend` files, which Unity only imports with Blender installed. They're now
  `.fbx` (exported with Unity's own Blender-to-FBX script, under the same GUIDs, so the prefabs that
  use them didn't change).
- Models with an outline highlight need Read/Write enabled in their import settings (QuickOutline
  reads their vertices).
- Three plastic flowers in the living-room vase (part of the apartment kit) have no mesh in the
  repo, so they don't show.
- The woman and the shadow are the player's own mannequin model, with a skirt and hair added.
- Web build: it uses the project's WebGL template (`Assets/WebGLTemplates/CubeEscape`, Unity's
  default page plus two changes: it ignores the browser refusing pointer lock, because a room locks
  the cursor before the player has clicked and Unity's loader would show an error alert for it; and it
  renders at the page's resolution, not the screen's pixel density). Decompression fallback is on,
  because itch.io doesn't serve the compressed files with the headers Unity otherwise needs. In a
  browser, click the game to capture the mouse; Esc releases it, so pause with E.
- Not built from the original: achievements.

## Performance

The apartment kit and the props were built for screenshots, not for a browser. What keeps it fast:

- Only the sun casts shadows. The room's point lights don't (each one re-drew the whole apartment
  six times a frame), nor do small props.
- The mirror's reflection probe renders once, as the room loads, not every frame.
- Dense models (the 490k-triangle clock, the 120k egg cup, the curtains, the blender...) have
  decimated stand-ins in `Assets/Assets/Meshes/Optimized`, about 8k triangles each, made with
  Blender's Decimate. They're named after the original mesh and its model's GUID; the scenes use
  them in place of the originals, colliders included.
- The walls, floors and furniture that never move are static: batched into fewer draw calls, and
  occlusion-culled (from inside one room the walls hide the others). If the apartment changes,
  re-bake occlusion culling for each room scene (Window > Rendering > Occlusion Culling).
- Quality: Windows builds use Ultra (soft sun shadows to 30 m, 3 per-pixel lights); the web build
  uses High (no shadows, 1 per-pixel light, the rest per vertex). No screen-space reflections;
  bloom in fast mode.
- The web build caps textures at 1024 px and crunch-compresses them, and compresses the music
  harder. Hotbar icons are 128 px pixel art (4 px per art pixel), uncompressed.
- `Benchmark`: start a build with `-benchmark SpringLevel,WinterLevel`, or open the web build with
  `?benchmark=SpringLevel`, and it turns the player round on the spot and logs frame times and draw
  counts (`[Benchmark] ...`).

Measured: the web build went from 5.3 to 165 fps in Spring on a laptop's integrated Intel GPU, and
its download from 203 MB to 61 MB. The Windows build holds 240 fps (the display's refresh rate) in
Spring and Winter on an RTX 4070 laptop GPU.
