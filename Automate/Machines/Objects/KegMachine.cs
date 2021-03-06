using Microsoft.Xna.Framework;
using Pathoschild.Stardew.Automate.Framework;
using SObject = StardewValley.Object;

namespace Pathoschild.Stardew.Automate.Machines.Objects
{
    /// <summary>A keg that accepts input and provides output.</summary>
    internal class KegMachine : GenericMachine
    {
        /*********
        ** Properties
        *********/
        /// <summary>The recipes to process.</summary>
        private readonly Recipe[] Recipes =
        {
            // honey => mead
            new Recipe(
                input: 340,
                inputCount: 1,
                output: input => new SObject(Vector2.Zero, 459, "Mead", false, true, false, false) { name = "Mead" },
                minutes: 600
            ),

            // coffee bean => coffee
            new Recipe(
                input: 433,
                inputCount: 5,
                output: input => new SObject(Vector2.Zero, 395, "Coffee", false, true, false, false) { name = "Coffee" },
                minutes: 120
            ),

            // wheat => beer
            new Recipe(
                input: 262,
                inputCount: 1,
                output: input => new SObject(Vector2.Zero, 346, "Beer", false, true, false, false) { name = "Beer" },
                minutes: 1750
            ),

            // hops => pale ale
            new Recipe(
                input: 304,
                inputCount: 1,
                output: input => new SObject(Vector2.Zero, 303, "Pale Ale", false, true, false, false) { name = "Pale Ale" },
                minutes: 2250
            ),

            // fruit => wine
            new Recipe(
                input: SObject.FruitsCategory,
                inputCount: 1,
                output: input => new SObject(Vector2.Zero, 348, input.Name + " Wine", false, true, false, false)
                {
                    name = input.Name + " Wine",
                    Price = ((SObject)input).Price * 3,
                    preserve = SObject.PreserveType.Wine,
                    preservedParentSheetIndex = input.parentSheetIndex
                },
                minutes: 10000
            ),
            new Recipe(
                input: SObject.VegetableCategory,
                inputCount: 1,
                output: input => new SObject(Vector2.Zero, 350, input.Name + " Juice", false, true, false, false)
                {
                    name = input.Name + " Juice",
                    Price = (int)(((SObject)input).Price * 2.25),
                    preserve = SObject.PreserveType.Juice,
                    preservedParentSheetIndex = input.parentSheetIndex
                },
                minutes: 6000
            )
        };


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="machine">The underlying machine.</param>
        public KegMachine(SObject machine)
            : base(machine) { }

        /// <summary>Pull items from the connected pipes.</summary>
        /// <param name="pipes">The connected IO pipes.</param>
        /// <returns>Returns whether the machine started processing an item.</returns>
        public override bool Pull(IPipe[] pipes)
        {
            return this.GenericPullRecipe(pipes, this.Recipes);
        }
    }
}
