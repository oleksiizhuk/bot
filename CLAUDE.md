# RF Auto Loot — project context (for Claude Code)

C# WinForms (.NET Framework, target net4.6.1) bot for **RF Online**. Injects keys via
PostMessage+AttachThreadInput and clicks via physical mouse (SetCursorPos + mouse_event).
Two modes: loot-only, and KILL+LOOT. Supports up to 3 game windows. User communicates in **Russian**.

Solution: `Auto Loot RF by Yasir Haq.sln`.

## Build (NO Visual Studio installed)
Only MSBuild available: `C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe`
- This is an **old compiler → C# 5 only**: no string interpolation `$""`, no `?.`, no
  expression-bodied members, no `nameof`.
- OpenCvSharp gotcha: use `Mat.Get<float>(r,c)`, NOT `Mat.At<float>` (At returns `ref`,
  which the C#5 compiler rejects — CS0570).
- Build (incremental, keeps native DLLs):
  `MSBuild.exe "Auto Loot RF by Yasir Haq.sln" -t:Build -p:Configuration=Release`
- After a full **Rebuild** (clean), re-copy native DLLs into `bin/Release/` from
  `packages/OpenCvSharp4.runtime.win.*/runtimes/win-x64/native/`:
  `OpenCvSharpExtern.dll` + `opencv_videoio_ffmpeg4130_64.dll`. (Plain Build keeps them.)
- The exe locks itself while running — close the app before rebuilding.

## Source files
- `Form1.cs` / `Form1.Designer.cs` — UI. Checkboxes: "AI detect" (YOLO), "Motion detect",
  "Auto-target" (templates), "Collect screens" (saves frames to `bin/Release/dataset/`).
  `LoadYoloModel()` loads `bin/Release/mobs.onnx` (disables checkbox if missing/bad).
- `LootBot.cs` — KILL+LOOT is a **closed-loop vision controller** (background Task, not a
  timer FSM): states Acquire → Engage → Loot. Acquire locks nearest mob within
  `EngageFrac=0.45` of screen centre; if none, **blind fallback** (attack at configured
  coords/centre + loot) so it works even when detection fails. Engage attacks the locked
  target until its detection disappears (`MissScansToKill=2`) = dead, `TolFrac=0.16` keeps
  the lock as it moves; `killSec` (UI "Kill time") is a MAX-engage safety cap. Loot spams
  loot key for `lootSec`. `FindMobs` prefers YOLO, falls through to motion/templates when
  YOLO returns empty. Writes diagnostics to `bin/Release/debug.log`; saves the frame fed to
  the net as `cap_debug.jpg` (iter==2).
- `YoloDetector.cs` — loads ONNX via `CvDnn.ReadNetFromOnnx`, letterbox 640, output
  [1,5,8400], NMS. `DetectScreenPoints(hwnd)` returns all mob points (screen coords, player
  centre zone excluded by `PlayerExcludeFrac=0.18`). `Net.Forward` is wrapped in `lock` (3
  windows share one Net; Forward is not thread-safe). `Confidence=0.20`. Diagnostic props:
  `LastMaxScore/LastRawCount/LastOutRows/LastOutCols`.
- `MobDetector.cs` — OpenCV motion (frame-diff 400ms) + template matching. `BitmapToMat`
  locks `Format24bppRgb` → BGR Mat (BlobFromImage swapRB=true → RGB, correct for YOLO).
- `InputSender.cs` — keys (PostMessage+AttachThreadInput, background-safe); mouse Click
  (SetForegroundWindow + SetCursorPos + mouse_event, restores cursor).
- `ScreenCapture.cs` — captures the window **client area** in screen coords (clicks can't
  land on chrome). `GameFinder.cs`, `SnipOverlay.cs`, `Win32.cs`.

## In-game facts (user's server)
- Tab-targeting does NOT work; mobs have NO nameplates until hovered → nameplate-colour
  detection rejected. Mob types: Crow Splinter/Warbeast (winged lion), Crawler Maul (green
  insectoid), Naiad Heller (blue mantis), Robust Lava, dark armored beetle/golem quadrupeds.
- User plays at **1600×900** windowed; the camera is locked on the player, so the unit at the
  exact screen centre is ALWAYS the player (rhino/beast mount or armored MAU mech) — never a mob.

## AI mob-detection saga / status (2026-06-21)
Goal: YOLOv8n ONNX to target mobs. Training is done by the USER in Google Colab
(`training/train_colab.py` style cell), then `mobs.onnx` is placed in `bin/Release/`.
- v1 model: recall 0.35 (trained on close-up crops with huge boxes ~0.92×0.88 → learned
  "mob fills screen", failed on real small/centred mobs).
- v2 full-frame: collected 140 frames, labeled by 14 parallel agents (visual estimates).
  Trained but **dead** — maxScore ~0.014 everywhere (mAP50 0.13, P≈0, best at epoch 2).
  Cause: labels too noisy/inconsistent + many tiny boxes + only 124 train imgs.
- **v3 (current, usable but weak)**: filtered labels to keep only big boxes (w&h≥0.06 →
  255 boxes/105 imgs +35 backgrounds), retrained (SGD lr0=0.01 cos_lr 200ep patience=0
  close_mosaic=30 single_cls). Result mAP50 0.117 but **val maxconf up to 0.769** — alive,
  detects clear mobs on ~half of frames. Deployed with Confidence 0.20 + motion fallback.

## NEXT STEPS
1. Live-test the v3 model in KILL+LOOT with **both** "AI detect" and "Motion detect" on.
   Check `bin/Release/debug.log`: maxScore should now be 0.2–0.7 (was 0.014).
2. For STABLE AI (not "every other frame"): recollect **300–600+ frames at 1600×900** with
   the bot's own window OFF the game field (it covered half the screen in `cap_debug.jpg`),
   label them **cleanly & consistently** (makesense.ai — tight boxes, no tiny specks, resolve
   player-mount-vs-mob), then retrain.
3. Planned combat upgrades (only #1 done): #2 survival (read player HP bar top-left, heal/flee
   on low HP — needs heal key), #3 loot confirmation (kill-counter OCR or target HP bar).
4. Cleanup once happy: remove diagnostic logging + `SaveDebugCapture` from `LootBot`.

## First run on a new machine
1. `git clone https://github.com/oleksiizhuk/bot.git` → open Claude Code in `bot/Auto_Loot`
   (it reads this file automatically).
2. Restore NuGet packages if `packages/` is missing: run `nuget.exe restore` (the
   `nuget.exe` is committed at repo root) or build once — packages are committed anyway.
3. Build: `"<path>\MSBuild.exe" "Auto Loot RF by Yasir Haq.sln" -t:Build -p:Configuration=Release`
   (need .NET Framework 4.6.1 dev pack or the Framework64\v4.0.30319 MSBuild).
4. Copy the non-git assets into `bin/Release/` (see list below) — at minimum `mobs.onnx`,
   or re-download it from the latest Colab training run.
5. After a clean Rebuild, copy native DLLs into `bin/Release/` (see Build section).

## NOT in git (gitignored: bin/, obj/, *.zip) — sync separately between machines
`bin/Release/mobs.onnx` (the model), `bin/Release/dataset/` (collected frames),
`bin/Release/labels_full/` + `labels_big/` (YOLO labels), `dataset_yolo_*.zip` (Colab datasets).
Carry these on a USB / cloud drive, or re-download `mobs.onnx` from the Colab run.
