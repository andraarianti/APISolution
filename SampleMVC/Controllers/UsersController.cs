﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using System.Text.Json;

namespace SampleMVC.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUserBLL _userBLL;
		private readonly IRoleBLL _roleBLL;
		public UsersController(IUserBLL userBLL, IRoleBLL roleBLL)
		{
			_userBLL = userBLL;
			_roleBLL = roleBLL;
		}

		public IActionResult Index()
		{
			var usersWithRoles = _userBLL.GetAllWithRoles();
			return View(usersWithRoles);
		}

		public IActionResult Login()
		{
			if (TempData["Message"] != null)
			{
				ViewBag.Message = TempData["Message"];
			}

			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginDTO loginDTO)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				var userDto = _userBLL.LoginMVC(loginDTO);
				//simpan username ke session
				var userDtoSerialize = JsonSerializer.Serialize(userDto);
				HttpContext.Session.SetString("user", userDtoSerialize);

				TempData["Message"] = "Welcome " + userDto.Username;
				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
				return View();
			}
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Remove("user");
			return RedirectToAction("Login");
		}

		//register user baru
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(UserCreateDTO userCreateDto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				_userBLL.Insert(userCreateDto);
				ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>Registration process successfully !</div>";

			}
			catch (Exception ex)
			{
				ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
			}

			return View();
		}

		public IActionResult Profile()
		{
			//var userWithRoles = _userBLL.GetUserWithRoles("ekurniawan");
			var usersWithRoles = _userBLL.GetAllWithRoles();
			return new JsonResult(usersWithRoles);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(UserCreateDTO userCreateDto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				_userBLL.Insert(userCreateDto);
				ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>Registration process successfully !</div>";

			}
			catch (Exception ex)
			{
				ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
			}

			return View();
		}

		public IActionResult Edit(string username)
		{
			var user = _userBLL.GetUserWithRoles(username);
			//inisialisaikan list role
			var roles = _roleBLL.GetAllRoles();
			ViewBag.Roles = roles;
			return View(user);
		}

		[HttpPost]
		public IActionResult Edit(string Username, int RoleID)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				_roleBLL.AddUserToRole(Username, RoleID);
				ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>Update process successfully !</div>";

			}
			catch (Exception ex)
			{
				ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
			}

			return RedirectToAction("Index");
		}

	}
}
