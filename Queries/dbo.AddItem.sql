CREATE PROCEDURE [dbo].[AddItem]
	@itemName as varchar(25)
AS
	IF NOT EXISTS (SELECT [ItemID] FROM [Item] WHERE [ItemName] = @itemName)
		INSERT INTO [Item] ([ItemName])
		VALUES (@itemName)
RETURN 0