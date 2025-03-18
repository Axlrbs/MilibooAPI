using Microsoft.EntityFrameworkCore;

namespace MilibooAPI.Models.EntityFramework
{
    public partial class MilibooDBContext : DbContext
    {
        
        public MilibooDBContext()
        {
        }

        public MilibooDBContext(DbContextOptions<MilibooDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AComme> ACommes { get; set; }
        public virtual DbSet<APour> APours { get; set; }
        public virtual DbSet<Adresse> Adresses { get; set; }
        public virtual DbSet<Appartient> Appartients { get; set; }
        public virtual DbSet<AvisClient> AvisClients { get; set; }
        public virtual DbSet<Boutique> Boutiques { get; set; }
        public virtual DbSet<CarteBancaire> Cartebancaires { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Coloris> Coloris { get; set; }
        public virtual DbSet<Commande> Commandes { get; set; }
        public virtual DbSet<Constitue> Constitues { get; set; }
        public virtual DbSet<Demande> Demandes { get; set; }
        public virtual DbSet<EnsembleProduit> EnsembleProduits { get; set; }
        public virtual DbSet<EstAjouteDans> EstAjouteDans { get; set; }
        public virtual DbSet<EstCommande> EstCommandes { get; set; }
        public virtual DbSet<EstConstitue> EstConstitues { get; set; }
        public virtual DbSet<EstDans> EstDans { get; set; }
        public virtual DbSet<EstDeCouleur> EstDeCouleurs { get; set; }
        public virtual DbSet<EstInclu> EstInclus { get; set; }
        public virtual DbSet<EstInscriteDans> EstIncriteDans { get; set; }
        public virtual DbSet<Liker> Likers { get; set; }
        public virtual DbSet<LivraisonDomicile> LivraisonDomiciles { get; set; }
        public virtual DbSet<Panier> Paniers { get; set; }
        public virtual DbSet<Pays> Pays { get; set; }
        public virtual DbSet<Paypal> Paypals { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Phototheque> Phototheques { get; set; }
        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<Professionnel> Professionnels { get; set; }
        public virtual DbSet<Recherche> Recherches { get; set; }
        public virtual DbSet<Regroupement> Regroupements { get; set; }
        public virtual DbSet<SeSitue> SeSitues { get; set; }
        public virtual DbSet<TypePaiement> TypePaiements { get; set; }
        public virtual DbSet<TypeProduit> TypeProduits { get; set; }
        public virtual DbSet<Ville> Villes { get; set; }
        public virtual DbSet<VirementBancaire> VirementBancaires { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=miliboo; uid=postgres; password=postgres;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.HasDefaultSchema("miliboo"); // Définit le schéma par défaut


            // Configuration des valeurs par défaut et autres propriétés spéciales


            modelBuilder.Entity<AComme>(entity =>
            {
                entity.HasKey(e => new { e.ProduitId, e.CategorieId })
                    .HasName("pk_acm");

                entity.HasOne(d => d.IdcategorieNavigation)
                    .WithMany(p => p.ACommes)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_acm_cat");

                entity.HasOne(d => d.IdproduitNavigation)
                    .WithMany(p => p.ACommes)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_acm_prd");
            });

            modelBuilder.Entity<APour>(entity =>
            {
                entity.HasKey(e => new { e.AdresseId, e.ClientId })
                    .HasName("pk_apr");

                entity.HasOne(d => d.IdAdresseNavigation)
                    .WithMany(p => p.APours)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_apr_adr");

                /*  entity.HasOne(d => d.IdClientNavigation)
                        .WithMany(p => p.Apours)
                        .OnDelete(DeleteBehavior.Restrict) ->>> Manque une invers property
                        .HasConstraintName("fk_adr_pyp");*/
            });

            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasKey(e => e.AdresseId)
                    .HasName("pk_adr");
                entity.Property(e => e.CodePostal).IsFixedLength();
                entity.Property(e => e.NumeroInsee).IsFixedLength();

                /* entity.HasOne(d => d.NumeroinseeNavigation)
                    .WithMany(p => p.NumeroInsee) <-- PB ici le num insee veut pas
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_adr_utl"); */

                entity.HasOne(d => d.IdPaysNavigation)
                    .WithMany(p => p.Adresses)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_adr_pyp");

            });

            modelBuilder.Entity<Appartient>(entity =>
            {
                entity.HasKey(e => new { e.RegroupementId, e.ProduitId })
                    .HasName("pk_aprt");


                entity.HasOne(d => d.IdProduitNavigation)
                    .WithMany(p => p.Appartients)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_aprt_prd");

                entity.HasOne(d => d.IdRegroupementNavigation)
                    .WithMany(p => p.Appartients)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_aprt_rgp");

            });



            modelBuilder.Entity<AvisClient>(entity =>
            {
                entity.HasKey(e => e.AvisId)
                    .HasName("pk_avc");
                entity.Property(e => e.DateAvis).HasDefaultValueSql("now()");

                entity.HasOne(d => d.IdproduitNavigation)
                    .WithMany(p => p.AvisClients)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_avc_prd");

            });

            modelBuilder.Entity<Boutique>(entity =>
            {
                entity.HasKey(e => e.BoutiqueId)
                    .HasName("pk_btq");

                entity.HasOne(d => d.IdadresseNavigation)
                    .WithMany(p => p.Boutiques)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_btq_adr");

            });

            modelBuilder.Entity<CarteBancaire>(entity =>
            {
                entity.HasKey(e => e.CarteBancaireId)
                    .HasName("pk_crtban");
                entity.Property(e => e.Libelletypepaiement).HasDefaultValue("Carte Bancaire");
            });

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.HasKey(e => e.CategorieId)
                    .HasName("pk_cat");

                entity.HasOne(d => d.CatIdcategorieNavigation)
                    .WithMany(p => p.InverseCatIdcategorieNavigation)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cat_cat");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("pk_clt");

                entity.Property(e => e.NbTotalPointsFidelite).HasDefaultValue(0);
                entity.Property(e => e.NombreAvisDepose).HasDefaultValue(0);
                entity.Property(e => e.DateDerniereUtilisation).HasDefaultValueSql("now()");
                entity.Property(e => e.IsVerified).HasDefaultValue(false);
            });

