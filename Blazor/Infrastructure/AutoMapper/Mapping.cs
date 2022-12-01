using AutoMapper;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

namespace Infrastructure.AutoMapper
{
   public class Mapping :Profile
    {
        public Mapping()
        {
            CreateMap<Employee, EditEmployeeModel>()
                .ForMember(des => des.ConfirmEmail, surce => surce.MapFrom(x => x.Email));

            CreateMap<EditEmployeeModel, Employee>();
           

        }
    }
}
