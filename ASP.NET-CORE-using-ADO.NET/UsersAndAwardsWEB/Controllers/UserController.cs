using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using BLL;
using System.Data.SqlClient;
using DBDAL;
using Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsersAndAwardsWEB.Models.ViewModel;

namespace UsersAndAwardsWEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IBusinessLogic _logic;

        public UserController()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "DESKTOP-596GDK6\\SQLEXPRESS";
            connectionStringBuilder.InitialCatalog = "Users And Awards";
            connectionStringBuilder.IntegratedSecurity = true;
            string connection = connectionStringBuilder.ToString();
            _logic = new BusinessLogic(new DBUserDAO(connection), new DBAwardsDAO(connection));
        }
        public IActionResult Index() //Get All users
        {
            List<User> users = (List<User>)_logic.GetAllUsers();
            return View(users);
        }

        public IActionResult GetAllAwardsOfCurrentUser(int id)
        {
            var user = _logic.GetUser(id);
            return View(user);
        }

        public IActionResult DeleteUser()
        {
            List<Entities.User> users = (List<User>)_logic.GetAllUsers();
            ViewBag.Users = new SelectList(users, "ID", "FirstName");
            return View();
        }

        [HttpPost]
        public IActionResult DeleteUser(int ID)
        {
            _logic.DeleteUser(_logic.GetUser(ID));
            return RedirectToAction("Index");
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user = new User
            {
                BirthDate = userVM.BirthDate,
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                Awards = userVM.Awards,
                ID = userVM.ID
            };
            _logic.AddUser(user);
            return RedirectToAction("Index");
        }

        public IActionResult ChangeUser(int ID)
        {
            UserViewModel userViewModel = new UserViewModel(_logic.GetUser(ID));
            var listOfAwards = new List<SelectListItem>();

            foreach (var item in _logic.GetAllAwards())
            {
                var selectListItem = new SelectListItem() { Text = item.Title, Value = item.ID.ToString() };
                listOfAwards.Add(selectListItem);
            }
            ViewBag.Awards = listOfAwards;
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult ChangeUser(PostSelectedViewModel postSelectedViewModel, UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                var listOfAwards = new List<SelectListItem>();

                foreach (var item in _logic.GetAllAwards())
                {
                    var selectListItem = new SelectListItem() { Text = item.Title, Value = item.ID.ToString() };
                    listOfAwards.Add(selectListItem);
                }
                ViewBag.Awards = listOfAwards;
                return View(userViewModel);
            }
            User user = new User
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                BirthDate = userViewModel.BirthDate,
                Awards = userViewModel.Awards
            };
            user.Awards.Clear();
            if (postSelectedViewModel.SelectedIds != null)
            {
                for (int i = 0; i < postSelectedViewModel.SelectedIds.Length; i++)
                {
                    user.AddAward(_logic.GetAward(postSelectedViewModel.SelectedIds[i]));
                }
            }
            
            _logic.ChangeUser(userViewModel.ID, user);
            return RedirectToAction("Index");
        }
    }
}
