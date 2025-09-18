CREATE PROCEDURE [dbo].[AddRecipeInput]
	@recipeID as int,
	@inputName as varchar(25),
	@inputQty as int

AS
	IF NOT EXISTS (SELECT [RecipeID] FROM [RecipeInputs] WHERE [RecipeID] = @recipeID AND [ItemID] = (SELECT [ItemID] FROM [Item] WHERE [ItemName] = @inputName))
		INSERT INTO [RecipeInputs] (ItemID, Quantity)
		VALUES ((SELECT [ItemID] FROM [Item] WHERE [ItemName] = @inputName), @inputQty)
RETURN 0
