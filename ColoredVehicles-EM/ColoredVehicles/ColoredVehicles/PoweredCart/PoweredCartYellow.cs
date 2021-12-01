namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.EM.Artistry;
    using Eco.EM.Framework.Resolvers;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Powered Cart Yellow")]
    [Weight(15000)]  
    [AirPollution(0.1f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredPoweredCart")]
    public partial class PoweredCartYellowItem : WorldObjectItem<PoweredCartYellowObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Large yellow cart for hauling sizable loads.");
    }

    public class PaintPoweredCartYellowRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PaintPoweredCartYellowRecipe).Name,
            Assembly = typeof(PaintPoweredCartYellowRecipe).AssemblyQualifiedName,
            HiddenName = "Paint Powered Cart Yellow",
            LocalizableName = Localizer.DoStr("Paint Powered Cart Yellow"),
            IngredientList = new()
            {
                new EMIngredient("PoweredCartItem", false, 1, true),
                new EMIngredient("YellowPaintItem", false, 1, true),
                new EMIngredient("PaintBrushItem", false, 1, true),
                new EMIngredient("PaintPaletteItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("PoweredCartYellowItem"),
                new EMCraftable("PaintBrushItem"),
                new EMCraftable("PaintPaletteItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 250,
            LaborIsStatic = false,
            BaseCraftTime = 5,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static PaintPoweredCartYellowRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PaintPoweredCartYellowRecipe()
        {
            this.Recipes = EMRecipeResolver.Obj.ResolveRecipe(this);
            this.LaborInCalories = EMRecipeResolver.Obj.ResolveLabor(this);
            this.CraftMinutes = EMRecipeResolver.Obj.ResolveCraftMinutes(this);
            this.ExperienceOnCraft = EMRecipeResolver.Obj.ResolveExperience(this);
            this.Initialize(Defaults.LocalizableName, GetType());
            CraftingComponent.AddRecipe(EMRecipeResolver.Obj.ResolveStation(this), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(AirPollutionComponent))]       
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class PoweredCartYellowObject : PhysicsWorldObject, IRepresentsItem, IStorageSlotObject
    {
        public override LocString DisplayName => Localizer.DoStr("Powered Cart Yellow");
        public Type RepresentedItemType => typeof(PoweredCartYellowItem);

        private static readonly StorageSlotModel SlotDefaults = new(typeof(PoweredCartYellowObject)) { StorageSlots = 18, };

        static PoweredCartYellowObject()
        {
            WorldObject.AddOccupancy<PoweredCartYellowObject>(new List<BlockOccupancy>(0));
            EMStorageSlotResolver.AddDefaults(SlotDefaults);
        }

        private static readonly string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };

        private PoweredCartYellowObject() { }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<PublicStorageComponent>().Initialize(EMStorageSlotResolver.Obj.ResolveSlots(this), 3500000);
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(35);
            this.GetComponent<AirPollutionComponent>().Initialize(0.1f);
            this.GetComponent<VehicleComponent>().Initialize(12, 1.5f, 1);
        }
    }
}