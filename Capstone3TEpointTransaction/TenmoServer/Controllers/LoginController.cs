﻿using Microsoft.AspNetCore.Mvc;
using System;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDAO userDAO;

        public LoginController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDAO _userDAO)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            userDAO = _userDAO;
        }

        [HttpGet]
        public ActionResult Loaded()
        {
            return StatusCode(200, "Server running.");
        }

        [HttpPost]
        public IActionResult Authenticate(LoginUser userParam)
        {
            // Default to bad username/password message
            IActionResult result = BadRequest(new { message = "Username or password is incorrect" });

            // Get the user by username
            User user = userDAO.GetUser(userParam.Username);

            // If we found a user and the password hash matches
            if (user != null && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
            {
                // Create an authentication token
                string token = tokenGenerator.GenerateToken(user.UserId, user.Username/*, user.Role*/);

                // Create a ReturnUser object to return to the client
                ReturnUser retUser = new ReturnUser() { UserId = user.UserId, Username = user.Username, /*Role = user.Role,*/ Token = token };

                // Switch to 200 OK
                result = Ok(retUser);
            }

            return result;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUser userParam)
        {
            IActionResult result;
            User existingUser;

            try
            {
                existingUser = userDAO.GetUser(userParam.Username);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error in GetUser - " + ex.Message });
            }

            if (existingUser != null)
            {
                return Conflict(new { message = "Username already taken. Please choose a different username." });
            }

            User user;
            try
            {
                user = userDAO.AddUser(userParam.Username, userParam.Password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error in AddUser - " + ex.Message });
            }

            if (user != null)
            {
                result = Created(user.Username, null); //values aren't read on client
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and user was not created." });
            }

            return result;
        }
    }
}
