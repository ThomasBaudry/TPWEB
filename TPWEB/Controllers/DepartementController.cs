using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GestionCegepWeb.Controllers
{
    public class DepartementController : Controller
    {
        [Route("Departement")]
        [Route("Departement/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string nomCegep)
        {
            try
            {
                if(nomCegep == null)
                {
                    nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                }
                //Préparation des données pour la vue...
                ViewBag.NomCegep = nomCegep;
                ViewBag.ListeDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            //Retour de la vue...
            return View();
        }
        [Route("/Departement/AjouterDepartement")]
        [HttpPost]
        public IActionResult AjouterDepartement([FromForm] DepartementDTO depDTO, [FromForm] string nomCegep)
        {
            try
            {
                if (nomCegep == null)
                {
                    nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                }
                ViewBag.NomCegep = nomCegep;
                CegepControleur.Instance.AjouterDepartement(nomCegep, depDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Departement", nomCegep);
        }
        [Route("/Departement/FormulaireModifierDepartement")]
        [HttpGet]
        public IActionResult FormulaireModifierDepartement([FromQuery] string nomCegep, [FromQuery] string nomDepartement)
        {
            try
            {
                DepartementDTO departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
                ViewBag.nomCegep = nomCegep;
                return View(departement);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Action ModifierCegep.
        /// Permet de modifier un Cégep.
        /// </summary>
        /// <param name="cegepDTO">Le Cégep a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Departement/ModifierDepartement")]
        [HttpPost]
        public IActionResult ModifierDepartement([FromForm] string nomCegep, [FromForm] DepartementDTO departementDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierDepartement(nomCegep, departementDTO);
            }
            catch (Exception e)
            {
                return RedirectToAction("FormulaireModifierDepartement", "Departement", new { nomDepartement = departementDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
    }
}
