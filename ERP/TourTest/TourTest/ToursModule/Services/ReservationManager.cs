using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourTest.Data;
using TourTest.ToursModule.InterFace;
using TourTest.ToursModule.Models;
using TourTest.ToursModule.ViewModels;

namespace TourTest.ToursModule.Services
{
    public class ReservationManager : IReservationManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReservationManager(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        //get
        public IEnumerable<CurrentTourVM>GetAllTours()
        { 
            var tours= _mapper.Map<List<CurrentTourVM>>(_context.Tours.ToList());
            var reservations = _context.Reservations.ToList();
            foreach (var item in tours)
            {
                item.ReservationCount= reservations.Where(x => x.TourId == item.Id).Count();
             
            }
            return tours;
        }

        public ClinetReservationVM NewReservation(int tourId)
        {
            var vm = new ClinetReservationVM();
            vm.Tour = _context.Tours.Find(tourId);
            return vm;
        }
        //post save
        public void SaveNewReservation(ClinetReservationVM vm)
        {
            using(IDbContextTransaction transaction=_context.Database.BeginTransaction())
            {
                try 
                {//1-Reservation Tabe add Reservation 
                    var re = new Reservation();
                    re.TourId = vm.Tour.Id;
                    re.ClientId = vm.ClientId;
                    re.Paid = vm.PaymentAmount;
                    _context.Reservations.Add(re);
                    _context.SaveChanges();
                 //2-add paymentHistory for this Reservation
                    var ph = new PaymentHistory();
                    ph.TourId = vm.Tour.Id;
                    ph.ClientId = vm.ClientId;
                    ph.PaidAmount = vm.PaymentAmount;
                    ph.PaymentDate = DateTime.UtcNow;
                    _context.paymentHistories.Add(ph);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch(Exception) { transaction.Rollback(); }
            }
        }
    }
}
