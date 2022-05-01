using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Interfaces;
using BLL;
using System.Data.SqlClient;
using DBDAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsersAndAwardsWEB.Models.ViewModel;

namespace UsersAndAwardsWEB.Controllers
{
    public class AwardController : Controller
    {
        private readonly IBusinessLogic _logic;
        public AwardController()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "DESKTOP-596GDK6\\SQLEXPRESS";
            connectionStringBuilder.InitialCatalog = "Users And Awards";
            connectionStringBuilder.IntegratedSecurity = true;
            string connection = connectionStringBuilder.ToString();
            _logic = new BusinessLogic(new DBUserDAO(connection), new DBAwardsDAO(connection));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllAwards()
        {
            var awards = _logic.GetAllAwards();
            return View(awards);
        }

        public IActionResult DeleteAward()
        {
            List<Entities.Award> awards = (List<Entities.Award>)_logic.GetAllAwards();
            ViewBag.Awards = new SelectList(awards, "ID", "Title");
            return View();
        }


        [HttpPost]
        public IActionResult DeleteAward(int ID)
        {
            _logic.DeleteAward(_logic.GetAward(ID));
            return RedirectToAction("GetAllAwards");
        }

        public IActionResult AddAward()
        {
            AwardViewModel awardViewModel = new AwardViewModel();
            return View(awardViewModel);
        }

        [HttpPost]
        public IActionResult AddAward(AwardViewModel awardViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Entities.Award award = new Entities.Award
            {
                Title = awardViewModel.Title,
                Description = awardViewModel.Description
            };
            _logic.AddNewAward(award);
            return RedirectToAction("GetAllAwards");
        }

        public IActionResult ChangeAward() // страничка выбора кого менять
        {
            List<Entities.Award> awards = (List<Entities.Award>)_logic.GetAllAwards();
            ViewBag.Awards = new SelectList(awards, "ID", "Title");
            return View();
        }

        
        public IActionResult EditAward(int ID) //страничка для изменений
        {
            var award = new AwardViewModel(_logic.GetAward(ID));
            return View(award);
        }

        [HttpPost]
        public IActionResult EditAward(AwardViewModel awardViewModel) //запрос на изменение
        {
            if (!ModelState.IsValid)
            {
                var award = new AwardViewModel(_logic.GetAward(awardViewModel.ID));
                return View(award);
            }
            Entities.Award updateAward = new Entities.Award { Title = awardViewModel.Title, Description = awardViewModel.Description };
            _logic.ChangeAward(updateAward, awardViewModel.ID);
            return RedirectToAction("GetAllAwards");
        }
    }
}
