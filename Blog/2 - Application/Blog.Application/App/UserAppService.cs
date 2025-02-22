﻿using AutoMapper;
using Blog.Application.App.Core;
using Blog.Application.Interfaces;
using Blog.Application.ViewModels;
using Blog.Domain.Aggreagates.Users;
using Blog.Domain.Aggreagates.Users.Services;
using Blog.Domain.Notifications;
using MediatR;

namespace Blog.Application.App
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public UserAppService(IMediator mediator,
                              INotificationHandler<DomainNotification> notifications,
                              IMapper mapper,
                              IUserService userService
                                      ) : base(mediator, notifications)
        {
            _mapper = mapper;
            _userService = userService;

        }


        public string Signin(LoginModel loginModel)
        {
            return _userService.Signin(_mapper.Map<Login>(loginModel));
        }


        public UserModel Insert(UserModel user)
        {
            var response = _userService.Insert(_mapper.Map<User>(user));
            return _mapper.Map<UserModel>(response);
        }

        public UserModel Update(int id, UserModel user)
        {
            var response = _userService.Update(id, _mapper.Map<User>(user));
            return _mapper.Map<UserModel>(response);
        }

        public bool Delete(int id)
        {
            return _userService.Delete(id);
        }

        public UserModel GetById(int id)
        {
            return _mapper.Map<UserModel>(_userService.GetById(id));
        }

        public List<UserModel> GetAll()
        {
            return _mapper.Map<List<UserModel>>(_userService.GetAll());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _userService.Dispose();
        }

    }
}
