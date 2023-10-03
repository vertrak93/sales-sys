using Microsoft.EntityFrameworkCore;
using Sales.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Sales.BLL.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetUsers()
        {
            var users = await _unitOfWork.Users.Get().ToListAsync();

            return users.Select( el => new {el.Username, el.CreatedDate, el.Email, el.FisrtName, el.LastName } );
        }

    }
}
