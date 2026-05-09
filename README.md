# RF Auto Loot

Automated looting and mob killing for RF Online.  
Keystrokes are sent **directly to the game window** — browser and other apps are not affected.

---

## Quick Start

1. Open RF Online
2. Run `build.bat` — the project will build and launch automatically
3. Click **Refresh**, select the game window from the list
4. Configure your keys and click **START LOOT** or **KILL + LOOT**

> If RF Online runs as Administrator — run `build.bat` as Administrator too.

---

## Files

| File | Description |
|------|-------------|
| `build.bat` | Builds the project and launches the app |
| `Auto Loot RF by Yasir Haq.sln` | Visual Studio solution (for editing source code) |
| `Auto Loot RF by Yasir Haq\bin\Release\` | Output folder with the compiled `.exe` |

---

## Requirements

- Windows 7 / 10 / 11
- Visual Studio 2017 / 2019 / 2022 (any edition, including Community)  
  or [Build Tools for Visual Studio](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2022)

---

## Interface

```
+---------------------------------------------+
| RF AUTO LOOT                                |
| RF Online Automation Tool                   |
+---------------------------------------------+
| GAME WINDOW                                 |
| [rfonline]  RF Online Client    [Refresh]   |
+---------------------------------------------+
| KEY BINDINGS                                |
| Target Key      Attack Key      Loot Key    |
|   [TAB]           [F1]            [X]       |
+---------------------------------------------+
| TIMING                                      |
| Kill Time  [5] sec      Loot Time  [3] sec  |
+---------------------------------------------+
|  [> START LOOT]       [x KILL + LOOT]      |
+---------------------------------------------+
| * Ready -> [rfonline]                       |
+---------------------------------------------+
```

---

## Configuration

### Game Window

- Click **Refresh** — the list will populate with all open windows
- Select the game window (usually `[rfonline]  RF Online`)

> If you opened the game after launching the bot — click **Refresh** again.

### Key Bindings

| Field | Purpose | Examples |
|-------|---------|---------|
| **Target Key** | Select nearest mob | `TAB`, `Q` |
| **Attack Key** | Attack / use skill | `F1`, `F2`, `1`, `2` |
| **Loot Key** | Pick up items | `X`, `Z` |

Supported formats: single characters (`X`, `Q`, `1`), function keys (`F1`–`F12`), special keys (`TAB`, `ENTER`, `SPACE`).

### Timing (Kill + Loot mode only)

- **Kill Time** — how many seconds the bot attacks before switching to loot
- **Loot Time** — how many seconds the bot presses the loot key

Adjust based on how fast your character kills mobs.

---

## Modes

### START LOOT

Presses the loot key every 150 ms. Use this when you attack manually and only need auto-looting.

**Start:** click `> START LOOT`  
**Stop:** click the same button again

### KILL + LOOT

Runs a full automatic cycle:

```
[1] Target -> press Target Key once
[2] Attack -> press Attack Key for Kill Time seconds
[3] Loot   -> press Loot Key for Loot Time seconds
-> repeat
```

**Start:** click `x KILL + LOOT`  
**Stop:** click the same button again

---

## Status Bar

| Message | Meaning |
|---------|---------|
| `* Ready -> [rfonline]` | Window selected, bot is ready |
| `* Looting -> [rfonline]` | Start Loot mode is running |
| `* Kill + Loot -> [rfonline]` | Kill + Loot mode is running |
| `* No game window selected` | No window selected, buttons are disabled |

---

## Troubleshooting

**Keys are not reaching the game**  
Make sure the correct window is selected and click Refresh. If RF Online runs as Administrator, launch `build.bat` as Administrator too.

**Game window not in the list**  
Open the game first, then click Refresh.

**Build error in build.bat**  
Make sure Visual Studio or Build Tools is installed. The error details will be shown in the console window.

---

*Original project: [github.com/yasirrhaq](https://github.com/yasirrhaq)*
