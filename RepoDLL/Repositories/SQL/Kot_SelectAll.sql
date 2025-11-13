select KOT_ID  , KOT_NAME , ETU_MATRICULE , ETU_NOM , ETU_PRENOM  from dbo.Kot
left join dbo.Etudiant on Etudiant.ETU_ID = Kot.KOT_ETUD_ID