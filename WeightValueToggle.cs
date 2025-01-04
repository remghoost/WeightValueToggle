using HarmonyLib;
using Qud.UI;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;


public class TradeDisplayToggle : MonoBehaviour
{
    public static bool ShowValue = true; // Shared toggle state for trade display
    public static bool ShowBoth = false; // Shared toggle state for displaying both weight and value
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
                if (TradeDisplayToggle.ShowBoth)
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
                        __instance.rightFloatText.SetText("[N/A]");
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
                        __instance.rightFloatText.SetText("[N/A]");
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
                        __instance.rightFloatText.SetText("[N/A]");
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
            TradeDisplayToggle.ShowValue = false; // Reset ShowValue when F8 is pressed
            //UnityEngine.Debug.LogError("Hotkey F8 pressed. Toggled ShowBoth to: " + TradeDisplayToggle.ShowBoth);

            if (TradeDisplayToggle.ShowBoth)
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
