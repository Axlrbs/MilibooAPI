using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MilibooAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreationBDmilibooAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "a_comme_idacomme_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "a_pour_idapour_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "appartient_idappartient_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "constitue_idconstitue_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "demande_iddemande_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "est_ajoute_dans_idestajoutedans_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "est_constitue_idestconstitue_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "est_de_couleur_idestdecouleur_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "est_inclu_idestinclu_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "est_incrite_dans_idestinscritedans_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "failed_jobs_id_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence(
                name: "personal_access_tokens_id_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "recherche_idrecherche_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence<int>(
                name: "se_situe_idsesitue_seq",
                schema: "miliboo");

            migrationBuilder.CreateSequence(
                name: "users_id_seq",
                schema: "miliboo");

            migrationBuilder.CreateTable(
                name: "t_e_carte_bancaire_crtban",
                schema: "miliboo",
                columns: table => new
                {
                    crtban_idcartebancaire = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    crtban_libelletypepaiement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValue: "Carte Bancaire"),
                    crtban_nomcarte = table.Column<string>(type: "varchar(20)", nullable: false),
                    crtban_numcarte = table.Column<string>(type: "varchar(16)", nullable: false),
                    crtban_dateexpiration = table.Column<string>(type: "varchar(10)", nullable: false),
                    crtban_cvvcarte = table.Column<string>(type: "varchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crtban", x => x.crtban_idcartebancaire);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categorie_cat",
                schema: "miliboo",
                columns: table => new
                {
                    cat_idcategorie = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_cat_idcategorie = table.Column<int>(type: "integer", nullable: true),
                    cat_nomcategorie = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cat", x => x.cat_idcategorie);
                    table.ForeignKey(
                        name: "fk_cat_cat",
                        column: x => x.cat_cat_idcategorie,
                        principalSchema: "miliboo",
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_idcategorie",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_client_clt",
                schema: "miliboo",
                columns: table => new
                {
                    clt_idclient = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clt_nompersonne = table.Column<string>(type: "varchar(20)", nullable: true),
                    clt_prenompersonne = table.Column<string>(type: "varchar(20)", nullable: true),
                    clt_telpersonne = table.Column<string>(type: "varchar(10)", nullable: true),
                    clt_emailclient = table.Column<string>(type: "varchar(100)", nullable: true),
                    clt_mdpclient = table.Column<string>(type: "varchar(300)", nullable: true),
                    clt_nbtotalpointsfidelite = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    clt_moyenneavis = table.Column<decimal>(type: "numeric", nullable: true),
                    clt_nombreavisdepose = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    clt_is_verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    clt_datederniereutilisation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    clt_dateanonymisation = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clt", x => x.clt_idclient);
                });

            migrationBuilder.CreateTable(
                name: "t_e_coloris_clr",
                schema: "miliboo",
                columns: table => new
                {
                    clr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clr_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clr", x => x.clr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_ensemble_produit_esp",
                schema: "miliboo",
                columns: table => new
                {
                    esp_idensemble = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    esp_descriptionensemble = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    esp_aspecttechnique = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    esp_stock = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_esp", x => x.esp_idensemble);
                });

            migrationBuilder.CreateTable(
                name: "t_e_pays_pys",
                schema: "miliboo",
                columns: table => new
                {
                    pys_idpays = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    pys_libellepays = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pys", x => x.pys_idpays);
                });

            migrationBuilder.CreateTable(
                name: "t_e_photo_pht",
                schema: "miliboo",
                columns: table => new
                {
                    pht_codephoto = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    pht_urlphoto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pht", x => x.pht_codephoto);
                });

            migrationBuilder.CreateTable(
                name: "t_e_phototheque_ptt",
                schema: "miliboo",
                columns: table => new
                {
                    ptt_codephototheque = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ptt", x => x.ptt_codephototheque);
                });

            migrationBuilder.CreateTable(
                name: "t_e_produit_prd",
                schema: "miliboo",
                columns: table => new
                {
                    prd_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prd_carac = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    prd_ref = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    prd_like = table.Column<int>(type: "integer", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prd", x => x.prd_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_professionnel_prf",
                schema: "miliboo",
                columns: table => new
                {
                    prf_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prf_nompersonne = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    prf_prenompersonne = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    prf_telpersonne = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    prf_raisonsociale = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    prf_telfixe = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prf", x => x.prf_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_regroupement_rgp",
                schema: "miliboo",
                columns: table => new
                {
                    rgp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rgp_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rgp", x => x.rgp_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typepaiement_typ",
                schema: "miliboo",
                columns: table => new
                {
                    typ_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typ_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typ", x => x.typ_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeproduit_tpp",
                schema: "miliboo",
                columns: table => new
                {
                    tpp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpp_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tpp", x => x.tpp_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_ville_vil",
                schema: "miliboo",
                columns: table => new
                {
                    vil_numeroinsee = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    vil_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vil", x => x.vil_numeroinsee);
                });

            migrationBuilder.CreateTable(
                name: "t_e_panier_pan",
                schema: "miliboo",
                columns: table => new
                {
                    pan_idpanier = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pan_idclient = table.Column<int>(type: "integer", nullable: false),
                    pan_dateetheure = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pan", x => x.pan_idpanier);
                    table.ForeignKey(
                        name: "FK_t_e_panier_pan_t_e_client_clt_pan_idclient",
                        column: x => x.pan_idclient,
                        principalSchema: "miliboo",
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_idclient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estinclu_esi",
                schema: "miliboo",
                columns: table => new
                {
                    esi_idcategorie = table.Column<int>(type: "integer", nullable: false),
                    esi_idensemble = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_esi", x => new { x.esi_idcategorie, x.esi_idensemble });
                    table.ForeignKey(
                        name: "fk_esi_cat",
                        column: x => x.esi_idcategorie,
                        principalSchema: "miliboo",
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_idcategorie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_esi_esp",
                        column: x => x.esi_idensemble,
                        principalSchema: "miliboo",
                        principalTable: "t_e_ensemble_produit_esp",
                        principalColumn: "esp_idensemble",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estinscritedans_eid",
                schema: "miliboo",
                columns: table => new
                {
                    eid_codephoto = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    eid_codephototheque = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_eid", x => new { x.eid_codephoto, x.eid_codephototheque });
                    table.ForeignKey(
                        name: "fk_eid_pht",
                        column: x => x.eid_codephoto,
                        principalSchema: "miliboo",
                        principalTable: "t_e_photo_pht",
                        principalColumn: "pht_codephoto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_esi_ptt",
                        column: x => x.eid_codephototheque,
                        principalSchema: "miliboo",
                        principalTable: "t_e_phototheque_ptt",
                        principalColumn: "ptt_codephototheque",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_avis_client_avc",
                schema: "miliboo",
                columns: table => new
                {
                    avc_idavis = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    avc_idclient = table.Column<int>(type: "integer", nullable: false),
                    avc_idproduit = table.Column<int>(type: "integer", nullable: false),
                    avc_descriptionavis = table.Column<string>(type: "varchar(300)", nullable: true),
                    avc_note = table.Column<int>(type: "integer", nullable: true),
                    avc_dateavis = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    avc_titreavis = table.Column<string>(type: "varchar(100)", nullable: true),
                    avc_idavisparent = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avc", x => x.avc_idavis);
                    table.ForeignKey(
                        name: "FK_t_e_avis_client_avc_t_e_client_clt_avc_idclient",
                        column: x => x.avc_idclient,
                        principalSchema: "miliboo",
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_idclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_avc_prd",
                        column: x => x.avc_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_constitue_cst",
                schema: "miliboo",
                columns: table => new
                {
                    cst_idproduit = table.Column<int>(type: "integer", nullable: false),
                    cst_idensemble = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cst", x => new { x.cst_idproduit, x.cst_idensemble });
                    table.ForeignKey(
                        name: "fk_cst_esp",
                        column: x => x.cst_idensemble,
                        principalSchema: "miliboo",
                        principalTable: "t_e_ensemble_produit_esp",
                        principalColumn: "esp_idensemble",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cst_prd",
                        column: x => x.cst_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_liker_lik",
                schema: "miliboo",
                columns: table => new
                {
                    lik_idproduit = table.Column<int>(type: "integer", nullable: false),
                    lik_idclient = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lik", x => new { x.lik_idproduit, x.lik_idclient });
                    table.ForeignKey(
                        name: "FK_t_e_liker_lik_t_e_client_clt_lik_idclient",
                        column: x => x.lik_idclient,
                        principalSchema: "miliboo",
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_idclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_liker_lik_t_e_produit_prd_lik_idproduit",
                        column: x => x.lik_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_recherche_rch",
                schema: "miliboo",
                columns: table => new
                {
                    rch_idclient = table.Column<int>(type: "integer", nullable: false),
                    rch_idproduit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rch", x => new { x.rch_idclient, x.rch_idproduit });
                    table.ForeignKey(
                        name: "FK_t_e_recherche_rch_t_e_client_clt_rch_idclient",
                        column: x => x.rch_idclient,
                        principalSchema: "miliboo",
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_idclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rch_prd",
                        column: x => x.rch_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_a_comme_acm",
                schema: "miliboo",
                columns: table => new
                {
                    acm_idproduit = table.Column<int>(type: "integer", nullable: false),
                    acm_idcategorie = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_acm", x => new { x.acm_idproduit, x.acm_idcategorie });
                    table.ForeignKey(
                        name: "fk_acm_cat",
                        column: x => x.acm_idcategorie,
                        principalSchema: "miliboo",
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_idcategorie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_acm_prd",
                        column: x => x.acm_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estdecouleur_edc",
                schema: "miliboo",
                columns: table => new
                {
                    edc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    edc_idcoloris = table.Column<int>(type: "integer", nullable: false),
                    edc_idproduit = table.Column<int>(type: "integer", nullable: false),
                    edc_codephoto = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    edc_nomproduit = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    edc_prixttc = table.Column<decimal>(type: "numeric", nullable: true),
                    edc_description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    edc_quantite = table.Column<int>(type: "integer", nullable: true),
                    edc_promotion = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_edc", x => x.edc_id);
                    table.ForeignKey(
                        name: "fk_edc_clr",
                        column: x => x.edc_idcoloris,
                        principalSchema: "miliboo",
                        principalTable: "t_e_coloris_clr",
                        principalColumn: "clr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_edc_prd",
                        column: x => x.edc_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_demande_dmd",
                schema: "miliboo",
                columns: table => new
                {
                    dmd_idproduit = table.Column<int>(type: "integer", nullable: false),
                    dmd_idprofessionnel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dmd", x => new { x.dmd_idproduit, x.dmd_idprofessionnel });
                    table.ForeignKey(
                        name: "FK_t_e_demande_dmd_t_e_professionnel_prf_dmd_idprofessionnel",
                        column: x => x.dmd_idprofessionnel,
                        principalSchema: "miliboo",
                        principalTable: "t_e_professionnel_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dmd_prd",
                        column: x => x.dmd_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_appartient_aprt",
                schema: "miliboo",
                columns: table => new
                {
                    aprt_idregroupement = table.Column<int>(type: "integer", nullable: false),
                    aprt_idproduit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aprt", x => new { x.aprt_idregroupement, x.aprt_idproduit });
                    table.ForeignKey(
                        name: "fk_aprt_prd",
                        column: x => x.aprt_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_aprt_rgp",
                        column: x => x.aprt_idregroupement,
                        principalSchema: "miliboo",
                        principalTable: "t_e_regroupement_rgp",
                        principalColumn: "rgp_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_paypal_pyp",
                schema: "miliboo",
                columns: table => new
                {
                    pyp_idpaypal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pyp_idtypepaiement = table.Column<int>(type: "integer", nullable: false),
                    pyp_libelletypepaiement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValue: "Paypal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pyp", x => x.pyp_idpaypal);
                    table.ForeignKey(
                        name: "FK_t_e_paypal_pyp_t_e_typepaiement_typ_pyp_idtypepaiement",
                        column: x => x.pyp_idtypepaiement,
                        principalSchema: "miliboo",
                        principalTable: "t_e_typepaiement_typ",
                        principalColumn: "typ_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_virementbancaire_vba",
                schema: "miliboo",
                columns: table => new
                {
                    vba_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vba_idtypepaiement = table.Column<int>(type: "integer", nullable: false),
                    vba_iban = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vba", x => x.vba_id);
                    table.ForeignKey(
                        name: "FK_t_e_virementbancaire_vba_t_e_typepaiement_typ_vba_idtypepai~",
                        column: x => x.vba_idtypepaiement,
                        principalSchema: "miliboo",
                        principalTable: "t_e_typepaiement_typ",
                        principalColumn: "typ_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estdans_esd",
                schema: "miliboo",
                columns: table => new
                {
                    esd_idcategorie = table.Column<int>(type: "integer", nullable: false),
                    esd_idtypeproduit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_esd", x => new { x.esd_idcategorie, x.esd_idtypeproduit });
                    table.ForeignKey(
                        name: "fk_esd_cat",
                        column: x => x.esd_idcategorie,
                        principalSchema: "miliboo",
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_idcategorie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_esd_prd",
                        column: x => x.esd_idtypeproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_typeproduit_tpp",
                        principalColumn: "tpp_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                schema: "miliboo",
                columns: table => new
                {
                    adr_idadresse = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adr_numeroinsee = table.Column<string>(type: "varchar(15)", fixedLength: true, nullable: false),
                    adr_idpays = table.Column<string>(type: "varchar(20)", nullable: false),
                    adr_rue = table.Column<string>(type: "varchar(50)", nullable: true),
                    adr_codepostal = table.Column<decimal>(type: "numeric(5,0)", fixedLength: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adr", x => x.adr_idadresse);
                    table.ForeignKey(
                        name: "FK_t_e_adresse_adr_t_e_ville_vil_adr_numeroinsee",
                        column: x => x.adr_numeroinsee,
                        principalSchema: "miliboo",
                        principalTable: "t_e_ville_vil",
                        principalColumn: "vil_numeroinsee",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_adr_pyp",
                        column: x => x.adr_idpays,
                        principalSchema: "miliboo",
                        principalTable: "t_e_pays_pys",
                        principalColumn: "pys_idpays",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estajoutedans_ejd",
                schema: "miliboo",
                columns: table => new
                {
                    ejd_idproduit = table.Column<int>(type: "integer", nullable: false),
                    ejd_idpanier = table.Column<int>(type: "integer", nullable: false),
                    ejd_idcoloris = table.Column<int>(type: "integer", nullable: false),
                    ejd_quantitepanier = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ejd", x => new { x.ejd_idproduit, x.ejd_idcoloris, x.ejd_idpanier });
                    table.ForeignKey(
                        name: "fk_ejd_clr",
                        column: x => x.ejd_idcoloris,
                        principalSchema: "miliboo",
                        principalTable: "t_e_coloris_clr",
                        principalColumn: "clr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ejd_pan",
                        column: x => x.ejd_idpanier,
                        principalSchema: "miliboo",
                        principalTable: "t_e_panier_pan",
                        principalColumn: "pan_idpanier",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ejd_prd",
                        column: x => x.ejd_idproduit,
                        principalSchema: "miliboo",
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estconstitue_esc",
                schema: "miliboo",
                columns: table => new
                {
                    esc_idavis = table.Column<int>(type: "integer", nullable: false),
                    esc_codephoto = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_esco", x => new { x.esc_idavis, x.esc_codephoto });
                    table.ForeignKey(
                        name: "fk_esco_avs",
                        column: x => x.esc_idavis,
                        principalSchema: "miliboo",
                        principalTable: "t_e_avis_client_avc",
                        principalColumn: "avc_idavis",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_esco_pht",
                        column: x => x.esc_codephoto,
                        principalSchema: "miliboo",
                        principalTable: "t_e_photo_pht",
                        principalColumn: "pht_codephoto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_boutique_btq",
                schema: "miliboo",
                columns: table => new
                {
                    btq_idboutique = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    btq_idtypelivraison = table.Column<int>(type: "integer", nullable: false),
                    btq_idadresse = table.Column<int>(type: "integer", nullable: false),
                    btq_mailboutique = table.Column<string>(type: "varchar(50)", nullable: false),
                    btq_telboutique = table.Column<string>(type: "varchar(50)", nullable: false),
                    btq_accesboutique = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_btq", x => x.btq_idboutique);
                    table.ForeignKey(
                        name: "fk_btq_adr",
                        column: x => x.btq_idadresse,
                        principalSchema: "miliboo",
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_idadresse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_livraison_domicile_liv",
                schema: "miliboo",
                columns: table => new
                {
                    liv_idlivraison = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    liv_idtypelivraison = table.Column<int>(type: "integer", nullable: false),
                    liv_idadresse = table.Column<int>(type: "integer", nullable: false),
                    liv_libelletypelivraison = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValue: "Domicile"),
                    liv_estexpress = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_liv", x => x.liv_idlivraison);
                    table.ForeignKey(
                        name: "fk_liv_adr",
                        column: x => x.liv_idadresse,
                        principalSchema: "miliboo",
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_idadresse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_a_pour_apr",
                schema: "miliboo",
                columns: table => new
                {
                    apr_idadresse = table.Column<int>(type: "integer", nullable: false),
                    apr_idclient = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_apr", x => new { x.apr_idadresse, x.apr_idclient });
                    table.ForeignKey(
                        name: "FK_t_j_a_pour_apr_t_e_client_clt_apr_idclient",
                        column: x => x.apr_idclient,
                        principalSchema: "miliboo",
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_idclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_apr_adr",
                        column: x => x.apr_idadresse,
                        principalSchema: "miliboo",
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_idadresse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_sesitue_sst",
                schema: "miliboo",
                columns: table => new
                {
                    sst_idadresse = table.Column<int>(type: "integer", nullable: false),
                    sst_idprofessionnel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sst", x => new { x.sst_idadresse, x.sst_idprofessionnel });
                    table.ForeignKey(
                        name: "FK_t_j_sesitue_sst_t_e_professionnel_prf_sst_idprofessionnel",
                        column: x => x.sst_idprofessionnel,
                        principalSchema: "miliboo",
                        principalTable: "t_e_professionnel_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sst_adr",
                        column: x => x.sst_idadresse,
                        principalSchema: "miliboo",
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_idadresse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_commande_cmd",
                schema: "miliboo",
                columns: table => new
                {
                    cmd_idcommande = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cmd_idpanier = table.Column<int>(type: "integer", nullable: false),
                    cmd_idvirement = table.Column<int>(type: "integer", nullable: true),
                    cmd_idclient = table.Column<int>(type: "integer", nullable: false),
                    cmd_idlivraison = table.Column<int>(type: "integer", nullable: true),
                    cmd_idboutique = table.Column<int>(type: "integer", nullable: true),
                    cmd_idpaypal = table.Column<int>(type: "integer", nullable: true),
                    cmd_idcarte = table.Column<int>(type: "integer", nullable: true),
                    cmd_montantcommande = table.Column<decimal>(type: "numeric", nullable: true),
                    cmd_datefacture = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    cmd_nbpointfidelite = table.Column<int>(type: "integer", nullable: true),
                    cmd_statut = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmd", x => x.cmd_idcommande);
                    table.ForeignKey(
                        name: "FK_t_e_commande_cmd_t_e_boutique_btq_cmd_idboutique",
                        column: x => x.cmd_idboutique,
                        principalSchema: "miliboo",
                        principalTable: "t_e_boutique_btq",
                        principalColumn: "btq_idboutique");
                    table.ForeignKey(
                        name: "FK_t_e_commande_cmd_t_e_carte_bancaire_crtban_cmd_idcarte",
                        column: x => x.cmd_idcarte,
                        principalSchema: "miliboo",
                        principalTable: "t_e_carte_bancaire_crtban",
                        principalColumn: "crtban_idcartebancaire");
                    table.ForeignKey(
                        name: "FK_t_e_commande_cmd_t_e_client_clt_cmd_idclient",
                        column: x => x.cmd_idclient,
                        principalSchema: "miliboo",
                        principalTable: "t_e_client_clt",
                        principalColumn: "clt_idclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_commande_cmd_t_e_livraison_domicile_liv_cmd_idlivraison",
                        column: x => x.cmd_idlivraison,
                        principalSchema: "miliboo",
                        principalTable: "t_e_livraison_domicile_liv",
                        principalColumn: "liv_idlivraison");
                    table.ForeignKey(
                        name: "FK_t_e_commande_cmd_t_e_panier_pan_cmd_idpanier",
                        column: x => x.cmd_idpanier,
                        principalSchema: "miliboo",
                        principalTable: "t_e_panier_pan",
                        principalColumn: "pan_idpanier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_commande_cmd_t_e_paypal_pyp_cmd_idpaypal",
                        column: x => x.cmd_idpaypal,
                        principalSchema: "miliboo",
                        principalTable: "t_e_paypal_pyp",
                        principalColumn: "pyp_idpaypal");
                    table.ForeignKey(
                        name: "FK_t_e_commande_cmd_t_e_virementbancaire_vba_cmd_idvirement",
                        column: x => x.cmd_idvirement,
                        principalSchema: "miliboo",
                        principalTable: "t_e_virementbancaire_vba",
                        principalColumn: "vba_id");
                });

            migrationBuilder.CreateTable(
                name: "t_j_estcommande_esc",
                schema: "miliboo",
                columns: table => new
                {
                    esc_idcommande = table.Column<int>(type: "integer", nullable: false),
                    esc_idestdecouleur = table.Column<int>(type: "integer", nullable: false),
                    esc_quantite = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_esc", x => new { x.esc_idcommande, x.esc_idestdecouleur });
                    table.ForeignKey(
                        name: "FK_t_j_estcommande_esc_t_e_commande_cmd_esc_idcommande",
                        column: x => x.esc_idcommande,
                        principalSchema: "miliboo",
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_idcommande",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_estcommande_esc_t_j_estdecouleur_edc_esc_idestdecouleur",
                        column: x => x.esc_idestdecouleur,
                        principalSchema: "miliboo",
                        principalTable: "t_j_estdecouleur_edc",
                        principalColumn: "edc_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_idpays",
                schema: "miliboo",
                table: "t_e_adresse_adr",
                column: "adr_idpays");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_numeroinsee",
                schema: "miliboo",
                table: "t_e_adresse_adr",
                column: "adr_numeroinsee");

            migrationBuilder.CreateIndex(
                name: "uqix_adresse_rue",
                schema: "miliboo",
                table: "t_e_adresse_adr",
                column: "adr_rue");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_client_avc_avc_idclient",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                column: "avc_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_client_avc_avc_idproduit",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                column: "avc_idproduit");

            migrationBuilder.CreateIndex(
                name: "uqix_avisclient_titreavis",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                column: "avc_titreavis");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_boutique_btq_btq_idadresse",
                schema: "miliboo",
                table: "t_e_boutique_btq",
                column: "btq_idadresse");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_categorie_cat_cat_cat_idcategorie",
                schema: "miliboo",
                table: "t_e_categorie_cat",
                column: "cat_cat_idcategorie");

            migrationBuilder.CreateIndex(
                name: "clr_id",
                schema: "miliboo",
                table: "t_e_coloris_clr",
                column: "clr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idboutique",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idboutique");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idcarte",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idcarte");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idclient",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idlivraison",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idlivraison");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idpanier",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idpanier");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idpaypal",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idpaypal");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idvirement",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idvirement");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_constitue_cst_cst_idensemble",
                schema: "miliboo",
                table: "t_e_constitue_cst",
                column: "cst_idensemble");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_demande_dmd_dmd_idprofessionnel",
                schema: "miliboo",
                table: "t_e_demande_dmd",
                column: "dmd_idprofessionnel");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_liker_lik_lik_idclient",
                schema: "miliboo",
                table: "t_e_liker_lik",
                column: "lik_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_livraison_domicile_liv_liv_idadresse",
                schema: "miliboo",
                table: "t_e_livraison_domicile_liv",
                column: "liv_idadresse");

            migrationBuilder.CreateIndex(
                name: "idx_panier_idclient",
                schema: "miliboo",
                table: "t_e_panier_pan",
                column: "pan_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_paypal_pyp_pyp_idtypepaiement",
                schema: "miliboo",
                table: "t_e_paypal_pyp",
                column: "pyp_idtypepaiement");

            migrationBuilder.CreateIndex(
                name: "idx_pays_libellepays",
                schema: "miliboo",
                table: "t_e_pays_pys",
                column: "pys_libellepays");

            migrationBuilder.CreateIndex(
                name: "idx_photo_urlphoto",
                schema: "miliboo",
                table: "t_e_photo_pht",
                column: "pht_urlphoto");

            migrationBuilder.CreateIndex(
                name: "idx_produit_reference",
                schema: "miliboo",
                table: "t_e_produit_prd",
                column: "prd_ref");

            migrationBuilder.CreateIndex(
                name: "uc_professionnel",
                schema: "miliboo",
                table: "t_e_professionnel_prf",
                column: "prf_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_recherche_rch_rch_idproduit",
                schema: "miliboo",
                table: "t_e_recherche_rch",
                column: "rch_idproduit");

            migrationBuilder.CreateIndex(
                name: "tpp_id",
                schema: "miliboo",
                table: "t_e_typeproduit_tpp",
                column: "tpp_id");

            migrationBuilder.CreateIndex(
                name: "idx_ville_libelleville",
                schema: "miliboo",
                table: "t_e_ville_vil",
                column: "vil_numeroinsee");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_virementbancaire_vba_vba_idtypepaiement",
                schema: "miliboo",
                table: "t_e_virementbancaire_vba",
                column: "vba_idtypepaiement");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_a_comme_acm_acm_idcategorie",
                schema: "miliboo",
                table: "t_j_a_comme_acm",
                column: "acm_idcategorie");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_a_pour_apr_apr_idclient",
                schema: "miliboo",
                table: "t_j_a_pour_apr",
                column: "apr_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_appartient_aprt_aprt_idproduit",
                schema: "miliboo",
                table: "t_j_appartient_aprt",
                column: "aprt_idproduit");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estajoutedans_ejd_ejd_idcoloris",
                schema: "miliboo",
                table: "t_j_estajoutedans_ejd",
                column: "ejd_idcoloris");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estajoutedans_ejd_ejd_idpanier",
                schema: "miliboo",
                table: "t_j_estajoutedans_ejd",
                column: "ejd_idpanier");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estcommande_esc_esc_idestdecouleur",
                schema: "miliboo",
                table: "t_j_estcommande_esc",
                column: "esc_idestdecouleur");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estconstitue_esc_esc_codephoto",
                schema: "miliboo",
                table: "t_j_estconstitue_esc",
                column: "esc_codephoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estdans_esd_esd_idtypeproduit",
                schema: "miliboo",
                table: "t_j_estdans_esd",
                column: "esd_idtypeproduit");

            migrationBuilder.CreateIndex(
                name: "idx_estdecouleur_nomproduit",
                schema: "miliboo",
                table: "t_j_estdecouleur_edc",
                column: "edc_nomproduit");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estdecouleur_edc_edc_idcoloris",
                schema: "miliboo",
                table: "t_j_estdecouleur_edc",
                column: "edc_idcoloris");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estdecouleur_edc_edc_idproduit",
                schema: "miliboo",
                table: "t_j_estdecouleur_edc",
                column: "edc_idproduit");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estinclu_esi_esi_idensemble",
                schema: "miliboo",
                table: "t_j_estinclu_esi",
                column: "esi_idensemble");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estinscritedans_eid_eid_codephototheque",
                schema: "miliboo",
                table: "t_j_estinscritedans_eid",
                column: "eid_codephototheque");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_sesitue_sst_sst_idprofessionnel",
                schema: "miliboo",
                table: "t_j_sesitue_sst",
                column: "sst_idprofessionnel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_constitue_cst",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_demande_dmd",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_liker_lik",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_recherche_rch",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_a_comme_acm",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_a_pour_apr",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_appartient_aprt",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_estajoutedans_ejd",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_estcommande_esc",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_estconstitue_esc",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_estdans_esd",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_estinclu_esi",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_estinscritedans_eid",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_sesitue_sst",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_regroupement_rgp",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_commande_cmd",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_j_estdecouleur_edc",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_avis_client_avc",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_typeproduit_tpp",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_categorie_cat",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_ensemble_produit_esp",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_photo_pht",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_phototheque_ptt",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_professionnel_prf",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_boutique_btq",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_carte_bancaire_crtban",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_livraison_domicile_liv",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_panier_pan",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_paypal_pyp",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_virementbancaire_vba",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_coloris_clr",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_produit_prd",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_adresse_adr",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_client_clt",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_typepaiement_typ",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_ville_vil",
                schema: "miliboo");

            migrationBuilder.DropTable(
                name: "t_e_pays_pys",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "a_comme_idacomme_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "a_pour_idapour_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "appartient_idappartient_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "constitue_idconstitue_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "demande_iddemande_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "est_ajoute_dans_idestajoutedans_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "est_constitue_idestconstitue_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "est_de_couleur_idestdecouleur_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "est_inclu_idestinclu_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "est_incrite_dans_idestinscritedans_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "failed_jobs_id_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "personal_access_tokens_id_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "recherche_idrecherche_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "se_situe_idsesitue_seq",
                schema: "miliboo");

            migrationBuilder.DropSequence(
                name: "users_id_seq",
                schema: "miliboo");
        }
    }
}
