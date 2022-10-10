using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFPCalculator
{
    /// <summary>
    /// The Script API To Save Your Production Plan
    /// </summary>
    public class SFPScripts
    {
        /// <summary>
        /// Converts A Process To A string[] Of The Production
        /// </summary>
        /// <param name="Plan">The Production Plan</param>
        /// <returns>string[] Of The Production</returns>
        public static Task<string[]> ProcessToRPScript(Process Plan)
        {
            List<string> RPScrip = new List<string>();
            GetRP(Plan);

            void GetRP(Process Item)
            {
                RPScrip.Add(Item.Recipe.ToString() + "," + Item.Outputs.Select(x => x.PerMin).First());
                if (Item.Children.ToList().Count <= 0) return;
                RPScrip.Add("[");
                foreach (var item in Item.Children)
                    GetRP(item);
                RPScrip.Add("]");
                return;
            }
            return Task.FromResult(RPScrip.ToArray());
        }

        /// <summary>
        /// Converts A string[] Of The Production To A Process
        /// Warning! This Method Is Can Fail If The One Of The Strings Is Not In The Correct Format Or If The Mod Does Not Exist
        /// </summary>
        /// <param name="RPScript">string[] Of The Production</param>
        /// <returns>The Production Plan</returns>
        public static Task<Process> RPScriptToProcess(string[] RPScript)
        {
            int Index = -1;
            var Plan = Converter().First();
            return Task.FromResult(Plan);

            List<Process> Converter()
            {
                int I = -1;
                var Children = new List<Process>();
                while (Index < RPScript.Length - 1)
                {
                    Index++;
                    if (RPScript[Index] == "[" && I >= 0)
                    {
                        Converter().ForEach(x => Children[I].Children.ToList().Add(x));
                        continue;
                    }
                    if (RPScript[Index] == "]")
                        break;
                    var Line = RPScript[Index].Split(',');
                    Children.Add(SFPPlanner.SingleProcess(Line[0], Convert.ToDouble(Line[1])));
                    I++;
                }
                return Children;
            }

        }

        /// <summary>
        /// Converts A Process To A Smaller Process Object
        /// </summary>
        /// <param name="Process">The Production Plan</param>
        /// <returns>Smaller Process Object</returns>
        public static Task<MacroProcess> ProcessToMicroProcess(Process Process)
        {
            return Task.FromResult(Converter(Process));

            MacroProcess Converter(Process Plan)
            {
                var Children = new List<MacroProcess>();
                foreach (var Item in Plan.Children)
                    Children.Add(Converter(Item));
                return new MacroProcess
                {
                    Recipe = Plan.Recipe.ToString(),
                    PerMin = Plan.Product.PerMin,
                    Production = Plan.Production,
                    Children = Children
                };
            }
        }

        /// <summary>
        /// Converts The Smaller Process Object To A Full Process
        /// </summary>
        /// <param name="Macro">Smaller Process Object</param>
        /// <returns>The Production Plan</returns>
        public static Task<Process> MicroProcessToProcess(MacroProcess Macro)
        {
            return Task.FromResult(Converter(Macro));

            Process Converter(MacroProcess MicroPlan)
            {
                var Children = new List<Process>();
                foreach (var Item in MicroPlan.Children)
                    Children.Add(Converter(Item));
                var Plan = SFPPlanner.SingleProcess(MicroPlan.Recipe, MicroPlan.PerMin);
                Plan.Production = MicroPlan.Production;
                Plan.Children = Children;
                foreach (var Child in Plan.Children)
                    Plan.Inputs.Where(x => x.Item.ID == Child.Product.Item.ID).First().Production = Child.Production;
                return Plan;
            }
        }
    }
}
