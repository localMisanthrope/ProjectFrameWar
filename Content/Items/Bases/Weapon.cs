using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Helpers;
using ProjectFrameWar.Core.Items;
using System;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    struct WeaponData
    {
        public string Name { get; set; }
        public int Damage { get; set; }

        public float CriticalChance { get; set; }
        public float CriticalDamage { get; set; } 

        public float StatusChance { get; set; }
    }

    struct DamageData
    {
        public int ImpactDamage { get; set; }
        public int PunctureDamage { get; set; }
        public int SlashDamage { get; set; }

        public int ColdDamage { get; set; }
        public int HeatDamage { get; set; }
        public int ShockDamage {  get; set; }
        public int ToxinDamage { get; set; }

        public int BlastDamage { get; set; }
        public int CorrosiveDamage {  get; set; }
        public int GasDamage {  get; set; }
        public int MagneticDamage { get; set; }
        public int RadiationDamage { get; set; }
        public int ViralDamage { get; set; }
    }

    [Autoload(false)]
    internal class Weapon(WeaponData data, WeaponClass type) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"weapon_{data.Name}";

        public override string Texture => MiscHelpers.CheckTexturePath(Mod, $"{ProjectFrameWar.texPath}/weapons/weapon_Inventory_{data.Name}");

        public override void SetDefaults()
        {
            Item.TryEnableComponent<WeaponComponent>(x => 
            {
                x.data = data;
                x.type = type;
            });
            base.SetDefaults();
        }
    }

    public class WeaponLoader : ILoadable
    {
        int count;
        int subCount;

        public void Load(Mod mod)
        {
            count = 0;
            var watch = Stopwatch.StartNew();
            var subWatch = Stopwatch.StartNew();

            foreach (var value in Enum.GetValues(typeof(WeaponClass)))
            {
                subCount = 0;
                subWatch.Reset();
                var list = JSONHelpers.CheckObjectList<WeaponData>(mod, $"dat/weapon_data/{value}Data.json");

                if (list is null)
                {
                    mod.Logger.Warn($"[FRAMEWAR]: Failed to load \"{value}Data.json\". Skipping...");
                    continue;
                }

                foreach (var data in list)
                {
                    mod.AddContent(new Weapon(data, (WeaponClass)value));
                    count++;
                    subCount++;
                }

                subWatch.Stop();
                mod.Logger.Info($"[FRAMEWAR]: Finished loading {value} Weapons ({subCount}), took {subWatch.Elapsed.TotalMilliseconds} ms.");
            }

            watch.Stop();
            mod.Logger.Info($"[FRAMEWAR]: Finished loading Weapons ({count}), took {watch.Elapsed.TotalMilliseconds} ms.");
        }

        public void Unload() { }
    }
}