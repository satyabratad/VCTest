-- PEACHCARE
SELECT * FROM [dbo].[MerchantDetails] WHERE PayeeAccountNumber = 'PEACHCARE' AND IsActive = 1
-- OLD VALUE: 'Peachcare for Kids'
GO
UPDATE [dbo].[MerchantDetails] SET 
FirstPayeeName = 'MAXIMUS Health Services' 
WHERE PayeeAccountNumber = 'PEACHCARE' AND IsActive = 1
GO
SELECT * FROM [dbo].[MerchantDetails] WHERE PayeeAccountNumber = 'PEACHCARE' AND IsActive = 1
-- NEW VALUE: 'MAXIMUS Health Services'
GO
--LCU