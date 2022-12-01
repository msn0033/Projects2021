using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourTest.ToursModule.Models;
using TourTest.ToursModule.ViewModels;

namespace TourTest.Infrastructure
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Tour, CurrentTourVM>()
                .ForMember(x => x.TourDate, src => src.MapFrom(x => x.TourDate.ToString("dd/mm/yyyy")));
           
            
            
        }
    }
}
