using GestionCegepWeb.Logics.Modeles;

/// <summary>
/// Namespace pour les objets de type DTO.
/// </summary>
namespace GestionCegepWeb.Models
{
    /// <summary>
    /// Classe de DTO pour un département.
    /// </summary>
    public class DepartementDTO
    {
        #region Proprietes

        /// <summary>
        /// Propriété représentant le numéro du département.
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// Propriété représentant le nom du département.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        #endregion Proprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="no">Le numéro du département.</param>
        /// <param name="nom">Le nom du département.</param>
        /// <param name="description">La description du département.</param>
        public DepartementDTO(string no="", string nom="", string description="")
        {
            No = no;
            Nom = nom;
            Description = description;
        }
        public DepartementDTO()
        {

        }

        /// <summary>
        /// Constructeur avec un modèle.
        /// </summary>
        /// <param name="leDepartement">Le modèle du département.</param>
        public DepartementDTO(Departement leDepartement)
        {
            No = leDepartement.No;
            Nom = leDepartement.Nom;
            Description = leDepartement.Description;
        }

        #endregion Constructeurs
    }
}
