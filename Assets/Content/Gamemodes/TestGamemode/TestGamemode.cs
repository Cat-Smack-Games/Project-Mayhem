using System;
using ProjectMayhemContentFramework.Content;

[Serializable]
public class TestGamemode : Gamemode
{
   
    public TestGamemode()
    {
        this.AttachGamemodeInfo(new GamemodeInfo()
        {
            gamemode_id = "test-gamemode",
            gamemode_desc = "Test Dev Gamemode used to test various things and what not.",
            gamemode_name = "Test Gamemode",
            author = "Project Mayhem",
            core = "TestGamemode.cs"

        });
        

         
    }

   

    public static Gamemode Main()
    {
        return new TestGamemode();
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}
