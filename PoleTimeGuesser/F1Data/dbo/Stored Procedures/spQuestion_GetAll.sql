CREATE PROCEDURE [dbo].[spQuestion_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id,Question,AnswerA,AnswerB,AnswerC
	FROM [dbo].[Questions];
END