using System;
using System.Collections;
using System.Collections.Generic;
using ProjectCoreFramework;
using ProjectMayhemContentFramework.Content;

namespace ProjectCoreFramework
{
    [Serializable]
    public class MockGamemode : Gamemode
    {
        public MockGamemode()
        {
            this.AttachGamemodeInfo(new GamemodeInfo()
            {
                gamemode_id = "mock-gamemode",
                gamemode_desc = "Another! Test Dev Gamemode used to test various things and what not.",
                gamemode_name = "Gamemode of the Test",
                author = Project.GameName,
                core = "GamemodeTest.cs"

            });



        }



        public static Gamemode Main()
        {
            return new MockGamemode();
        }

        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }

}
