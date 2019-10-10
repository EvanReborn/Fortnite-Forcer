# Fortnite Forcer

A easy to use anti-cheat switcher for Fortnite.

## Usage

- Rename the original FortniteLauncher.exe to 'Original.exe' in the Win64 folder
- Move the FortniteLauncher.exe from the project to the Win64 folder
- Launch through EpicGamesLauncher

## How It Works

The modified launcher intercepts arguments from EpicGamesLauncher and generates new arguments that pertain to a specific anti-cheat.
```cs
_antiCheat = new Process
{
  StartInfo =
  {
    FileName               = EAC_EXECUTABLE,
    Arguments              = $"{formattedArgs} -nobe -fltoken=none",
    RedirectStandardOutput = true,
    RedirectStandardError  = true,
    UseShellExecute        = false
  }
};
```

## Known Issues

- Every Fortnite update, Epic updates the **-fltoken** arguments and changes the way tokens are generated. The current tokens supplied will not work next update, ```Evan#3797``` on discord will teach you how to get the latest tokens.
- If the runtime file swap fails or Epic adds some sort of new detection for this it will result in a game **kick**, but not a **ban**.

## License
[MIT](https://choosealicense.com/licenses/mit/)
