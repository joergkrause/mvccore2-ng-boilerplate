﻿using System;
using System.Threading;
using System.Threading.Tasks;
using JoergIsAGeek.Workshop.Enterprise.WebApplication.ViewModels.Authentication;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using ServiceReference2;

namespace JoergIsAGeek.Workshop.Enterprise.WebApplication
{

  /// <summary>
  /// More on interfaces. https://docs.microsoft.com/de-de/aspnet/identity/overview/extensibility/overview-of-custom-storage-providers-for-aspnet-identity
  /// </summary>
  internal class CustomUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
  {

    AuthSrvClient _authclient;
    IMapper _mapper;

    public CustomUserStore(AuthSrvClient authclient, IMapper mapper)
    {
      _authclient = authclient;
      _mapper = mapper;
    }

    public async Task<Microsoft.AspNetCore.Identity.IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      var userDto = _mapper.Map<UserDto>(user);
      var result = await _authclient.CreateUserAsync(userDto);
      return _mapper.Map<Microsoft.AspNetCore.Identity.IdentityResult>(result);
    }

    public Task<Microsoft.AspNetCore.Identity.IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      //
    }

    public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
      var user = await _authclient.FindByEmailAsync(normalizedEmail);
      return _mapper.Map<ApplicationUser>(user);
    }

    public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      var result = await _authclient.FindUserByIdAsync(Int32.Parse(userId));
      return _mapper.Map<ApplicationUser>(result);
    }

    public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      var result = await _authclient.FindUserByNameAsync(normalizedUserName);
      return _mapper.Map<ApplicationUser>(result);
    }

    public async Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      return await _authclient.GetEmailAsync(_mapper.Map<UserDto>(user));
    }

    public async Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      return await _authclient.GetEmailConfirmedAsync(_mapper.Map<UserDto>(user));
    }

    public async Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      return await _authclient.GetNormalizedEmailAsync(_mapper.Map<UserDto>(user));
    }

    public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      var userDto = _mapper.Map<UserDto>(user);
      var result = await _authclient.GetNormalizedUserNameAsync(userDto);
      return result;
    }

    public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      return await _authclient.GetPasswordHashAsync(_mapper.Map<UserDto>(user));
    }

    public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      var userDto = _mapper.Map<UserDto>(user);
      return (await _authclient.GetUserDtoIdAsync(userDto)).ToString();
    }

    public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      var userDto = _mapper.Map<UserDto>(user);
      return await _authclient.GetUserDtoNameAsync(userDto);
    }

    public async Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      return await _authclient.HasPasswordAsync(_mapper.Map<UserDto>(user));
    }

    public async Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
    {
      await _authclient.SetEmailAsync(_mapper.Map<UserDto>(user), email);
    }

    public async Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
    {
      await _authclient.SetEmailConfirmedAsync(_mapper.Map<UserDto>(user), confirmed);
    }

    public async Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
    {
      await _authclient.SetNormalizedEmailAsync(_mapper.Map<UserDto>(user), normalizedEmail);
    }

    public async Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
    {
      var userDto = _mapper.Map<UserDto>(user);
      await _authclient.SetNormalizedUserNameAsync(userDto, normalizedName);
    }

    public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
    {
      await _authclient.SetPasswordHashAsync(_mapper.Map<UserDto>(user), passwordHash);
    }

    public async Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
    {
      var userDto = _mapper.Map<UserDto>(user);
      await _authclient.SeUserDtoNameAsync(userDto, userName);
    }

    public async Task<Microsoft.AspNetCore.Identity.IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
      var userDto = _mapper.Map<UserDto>(user);
      var result = await _authclient.UpdateUserAsync(userDto);
      return _mapper.Map<Microsoft.AspNetCore.Identity.IdentityResult>(result);
    }

  }
}