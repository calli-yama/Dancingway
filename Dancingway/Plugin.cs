using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using System.Reflection;
using Dalamud.Interface.Windowing;
using Dancingway.Windows;
using XivCommon.Functions;
using XivCommon;
using System.Dynamic;

namespace Dancingway
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Dancingway";

        private DalamudPluginInterface PluginInterface { get; init; }
        private CommandManager CommandManager { get; init; }

        private XivCommonBase XivCommon { get; }

        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("Dancingway");

        public Dancingway.DanceList emoteLister = new DanceList();

        public Plugin(DalamudPluginInterface pluginInterface)
        {
            pluginInterface.Create<Service>();

            this.PluginInterface = pluginInterface;

            XivCommon = new XivCommonBase();

            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);

            // you might normally want to embed resources and load them from the manifest stream
            var imagePath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "goat.png");
            var goatImage = this.PluginInterface.UiBuilder.LoadImage(imagePath);

            WindowSystem.AddWindow(new ConfigWindow(this));
            WindowSystem.AddWindow(new MainWindow(this, goatImage));

            // [2.0 feature] for when the plugin is turned on,just display our main ui
            // until viewing/editing of the cached list is implemented, don't need this
            /*
            this.CommandManager.AddHandler("/dancingway", new CommandInfo(OnDancingway)
            {
                HelpMessage = "Open Dancingway settings window. Use '/dancingway'."
            });
            */

            // for when the Dancingway Decision Roulette is called without modifiers
            Service.CommandManager.AddHandler("/ddr", new CommandInfo(OnDDR)
            {
                HelpMessage = "Executes a random dance from the chosen list. Use '/ddr'."
            });

            this.PluginInterface.UiBuilder.Draw += DrawUI;
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

            // initialise the list of emotes
            emoteLister.InitialiseDanceList();

            // TEST TEST
            //this.EmoteList.TestBuild();
            emoteLister.BuildDanceList();
        }

        public void Dispose()
        {
            this.WindowSystem.RemoveAllWindows();
            this.CommandManager.RemoveHandler("/dancingway");
            this.CommandManager.RemoveHandler("/ddr");
        }

        /* this isn't needed until viewing/editing of the cached list is implemented. [2.0 feature]
        private void OnDancingway(string command, string args)
        {
            // in response to the slash command, 
            WindowSystem.GetWindow("Dancingway Settings Window").IsOpen = true;
        }
        */


        private void OnDDR(string command, string args)
        {
            /* in response to the slash command, dance, baby, dance!
            XivCommon.Functions.Chat.SendMessage("/dance");
            old code */

            // new test code
            XivCommon.Functions.Chat.SendMessage(emoteLister.getRandomDance());
        }

        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            WindowSystem.GetWindow("A Wonderful Configuration Window").IsOpen = true;
        }
    }
}
