using System.Data;

namespace SatisfactoryBuildPlanner
{
    public partial class SatisfactorybuildPlanner : Form
    {
        private List<int> RawMaterialList = new List<int>()
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 19, 20
        };

        public SatisfactorybuildPlanner()
        {
            InitializeComponent();

            DatabaseController.InitializeDatabase("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TJ\\Desktop\\VSProjects\\SatisfactoryBuildPlanner\\Satisfactory.mdf;Integrated Security=True");

            GetItemList();
        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            DatabaseController.AddItem(AddItemTextbox.Text);


            //Update recipe combo boxes
            InputBox1.Items.Clear();
            InputBox2.Items.Clear();
            InputBox3.Items.Clear();
            InputBox4.Items.Clear();
            OutputBox1.Items.Clear();
            OutputBox2.Items.Clear();
            ItemBreakdownBox.Items.Clear();

            GetItemList();

        }

        private void AddRecipe_Click(object sender, EventArgs e)
        {
            //Add Recipe
            string powerUsage = PowerUsageBox.Text;
            string time = TimeBox.Text;
            string recipeName = RecipeNameBox.Text;

            int recipeID = DatabaseController.AddRecipe(powerUsage, time, recipeName);

            //Add Inputs
            if (!String.IsNullOrEmpty(InputBox1.Text) && !String.IsNullOrEmpty(InputQuantityBox1.Text))
            {
                string itemName = InputBox1.Text;
                float inputQuantity = (float)Convert.ToDouble(InputQuantityBox1.Text);

                DatabaseController.AddRecipeInput(recipeID, itemName, inputQuantity);
            }

            if (!String.IsNullOrEmpty(InputBox2.Text) && !String.IsNullOrEmpty(InputQuantityBox2.Text))
            {
                string itemName = InputBox2.Text;
                float inputQuantity = (float)Convert.ToDouble(InputQuantityBox2.Text);

                DatabaseController.AddRecipeInput(recipeID, itemName, inputQuantity);
            }

            if (!String.IsNullOrEmpty(InputBox3.Text) && !String.IsNullOrEmpty(InputQuantityBox3.Text))
            {
                string itemName = InputBox3.Text;
                float inputQuantity = (float)Convert.ToDouble(InputQuantityBox3.Text);

                DatabaseController.AddRecipeInput(recipeID, itemName, inputQuantity);
            }

            if (!String.IsNullOrEmpty(InputBox4.Text) && !String.IsNullOrEmpty(InputQuantityBox4.Text))
            {
                string itemName = InputBox4.Text;
                float inputQuantity = (float)Convert.ToDouble(InputQuantityBox4.Text);

                DatabaseController.AddRecipeInput(recipeID, itemName, inputQuantity);
            }


            //Add Outputs

            if (!String.IsNullOrEmpty(OutputBox1.Text) && !String.IsNullOrEmpty(OutputQuantityBox1.Text))
            {
                string itemName = OutputBox1.Text;
                float inputQuantity = (float)Convert.ToDouble(OutputQuantityBox1.Text);

                DatabaseController.AddRecipeOutput(recipeID, itemName, inputQuantity);
            }

            if (!String.IsNullOrEmpty(OutputBox2.Text) && !String.IsNullOrEmpty(OutputQuantityBox2.Text))
            {
                string itemName = OutputBox2.Text;
                float inputQuantity = (float)Convert.ToDouble(OutputQuantityBox2.Text);

                DatabaseController.AddRecipeOutput(recipeID, itemName, inputQuantity);
            }
        }

        private void GetItemList()
        {
            DataTable table = DatabaseController.ExecuteQuery("SELECT * FROM [Item] ORDER BY ItemName");

            object[] items = new object[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                items[i] = table.Rows[i]["ItemName"].ToString();
            }

            InputBox1.Items.AddRange(items);
            InputBox2.Items.AddRange(items);
            InputBox3.Items.AddRange(items);
            InputBox4.Items.AddRange(items);

            OutputBox1.Items.AddRange(items);
            OutputBox2.Items.AddRange(items);

            ItemBreakdownBox.Items.AddRange(items);
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            string tableName = TablesBox.Text;

            if (tableName != null || tableName != "")
            {
                DataTable resultTable = DatabaseController.ExecuteQuery($"Select * From [{tableName}]");
                ResultDataGridView.DataSource = resultTable;
            }
        }

