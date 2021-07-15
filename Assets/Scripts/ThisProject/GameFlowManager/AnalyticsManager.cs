using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager
{

    private static int playerDeathTimes;
    private static int enemyDeathTimes;
    private static int drinkPotionTimes;
    private static int howManyPotionsBeingCollected;
    private static int howManyLetterBeingCollected;
    private static string levelName;
    private static float levelStayTime;

    public static void RecordBasicValue() {
        Dictionary<string, object> analyticsData = new Dictionary<string, object>
        {
            {"GameTime", Time.realtimeSinceStartup },
            {"PlayerDeaths", playerDeathTimes },
            {"KilledEnemies", enemyDeathTimes },
            {"DrinkPotionTimes", drinkPotionTimes },
            {"howManyPotionsBeingCollected", howManyPotionsBeingCollected},
            {"howManyLettersBeingCollected", howManyLetterBeingCollected},
        };

        AnalyticsResult result = Analytics.CustomEvent("GameEnd", analyticsData);
        Debug.Log("Analytics Result Basic " + result);
    }

    public static void RecordLevelStayTime() {
        //Analytics.CustomEvent("Stay_WarArea_01_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_WarArea_02_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_WarArea_03_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_Hub01_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_Hub02_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_Hub03_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_LifeArea01_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_LifeArea02_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_LifeArea03_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_DeathArea_01_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_DeathArea_02_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        //Analytics.CustomEvent("Stay_DeathArea_03_Time" + LevelManager.i.GetLevelStayTime(Levels.WarArea_01));
        Dictionary<string, object> WarArea = new Dictionary<string, object>
        {
            {"Stay_WarArea_01_Time", LevelManager.i.GetLevelStayTime(Levels.WarArea_01)},
            {"Stay_WarArea_02_Time", LevelManager.i.GetLevelStayTime(Levels.WarArea_02)},
            {"Stay_WarArea_03_Time", LevelManager.i.GetLevelStayTime(Levels.WarArea_03)},
            //{"Stay_Hub01_Time", LevelManager.i.GetLevelStayTime(Levels.MainHub_01)},
            //{"Stay_Hub02_Time", LevelManager.i.GetLevelStayTime(Levels.MainHub_02)},
            //{"Stay_Hub03_Time", LevelManager.i.GetLevelStayTime(Levels.MainHub_03)},
            //{"Stay_LifeArea01_Time", LevelManager.i.GetLevelStayTime(Levels.LifeArea_01)},
            //{"Stay_LifeArea02_Time", LevelManager.i.GetLevelStayTime(Levels.LifeArea_02)},
            //{"Stay_LifeArea03_Time", LevelManager.i.GetLevelStayTime(Levels.LifeArea_03)},
            //{"Stay_DeathArea_01_Time", LevelManager.i.GetLevelStayTime(Levels.DeathArea_01)},
            //{"Stay_DeathArea_02_Time", LevelManager.i.GetLevelStayTime(Levels.DeathArea_02)},
            //{"Stay_DeathArea_03_Time", LevelManager.i.GetLevelStayTime(Levels.DeathArea_03)},
        };

        Dictionary<string, object> HubArea = new Dictionary<string, object>
        {
              
            {"Stay_Hub01_Time", LevelManager.i.GetLevelStayTime(Levels.MainHub_01)},
            {"Stay_Hub02_Time", LevelManager.i.GetLevelStayTime(Levels.MainHub_02)},
            {"Stay_Hub03_Time", LevelManager.i.GetLevelStayTime(Levels.MainHub_03)},
        };

        Dictionary<string, object> LifeArea = new Dictionary<string, object>
        {
          
            {"Stay_LifeArea01_Time", LevelManager.i.GetLevelStayTime(Levels.LifeArea_01)},
            {"Stay_LifeArea02_Time", LevelManager.i.GetLevelStayTime(Levels.LifeArea_02)},
            {"Stay_LifeArea03_Time", LevelManager.i.GetLevelStayTime(Levels.LifeArea_03)},
        };
        Dictionary<string, object> DeathArea = new Dictionary<string, object>
        {
            {"Stay_DeathArea_01_Time", LevelManager.i.GetLevelStayTime(Levels.DeathArea_01)},
            {"Stay_DeathArea_02_Time", LevelManager.i.GetLevelStayTime(Levels.DeathArea_02)},
            {"Stay_DeathArea_03_Time", LevelManager.i.GetLevelStayTime(Levels.DeathArea_03)},
        };
        AnalyticsResult WR = Analytics.CustomEvent("WarAreaStayTime", WarArea);
        AnalyticsResult HR = Analytics.CustomEvent("HubAreaStayTime", HubArea);
        AnalyticsResult LR = Analytics.CustomEvent("LifeAreaStayTime", LifeArea);
        AnalyticsResult DR = Analytics.CustomEvent("DeathAreaStayTime", DeathArea);
        Debug.Log("Analytics Result LevelTime WR    " + WR);
        Debug.Log("Analytics Result LevelTime HR    " + HR);
        Debug.Log("Analytics Result LevelTime LR    " + LR);
        Debug.Log("Analytics Result LevelTime DR    " + DR);
    }

    public static void AddPlayerDeathTimes() {
        playerDeathTimes += 1;
    }

    public static void AddEnemyDeathTimes() {
        enemyDeathTimes += 1;
    }

    public static void AddDrinkPotionTimes() {
        drinkPotionTimes += 1;
    }

    public static void AddCollectedPotion() {
        howManyPotionsBeingCollected += 1;
    }

    public static void AddCollectedLetter() {
        howManyLetterBeingCollected += 1;
    }

}
