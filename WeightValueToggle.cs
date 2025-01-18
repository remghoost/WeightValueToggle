using System;
using HarmonyLib;
using Qud.UI;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;


public class TradeDisplayToggle : MonoBehaviour
{
    public static bool ShowValue = true; // Shared toggle state for trade display
    public static bool ShowBoth = false; // Shared toggle state for displaying both weight and value
    public static bool ShowRatio = false; // Shared toggle state for displaying the ratio of weight to value
}

[HarmonyPatch(typeof(TradeLine), "setData")]
public static class TradeLineSetDataPatch
{
    static void Postfix(TradeLine __instance, FrameworkDataElement data)
    {
        //UnityEngine.Debug.LogError("WeightValueToggle Postfix called");
        if (data is TradeLineData tradeLineData)
        {
            if (__instance.rightFloatText != null)
            {
                if (TradeDisplayToggle.ShowRatio)
                {
                    // Display ratio of Item Weight to Value
                    if (tradeLineData.go != null)
                    {
                        float value = (float)TradeUI.GetValue(tradeLineData.go, new bool?(tradeLineData.traderInventory));
                        float weight = tradeLineData.go.Weight;
                        string ratioStr = string.Format("{0:0.00}", (float)(value / weight));

                        if (tradeLineData.go.IsCurrency)
                            __instance.rightFloatText.SetText("Value/Weight: " + ratioStr);
                        else
                            __instance.rightFloatText.SetText("Value/Weight: " + ratioStr);
                    }
                    else
                    {
                        __instance.rightFloatText.SetText("");
                    }
                }
                else if (TradeDisplayToggle.ShowBoth)
                {
                    // Display both Item Weight and Value
                    if (tradeLineData.go != null)
                    {
                        string valueStr = string.Format("{0:0.00}",
                        TradeUI.GetValue(tradeLineData.go, new bool?(tradeLineData.traderInventory)));
                        string weightStr = string.Format("[{0} lbs.]", tradeLineData.go.Weight);

                        if (tradeLineData.go.IsCurrency)
                            __instance.rightFloatText.SetText(weightStr + " [{{W|$" + valueStr + "}}]");
                        else
                            __instance.rightFloatText.SetText(weightStr + " [$" + valueStr + "]");
                    }
                    else
                    {
                        __instance.rightFloatText.SetText("");
                    }
                }
                else if (TradeDisplayToggle.ShowValue)
                {
                    // Display Item Value
                    if (tradeLineData.go != null)
                    {
                        string valueStr = string.Format("{0:0.00}",
                        TradeUI.GetValue(tradeLineData.go, new bool?(tradeLineData.traderInventory)));

                        if (tradeLineData.go.IsCurrency)
                            __instance.rightFloatText.SetText("[{{W|$" + valueStr + "}}]");
                        else
                            __instance.rightFloatText.SetText("[$" + valueStr + "]");
                    }
                    else
                    {
                        __instance.rightFloatText.SetText("");
                    }
                }
                else
                {
                    // Display Item Weight
                    if (tradeLineData.go != null)
                    {
                        string weightStr = string.Format("[{0} lbs.]", tradeLineData.go.Weight);

                        __instance.rightFloatText.SetText(weightStr);
                    }
                    else
                    {
                        __instance.rightFloatText.SetText("");
                    }
                }
            }
        }
    }
}

