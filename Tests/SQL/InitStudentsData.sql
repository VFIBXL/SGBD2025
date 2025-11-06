USE [CoursSGBD]

delete from [dbo].[Etudiant]

insert into [dbo].[Etudiant]  ([ETU_NOM], [ETU_PRENOM], [ETU_MATRICULE])
VALUES
( 'Fievez', 'vincent2', 'HE1' ),
( 'Toto', 'tot2', 'HE3' ),
( 'Johnson', 'Alice', 'A003' )