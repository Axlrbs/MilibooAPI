using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilibooAPI.Migrations
{
    /// <inheritdoc />
    public partial class testpush : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_adresse_adr_t_e_ville_vil_adr_numeroinsee",
                schema: "miliboo",
                table: "t_e_adresse_adr");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_avis_client_avc_t_e_client_clt_avc_idclient",
                schema: "miliboo",
                table: "t_e_avis_client_avc");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_boutique_btq_cmd_idboutique",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_carte_bancaire_crtban_cmd_idcarte",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_client_clt_cmd_idclient",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_livraison_domicile_liv_cmd_idlivraison",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_panier_pan_cmd_idpanier",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_paypal_pyp_cmd_idpaypal",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_virementbancaire_vba_cmd_idvirement",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_demande_dmd_t_e_professionnel_prf_dmd_idprofessionnel",
                schema: "miliboo",
                table: "t_e_demande_dmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_liker_lik_t_e_client_clt_lik_idclient",
                schema: "miliboo",
                table: "t_e_liker_lik");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_liker_lik_t_e_produit_prd_lik_idproduit",
                schema: "miliboo",
                table: "t_e_liker_lik");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_panier_pan_t_e_client_clt_pan_idclient",
                schema: "miliboo",
                table: "t_e_panier_pan");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_paypal_pyp_t_e_typepaiement_typ_pyp_idtypepaiement",
                schema: "miliboo",
                table: "t_e_paypal_pyp");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_recherche_rch_t_e_client_clt_rch_idclient",
                schema: "miliboo",
                table: "t_e_recherche_rch");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_a_pour_apr_t_e_client_clt_apr_idclient",
                schema: "miliboo",
                table: "t_j_a_pour_apr");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_estcommande_esc_t_e_commande_cmd_esc_idcommande",
                schema: "miliboo",
                table: "t_j_estcommande_esc");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_estcommande_esc_t_j_estdecouleur_edc_esc_idestdecouleur",
                schema: "miliboo",
                table: "t_j_estcommande_esc");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_sesitue_sst_t_e_professionnel_prf_sst_idprofessionnel",
                schema: "miliboo",
                table: "t_j_sesitue_sst");

            migrationBuilder.AddForeignKey(
                name: "fk_apr_vil",
                schema: "miliboo",
                table: "t_e_adresse_adr",
                column: "adr_numeroinsee",
                principalSchema: "miliboo",
                principalTable: "t_e_ville_vil",
                principalColumn: "vil_numeroinsee",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_avc_clt",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                column: "avc_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_btq",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idboutique",
                principalSchema: "miliboo",
                principalTable: "t_e_boutique_btq",
                principalColumn: "btq_idboutique",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_clt",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_crtban",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idcarte",
                principalSchema: "miliboo",
                principalTable: "t_e_carte_bancaire_crtban",
                principalColumn: "crtban_idcartebancaire",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_liv",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idlivraison",
                principalSchema: "miliboo",
                principalTable: "t_e_livraison_domicile_liv",
                principalColumn: "liv_idlivraison",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_pan",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idpanier",
                principalSchema: "miliboo",
                principalTable: "t_e_panier_pan",
                principalColumn: "pan_idpanier",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_pyp",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idpaypal",
                principalSchema: "miliboo",
                principalTable: "t_e_paypal_pyp",
                principalColumn: "pyp_idpaypal",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_vba",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idvirement",
                principalSchema: "miliboo",
                principalTable: "t_e_virementbancaire_vba",
                principalColumn: "vba_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_dmd_prf",
                schema: "miliboo",
                table: "t_e_demande_dmd",
                column: "dmd_idprofessionnel",
                principalSchema: "miliboo",
                principalTable: "t_e_professionnel_prf",
                principalColumn: "prf_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_lik_clt",
                schema: "miliboo",
                table: "t_e_liker_lik",
                column: "lik_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_lik_prd",
                schema: "miliboo",
                table: "t_e_liker_lik",
                column: "lik_idproduit",
                principalSchema: "miliboo",
                principalTable: "t_e_produit_prd",
                principalColumn: "prd_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_pan_clt",
                schema: "miliboo",
                table: "t_e_panier_pan",
                column: "pan_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_pyp_tpp",
                schema: "miliboo",
                table: "t_e_paypal_pyp",
                column: "pyp_idtypepaiement",
                principalSchema: "miliboo",
                principalTable: "t_e_typepaiement_typ",
                principalColumn: "typ_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_rch_clt",
                schema: "miliboo",
                table: "t_e_recherche_rch",
                column: "rch_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_apr_clt",
                schema: "miliboo",
                table: "t_j_a_pour_apr",
                column: "apr_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_esc_cmd",
                schema: "miliboo",
                table: "t_j_estcommande_esc",
                column: "esc_idcommande",
                principalSchema: "miliboo",
                principalTable: "t_e_commande_cmd",
                principalColumn: "cmd_idcommande",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_esc_edc",
                schema: "miliboo",
                table: "t_j_estcommande_esc",
                column: "esc_idestdecouleur",
                principalSchema: "miliboo",
                principalTable: "t_j_estdecouleur_edc",
                principalColumn: "edc_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_sst_prf",
                schema: "miliboo",
                table: "t_j_sesitue_sst",
                column: "sst_idprofessionnel",
                principalSchema: "miliboo",
                principalTable: "t_e_professionnel_prf",
                principalColumn: "prf_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_apr_vil",
                schema: "miliboo",
                table: "t_e_adresse_adr");

            migrationBuilder.DropForeignKey(
                name: "fk_avc_clt",
                schema: "miliboo",
                table: "t_e_avis_client_avc");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_btq",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_clt",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_crtban",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_liv",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_pan",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_pyp",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_vba",
                schema: "miliboo",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_dmd_prf",
                schema: "miliboo",
                table: "t_e_demande_dmd");

            migrationBuilder.DropForeignKey(
                name: "fk_lik_clt",
                schema: "miliboo",
                table: "t_e_liker_lik");

            migrationBuilder.DropForeignKey(
                name: "fk_lik_prd",
                schema: "miliboo",
                table: "t_e_liker_lik");

            migrationBuilder.DropForeignKey(
                name: "fk_pan_clt",
                schema: "miliboo",
                table: "t_e_panier_pan");

            migrationBuilder.DropForeignKey(
                name: "fk_pyp_tpp",
                schema: "miliboo",
                table: "t_e_paypal_pyp");

            migrationBuilder.DropForeignKey(
                name: "fk_rch_clt",
                schema: "miliboo",
                table: "t_e_recherche_rch");

            migrationBuilder.DropForeignKey(
                name: "fk_apr_clt",
                schema: "miliboo",
                table: "t_j_a_pour_apr");

            migrationBuilder.DropForeignKey(
                name: "fk_esc_cmd",
                schema: "miliboo",
                table: "t_j_estcommande_esc");

            migrationBuilder.DropForeignKey(
                name: "fk_esc_edc",
                schema: "miliboo",
                table: "t_j_estcommande_esc");

            migrationBuilder.DropForeignKey(
                name: "fk_sst_prf",
                schema: "miliboo",
                table: "t_j_sesitue_sst");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_adresse_adr_t_e_ville_vil_adr_numeroinsee",
                schema: "miliboo",
                table: "t_e_adresse_adr",
                column: "adr_numeroinsee",
                principalSchema: "miliboo",
                principalTable: "t_e_ville_vil",
                principalColumn: "vil_numeroinsee",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_avis_client_avc_t_e_client_clt_avc_idclient",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                column: "avc_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_boutique_btq_cmd_idboutique",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idboutique",
                principalSchema: "miliboo",
                principalTable: "t_e_boutique_btq",
                principalColumn: "btq_idboutique");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_carte_bancaire_crtban_cmd_idcarte",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idcarte",
                principalSchema: "miliboo",
                principalTable: "t_e_carte_bancaire_crtban",
                principalColumn: "crtban_idcartebancaire");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_client_clt_cmd_idclient",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_livraison_domicile_liv_cmd_idlivraison",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idlivraison",
                principalSchema: "miliboo",
                principalTable: "t_e_livraison_domicile_liv",
                principalColumn: "liv_idlivraison");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_panier_pan_cmd_idpanier",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idpanier",
                principalSchema: "miliboo",
                principalTable: "t_e_panier_pan",
                principalColumn: "pan_idpanier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_paypal_pyp_cmd_idpaypal",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idpaypal",
                principalSchema: "miliboo",
                principalTable: "t_e_paypal_pyp",
                principalColumn: "pyp_idpaypal");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_virementbancaire_vba_cmd_idvirement",
                schema: "miliboo",
                table: "t_e_commande_cmd",
                column: "cmd_idvirement",
                principalSchema: "miliboo",
                principalTable: "t_e_virementbancaire_vba",
                principalColumn: "vba_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_demande_dmd_t_e_professionnel_prf_dmd_idprofessionnel",
                schema: "miliboo",
                table: "t_e_demande_dmd",
                column: "dmd_idprofessionnel",
                principalSchema: "miliboo",
                principalTable: "t_e_professionnel_prf",
                principalColumn: "prf_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_liker_lik_t_e_client_clt_lik_idclient",
                schema: "miliboo",
                table: "t_e_liker_lik",
                column: "lik_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_liker_lik_t_e_produit_prd_lik_idproduit",
                schema: "miliboo",
                table: "t_e_liker_lik",
                column: "lik_idproduit",
                principalSchema: "miliboo",
                principalTable: "t_e_produit_prd",
                principalColumn: "prd_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_panier_pan_t_e_client_clt_pan_idclient",
                schema: "miliboo",
                table: "t_e_panier_pan",
                column: "pan_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_paypal_pyp_t_e_typepaiement_typ_pyp_idtypepaiement",
                schema: "miliboo",
                table: "t_e_paypal_pyp",
                column: "pyp_idtypepaiement",
                principalSchema: "miliboo",
                principalTable: "t_e_typepaiement_typ",
                principalColumn: "typ_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_recherche_rch_t_e_client_clt_rch_idclient",
                schema: "miliboo",
                table: "t_e_recherche_rch",
                column: "rch_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_a_pour_apr_t_e_client_clt_apr_idclient",
                schema: "miliboo",
                table: "t_j_a_pour_apr",
                column: "apr_idclient",
                principalSchema: "miliboo",
                principalTable: "t_e_client_clt",
                principalColumn: "clt_idclient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_estcommande_esc_t_e_commande_cmd_esc_idcommande",
                schema: "miliboo",
                table: "t_j_estcommande_esc",
                column: "esc_idcommande",
                principalSchema: "miliboo",
                principalTable: "t_e_commande_cmd",
                principalColumn: "cmd_idcommande",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_estcommande_esc_t_j_estdecouleur_edc_esc_idestdecouleur",
                schema: "miliboo",
                table: "t_j_estcommande_esc",
                column: "esc_idestdecouleur",
                principalSchema: "miliboo",
                principalTable: "t_j_estdecouleur_edc",
                principalColumn: "edc_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_sesitue_sst_t_e_professionnel_prf_sst_idprofessionnel",
                schema: "miliboo",
                table: "t_j_sesitue_sst",
                column: "sst_idprofessionnel",
                principalSchema: "miliboo",
                principalTable: "t_e_professionnel_prf",
                principalColumn: "prf_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
