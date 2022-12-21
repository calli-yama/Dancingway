using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Data;
using Dalamud.Game;
using Dalamud.IoC;
using CommandManager = Dalamud.Game.Command.CommandManager;



namespace Dancingway
{
    internal class Service
    {
        [PluginService] public static DataManager DataManager { get; private set; } = null!;
        [PluginService] public static CommandManager CommandManager { get; private set; } = null!;

    }
}