[HarmonyPatch(typeof(InventoryLine), "setData")]
public static class InventoryLineSetDataPatch
{
    static void Postfix(InventoryLine __instance, FrameworkDataElement data)
    {
        //UnityEngine.Debug.LogError("WeightValueToggle Postfix called");
        if (data is InventoryLineData inventoryLineData)
        {
            if (__instance.itemWeightText != null)
            {
                if (TradeDisplayToggle.ShowRatio)
                {
                    // Display ratio of Item Weight to Value
                    if (inventoryLineData.go != null)
                    {
                        float value = (float)TradeUI.GetValue(inventoryLineData.go, new bool?(false));
                        float weight = inventoryLineData.go.Weight;
                        string ratioStr = string.Format("{0:0.00}", (float)(value / weight));

                        if (inventoryLineData.go.IsCurrency)
                            __instance.itemWeightText.SetText("Value/Weight: " + ratioStr);
                        else
                            __instance.itemWeightText.SetText("Value/Weight: " + ratioStr);
                    }
                    else
                    {
                        __instance.itemWeightText.SetText("");
                    }
                }
                else if (TradeDisplayToggle.ShowBoth)
                {
                    // Display both Item Weight and Value
                    if (inventoryLineData.go != null)
                    {
                        string valueStr = string.Format("{0:0.00}",
                        TradeUI.GetValue(inventoryLineData.go, new bool?(false)));
                        string weightStr = string.Format("[{0} lbs.]", inventoryLineData.go.Weight);

                        if (inventoryLineData.go.IsCurrency)
                            __instance.itemWeightText.SetText(weightStr + " [{{W|$" + valueStr + "}}]");
                        else
                            __instance.itemWeightText.SetText(weightStr + " [$" + valueStr + "]");
                    }
                    else
                    {
                        __instance.itemWeightText.SetText("");
                    }
                }
                else if (TradeDisplayToggle.ShowValue)
                {
                    // Display Item Value
                    if (inventoryLineData.go != null)
                    {
                        string valueStr = string.Format("{0:0.00}",
                        TradeUI.GetValue(inventoryLineData.go, new bool?(false)));

                        if (inventoryLineData.go.IsCurrency)
                            __instance.itemWeightText.SetText("[{{W|$" + valueStr + "}}]");
                        else
                            __instance.itemWeightText.SetText("[$" + valueStr + "]");
                    }
                    else
                    {
                        __instance.itemWeightText.SetText("");
                    }
                }
                else
                {
                    // Display Item Weight
                    if (inventoryLineData.go != null)
                    {
                        string weightStr = string.Format("[{0} lbs.]", inventoryLineData.go.Weight);

                        __instance.itemWeightText.SetText(weightStr);
                    }
                    else
                    {
                        __instance.itemWeightText.SetText("");
                    }
                }
            }
        }
    }
}

// Ensure Harmony is initialized correctly
[HarmonyPatch(typeof(Qud.UI.UIManager), "Update")]
public static class HotkeyPatch
{
    static void Postfix()
    {
        if (Input.GetKeyDown(KeyCode.F7))
        {
            TradeDisplayToggle.ShowValue = !TradeDisplayToggle.ShowValue;
            TradeDisplayToggle.ShowBoth = false; // Reset ShowBoth when F7 is pressed
            TradeDisplayToggle.ShowRatio = false; // Reset ShowRatio when F7 is pressed
            //UnityEngine.Debug.LogError("Hotkey F7 pressed. Toggled ShowValue to: " + TradeDisplayToggle.ShowValue);

            if (TradeDisplayToggle.ShowValue)
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("Showing currency");
            }
            else
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("Showing weight");
            }
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            TradeDisplayToggle.ShowBoth = !TradeDisplayToggle.ShowBoth;
            TradeDisplayToggle.ShowRatio = !TradeDisplayToggle.ShowRatio;

            if (!TradeDisplayToggle.ShowRatio && !TradeDisplayToggle.ShowBoth){
                TradeDisplayToggle.ShowBoth = true;
            }
            // TradeDisplayToggle.ShowValue = false; // Reset ShowValue when F8 is pressed
            //UnityEngine.Debug.LogError("Hotkey F8 pressed. Toggled ShowBoth to: " + TradeDisplayToggle.ShowBoth);

            if (TradeDisplayToggle.ShowRatio)
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("Showing ratio of weight to value");
            }
            else if (TradeDisplayToggle.ShowBoth)
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("Showing both weight and value");
            }
            else
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("Showing weight");
            }
        }
    }
}

public static class HarmonyInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        var harmony = new Harmony("com.remghoost.weightvaluetoggle");
        //UnityEngine.Debug.LogError("WeightValueToggle patch applied.");

        harmony.PatchAll();
    }
}
