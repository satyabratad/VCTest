DELETE  FROM Status 
GO
INSERT INTO Status (Id,Name) values(1,'Not Submitted')
GO
INSERT INTO Status (Id,Name) values(2,'File Generated')
GO
INSERT INTO Status (Id,Name) values(3,'Correction Required')
GO
INSERT INTO Status (Id,Name) values(4,'CorrectionUploaded')
GO
INSERT INTO Status (Id,Name) values(5,'ReSubmitted')
GO
INSERT INTO Status (Id,Name) values(6,'Submitted')
GO


TRUNCATE TABLE TINStatus 
GO

INSERT INTO TINStatus (Id,Name) values(0,'Name TIN combination matches IRS records')
GO
INSERT INTO TINStatus (Id,Name) values(1,'TIN was missing or TIN is not a 9 digit number')
GO
INSERT INTO TINStatus (Id,Name) values(2,'TIN entered is not currently issued')
GO
INSERT INTO TINStatus (Id,Name) values(3,'Name TIN combination do not match IRS records')
GO
INSERT INTO TINStatus (Id,Name) values(4,'Invalid TIN Matching request')
GO
INSERT INTO TINStatus (Id,Name) values(5,'Duplicate TIN Matching request')
GO
INSERT INTO TINStatus (Id,Name) values(6,'Matched on SSN')
GO
INSERT INTO TINStatus (Id,Name) values(7,'Matched on EIN')
GO
INSERT INTO TINStatus (Id,Name) values(8,'Matched on EIN and SSN')
GO



declare @TransmitterID int

insert into TransmitterDetails (TransmitterName,IsActive,DateAdded,TotalNumberofPayees)
values('Bill2Pay',1,getdate(),0)
set @TransmitterID = @@IDENTITY
select @TransmitterID

declare @PayerID int
insert into PayerDetails(FirstPayerName,IsActive,DateAdded,TransmitterId)
values('Bill2Pay',1,getdate(),@TransmitterID)

set @PayerID = @@IDENTITY



DECLARE @USEERID BIGINT,@PaymentYear int = 2016
SELECT TOP 1 @USEERID=ID FROM AspNetUsers

INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('ATLANTIC',NULL,'596000267',NULL,'City of Atlantic Beach',NULL,'800 Seminole Road','Atlantic Beach ','FL','32233',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('BHADSALES',NULL,'020636401',NULL,'Bright House Networks',NULL,'700 Carillon Parkway Suite 3','Saint Petersburg ','FL','33716',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('BOONE',NULL,'366006525',NULL,'Boone County Treasurer',NULL,'1212 LOGAN AVE STE 104 ','Belvidere ','IL','61008',1,1,9311,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('BOYNTON',NULL,'596000282',NULL,'City of Boynton Beach',NULL,'100 E BOYNTON BEACH BLVD ','Boynton Beach ','FL','33435',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('CARROLL',NULL,'526000910',NULL,'Carroll County Treasurer',NULL,'225 N CENTER ST Room 103 ','Westminster ','MD','21157',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('CLAYMO',NULL,'446000477',NULL,'Clay County Collector, MO',NULL,'Clay County Admin Blding, 1 Courthouse Square','Liberty ','MO','64068',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('COJ',NULL,'596000344',NULL,'City of Jacksonville/Duval County Tax Collector',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL','32202',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('COLLIERTAX',NULL,'596000562',NULL,'Collier County Tax Collector',NULL,'3291 TAMIAMI Trail E ast ','Naples ','FL','34112',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('COLUMBIATC',NULL,'596000570',NULL,'Columbia Tax Collector',NULL,'135 NE HERNANDO AVE STE 125','Lake City ','FL','32055',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('CTL',NULL,'593540757',NULL,'Cypress Insurance Group, Inc.',NULL,'13901 SUTTON PARK DR S STE 310 ','Jacksonville ','FL','32224',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('CYPAGENT',NULL,'593540757',NULL,'Cypress Insurance Group, Inc.',NULL,'13901 SUTTON PARK DR S STE 310 ','Jacksonville ','FL','32224',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('DIXIE',NULL,'596032854',NULL,'Dixie County Tax Collector',NULL,'214 NE HIGHWAY 351 STE A ','Cross City ','FL','32628',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('DUVAL',NULL,'596000344',NULL,'Duval county Tax Collector',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL','32202',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('DOT',NULL,'',NULL,'Florida Department of Transportation',NULL,'605 Suwannee Street','Tallahassee ','FL','32399',1,1,NULL,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('HYDESCHOOL',NULL,'016021559',NULL,'Hyde Schools',NULL,'616 Hights','Bath ','ME','04530',1,1,8211,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('JAXFIRE',NULL,'596000344',NULL,'Jacksonville Fire & Rescue/Duval County Tax Collector',NULL,'231 E FORSYTH ST Room 212 ','Jacksonville ','FL','32202',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('JEA',NULL,'592983007',NULL,'Jacksonville Electric Authority',NULL,'21 W CHURCH ST','Jacksonville ','FL','32202',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('KIDCARE',NULL,'593032613',NULL,'Florida Healthy Kids Corporation',NULL,'661 E JEFFERSON ST FL 2ND ','Tallahassee ','FL','32301',1,1,6300,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LASALLE',NULL,'366006612',NULL,'La Salle County',NULL,'707 E ETNA RD','Ottawa ','IL','61350',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LAUDERHILL',NULL,'596044104',NULL,'City of Lauderhill',NULL,'5581 W OAKLAND PARK BLVD ','Lauderhill ','FL','33313',1,1,4900,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LDRHILL',NULL,'596044104',NULL,'City of Lauderhill',NULL,'5581 W OAKLAND PARK BLVD ','Lauderhill ','FL','33313',1,1,4900,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LCU',NULL,'',NULL,'Lee County Electric Cooperative, Inc',NULL,'4980 Bayline Drive','North Fort Myers','FL','33917',1,1,NULL,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('LEON',NULL,'596000714',NULL,'Leon County Tax Collector',NULL,'1276 METROPOLITAN BLVD ','Tallahassee ','FL','32312',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('MADISON',NULL,'376001410',NULL,'Madson County Treasurer',NULL,'157 N MAIN ST RM 125 ','Edwardsville ','IL','62025',1,1,9311,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('MANATEE',NULL,'596000733',NULL,'Manatee County Tax Collector',NULL,'819 301 BLVD W ','Bradenton ','FL','34205',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('MELUTIL',NULL,'596000371',NULL,'City of Melbourne',NULL,'900 E STRAWBRIDGE AVE ','Melbourne ','FL','32901',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('NASSAUBOCC',NULL,'591863042',NULL,'Nassau County BOCC',NULL,'9613 Nassau Place','Yulee ','FL','32097',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PALMBEACH',NULL,'596000791',NULL,'Constisutional Tax Collector, Palm Beach County',NULL,'301 North Olive Avenue 3re Floor','West Palm Beach ','FL','33401',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PASCOEMS',NULL,'596000793',NULL,'Pasco County EMS',NULL,'4111 Land O'' Lakes Blvd.','Land O'' Lakes ','FL','34639',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PEACHCARE',NULL,'260307682',NULL,'Peachcare for Kids',NULL,'2743 PETERS RD BAY 33-34 ','Fort Pierce ','FL','34945',1,1,6300,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PLANTATION',NULL,'596017775',NULL,'City of Plantation, Florida',NULL,'400 NW 73RD AVE ','Plantation ','FL','33317',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PINELLASCU',NULL,'596000800',NULL,'Pinellas County Utilities',NULL,'400 S FORT HARRISON AVE FL 6TH ','Clearwater ','FL','33756',1,1,4900,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('PNLSEMS',NULL,'596000800',NULL,'Pinellas County EMS',NULL,'12490 ULMERTON RD ','Largo ','FL','33774',1,1,4119,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('ROCKISLAND',NULL,'366006649',NULL,'Rock Island County Treasurer',NULL,'1504 3RD AVENUE ','Rock Island ','IL','61201',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('STCLAIR',NULL,'376001924',NULL,'St. Clair County',NULL,'10 PUBLIC SQ ','Belleville ','IL','62220',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('STJOHNS',NULL,'596000831',NULL,'St. John''s County Tax Collector',NULL,'4030 Lewis Speedway','Saint Augustine ','FL','32084',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('STMARYS',NULL,'526001015',NULL,'St. Mary''s County Treasurer ',NULL,'23150 LEONARD HALL DR ','Leonardtown ','MD','20650',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('TAYLOR',NULL,'596045442',NULL,'Taylor County Tax Collector',NULL,'108 N JEFFERSON ST STE 101 ','Perry ','FL','32347',1,1,9311,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('TOHO',NULL,'562378950',NULL,'Tohopekaliga Water Authority',NULL,'951 MARTIN LUTHER KING BLVD ','Kissimmee ','FL','34741',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)
INSERT INTO MerchantDetails(PayeeAccountNumber,TINType,PayeeTIN,PayeeOfficeCode,PayeeFirstName,PayeeSecondName,PayeeMailingAddress,PayeeCity,PayeeState,PayeeZIP,FilerIndicatorType,PaymentIndicatorType,MCC,IsActive,DateAdded,UserId,PayerId,PaymentYear) VALUES ('WES',NULL,'900797817',NULL,'Weston Insurance Holdings Corporation',NULL,'2525 Ponce de Leon Blvd, Ste 1080','Coral Gables','FL','33134',1,1,9399,1,GETDATE(),@USEERID,@PayerId,@PaymentYear)

