using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryBuildPlanner
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public float RecipeQuantity {get;set;}

        public List<string> InputItems { get; set; }
        public List<int> InputIDs { get; set; }
        public List<float> InputQuantity { get; set; }

        public Recipe()
        {
            InputItems = new List<string>();
            InputIDs = new List<int>();
            InputQuantity = new List<float>();
        }

    }
}
