/*Create the type*/
CREATE TYPE IntegersList as Table (Id int);

/*Create the example table*/
CREATE TABLE [dbo].[Values](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Values] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*Create the Stored Procedure*/
CREATE PROCEDURE Values_GetValues
	@ListIds IntegersList READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM [Values]
	WHERE Id IN (SELECT Id FROM @ListIds);
END
GO
