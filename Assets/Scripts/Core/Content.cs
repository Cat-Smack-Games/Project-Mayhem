using ProjectMayhemContentFramework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCoreFramework
{
    [Serializable]
    public class Content
    {
       public Dictionary<string, Gamemode> gamemodes { get; set; }
        public Dictionary<string, Model> models { get; set; }
        public Dictionary<string, Texture> textures { get; set; }
    }
}
