using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Configuration;
using Dalamud.Plugin;

namespace Dancingway
{
    public struct Dance
    {
        public uint keyID;
        public string name;
        public string command;
        public Boolean enabled;

        public Dance(uint id, string nm, string cmd, Boolean en)
        {
            keyID = id;
            name = nm;
            command = cmd;
            enabled = en;
        }

        public Dance(uint id, string cmd)
        {
            keyID = id;
            name = cmd;
            command = cmd;
            enabled = false;
        }


    }

    
}
