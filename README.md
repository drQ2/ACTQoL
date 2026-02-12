# Another Crab's Quality of Life (ACTQoL) - Performance Fork

A focused fork of the original *Another Crab's Treasure QoL* mod, dedicated to **Item Tracking** and **Performance**.

## About This Fork
This version was created to solve severe performance issues in the original mod.
- **Removed:** "Pre-Title Skip", "Auto Start", and "Shell Health Regen".
- **Refocused:** The mod now purely handles **Crystal & Item ESP**.
- **Optimized:** Rewrote the scanning logic to eliminate lag spikes caused by constant background processing.

## Features

### 💎 Crystal Map Tracker
- Displays locations of uncollected crystals on the map.
- Automatically hides collected crystals.

### 🗺️ Item Map Tracker
- Displays locations of uncollected items on the map.
- Automatically hides collected items.

### ⚡ Performance Optimization
- **Zero Lag:** Previous versions scanned the entire world every frame, causing massive FPS drops.
- **Manual Rescan:** The mod now only scans for items when you press **F5**. This eliminates background processing entirely.
- **Smart Caching:** Item sprites are cached in memory to avoid constant loading.

## Controls

| Key | Action |
| :--- | :--- |
| **F5** | **Rescan World:** Updates the map markers. Press this when entering a new area or if you notice missing icons. |
| **M** | **Toggle ESP:** Turns the map icons on or off instantly. |

## 🛠️ How to Compile (Advanced)

If you want to modify the code or build the DLL yourself, follow these steps.

### Prerequisites
1.  **Download .NET SDK 6.0**: [Link to Microsoft](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
    - Install the SDK version (not just the runtime).
    - Verify it's installed by opening a terminal (Command Prompt or PowerShell) and typing `dotnet --version`.

### Build Steps
1.  **Download Source**: Download this repository as a ZIP or clone it.
2.  **Open Terminal**: 
    - Right-click the folder containing `ACTQoL.csproj`.
    - Select "Open in Terminal".
3.  **Run Build**:
    - Type the following command and press Enter:
      ```powershell
      dotnet build
      ```
    - You should see "Build succeeded" in green text.
4.  **Find the File**:
    - Go to the folder: `bin/Debug/netstandard2.0/`
    - You will find `ACTQoL.dll` inside.

## 💾 Installation

1.  **Install BepInEx**:
    - If you don't have it, download BepInEx 5.4.21 (x64) from [GitHub](https://github.com/BepInEx/BepInEx/releases).
    - Extract it into your game folder:
      `Steam\steamapps\common\Another Crab's Treasure`
2.  **Install the Mod**:
    - Take the `ACTQoL.dll` file you built (or downloaded).
    - Place it in: `Another Crab's Treasure\BepInEx\plugins`
    - (Create the `plugins` folder if it doesn't exist).
3.  **Launch Game**:
    - Run the game through Steam. The mod will load automatically.
    - Press **M** to verify it is working (check BepInEx console if enabled).

## ❓ Troubleshooting
note: icons automatically disappear during combat. You can make them reappear by pausing and unpausing then rescanning

- **"I don't see any icons!"**
  - Press **F5** to force a rescan.
  - Make sure you are not in a cutscene or menu.
  - Press **M** to ensure ESP is toggled ON.

- **"The mod isn't loading!"**
  - Ensure you copied the file to `BepInEx/plugins`.
  - Check `BepInEx/LogOutput.log` for errors.
