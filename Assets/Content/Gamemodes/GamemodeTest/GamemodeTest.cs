using System.Collections;
using System.Collections.Generic;
using ProjectCoreFramework;
using ProjectFramework.Content;
public class GamemodeTest : Gamemode
{
   public GamemodeTest()
    {
        this.AttachGamemodeInfo(new GamemodeInfo()
        {
            gamemode_id = "gamemode-test",
            gamemode_desc = "Another! Test Dev Gamemode used to test various things and what not.",
            gamemode_name = "Gamemode of the Test",
            author = Project.GameName,
            core = "GamemodeTest.cs"

        });
        

         
    }

   

    public static Gamemode Main()
    {
        return new GamemodeTest();
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}
