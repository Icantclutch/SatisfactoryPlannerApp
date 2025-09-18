namespace SatisfactoryBuildPlanner
{
    partial class RecipeSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RecipeNameBox = new ComboBox();
            SelectRecipeButton = new Button();
            RecipeInputsBox = new RichTextBox();
            SuspendLayout();
            // 
            // RecipeNameBox
            // 
            RecipeNameBox.FormattingEnabled = true;
            RecipeNameBox.Location = new Point(22, 33);
            RecipeNameBox.Name = "RecipeNameBox";
            RecipeNameBox.Size = new Size(121, 23);
            RecipeNameBox.TabIndex = 0;
            RecipeNameBox.SelectedIndexChanged += RecipeNameBox_SelectedIndexChanged;
            // 
            // SelectRecipeButton
            // 
            SelectRecipeButton.Location = new Point(374, 33);
            SelectRecipeButton.Name = "SelectRecipeButton";
            SelectRecipeButton.Size = new Size(75, 23);
            SelectRecipeButton.TabIndex = 1;
            SelectRecipeButton.Text = "Select";
            SelectRecipeButton.UseVisualStyleBackColor = true;
            SelectRecipeButton.Click += SelectRecipeButton_Click;
            // 
            // RecipeInputsBox
            // 
            RecipeInputsBox.Location = new Point(160, 33);
            RecipeInputsBox.Name = "RecipeInputsBox";
            RecipeInputsBox.Size = new Size(208, 96);
            RecipeInputsBox.TabIndex = 2;
            RecipeInputsBox.Text = "";
            // 
            // RecipeSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 188);
            Controls.Add(RecipeInputsBox);
            Controls.Add(SelectRecipeButton);
            Controls.Add(RecipeNameBox);
            Name = "RecipeSelection";
            Text = "RecipeSelection";
            ResumeLayout(false);
        }

        #endregion

        private ComboBox RecipeNameBox;
        private Button SelectRecipeButton;
        private RichTextBox RecipeInputsBox;
    }
}