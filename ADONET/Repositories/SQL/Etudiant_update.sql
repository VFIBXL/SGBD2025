update [dbo].[Etudiant]
   set [ETU_NOM] = @Nom
      ,[ETU_PRENOM] = @Prenom
      ,[ETU_MATRICULE] = @Matricule
 where ETU_ID = @Id