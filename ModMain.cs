using ACTQoL.Utils;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ACTQoL
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("AnotherCrabsTreasure.exe")]
    public class ModMain : BaseUnityPlugin
    {

        public static bool DEBUG = true;
        public static ManualLogSource logSource;
        public static List<Enemy> crystalEnemies;
        public static List<Item> items;
        public static Dictionary<Item, Sprite> itemSprites;
        public static List<GameObject> mapMarkers = new();

        public static bool RenderWorldMarkers = true;
        public static bool UserToggleESP = true;

        private void Awake()
        {
            // Plugin startup logic
            logSource = Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony harmony = new("com.example.patch");
            harmony.PatchAll();

            ScanObjects();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                ScanObjects();
                Logger.LogInfo("Rescanned items and enemies!");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                UserToggleESP = !UserToggleESP;
                Logger.LogInfo($"ESP Toggled: {UserToggleESP}");
            }
        }

        private void ScanObjects()
        {
            crystalEnemies = FindObjectsOfType<Enemy>(true).ToList();
            items = FindObjectsOfType<Item>(true).ToList();
            itemSprites = new Dictionary<Item, Sprite>();

            foreach(Item item in items)
            {
                string itemName = item.DisplayName.Replace("Item_", "").Replace("_Name", "");
                string resourceName = ItemNameToResource.GetResourceName(itemName);
                Sprite sprite = ModHelper.GetSprite(resourceName);
                if (!itemSprites.ContainsKey(item))
                {
                    itemSprites.Add(item, sprite);
                }
            }
        }

        public void OnGUI()
        {
            if (RenderWorldMarkers && UserToggleESP)
            {
                if (crystalEnemies != null)
                {
                    foreach (Enemy enemy in crystalEnemies)
                    {
                        if (enemy == null) continue;
                        
                        SaveStateKillableEntity state = Traverse.Create(enemy).Field("saveState").GetValue() as SaveStateKillableEntity;
                        if (state == null) { continue; }
                        if (enemy.transform == null || enemy.isBoss || state.killedPreviously)
                        {
                            continue;
                        }
                        if (enemy.umamiDrops > 0)
                        {
                            Vector3 center = enemy.GetCenter();
                            Vector3 screenPoint = Camera.main.WorldToScreenPoint(center);
                            Texture2D crystal = ModHelper.GetSprite("crystal").texture;
                            float iconSize = 64;

                            if (screenPoint.z > 0)
                            {
                                GUI.DrawTexture(new Rect(new Vector2(screenPoint.x - iconSize/2, Screen.height - screenPoint.y - iconSize/2), new Vector2(iconSize, iconSize)), crystal, ScaleMode.ScaleToFit);
                            }
                        }
                    }
                }
                if (items != null)
                {
                    foreach(Item item in items)
                    {
                        if (item == null) continue;

                        SaveStateKillableEntity state = Traverse.Create(item).Field("save").GetValue() as SaveStateKillableEntity;
                        if (state == null)
                        {
                            continue;
                        }
                        if (state.killedPreviously)
                        {
                            continue;
                        }

                        Texture2D iconTexture = null;
                        if (itemSprites != null && itemSprites.ContainsKey(item) && itemSprites[item] != null)
                        {
                            iconTexture = itemSprites[item].texture;
                        }
                        else
                        {
                             // Fallback or skip if not cached/found
                             continue; 
                        }

                        Vector3 center = item.GetCenter();
                        Vector3 screenPoint = Camera.main.WorldToScreenPoint(center);
                        float iconSize = 64;

                        if (screenPoint.z > 0)
                        {
                            GUI.DrawTexture(new Rect(new Vector2(screenPoint.x - iconSize / 2, Screen.height - screenPoint.y - iconSize / 2), new Vector2(iconSize, iconSize)), iconTexture, ScaleMode.ScaleToFit);
                        }
                    }
                }
            }
        }

        public static bool EnemiesAggro()
        {
            bool aggro = false;
            foreach (Enemy enemy in crystalEnemies)
            {
                if (enemy.aggro)
                {
                    aggro = true;
                }
            }
            return aggro;
        }
    }
}
