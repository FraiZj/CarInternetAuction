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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            SetInitialData();
        }

        public async Task<UserModel> GetUserModelByIdAsync(string id)
        {
            try
            {
                var user = await _unitOfWork.ApplicationUserManager.FindByIdAsync(id);
                return _mapper.Map<UserModel>(user);
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while searching a user", ex);
            }
        }

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
                throw new InternetAuctionException("An error occured while login", ex);
            }
        }

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
                throw new InternetAuctionException("An error occured while regitestering user", ex);
            }
        }

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
                throw new InternetAuctionException("An error occured while searching all users", ex);
            }
        }

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
                throw new InternetAuctionException("An error occured while updating user", ex);
            }
        }

        public async Task<OperationDetails> Delete(string id)
        {
            try
            {
                var user = await _unitOfWork.ApplicationUserManager.FindByIdAsync(id);

                if (user is null)
                    return new OperationDetails(false, CreateValidationResults("id", $"User with Id = {id} does not exist"));

                var result = await _unitOfWork.ApplicationUserManager.DeleteAsync(user);

                if (result.Succeeded)
                    return new OperationDetails(true);

                return new OperationDetails(false, CreateValidationResults("id", result.Errors.First()));
            }
            catch (Exception ex)
            {
                throw new InternetAuctionException("An error occured while deleting user", ex);
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

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

        private IEnumerable<ValidationResult> CreateValidationResults(string error, string memberName)
        {
            return new List<ValidationResult> { new ValidationResult(error, new List<string> { memberName }) };
        }
    }
}
