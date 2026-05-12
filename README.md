# RF Auto Loot

Automated looting and mob killing for RF Online.  
Keystrokes and mouse clicks are sent **directly to the game window** — other apps are not affected.

---

## Requirements

- Windows 10 / 11
- **Visual Studio 2017–2022** (any edition, including Community)  
  or [Build Tools for Visual Studio](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2022)

NuGet packages are already included in the `packages/` folder — no internet needed.

---

## Build & Run

1. Open RF Online
2. Double-click **`build.bat`**  
   → builds the project and launches the app automatically

> If RF Online runs as Administrator — right-click `build.bat` → **Run as administrator**

> Press **ESC** at any time to close the bot

---

## First Run — DLL Fix (if app crashes immediately)

If the app crashes on startup with a native library error, copy these two files manually:

**From:**
```
packages\OpenCvSharp4.runtime.win.4.13.0.20260302\runtimes\win-x64\native\
```
**To:**
```
Auto Loot RF by Yasir Haq\bin\Release\
```

Files to copy:
- `OpenCvSharpExtern.dll`
- `opencv_videoio_ffmpeg4130_64.dll`

Then launch the `.exe` directly from `bin\Release\`.

---

## Interface

### KEY BINDINGS

| Control | Description |
|---------|-------------|
| **Click Coords X / Y** | Screen coordinates where the bot clicks to target a mob |
| **Pick** button | Click Pick → move your cursor to the mob target area → left-click to lock coordinates |
| **Attack Sequence** | Up to 5 keys pressed in order each attack cycle (`LMB`, `RMB`, `F1`–`F12`, `TAB`, letters) |
| **Delay (ms)** | Pause between each attack key (default 1500 ms) |
| **Loot Key** | Key pressed to pick up loot (default `X`) |

### TIMING

| Control | Description |
|---------|-------------|
| **Kill Time (sec)** | How long the bot attacks before switching to loot phase |
| **Loot Time (sec)** | How long the bot presses the loot key |

### MOB DETECTION

| Control | Description |
|---------|-------------|
| **Snip Template** | Select a screen region containing the mob — saved as a detection template |
| **Remove** | Delete the selected template |
| **Auto-target** | When enabled, bot uses template matching to click on the detected mob instead of fixed coords |
| **Threshold** | Match confidence (0.50–1.00). Lower = more lenient, higher = stricter |
| **Templates list** | Click a template to preview it. Double-click to open full 800×600 view |

---

## Modes

### START LOOT

Presses the loot key every 150 ms.  
Use when you attack manually and only need auto-looting.

**Start:** click `START LOOT`  
**Stop:** click the button again

### KILL + LOOT

Runs a full automatic cycle:

```
Phase 0 — Click target coords (or detected mob if Auto-target is on)
Phase 1 — Press Attack Sequence keys for Kill Time seconds
Phase 2 — Press Loot Key for Loot Time seconds
→ repeat
```

**Start:** click `KILL + LOOT`  
**Stop:** click the button again

---

## Key Format

| Input | Meaning |
|-------|---------|
| `LMB` | Left mouse button |
| `RMB` | Right mouse button |
| `F1` – `F12` | Function keys |
| `TAB`, `ENTER`, `SPACE` | Special keys |
| `X`, `Q`, `1` | Single character keys |

Leave attack slots empty to skip them.

---

## Troubleshooting

**Clicks not registering in game**  
RF Online requires the game window to be active for mouse input. The bot automatically brings the game to the foreground before each click and leaves it focused — make sure nothing else is stealing focus.

**App crashes on startup**  
See [First Run — DLL Fix](#first-run--dll-fix-if-app-crashes-immediately) above.

**Build fails**  
Ensure Visual Studio or Build Tools for VS 2017–2022 is installed. The error details appear in the console window.