        private void ExecuteQueryButton_Click(object sender, EventArgs e)
        {
            DataTable resultTable = DatabaseController.ExecuteQuery(QueryBox.Text);

            ResultDataGridView.DataSource = resultTable;
        }

        private void BreakdownButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ItemBreakdownBox.Text))
            {
                //Get itme ID from Database
                int baseItemID = Convert.ToInt32(DatabaseController.ExecuteQuery($"SELECT * FROM [Item] WHERE [ItemName] = '{ItemBreakdownBox.Text}'").Rows[0]["ItemID"]);

                //Begin Algorithm
                //Check if item layer is base raw material
                //Get all Alternate Recipes
                //Popup box to get user input on which recipe to use
                //Gather all inputs for recipe
                //Loop through each input

                //Do algorithm on this layer
                List<Item> items = new List<Item>();
                ItemBreakdown(baseItemID, Convert.ToInt32(ItemBreakdownQtyBox.Text), ref items);

                List<Item> SortedItemList = new List<Item>();

                //Make result datatable
                DataTable resultTable = CreateBreakdownResultTable();

                //Combine similar item quantities
                foreach (Item item in items)
                {
                    resultTable.Rows[item.ItemID]["ItemQuantity"] = item.ItemQuantity + Convert.ToDouble(resultTable.Rows[item.ItemID]["ItemQuantity"]);
                }

                //Remove unused rows
                for (int i = 0; i < resultTable.Rows.Count; i++)
                {
                    if (Convert.ToDouble(resultTable.Rows[i]["ItemQuantity"]) == 0)
                    {
                        resultTable.Rows.RemoveAt(i);
                        i--;
                    }
                }

                ItemBreakdownDataGridView.DataSource = resultTable;
            }
        }


        private void ItemBreakdown(int itemID, double quantity, ref List<Item> items)
        {

            //check if item is a base raw material. 0-9 are raw materials
            if(RawMaterialList.Contains(itemID))
            {                
                items.Add(new Item(itemID, quantity));
                return;
            }

            //Check all recipes for itemID
            DataTable resultTable = DatabaseController.GetItemRecipes(itemID);

            List<Recipe> recipes = new List<Recipe>();
            //For each recipe, get the inputs
            foreach (DataRow row in resultTable.Rows)
            {
                Recipe recipe = new Recipe();
                recipe.RecipeID = Convert.ToInt32(row["RecipeID"]);
                recipe.RecipeName = row["RecipeName"].ToString();
                recipe.RecipeQuantity = (float)Convert.ToDouble(row["Quantity"]);

                recipes.Add(recipe);
            }

            //Get the inputs for each recipe
            foreach(Recipe recipe in recipes)
            {
                DataTable inputsTable = DatabaseController.GetRecipeInputs(recipe.RecipeID);

                foreach(DataRow row in inputsTable.Rows)
                {
                    recipe.InputItems.Add(row["ItemName"].ToString());
                    recipe.InputIDs.Add(Convert.ToInt32(row["ItemID"]));
                    recipe.InputQuantity.Add(Convert.ToInt32(row["Quantity"]));
                }
            }
            Recipe selectedRecipe = recipes[0];

            if (recipes.Count > 1)
            {
                RecipeSelection recipeSelectionForm = new RecipeSelection();
                recipeSelectionForm.InitializeRecipeForm(recipes);
                recipeSelectionForm.ShowDialog();
                selectedRecipe = recipeSelectionForm.SelectedRecipe;
            }
           
            //Breakdown each ingrediant. Divide by the ingredient output for the recipe, we only want the input ingredients for 1 item. But also multiply by the amount of said ingredient we need up to this point
            for(int i = 0; i < selectedRecipe.InputItems.Count; i++)
            {
                ItemBreakdown(selectedRecipe.InputIDs[i], ((double)selectedRecipe.InputQuantity[i]) / selectedRecipe.RecipeQuantity * quantity, ref items);
            }
        }


        private DataTable CreateBreakdownResultTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ItemID");
            table.Columns.Add("ItemName");
            table.Columns.Add("ItemQuantity");

            for(int i = 0; i < ItemBreakdownBox.Items.Count; i++)
            {
                table.Rows.Add(i, ItemBreakdownBox.Items[i], 0);
            }

            return table;
        }
    }
}
