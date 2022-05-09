using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginShortcutPro
{
    internal class infoPlugin
    {
        public infoPlugin()
        {
            page = "Tools";
            group = "Local";
            button = "ShortCuts Pro";
            nameMods = "ShortCuts";
        }
        public string page { get; set; }
        public string group { get; set; }
        public string button { get; set; }
        public string nameMods { get; set; }
    }
}
