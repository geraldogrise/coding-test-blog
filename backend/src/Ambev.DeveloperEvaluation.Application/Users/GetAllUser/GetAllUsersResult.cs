using Ambev.DeveloperEvaluation.Application.Users.DTOS;
using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUser
{

    /// <summary>
    /// Response model for GetAllUsers operation
    /// </summary>
    public class GetAllUsersResult
    {
        /// <summary>
        /// The total number of users
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The list of users
        /// </summary>
        public List<UserDto> Data { get; set; } = new();

    }
}
