using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilestoneUpdater
{
    public class HotfixFile
    {
        public string Version { get; set; }
        public string Url { get; set; }

        public HotFixType Type { get; set; }
        public string ServerVersion { get; set; }
        public string File { get; set; }

        public string LocalLocation{ get; set; }
    }

}
