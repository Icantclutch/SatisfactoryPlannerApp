using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatisfactoryBuildPlanner
{
    public partial class RecipeSelection : Form
    {
        private List<Recipe> Recipes;

        public Recipe SelectedRecipe { get; set; }
        public RecipeSelection()
        {
            InitializeComponent();
        }

        public void InitializeRecipeForm(List<Recipe> recipes)
        {
            Recipes = recipes;

            foreach (Recipe recipe in Recipes)
            {
                RecipeNameBox.Items.Add(recipe.RecipeName);
            }
        }

        private void RecipeNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = RecipeNameBox.SelectedIndex;
            RecipeInputsBox.Text = "";

            for (int i = 0; i < Recipes[selectedIndex].InputItems.Count; i++)
            {
                RecipeInputsBox.Text += $"{Recipes[selectedIndex].InputQuantity[i]} {Recipes[selectedIndex].InputItems[i]}\n";
            }
        }

        private void SelectRecipeButton_Click(object sender, EventArgs e)
        {
            SelectedRecipe = Recipes[RecipeNameBox.SelectedIndex];
            Close();
        }
    }
}
