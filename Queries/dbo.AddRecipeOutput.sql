CREATE PROCEDURE [dbo].[AddRecipeOutput]
	@recipeID as int,
	@outputName as varchar(25),
	@outputQty as int
AS
	IF NOT EXISTS (SELECT [RecipeID] FROM [RecipeOutputs] WHERE [RecipeID] = @recipeID AND [ItemID] = (SELECT [ItemID] FROM [Item] WHERE [ItemName] = @outputName))
		INSERT INTO [RecipeOutputs] (ItemID, Quantity)
		VALUES ((SELECT [ItemID] FROM [Item] WHERE [ItemName] = @outputName), @outputQty)
RETURN 0
