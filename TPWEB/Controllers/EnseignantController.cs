using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TPWEB.Controllers
{
    public class EnseignantController : Controller
    {
        [Route("Enseignant")]
        [Route("Enseignant/Index")]
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
                foreach( DepartementDTO dep in CegepControleur.Instance.ObtenirListeDepartement(nomCegep))
                {
                    if(dep.Nom == nomDepartement)
                    {
                        ok  = true;
                    }
                }
                if (nomDepartement == null || ok == false)
                {
                    nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                }
                ViewBag.NomCegep = nomCegep;
                ViewBag.NowDepartement = nomDepartement;
                //Préparation des données pour la vue...
                ViewBag.ListeEnseignants = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            //Retour de la vue...
            return View();
        }
        [Route("/Enseignant/AjouterEnseignant")]
        [HttpPost]
        public IActionResult AjouterEnseignant([FromForm] EnseignantDTO  enseignant, [FromForm] string nomCegep, [FromForm] string nomDep)
        {
            try
            {
                CegepControleur.Instance.AjouterEnseignant(nomCegep, nomDep, enseignant);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Enseignant", new{nomCegep = nomCegep, nomDep = nomDep});
        }
        [Route("/Enseignant/FormulaireModifierEnseignant")]
        [HttpGet]
        public IActionResult FormulaireModifierEnseignant([FromQuery] string nomCegep, [FromQuery] string nomDepartement, [FromQuery] int noEnseignant)
        {
            try
            {
                EnseignantDTO enseignant = CegepControleur.Instance.ObtenirEnseignant(nomCegep, nomDepartement, noEnseignant);
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement = nomDepartement;
                return View(enseignant);
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
        [Route("/Enseignant/ModifierEnseignant")]
        [HttpPost]
        public IActionResult ModifierEnseignant([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] EnseignantDTO enseignantDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierEnseignant(nomCegep, nomDepartement, enseignantDTO);
            }
            catch (Exception e)
            {
                return RedirectToAction("FormulaireModifierEnseignant", "Enseignant", new { nomEnseignant = enseignantDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
    }
}
