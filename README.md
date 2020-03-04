# Fortnite Forcer

A easy to use anti-cheat switcher for Fortnite.

**Evan#3797** is the sole creator and owner of this project.

**If you really need a forcer DM me on discord for a private build for chapter 2!**

## Usage

- Rename the original FortniteLauncher.exe to 'Original.exe' in the Win64 folder
- Move the FortniteLauncher.exe from the project to the Win64 folder
- Launch through [EpicGamesLauncher](https://launcher-public-service-prod06.ol.epicgames.com/launcher/api/installer/download/EpicGamesLauncherInstaller.msi?productName=unrealtournament)

## How It Works

The modified launcher intercepts arguments from EpicGamesLauncher and generates new arguments that pertain to a specific anti-cheat.
```cs
_antiCheat = new Process
{
  StartInfo =
  {
    FileName        = EAC_EXECUTABLE,
    Arguments       = $"{formattedArgs} -nobe -fromfl=eac -fltoken={EAC_TOKEN}",
    CreateNoWindow  = false
  }
};
```

## Known Issues

- Every Fortnite update, Epic updates the **-fltoken** arguments and changes the way tokens are generated. The current tokens supplied will not work next update, ```Evan#3797``` on discord will teach you how to get the latest tokens.
- If the runtime file swap fails or Epic adds some sort of new detection for this it will result in a game **kick**, but not a **ban**.
