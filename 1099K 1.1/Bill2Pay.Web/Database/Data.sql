DELETE FROM [dbo].[AspNetUserRoles]
DELETE FROM [dbo].[AspNetUsers]
DELETE FROM [dbo].[AspNetRoles]
GO

SET IDENTITY_INSERT [dbo].[AspNetRoles] ON
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (1, N'Admin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF

SET IDENTITY_INSERT [dbo].[AspNetUsers] ON
INSERT INTO [dbo].[AspNetUsers] ([Id], [IsDefaultPasswordChanged], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (1, 0, N'admin@b2p.com', 0, N'ABpQpiPvYOUL4ISaIFGRfiz8bZzU78gIImk+99Au7dPqSBBQ+Y/4d12WOguP0LlyUw==', N'68a8737a-f7d5-46fd-a5b8-7533801fc708', NULL, 0, 0, NULL, 0, 0, N'admin@b2p.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [IsDefaultPasswordChanged], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (2, 0, N'user@b2p.com', 0, N'AEHqLm9ZmkMq/4VGPLDKC+m0FPbXWa8Fa2ysH/CNMCpqC1vNgIyaNB83AadqmE8l0g==', N'b72a5d2c-3c39-4d30-982e-c9f0422ee116', NULL, 0, 0, NULL, 0, 0, N'user@b2p.com')
SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (1, 1)
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (2, 2)

DELETE  FROM [dbo].[Status]  
GO
SET IDENTITY_INSERT [dbo].[Status] ON
INSERT INTO [dbo].[Status]  (Id,Name) values(1,'Not Submitted')
INSERT INTO [dbo].[Status]  (Id,Name) values(2,'File Generated')
INSERT INTO [dbo].[Status]  (Id,Name) values(3,'One-Transaction Correction')
INSERT INTO [dbo].[Status]  (Id,Name) values(4,'One-Correction Uploaded')
INSERT INTO [dbo].[Status]  (Id,Name) values(5,'ReSubmitted')
INSERT INTO [dbo].[Status]  (Id,Name) values(6,'Submitted')
INSERT INTO [dbo].[Status]  (Id,Name) values(7,'Two-Transaction Correction')
INSERT INTO [dbo].[Status]  (Id,Name) values(8,'Two-Correction Uploaded')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO

TRUNCATE TABLE [dbo].[TINStatus]
GO
SET IDENTITY_INSERT [dbo].[TINStatus] ON
INSERT INTO [dbo].[TINStatus] (Id,Name) values(0,'Name TIN combination matches IRS records')
INSERT INTO [dbo].[TINStatus] (Id,Name) values(1,'TIN was missing or TIN is not a 9 digit number')
INSERT INTO [dbo].[TINStatus] (Id,Name) values(2,'TIN entered is not currently issued')
INSERT INTO [dbo].[TINStatus] (Id,Name) values(3,'Name TIN combination do not match IRS records')
INSERT INTO [dbo].[TINStatus] (Id,Name) values(4,'Invalid TIN Matching request')
INSERT INTO [dbo].[TINStatus] (Id,Name) values(5,'Duplicate TIN Matching request')
INSERT INTO [dbo].[TINStatus] (Id,Name) values(6,'Matched on SSN')
INSERT INTO [dbo].[TINStatus] (Id,Name) values(7,'Matched on EIN')
INSERT INTO [dbo].[TINStatus] (Id,Name) values(8,'Matched on EIN and SSN')
SET IDENTITY_INSERT [dbo].[TINStatus] OFF
GO

declare @TransmitterID int
DELETE FROM [dbo].[TransmitterDetails]
INSERT INTO [dbo].[TransmitterDetails] ([TransmitterTIN], [TransmitterControlCode], [TestFileIndicator], [TransmitterForeignEntityIndicator], [TransmitterName], [TransmitterNameContinued], [CompanyName], [CompanyNameContinued], [CompanyMailingAddress], [CompanyCity], [CompanyState], [CompanyZIP], [TotalNumberofPayees], [ContactName], [ContactTelephoneNumber], [ContactEmailAddress], [VendorIndicator], [VendorName], [VendorMailingAddress], [VendorCity], [VendorState], [VendorZIP], [VendorContactName], [VendorContactTelephoneNumber], [VendorForeignEntityIndicator], [IsActive], [DateAdded], [PaymentYear]) 
VALUES ( N'471471912', N'90T19', N'', N'', N'Bill2Pay, LLC', N'', N'Bill2Pay, LLC', N'', N'9428 Baymeadows Road, #600', N'Jacksonville', N'FL', N'32256', 0, N'Bill2Pay Support', N'8777676148', N'Help@Bill2Pay.com', N'I', N'', N'', N'', N'', N'', N'', N'', N'', 1, N'2017-02-23 04:45:25', 2016)

set @TransmitterID = @@IDENTITY

DECLARE @PayerID int
DELETE FROM [dbo].[PayerDetails]
INSERT INTO [dbo].[PayerDetails] ([CFSF], [PayerTIN], [PayerNameControl], [LastFilingIndicator], [ReturnType], [AmountCodes], [PayerForeignEntityIndicator], [FirstPayerName], [SecondPayerName], [TransferAgentIndicator], [PayerShippingAddress], [PayerCity], [PayerState], [PayerZIP], [PayerTelephoneNumber], [TransmitterId], [IsActive], [DateAdded], [PaymentYear])
VALUES ( N'1', N'471471912', N'BILL', N'', N'MC', N'12456789ABCDEFG', N'', N'Bill2Pay, LLC', N'', N'0', N'9428 Baymeadows Road, #600', N'Jacksonville', N'FL', N'32256', N'9044214100', @TransmitterID, 1, N'2017-02-23 04:45:25', 2016)
SET @PayerID = @@IDENTITY

DECLARE @USEERID BIGINT,@PaymentYear int = 2016
SELECT TOP 1 @USEERID=ID FROM AspNetUsers

INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('ATLANTIC',NULL,'596000267',NULL,'City of Atlantic Beach',NULL,'800 Seminole Road','Atlantic Beach ','FL','32233',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('BHADSALES',NULL,'020636401',NULL,'Bright House Networks',NULL,'700 Carillon Parkway Suite 3','Saint Petersburg ','FL','33716',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('BOONE',NULL,'366006525',NULL,'Boone County Treasurer',NULL,'1212 LOGAN AVE STE 104 ','Belvidere ','IL','61008',1,1,9311,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('BOYNTON',NULL,'596000282',NULL,'City of Boynton Beach',NULL,'100 E BOYNTON BEACH BLVD ','Boynton Beach ','FL','33435',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('CARROLL',NULL,'526000910',NULL,'Carroll County Treasurer',NULL,'225 N CENTER ST Room 103 ','Westminster ','MD','21157',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('CLAYMO',NULL,'446000477',NULL,'Clay County Collector, MO',NULL,'Clay County Admin Blding, 1 Courthouse Square','Liberty ','MO','64068',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('COJ',NULL,'596000344',NULL,'City of Jacksonville/Duval County Tax Collector',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL','32202',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('COLLIERTAX',NULL,'596000562',NULL,'Collier County Tax Collector',NULL,'3291 TAMIAMI Trail E ast ','Naples ','FL','34112',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('COLUMBIATC',NULL,'596000570',NULL,'Columbia Tax Collector',NULL,'135 NE HERNANDO AVE STE 125','Lake City ','FL','32055',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('CTL',NULL,'593540757',NULL,'Cypress Insurance Group, Inc.',NULL,'13901 SUTTON PARK DR S STE 310 ','Jacksonville ','FL','32224',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('CYPAGENT',NULL,'593540757',NULL,'Cypress Insurance Group, Inc.',NULL,'13901 SUTTON PARK DR S STE 310 ','Jacksonville ','FL','32224',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('DIXIE',NULL,'596032854',NULL,'Dixie County Tax Collector',NULL,'214 NE HIGHWAY 351 STE A ','Cross City ','FL','32628',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('DUVAL',NULL,'596000344',NULL,'Duval county Tax Collector',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL','32202',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('DOT',NULL,'592870670',NULL,'Florida Department of Transportation',NULL,'605 Suwannee Street','Tallahassee ','FL','32399',1,1,NULL,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('HYDESCHOOL',NULL,'016021559',NULL,'Hyde Schools',NULL,'616 Hights','Bath ','ME','04530',1,1,8211,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('JAXFIRE',NULL,'596000344',NULL,'Jacksonville Fire & Rescue/Duval County Tax Collector',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL','32202',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('JEA',NULL,'592983007',NULL,'Jacksonville Electric Authority',NULL,'21 W CHURCH ST','Jacksonville ','FL','32202',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('KIDCARE',NULL,'593032613',NULL,'Florida Healthy Kids Corporation',NULL,'661 E JEFFERSON ST FL 2ND ','Tallahassee ','FL','32301',1,1,6300,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LASALLE',NULL,'366006612',NULL,'La Salle County',NULL,'707 E ETNA RD','Ottawa ','IL','61350',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LAUDERHILL',NULL,'596044104',NULL,'City of Lauderhill',NULL,'5581 W OAKLAND PARK BLVD ','Lauderhill ','FL','33313',1,1,4900,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LDRHILL',NULL,'596044104',NULL,'City of Lauderhill',NULL,'5581 W OAKLAND PARK BLVD ','Lauderhill ','FL','33313',1,1,4900,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LCU',NULL,'596000702',NULL,'County of Lee Office of County','Commissioners','4980 Bayline Drive','North Fort Myers','FL','33917',1,1,NULL,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LEON',NULL,'596000714',NULL,'Leon County Tax Collector',NULL,'1276 METROPOLITAN BLVD ','Tallahassee ','FL','32312',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('MADISON',NULL,'376001410',NULL,'Madson County Treasurer',NULL,'157 N MAIN ST RM 125 ','Edwardsville ','IL','62025',1,1,9311,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('MANATEE',NULL,'596000733',NULL,'Manatee County Tax Collector',NULL,'819 301 BLVD W ','Bradenton ','FL','34205',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('MELUTIL',NULL,'596000371',NULL,'City of Melbourne',NULL,'900 E STRAWBRIDGE AVE ','Melbourne ','FL','32901',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('NASSAUBOCC',NULL,'591863042',NULL,'Nassau County BOCC',NULL,'9613 Nassau Place','Yulee ','FL','32097',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PALMBEACH',NULL,'596000791',NULL,'Constisutional Tax Collector, Palm Beach County',NULL,'301 North Olive Avenue 3re Floor','West Palm Beach ','FL','33401',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PASCOEMS',NULL,'596000793',NULL,'Pasco County EMS',NULL,'4111 Land O'' Lakes Blvd.','Land O'' Lakes ','FL','34639',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PEACHCARE',NULL,'260307682',NULL,'Peachcare for Kids',NULL,'2743 PETERS RD BAY 33-34 ','Fort Pierce ','FL','34945',1,1,6300,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PLANTATION',NULL,'596017775',NULL,'City of Plantation, Florida',NULL,'400 NW 73RD AVE ','Plantation ','FL','33317',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PINELLASCU',NULL,'596000800',NULL,'Pinellas County Utilities',NULL,'400 S FORT HARRISON AVE FL 6TH ','Clearwater ','FL','33756',1,1,4900,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PNLSEMS',NULL,'596000800',NULL,'Pinellas County EMS',NULL,'12490 ULMERTON RD ','Largo ','FL','33774',1,1,4119,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('ROCKISLAND',NULL,'366006649',NULL,'Rock Island County Treasurer',NULL,'1504 3RD AVENUE ','Rock Island ','IL','61201',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('STCLAIR',NULL,'376001924',NULL,'St. Clair County',NULL,'10 PUBLIC SQ ','Belleville ','IL','62220',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('STJOHNS',NULL,'596000831',NULL,'St. John''s County Tax Collector',NULL,'4030 Lewis Speedway','Saint Augustine ','FL','32084',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('STMARYS',NULL,'526001015',NULL,'St. Mary''s County Treasurer ',NULL,'23150 LEONARD HALL DR ','Leonardtown ','MD','20650',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('TAYLOR',NULL,'596045442',NULL,'Taylor County Tax Collector',NULL,'108 N JEFFERSON ST STE 101 ','Perry ','FL','32347',1,1,9311,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('TOHO',NULL,'562378950',NULL,'Tohopekaliga Water Authority',NULL,'951 MARTIN LUTHER KING BLVD ','Kissimmee ','FL','34741',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('WES',NULL,'900797817',NULL,'Weston Insurance Holdings Corporation',NULL,'2525 Ponce de Leon Blvd, Ste 1080','Coral Gables','FL','33134',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)


INSERT INTO [dbo].[PayerDetails] ([CFSF], [PayerTIN], [PayerNameControl], [LastFilingIndicator], [ReturnType], [AmountCodes], [PayerForeignEntityIndicator], [FirstPayerName], [SecondPayerName], [TransferAgentIndicator], [PayerShippingAddress], [PayerCity], [PayerState], [PayerZIP], [PayerTelephoneNumber], [TransmitterId], [IsActive], [DateAdded], [PaymentYear]) 
VALUES ( 1, N'471481558', N'', N'', N'MC', N'12456789ABCDEFG', N'', N'Intuition College Savings Solutions, LLC', N'', N'0', N'9428 Baymeadows Road, #600', N'Jacksonville', N'FL', N'32256', N'9044214100', @TransmitterID, 1, N'2017-02-23 04:45:25', 2016)

SET @PayerID = @@IDENTITY

INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('FloridaCS',2,'589539357',NULL,'State of Florida Florida Prepaid College Plan',NULL,'PO Box 6567','Tallahassee','FL','323146567',1,1,9311,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('MississippiCS',2,'646000837',NULL,'Mississippi Prepaid Affordable College Tuition Program',NULL,'P.O. Box 120','Jackson','MS','392050120',1,1,1799,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('IllinoisCS',2,'521752528',NULL,'College Illinois! ',NULL,'1755 Lake Cook Rd','Deerfield','IL','600155209',1,1,8299,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)

UPDATE [dbo].[MerchantDetails] SET TINType = 1 WHERE TINType IS NULL

GO
SELECT * FROM [dbo].[AspNetUserRoles]
SELECT * FROM [dbo].[AspNetUsers]
SELECT * FROM [dbo].[AspNetRoles]
SELECT * FROM [dbo].[Status]
SELECT * FROM [dbo].[TINStatus]
SELECT * FROM [dbo].[TransmitterDetails]
SELECT * FROM [dbo].[PayerDetails]
SELECT * FROM [dbo].[MerchantDetails]
GO