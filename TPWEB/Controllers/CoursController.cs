using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TPWEB.Controllers
{
    public class CoursController : Controller
    {
        [Route("Cours")]
        [Route("Cours/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string nomCegep, [FromQuery] string nomDepartement)
        {
            try
            {
                if (nomCegep == null)
                {
                    nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                }
                bool ok = false;
                foreach (DepartementDTO dep in CegepControleur.Instance.ObtenirListeDepartement(nomCegep))
                {
                    if (dep.Nom == nomDepartement)
                    {
                        ok = true;
                    }
                }
                if (nomDepartement == null || ok == false)
                {
                    nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                }
                ViewBag.NomCegep = nomCegep;
                ViewBag.NomDepartement = nomDepartement;
                //Préparation des données pour la vue...
                ViewBag.ListeCours = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement).ToArray();
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep();
                ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(ViewBag.NomCegep);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            //Retour de la vue...
            return View();
        }
        [Route("/Cours/AjouterCours")]
        [HttpPost]
        public IActionResult AjouterCours([FromForm] CoursDTO cours, [FromForm] string nomCegep, [FromForm] string nomDep)
        {
            try
            {
                CegepControleur.Instance.AjouterCours(nomCegep, nomDep, cours);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDep = nomDep });
        }
        [Route("/Cours/FormulaireModifierCours")]
        [HttpGet]
        public IActionResult FormulaireModifierCours([FromQuery] string nomCegep, [FromQuery] string nomDepartement, [FromQuery] string nomCours)
        {
            try
            {
                CoursDTO cours = CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, nomCours);
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement = nomDepartement;
                return View(cours);
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
        [Route("/Cours/ModifierCours")]
        [HttpPost]
        public IActionResult ModifierCours([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] CoursDTO coursDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierCours(nomCegep, nomDepartement, coursDTO);
            }
            catch (Exception e)
            {
                return RedirectToAction("FormulaireModifierCours", "Cours", new { nomCours = coursDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
        [Route("/Cours/SupprimerCours")]
        [HttpPost]
        public IActionResult SupprimerCours([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] string nomCours)
        {
            try
            {
                CegepControleur.Instance.SupprimerCours(nomCegep, nomDepartement, nomCours);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
        [Route("/Cours/ViderListeCours")]
        [HttpPost]
        public IActionResult ViderListeCours([FromForm] string nomCegep, [FromForm] string nomDepartement)
        {
            try
            {
                CegepControleur.Instance.ViderListeCours(nomCegep, nomDepartement);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
    }
}
