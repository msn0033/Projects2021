using System.Collections.Generic;
using TourTest.ToursModule.ViewModels;

namespace TourTest.ToursModule.InterFace

{
    public interface IReservationManager
    {
        IEnumerable<CurrentTourVM> GetAllTours();
        ClinetReservationVM NewReservation(int tourId);
        void SaveNewReservation(ClinetReservationVM vm);
    }
}