            modelBuilder.Entity<Coloris>(entity =>
            {
                entity.HasKey(e => e.ColorisId)
                    .HasName("pk_clr");



            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => e.CommandeId)
                    .HasName("pk_cmd");

                entity.Property(e => e.DateFacture).HasDefaultValueSql("now()");

                // Modele a chier faut refaire les inverse property
            });

            modelBuilder.Entity<Constitue>(entity =>
            {
                entity.HasKey(e => new { e.ProduitId, e.EnsembleId })
                    .HasName("pk_cst");

                entity.HasOne(d => d.IdensembleNavigation)
                    .WithMany(p => p.Constitues)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cst_esp");

                entity.HasOne(d => d.IdproduitNavigation)
                    .WithMany(p => p.Constitues)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cst_prd");

            });

            modelBuilder.Entity<Demande>(entity =>
            {
                entity.HasKey(e => new { e.ProduitId, e.ProfessionnelId })
                    .HasName("pk_dmd");

                entity.HasOne(d => d.IdproduitNavigation)
                    .WithMany(p => p.Demandes)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_dmd_prd");


            });

            modelBuilder.Entity<EnsembleProduit>(entity =>
            {
                entity.HasKey(e => e.EnsembleId)
                    .HasName("pk_esp");

            });

            modelBuilder.Entity<EstAjouteDans>(entity =>
            {
                entity.HasKey(e => new { e.ProduitId, e.ColorisId, e.PanierId })
                    .HasName("pk_ejd");

                entity.HasOne(d => d.IdcolorisNavigation)
                    .WithMany(p => p.EstAjouteDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ejd_clr");

                entity.HasOne(d => d.IdpanierNavigation)
                    .WithMany(p => p.EstAjouteDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ejd_pan");

                entity.HasOne(d => d.IdproduitNavigation)
                    .WithMany(p => p.EstAjouteDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ejd_prd");


            });

            modelBuilder.Entity<EstAjouteDans>(entity =>
            {
                entity.HasKey(e => new { e.ProduitId, e.ColorisId, e.PanierId })
                    .HasName("pk_ejd");

                entity.HasOne(d => d.IdcolorisNavigation)
                    .WithMany(p => p.EstAjouteDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ejd_clr");

                entity.HasOne(d => d.IdpanierNavigation)
                    .WithMany(p => p.EstAjouteDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ejd_pan");

                entity.HasOne(d => d.IdproduitNavigation)
                    .WithMany(p => p.EstAjouteDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ejd_prd");


            });

            modelBuilder.Entity<EstCommande>(entity =>
            {
                entity.HasKey(e => new { e.CommandeId, e.EstDeCouleurId })
                    .HasName("pk_esc");

                // Manque property FK

            });

            modelBuilder.Entity<EstConstitue>(entity =>
            {
                entity.HasKey(e => new { e.AvisId, e.Codephoto })
                    .HasName("pk_esco");

                entity.HasOne(d => d.CodephotoNavigation)
                    .WithMany(p => p.EstConstitues)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_esco_pht");

                entity.HasOne(d => d.IdavisNavigation)
                    .WithMany(p => p.EstConstitues)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_esco_avs");


            });


            modelBuilder.Entity<EstDans>(entity =>
            {
                entity.HasKey(e => new { e.CategorieId, e.TypeProduitId })
                    .HasName("pk_esd");

                entity.HasOne(d => d.IdtypeproduitNavigation)
                    .WithMany(p => p.EstDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_esd_prd");

                entity.HasOne(d => d.IdcategorieNavigation)
                    .WithMany(p => p.EstDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_esd_cat");


            });

            modelBuilder.Entity<EstDeCouleur>(entity =>
            {
                entity.HasKey(e => e.EstDeCouleurId)
                    .HasName("pk_edc");

                entity.HasOne(d => d.IdcolorisNavigation)
                    .WithMany(p => p.EstDeCouleurs)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_edc_clr");

                entity.HasOne(d => d.IdproduitNavigation)
                    .WithMany(p => p.EstDeCouleurs)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_edc_prd");

                //maque fk code photo

            });

            modelBuilder.Entity<EstInclu>(entity =>
            {
                entity.HasKey(e => new { e.CategorieId, e.EnsembleId })
                    .HasName("pk_esi");

                entity.HasOne(d => d.IdcategorieNavigation)
                    .WithMany(p => p.EstInclus)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_esi_cat");

                entity.HasOne(d => d.IdensembleNavigation)
                    .WithMany(p => p.EstInclus)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_esi_esp");

            });

            modelBuilder.Entity<EstInscriteDans>(entity =>
            {
                entity.HasKey(e => new { e.Codephoto, e.Codephototheque })
                    .HasName("pk_eid");

                entity.HasOne(d => d.CodephotoNavigation)
                    .WithMany(p => p.EstIncriteDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_eid_pht");

                entity.HasOne(d => d.CodephotothequeNavigation)
                    .WithMany(p => p.EstIncriteDanss)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_esi_ptt");

            });

            modelBuilder.Entity<Liker>(entity =>
            {
                entity.HasKey(e => new { e.ProduitId, e.ClientId })
                    .HasName("pk_lik");

                // manque fk produit et client

            });


            modelBuilder.Entity<LivraisonDomicile>(entity =>
            {
                entity.HasKey(e => e.LivraisonId)
                    .HasName("pk_liv");

                entity.Property(e => e.Libelletypelivraison).HasDefaultValue("Domicile");
                entity.Property(e => e.Estexpress).HasDefaultValue(false);

                entity.HasOne(d => d.IdadresseNavigation)
                    .WithMany(p => p.LivraisonDomiciles)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_liv_adr");
            });

            modelBuilder.Entity<Panier>(entity =>
            {
                entity.HasKey(e => e.PanierId)
                    .HasName("pk_pan");

                entity.Property(e => e.Dateetheure).HasDefaultValueSql("now()");

                //Manque FK Client
            });

            modelBuilder.Entity<Paypal>(entity =>
            {
                entity.HasKey(e => e.PaypalId)
                    .HasName("pk_pyp");

                entity.Property(e => e.Libelletypepaiement).HasDefaultValue("Paypal");
            });

            modelBuilder.Entity<Pays>(entity =>
            {
                entity.HasKey(e => e.PaysId)
                    .HasName("pk_pys");

            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.CodePhoto)
                    .HasName("pk_pht");

            });


            modelBuilder.Entity<Phototheque>(entity =>
            {
                entity.HasKey(e => e.Codephototheque)
                    .HasName("pk_ptt");

            });


            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.ProduitId)
                    .HasName("pk_prd");

                entity.Property(e => e.Like).HasDefaultValue(0);
            });


            modelBuilder.Entity<Professionnel>(entity =>
            {
                entity.HasKey(e => e.ProfessionnelId)
                    .HasName("pk_prf");

            });

            modelBuilder.Entity<Recherche>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.ProduitId })
                    .HasName("pk_rch");

                entity.HasOne(d => d.IdproduitNavigation)
                    .WithMany(p => p.Recherches)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rch_prd");

                //Manque FK client

            });

            modelBuilder.Entity<Regroupement>(entity =>
            {
                entity.HasKey(e => e.RegroupementId)
                    .HasName("pk_rgp");

            });

            modelBuilder.Entity<SeSitue>(entity =>
            {
                entity.HasKey(e => new { e.AdresseId, e.ProfessionnelId })
                    .HasName("pk_sst");

                entity.HasOne(d => d.IdadresseNavigation)
                    .WithMany(p => p.SeSitues)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_sst_adr");

                //Manque FK professionnel

            });

            modelBuilder.Entity<TypePaiement>(entity =>
            {
                entity.HasKey(e => e.TypePaiementId)
                    .HasName("pk_typ");

            });

            modelBuilder.Entity<TypeProduit>(entity =>
            {
                entity.HasKey(e => e.TypeProduitId)
                    .HasName("pk_tpp");

            });


            modelBuilder.Entity<Ville>(entity =>
            {
                entity.HasKey(e => e.NumeroInsee)
                    .HasName("pk_vil");

            });


            modelBuilder.Entity<VirementBancaire>(entity =>
            {
                entity.HasKey(e => e.VirementId)
                    .HasName("pk_vba");

            });


            // Séquences
            modelBuilder.HasSequence<int>("a_comme_idacomme_seq", "miliboo");
            modelBuilder.HasSequence<int>("a_pour_idapour_seq", "miliboo");
            modelBuilder.HasSequence<int>("appartient_idappartient_seq", "miliboo");
            modelBuilder.HasSequence<int>("constitue_idconstitue_seq", "miliboo");
            modelBuilder.HasSequence<int>("demande_iddemande_seq", "miliboo");
            modelBuilder.HasSequence<int>("est_ajoute_dans_idestajoutedans_seq", "miliboo");
            modelBuilder.HasSequence<int>("est_constitue_idestconstitue_seq", "miliboo");
            modelBuilder.HasSequence<int>("est_de_couleur_idestdecouleur_seq", "miliboo");
            modelBuilder.HasSequence<int>("est_inclu_idestinclu_seq", "miliboo");
            modelBuilder.HasSequence<int>("est_incrite_dans_idestinscritedans_seq", "miliboo");
            modelBuilder.HasSequence<int>("failed_jobs_id_seq", "miliboo");
            modelBuilder.HasSequence("personal_access_tokens_id_seq", "miliboo");
            modelBuilder.HasSequence<int>("recherche_idrecherche_seq", "miliboo");
            modelBuilder.HasSequence<int>("se_situe_idsesitue_seq", "miliboo");
            modelBuilder.HasSequence("users_id_seq", "miliboo");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}
