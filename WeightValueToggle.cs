using HarmonyLib;
using Qud.UI;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;


public class TradeDisplayToggle : MonoBehaviour
{
    public static bool ShowValue = true; // Shared toggle state for trade display
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
                if (TradeDisplayToggle.ShowValue)
                {
                    // Display Item Value
                    if (tradeLineData.go != null)
                    {
                        string valueStr = string.Format("{0:0.00}",

                        TradeUI.GetValue(tradeLineData.go, new bool?(tradeLineData.traderInventory)));
                        //UnityEngine.Debug.LogError("Displaying Trade Value: " + valueStr);
                        
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
                        //UnityEngine.Debug.LogError("Displaying Item Weight: " + weightStr);
                        
                        __instance.rightFloatText.SetText(weightStr);
                    }
                    else
                    {
                        // Just in case things wig out so we don't crash.
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
