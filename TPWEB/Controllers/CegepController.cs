using System;
using Microsoft.AspNetCore.Mvc;
using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;

/// <summary>
/// Namespace pour les controleurs de vue.
/// </summary>
namespace GestionCegepWeb.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de vue des Cégeps.
    /// </summary>
    public class CegepController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Cégeps.
        ///   -Afficher le formulaire pour l'ajout d'un Cégep.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("")]
        [Route("Cegep")]
        [Route("Cegep/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            //Mettre le if et son contenu en commentaire avant de lancer les tests fonctionnels...
            //Si erreur provenant d'une autre action...
            if (TempData["MessageErreur"] != null)
                ViewBag.MessageErreur = TempData["MessageErreur"];

            try
            {
                //Préparation des données pour la vue...
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            //Retour de la vue...
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterCegep.
        /// Rôles de l'action : 
        ///   -Ajouter un Cégep.
        /// </summary>
        /// <param name="cegepDTO">Le DTO du Cégep.</param>
        /// <returns>IActionResult</returns>
        [Route("/Cegep/AjouterCegep")]
        [HttpPost]
        public IActionResult AjouterCegep([FromForm] CegepDTO cegepDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterCegep(cegepDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Cegep", cegepDTO);
        }
    }
}
