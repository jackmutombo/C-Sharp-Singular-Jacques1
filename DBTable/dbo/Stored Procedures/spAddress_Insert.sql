CREATE PROCEDURE [dbo].[spAddress_Insert]
	@Id int output,
	@Type nvarchar(50),
	@AddressName nvarchar(200),
	@Street nvarchar(100),
	@Suburb nvarchar(100),
	@City nvarchar(100),
	@PostalCode nvarchar(50),
	@AuthUserId nvarchar(128)
AS

BEGIN

	SET NOCOUNT ON;

	insert into dbo.Address([Type], AddressName, Street,Suburb, City, PostalCode, AuthUserId)
	values (@Type, @AddressName, @Street,Suburb, @City, @PostalCode, @AuthUserId);

	SELECT CAST(SCOPE_IDENTITY() as int);

END