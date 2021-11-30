namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.EM.Artistry;

    [Serialized]
    [LocDisplayName("Truck Orange")]
    [Weight(25000)]  
    [AirPollution(0.5f)] 
    [Ecopedia("CavRnMods", "Colored Vehicles", true, InPageTooltip.DynamicTooltip)]                  
    [Tag("ColoredTruck")]
    public partial class TruckOrangeItem : WorldObjectItem<TruckOrangeObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Modern orange truck for hauling sizable loads."); } }
    }
    
      
    public class PaintTruckOrangeRecipe : RecipeFamily
    {
        public PaintTruckOrangeRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Paint Truck Orange",
                    Localizer.DoStr("Paint Truck Orange"),
                    new IngredientElement[]
                    {
                        new IngredientElement(typeof(TruckItem), 1, true), 
                        new IngredientElement(typeof(OrangePaintItem), 60, typeof(IndustrySkill), typeof(IndustryLavishResourcesTalent)),
                    },
                    new CraftingElement<TruckOrangeItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;  
            this.LaborInCalories = CreateLaborInCaloriesValue(750, typeof(IndustrySkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaintTruckOrangeRecipe), 15, typeof(IndustrySkill));    

            this.Initialize(Localizer.DoStr("Paint Truck Orange"), typeof(PaintTruckOrangeRecipe));
            CraftingComponent.AddRecipe(typeof(AdvancedPaintingTableObject), this);
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
    [RequireComponent(typeof(ModularStockpileComponent))]   
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class TruckOrangeObject : PhysicsWorldObject, IRepresentsItem
    {
        static TruckOrangeObject()
        {
            WorldObject.AddOccupancy<TruckOrangeObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Truck Orange"); } }
        public Type RepresentedItemType { get { return typeof(TruckOrangeItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Liquid Fuel"
        };

        private TruckOrangeObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(36, 8000000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);    
            this.GetComponent<AirPollutionComponent>().Initialize(0.5f);            
            this.GetComponent<VehicleComponent>().Initialize(20, 2, 2);
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}