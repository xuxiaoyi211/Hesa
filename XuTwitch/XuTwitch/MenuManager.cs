﻿using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX;

namespace XuTwitch
{
    public static class MenuManager
    {
        public static Menu Home, comboMenu, chaseMenu, harassMenu, autoharassMenu, laneclearMenu, lasthitMenu, fleeMenu, potMenu, ssMenu, drawingMenu, miscMenu, killstealMenu;

        public static bool IsSummonersRift => Game.MapId == GameMapId.SummonersRift;
        public static bool IsTwistedTreeline => Game.MapId == GameMapId.TwistedTreeline;

        private static string prefix = " - ";

        public static void LoadMenu()
        {
            Home = Menu.AddMenu("Xu Twitch");


            comboMenu = Home.AddSubMenu(prefix + "Combo");
            comboMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            //comboMenu.Add(new MenuSlider("qCount", "Enemy Count to Use Q ", 1, 5, 2));
            comboMenu.Add(new MenuSlider("qRange", "Enemy In Range to Use Q", 600, 1800, 900));
            comboMenu.Add(new MenuCheckbox("useW", "Use W", true));
            comboMenu.Add(new MenuCheckbox("useE", "Use E", false));
            comboMenu.Add(new MenuSlider("eStack", "Numer of Stacks Auto E ", 1, 6, 5));
            comboMenu.Add(new MenuCheckbox("useR", "Use R", false));
            comboMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 50));

            harassMenu = Home.AddSubMenu(prefix + "Harass");
            harassMenu.Add(new MenuCheckbox("useW", "Use W", true));
            harassMenu.Add(new MenuCheckbox("useE", "Use E", true));
            harassMenu.Add(new MenuSlider("heStack", "Numer of Stacks Auto E ", 1, 6, 4));
            harassMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 50));


            laneclearMenu = Home.AddSubMenu(prefix + "Lane Clear");
            laneclearMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            laneclearMenu.Add(new MenuCheckbox("useW", "Use W", true));
            laneclearMenu.Add(new MenuCheckbox("useE", "Use E", true));
            laneclearMenu.Add(new MenuSlider("eNumber", "Numer of Minions Can Kill With E ", 1, 16, 3));
            laneclearMenu.Add(new MenuSlider("leStack", "Numer of Stacks Auto E ", 1, 6, 4));
            laneclearMenu.Add(new MenuCheckbox("useR", "Use R", false));
            laneclearMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 50));


            lasthitMenu = Home.AddSubMenu(prefix + "LastHit");
            lasthitMenu.Add(new MenuCheckbox("useE", "Use E", false));
            lasthitMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 50));

            fleeMenu = Home.AddSubMenu(prefix + "Flee");
            fleeMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            fleeMenu.Add(new MenuCheckbox("useW", "Use W", false));

            potMenu = Home.AddSubMenu(prefix + "Potion");
            potMenu.Add(new MenuCheckbox("enable", "Enable Potions", true));
            potMenu.Add(new MenuSlider("hp", "Use at HP% ", 1, 100, 45));

            ssMenu = Home.AddSubMenu(prefix + "Summoner Spells");
            ssMenu.Add(new MenuCheckbox("Heal", "Use Heal", true));
            ssMenu.Add(new MenuSlider("ssheal", "Heal at HP% ", 1, 100, 45));
            ssMenu.Add(new MenuCheckbox("Smite", "Use Smite", true));
            ssMenu.Add(new MenuCheckbox("SmiteKS", "Smite KS", true));
            if (IsSummonersRift)
            {
                var small = ssMenu.AddSubMenu("Small Mobs");
                small.Add(new MenuCheckbox("SRU_Gromp", "Gromp").SetValue(false));
                small.Add(new MenuCheckbox("SRU_Razorbeak", "Raptors").SetValue(false));
                small.Add(new MenuCheckbox("Sru_Crab", "Crab").SetValue(false));
                small.Add(new MenuCheckbox("SRU_Krug", "Krug").SetValue(false));
                small.Add(new MenuCheckbox("SRU_Murkwolf", "Wolves").SetValue(false));

                var big = ssMenu.AddSubMenu("Big Mobs");
                big.Add(new MenuCheckbox("SRU_Baron", "Baron Nashor").SetValue(true));
                big.Add(new MenuCheckbox("SRU_RiftHerald", "Rift Herald").SetValue(true));
                big.Add(new MenuCheckbox("SRU_Dragon", "Dragons").SetValue(true));
                big.Add(new MenuCheckbox("SRU_Blue", "Blue Buff").SetValue(true));
                big.Add(new MenuCheckbox("SRU_Red", "Red Buff").SetValue(true));
            }


            drawingMenu = Home.AddSubMenu(prefix + "Drawings");
            drawingMenu.Add(new MenuCheckbox("enable", "Enable", true));
            drawingMenu.Add(new MenuCheckbox("drawQ", "Draw Q", true));
            drawingMenu.Add(new MenuCheckbox("drawW", "Draw W", true));
            drawingMenu.Add(new MenuCheckbox("drawE", "Draw E", true));
            drawingMenu.Add(new MenuCheckbox("drawR", "Draw R", true));
            drawingMenu.Add(new MenuCheckbox("drawSmite", "Draw Smite Range", true));
            //drawingMenu.Add(new MenuCheckbox("drawMode", "Draw Q Count Down", true));

            killstealMenu = Home.AddSubMenu(prefix + "KillSteal");
            killstealMenu.Add(new MenuCheckbox("enable", "Enable", true));
            killstealMenu.Add(new MenuCheckbox("useE", "Use E", true));
            killstealMenu.Add(new MenuCheckbox("useR", "Use R", true));
            killstealMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 10));

            miscMenu = Home.AddSubMenu(prefix + "Misc");
            //miscMenu.Add(new MenuCheckbox("recallq", "Recall Q", true));
            miscMenu.Add(new MenuCheckbox("item", "Use Items", true));
            miscMenu.Add(new MenuCheckbox("QSS", "Auto QSS", true));
            miscMenu.Add(new MenuCheckbox("orb", "Use Orbs/Trinkets In Combo", true));
            miscMenu.Add(new MenuCheckbox("blue", "Use Farsigh Orbs", true));
            miscMenu.Add(new MenuCheckbox("yellow", "Use Trinkets", true));
            miscMenu.Add(new MenuCheckbox("agW", "AntiGapclose W", true));
            miscMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 30));
            //miscMenu.Add(new MenuCheckbox("save", "Always Save Mana For E", true));
        }

        public static bool GetCheckbox(this Menu menu, string value)
        {
            return menu.Get<MenuCheckbox>(value).Checked;
        }

        public static bool GetKeybind(this Menu menu, string value)
        {
            return menu.Get<MenuKeybind>(value).Active;
        }

        public static int GetSlider(this Menu menu, string value)
        {
            return menu.Get<MenuSlider>(value).CurrentValue;
        }

        public static int GetCombobox(this Menu menu, string value)
        {
            return menu.Get<MenuCombo>(value).CurrentValue;
        }
    }
}
