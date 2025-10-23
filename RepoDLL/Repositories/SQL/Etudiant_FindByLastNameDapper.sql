SELECT Etu_Id as Id, Etu_Nom as LastName, Etu_Matricule as Matricule, Etu_Prenom as FirstNAme from dbo.Etudiant
where Etu_Nom like @lastName