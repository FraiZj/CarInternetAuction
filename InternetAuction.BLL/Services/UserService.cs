using AutoMapper;
using InternetAuction.BLL.Infrastructure;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Validation;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Services
{
    /// <summary>
    /// Represents user service class
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private bool isDisposed;

        /// <summary>
        /// Initializes user service with unit of work and mapper
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            SetInitialData();
        }

        /// <summary>
        /// Returns user model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserModelByIdAsync(string id)
        {
            try
            {
                var user = await _unitOfWork.ApplicationUserManager.FindByIdAsync(id);
                return _mapper.Map<UserModel>(user);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while searching for a user", ex);
            }
        }

        /// <summary>
        /// Authenticate user to account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> Login(UserModel model)
        {
            try
            {
                ClaimsIdentity claim = null;
                ApplicationUser user = await _unitOfWork.ApplicationUserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                    claim = await _unitOfWork.ApplicationUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                return claim;
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while login", ex);
            }
        }

        /// <summary>
        /// Register user account
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<OperationDetails> Register(UserModel model, string role = Roles.Client)
        {
            try
            {
                var user = await _unitOfWork.ApplicationUserManager.FindByEmailAsync(model.Email);

                if (user != null)
                    return new OperationDetails(false, CreateValidationResults("User with this email already exist", "Email"));

                user = new ApplicationUser 
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email
                };
                var result = await _unitOfWork.ApplicationUserManager.CreateAsync(user, model.Password);

                if (result.Errors.Any())
                {
                    List<ValidationResult> validationResults = new List<ValidationResult>();
                    foreach (var error in result.Errors)
                    {
                        validationResults.AddRange(CreateValidationResults("", error));
                    }
                    return new OperationDetails(false, validationResults);
                }

                await _unitOfWork.ApplicationUserManager.AddToRoleAsync(user.Id, role);
                await _unitOfWork.SaveAsync();

                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while regitestering user", ex);
            }
        }

        /// <summary>
        /// Returns all users models
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> GetAll()
        {
            try
            {
                var users = _unitOfWork.ApplicationUserManager.Users.ToList();
                var usersModels = _mapper.Map<IEnumerable<UserModel>>(users);

                return usersModels;
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while searching for users", ex);
            }
        }

        /// <summary>
        /// Returns users models by user search model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> SearchUsers(UserSearchModel model)
        {
            try
            {
                var users = _unitOfWork.ApplicationUserManager.Users.ToList();

                if (model != null)
                {
                    var firstName = model.FirstName?.Trim().ToUpper();
                    if (!string.IsNullOrWhiteSpace(firstName))
                        users = users.Where(u => u.FirstName.Trim().ToUpper().Contains(firstName)).ToList();

                    var lastName = model.LastName?.Trim().ToUpper();
                    if (!string.IsNullOrWhiteSpace(lastName))
                        users = users.Where(u => u.LastName.Trim().ToUpper().Contains(lastName)).ToList();

                    var email = model.Email?.Trim().ToUpper();
                    if (!string.IsNullOrWhiteSpace(email))
                        users = users.Where(u => u.Email.Trim().ToUpper().Contains(email)).ToList();
                }

                var usersModels = _mapper.Map<IEnumerable<UserModel>>(users);

                return usersModels.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while searching for users", ex);
            }
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<OperationDetails> Update(UserModel model)
        {
            try
            {
                if (model is null)
                    return new OperationDetails(false, CreateValidationResults("User cannot be null", "Model"));

                var validationResult = new List<ValidationResult>();

                if (!Regex.IsMatch(model.FirstName, @"^[a-zA-Z-_ ]+$"))
                    validationResult.AddRange(CreateValidationResults("Invalid first name", "FirstName"));

                if (!Regex.IsMatch(model.LastName, @"^[a-zA-Z-_ ]+$"))
                    validationResult.AddRange(CreateValidationResults("Invalid Last Name", "LastName"));

                if (!Regex.IsMatch(model.PhoneNumber, @"^\s*\+?\s*([0-9][\s-]*){9,}$"))
                    validationResult.AddRange(CreateValidationResults("Invalid Phone Number", "PhoneNumber"));

                if (!Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                    validationResult.AddRange(CreateValidationResults("Invalid Email", "Email"));

                if (validationResult.Any())
                    return new OperationDetails(false, validationResult);

                var user = await _unitOfWork.ApplicationUserManager.FindByIdAsync(model.Id);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                await _unitOfWork.ApplicationUserManager.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while updating user", ex);
            }
        }

        /// <summary>
        /// Deletes user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationDetails> Delete(string id)
        {
            try
            {
                var user = await _unitOfWork.ApplicationUserManager.FindByIdAsync(id);

                if (user is null)
                    return new OperationDetails(false, CreateValidationResults($"User with Id = {id} does not exist", "id"));

                var result = await _unitOfWork.ApplicationUserManager.DeleteAsync(user);

                if (result.Succeeded)
                    return new OperationDetails(true);

                return new OperationDetails(false, CreateValidationResults(result.Errors.First(), "id"));
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occurred while deleting user", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                _unitOfWork.Dispose();
            }

            isDisposed = true;
        }

        /// <summary>
        /// Sets initial data
        /// </summary>
        private void SetInitialData()
        {
            try
            {
                if (_unitOfWork.ApplicationRoleManager.FindByName("Client") is null)
                {
                    var role = new ApplicationRole { Name = Roles.Client };
                    _unitOfWork.ApplicationRoleManager.Create(role);
                }

                if (_unitOfWork.ApplicationRoleManager.FindByName("Admin") is null)
                {
                    var role = new ApplicationRole { Name = Roles.Admin };
                    _unitOfWork.ApplicationRoleManager.Create(role);
                }

                if (_unitOfWork.ApplicationUserManager.FindByEmail("admin1") is null)
                {
                    var admin = new ApplicationUser
                    {
                        FirstName = "AdminOne",
                        LastName = "AdminOne",
                        Email = "admin1",
                        UserName = "admin1"
                    };
                    _unitOfWork.ApplicationUserManager.Create(admin, "admin1");
                    _unitOfWork.ApplicationUserManager.AddToRole(admin.Id, Roles.Admin);
                }
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while setting initila data", ex);
            }
        }

        /// <summary>
        /// Creates validation results
        /// </summary>
        /// <param name="error"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        private IEnumerable<ValidationResult> CreateValidationResults(string error, string memberName)
        {
            return new List<ValidationResult> { new ValidationResult(error, new List<string> { memberName }) };
        }
    }
}
