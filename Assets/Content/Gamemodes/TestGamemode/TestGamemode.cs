using System;
using ProjectMayhemContentFramework;
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

    public override void OnPlayerJoin(Player player)
    {
        throw new NotImplementedException();
    }

    public override void OnPlayerLeave(Player player)
    {
        throw new NotImplementedException();
    }
}
