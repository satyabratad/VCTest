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

DECLARE @USEERID BIGINT
SELECT TOP 1 @USEERID=ID FROM AspNetUsers

INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('ATLANTIC',1,'596000267',NULL,'City of Atlantic Beach',NULL,'800 Seminole Road','Atlantic Beach ','FL',32233,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('BHADSALES',1,'020636401',NULL,'Bright House Networks',NULL,'700 Carillon Parkway Suite 3','Saint Petersburg ','FL',33716,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('BOONE',1,'366006525',NULL,'Boone County Treasurer',NULL,'1212 LOGAN AVE STE 104 ','Belvidere ','IL',61008,2,1,9311,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('BOYNTON',1,'596000282',NULL,'City of Boynton Beach',NULL,'100 E BOYNTON BEACH BLVD ','Boynton Beach ','FL',33435,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('CARROLL',1,'526000910',NULL,'Carroll County Treasurer',NULL,'225 N CENTER ST Room 103 ','Westminster ','MD',21157,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('CLAYMO',1,'446000477',NULL,'Clay County Collector, MO',NULL,'ClayCountyAdminBlding,1CourthouseSquare','Liberty ','MO',64068,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('COJ',1,'596000344',NULL,'City of Jacksonville Duval County Tax Co',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL',32202,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('COLLIERTAX',1,'596000562',NULL,'Collier County Tax Collector',NULL,'3291 TAMIAMI Trail E ast ','Naples ','FL',34112,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('COLUMBIATC',1,'596000570',NULL,'County of Columbia',NULL,'135 NE HERNANDO AVE STE 125','Lake City ','FL',32055,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('CTL',1,'593540757',NULL,'Cypress Insurance Group, Inc.',NULL,'13901 SUTTON PARK DR S STE 310 ','Jacksonville ','FL',32224,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('CYPAGENT',1,'593540757',NULL,'Cypress Insurance Group, Inc.',NULL,'13901 SUTTON PARK DR S STE 310 ','Jacksonville ','FL',32224,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('DIXIE',1,'596032854',NULL,'Dixie County Tax Collector',NULL,'214 NE HIGHWAY 351 STE A ','Cross City ','FL',32628,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('DUVAL',1,'596000344',NULL,'City of Jacksonville Duval County Tax Co',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL',32202,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('DOT',1,'592870670',NULL,'Florida Department of Transportation',NULL,'605 Suwannee Street','Tallahassee ','FL',32399,2,1,NULL,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('HYDESCHOOL',1,'016021559',NULL,'Hyde Schools',NULL,'616 Hights','Bath ','ME',4530,2,1,8211,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('JAXFIRE',1,'596000344',NULL,'City of Jacksonville Duval County Tax Co',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL',32202,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('JEA',1,'592983007',NULL,'Jacksonville Electric Authority',NULL,'21 W CHURCH ST','Jacksonville ','FL',32202,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('KIDCARE',1,'593032613',NULL,'Florida Healthy Kids Corporation',NULL,'661 E JEFFERSON ST FL 2ND ','Tallahassee ','FL',32301,2,1,6300,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('LASALLE',1,'366006612',NULL,'La Salle County',NULL,'707 E ETNA RD','Ottawa ','IL',61350,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('LAUDERHILL',1,'596044104',NULL,'City of Lauderhill',NULL,'5581 W OAKLAND PARK BLVD ','Lauderhill ','FL',33313,2,1,4900,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('LDRHILL',1,'596044104',NULL,'City of Lauderhill',NULL,'5581 W OAKLAND PARK BLVD ','Lauderhill ','FL',33313,2,1,4900,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('LCU',1,'596000702',NULL,'County of Lee Office of County','Commissioners','4980 Bayline Drive','North Fort Myers','FL',33917,2,1,4900,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('LEON',1,'596000714',NULL,'Leon County Tax Collector',NULL,'1276 METROPOLITAN BLVD ','Tallahassee ','FL',32312,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('MADISON',1,'376001410',NULL,'Madson County Treasurer',NULL,'157 N MAIN ST RM 125 ','Edwardsville ','IL',62025,2,1,9311,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('MANATEE',1,'596000733',NULL,'Manatee County Tax Collector',NULL,'819 301 BLVD W ','Bradenton ','FL',34205,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('MELUTIL',1,'596000371',NULL,'City of Melbourne',NULL,'900 E STRAWBRIDGE AVE ','Melbourne ','FL',32901,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('NASSAUBOCC',1,'591863042',NULL,'Board of County Commissioners ','of Nassau County','9613 Nassau Place','Yulee ','FL',32097,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('PALMBEACH',1,'596000791',NULL,'County of Palm Beach ','Office of the Tax Collector','301 North Olive Avenue 3re Floor','West Palm Beach ','FL',33401,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('PASCOEMS',1,'596000793',NULL,'Pasco County EMS',NULL,'4111 Land O'' Lakes Blvd.','Land O'' Lakes ','FL',34639,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('PEACHCARE',1,'260307682',NULL,'Peachcare for Kids',NULL,'2743 PETERS RD BAY 33-34 ','Fort Pierce ','FL',34945,2,1,6300,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('PLANTATION',1,'596017775',NULL,'City of Plantation, Florida',NULL,'400 NW 73RD AVE ','Plantation ','FL',33317,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('PINELLASCU',1,'596000800',NULL,'Pinellas County Utilities',NULL,'400 S FORT HARRISON AVE FL 6TH ','Clearwater ','FL',33756,2,1,4900,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('PNLSEMS',1,'596000800',NULL,'Pinellas County EMS',NULL,'12490 ULMERTON RD ','Largo ','FL',33774,2,1,4119,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('ROCKISLAND',1,'366006649',NULL,'Rock Island County Treasurer',NULL,'1504 3RD AVENUE ','Rock Island ','IL',61201,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('STCLAIR',1,'376001924',NULL,'St. Clair County',NULL,'10 PUBLIC SQ ','Belleville ','IL',62220,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('STJOHNS',1,'596000831',NULL,'St. John''s County Tax Collector',NULL,'4030 Lewis Speedway','Saint Augustine ','FL',32084,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('STMARYS',1,'526001015',NULL,'St. Mary''s County Treasurer ',NULL,'23150 LEONARD HALL DR ','Leonardtown ','MD',20650,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('TAYLOR',1,'596045442',NULL,'Taylor County Tax Collector',NULL,'108 N JEFFERSON ST STE 101 ','Perry ','FL',32347,2,1,9311,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('TOHO',1,'562378950',NULL,'Tohopekaliga Water Authority',NULL,'951 MARTIN LUTHER KING BLVD ','Kissimmee ','FL',34741,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('WES',1,'900797817',NULL,'Weston Insurance Holdings Corporation',NULL,'2525 Ponce de Leon Blvd, Ste 1080','Coral Gables','FL',33134,2,1,9399,NULL,@PayerId,1,GETDATE(),@USEERID)


INSERT INTO [dbo].[PayerDetails] ([CFSF], [PayerTIN], [PayerNameControl], [LastFilingIndicator], [ReturnType], [AmountCodes], [PayerForeignEntityIndicator], [FirstPayerName], [SecondPayerName], [TransferAgentIndicator], [PayerShippingAddress], [PayerCity], [PayerState], [PayerZIP], [PayerTelephoneNumber], [TransmitterId], [IsActive], [DateAdded], [PaymentYear]) 
VALUES ( 1, N'471481558', N'', N'', N'MC', N'12456789ABCDEFG', N'', N'Intuition College Savings Solutions, LLC', N'', N'0', N'9428 Baymeadows Road, #600', N'Jacksonville', N'FL', N'32256', N'9044214100', @TransmitterID, 1, N'2017-02-23 04:45:25', 2016)

SET @PayerID = @@IDENTITY

INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('FloridaCS',1,'421530104',NULL,'Florida Prepaid College Plan',NULL,'PO Box 6567','Tallahassee','FL',323146567,2,1,9311,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('MississippiCS',1,'646000837',NULL,'Mississippi Office of the State Treasure',NULL,'P.O. Box 120','Jackson','MS',392050120,2,1,1799,NULL,@PayerId,1,GETDATE(),@USEERID)
INSERT INTO [dbo].[MerchantDetails](PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,FirstPayeeName,SecondPayeeName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,CFSF,PayerId,IsActive,DateAdded,UserId) VALUES ('IllinoisCS',1,'521752528',NULL,'Illinois Student Assistance Commission',NULL,'1755 Lake Cook Rd','Deerfield','IL',600155209,2,1,8299,NULL,@PayerId,1,GETDATE(),@USEERID)

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