CREATE PROCEDURE [dbo].[AddRecipe]
	@powerUsage as int,
	@recipeTime as int,
	@recipeName as varchar(20)

AS
	IF NOT Exists (Select [RecipeID] From [Recipe] WHERE [RecipeName] = @recipeName)
		INSERT INTO [Recipe] ([RecipeName], [PowerUsage], [RecipeTime])
		OUTPUT inserted.RecipeID
		VALUES (@recipeName, @powerUsage, @recipeTime)
RETURN 0
