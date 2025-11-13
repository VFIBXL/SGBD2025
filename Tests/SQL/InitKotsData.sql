USE [CoursSGBD]

delete from [dbo].[Kot]
delete from [dbo].[Etudiant]

SET IDENTITY_INSERT [dbo].[Etudiant] ON;

insert into [dbo].[Etudiant]  ( [ETU_ID],[ETU_NOM], [ETU_PRENOM], [ETU_MATRICULE])
VALUES
(3, 'Fievez', 'vincent2', 'HE1' ),
(5, 'Toto', 'tot2', 'HE3' ),
(6 ,'Johnson', 'Alice', 'A003' )
SET IDENTITY_INSERT [dbo].[Etudiant] OFF;

set IDENTITY_INSERT [dbo].[Kot] ON;

INSERT INTO [dbo].[Kot] ([KOT_ID], [KOT_NAME], [KOT_ETUD_ID])
VALUES
( 1, 'Kot1', 6 ),
( 2, 'Kot2', NULL )

SET IDENTITY_INSERT [dbo].[Kot] off;
