namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.EM.Artistry;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    
    [Serialized]
    [LocDisplayName("Powered Cart Black")]
    [Weight(15000)]  
    [AirPollution(0.1f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]  
    [Tag("ColoredPoweredCart")]
    public partial class PoweredCartBlackItem : WorldObjectItem<PoweredCartBlackObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Large black cart for hauling sizable loads."); } }
    }

    public class PaintPoweredCartBlackRecipe : RecipeFamily
    {
        public PaintPoweredCartBlackRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Powered Cart Black",
                    Localizer.DoStr("Paint Powered Cart Black"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(PoweredCartItem), 1, true), 
                        new IngredientElement(typeof(BlackDyeItem), 1, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),                        
                    },
                    new CraftingElement<PoweredCartBlackItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;  
            this.LaborInCalories = CreateLaborInCaloriesValue(250, typeof(BasicEngineeringSkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintPoweredCartBlackRecipe), 5, typeof(BasicEngineeringSkill));    

            this.Initialize(Localizer.DoStr("Paint Powered Cart Black"), typeof(PaintPoweredCartBlackRecipe));
            CraftingComponent.AddRecipe(typeof(PrimitivePaintingTableObject), this);
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
    public partial class PoweredCartBlackObject : PhysicsWorldObject, IRepresentsItem
    {
        static PoweredCartBlackObject()
        {
            WorldObject.AddOccupancy<PoweredCartBlackObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart Black"); } }
        public Type RepresentedItemType { get { return typeof(PoweredCartBlackItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };

        private PoweredCartBlackObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(18, 3500000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(35);    
            this.GetComponent<AirPollutionComponent>().Initialize(0.1f);            
            this.GetComponent<VehicleComponent>().Initialize(12, 1.5f, 1);
        }
    }
}