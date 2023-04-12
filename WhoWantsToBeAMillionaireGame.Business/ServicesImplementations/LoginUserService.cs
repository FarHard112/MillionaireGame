using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Business.ServicesImplementations
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public LoginUserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LoginUserDto>> GetAllUsersAsync()
        {

            return await _unitOfWork
                .LoginUser
                .Get()
                .AsNoTracking()
                .Select(user => _mapper.Map<LoginUserDto>(user))
                .ToListAsync();
        }
        public async Task<int> CreateUserAsync(LoginUserDto userDto)
        {
            var entity = _mapper.Map<LoginUser>(userDto);
            if (entity == null) { throw new ArgumentNullException(nameof(userDto)); }

            await _unitOfWork.LoginUser.AddAsync(entity);
            var result = await _unitOfWork.Commit();
            return result;
        }

        public async Task<int> DeleteUserAsync(Guid id)
        {
            var entity = await _unitOfWork.LoginUser
                .Get()
                .FirstOrDefaultAsync(user => user.Id.Equals(id));
            if (entity != null)
            {
                _unitOfWork.LoginUser.Remove(entity);
                return await _unitOfWork.Commit();
            }
            throw new ArgumentException(nameof(id));
        }

        public async Task<int> UpdateUserAsync(LoginUserDto userDto)
        {
            var entity = _mapper.Map<LoginUser>(userDto);
            if (entity == null) { throw new ArgumentNullException(nameof(userDto)); }

            await _unitOfWork.LoginUser.UpdateAsync(entity);
            var result = await _unitOfWork.Commit();
            return result;
        }

        public async Task<LoginUserDto> GetUserById(Guid Id)
        {
            var entity = await _unitOfWork.LoginUser.GetByIdAsync(Id);
            var result = _mapper.Map<LoginUserDto>(entity);
            return result;
        }
    }
}
