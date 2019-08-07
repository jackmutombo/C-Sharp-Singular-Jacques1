CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id int output,
	@AuthUserId nvarchar(128),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@EmailAddress nvarchar(256),
	@CellPhone varchar(20)

AS

BEGIN

	SET NOCOUNT ON;

	insert into [dbo].[User](AuthUserId, FirstName, LastName, EmailAddress, CellPhone)
	values (@AuthUserId, @FirstName, @LastName, @EmailAddress, @CellPhone);

	select @Id =@@IDENTITY;

END