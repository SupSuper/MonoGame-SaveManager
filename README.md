MonoGame Save Manager
======================

This is just a basic no-muss, no-fuss class written in C# to help you create a savefile for your games. The term "savefile" includes any kind of saving data to disk, such as options, profiles, save slots, etc.

> You just saved my ass, dude. It works perfectly and there's less
> end-of-stage save delay than my previous method. It's good shit
> and anybody else using MonoGame having problems should use it too.
>
> *- TheWhiteDragon*

Usage
------

There are two implementations available:

- **StorageDeviceSaveManager** - Uses the [StorageDevice](http://www.monogame.net/documentation/?page=T_Microsoft_Xna_Framework_Storage_StorageDevice) provided by the MonoGame/XNA Framework. 
- **IsolatedStorageSaveManager** - Uses the [IsolatedStorage](http://msdn.microsoft.com/en-us/library/3ak841sy.aspx) class provided by the .NET Framework. If you are targeting Windows Store, you [also need this](https://github.com/KonajuGames/MonoGame-Support/tree/master/System.IO.IsolatedStorage).

They both have the same functionality but target different platforms, so use the one that serves your needs. For example, **IsolatedStorageSaveManager** can be used without MonoGame.

The repository includes the source code for both as well as a sample `Game1.cs` demonstrating its use.

### Example

To create a new save file `mysave.dat` under the saves game folder `MyGameName` just do this:

```c#
SaveManager mySave = new IsolatedStorageSaveManager("MyGameName", "mysave.dat");
```

Or this:

```c#
SaveManager mySave = new StorageDeviceSaveManager("MyGameName", "mysave.dat", PlayerIndex.One);
```

Note that when using `StorageDevice`, you need to pass the current player's `PlayerIndex` (or *null* if it's a global save).

`SaveManager` uses a `SaveData` class that contains all the variables you wanna save:

```c#
[Serializable]
public class SaveData
{
    public string playerName;
    public int playerScore;
    public bool levelUnlocked;
}
```

You can put your own variables in there, even custom classes as long as they're `Serializable`.

To access the variables inside your save, you can just get at its `Data`:

```c#
mySave.Data.playerName = "Cool Dude";
mySave.Data.playerScore = 10000;
mySave.Data.levelUnlocked = true;
```

And when you finally need to load/save it:

```c#
mySave.Load();
mySave.Save();
```

It's that simple!

Changelog
----------

1.0

License
--------

MIT. Do whatever you want with it.
