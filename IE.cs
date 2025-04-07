using Landfall.Haste;
using Landfall.Modding;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Localization;
using Zorro.Settings;
using Object = UnityEngine.Object;

namespace IE;

[LandfallPlugin]
public class Program
{
    static Program()
    {
        On.Player.SetEnergy += Player_SetEnergy;
        var go = new GameObject(nameof(InfiniteEnergyBars));
        Object.DontDestroyOnLoad(go);
        go.AddComponent<InfiniteEnergyBars>();
    }

    private static void Player_SetEnergy(On.Player.orig_SetEnergy orig, Player self, float amount)
    {
        if (!GameHandler.Instance.SettingsHandler.GetSetting<InfiniteEnergySetting>().Value)
        {
            orig(self, amount);
        }
        else
        {
            orig(self, Int32.MaxValue);
        }
    }
}

[HasteSetting]
public class InfiniteEnergySetting : BoolSetting, IExposedSetting
{
    public override void ApplyValue() => Debug.Log($"Mod apply value {Value}");
    protected override bool GetDefaultValue() => true;
    public LocalizedString GetDisplayName() => new UnlocalizedString("Infinite Energy");
    public override LocalizedString OffString => new UnlocalizedString("Off");
    public override LocalizedString OnString => new UnlocalizedString("On");
    public string GetCategory() => SettingCategory.Difficulty;
}

public class InfiniteEnergyBars : MonoBehaviour
{
    void Update()
    {
        if (GameHandler.Instance.SettingsHandler.GetSetting<InfiniteEnergySetting>().Value)
        {
            if (Player.localPlayer != null)
            {
                Player.localPlayer.data.energyBars = 1;
            }
        }
    }

    void LateUpdate()
    {
        if (GameHandler.Instance.SettingsHandler.GetSetting<InfiniteEnergySetting>().Value)
        {
            if (Player.localPlayer != null)
            {
                Player.localPlayer.data.energyBars = 1;
            }
        }
    }
}
