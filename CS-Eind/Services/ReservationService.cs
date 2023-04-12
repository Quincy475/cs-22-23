using CS_Eind.Models;
using CS_Eind.Models.DTO;
using AutoMapper;
using CS_Eind.Repositories;
using CS_Eind.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Net;

namespace CS_Eind.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IAirBnBRepository _airBnBRepository;
        private readonly IMapper _mapper;

        public ReservationService(IAirBnBRepository airBnBRepository, IMapper mapper)
        {
            _mapper = mapper;
            _airBnBRepository = airBnBRepository;
        }
        public async Task<ResResponseDTO> MakeReservation(ResRequestDTO resRequest, CancellationToken cancellationToken)
        {
            var customer = await _airBnBRepository.GetCustomerByEmailAsync(resRequest.Email, cancellationToken);
            if (customer == null)
            {
                var Newcustomer = await _airBnBRepository.CreateCustomerFromRequestAsync(resRequest, cancellationToken);
                customer = Newcustomer;

            }
            var location = await _airBnBRepository.GetLocationById(resRequest.LocationId, cancellationToken);

            // Maak reservation
            var reservation = await _airBnBRepository.CreateReservation(resRequest, cancellationToken);
            var reservationDto = _mapper.Map<ResResponseDTO>(reservation);

            reservationDto.CustomerName = customer.FirstName;
            reservationDto.LocationName = location.Title;






            var duration = (resRequest.EndDate - resRequest.StartDate).TotalDays;
            var pricePerDay = location.PricePerDay;
            var totalPrice = (float)duration * pricePerDay;
            var discount = resRequest.Discount ?? 0;
            var discountAmount = totalPrice * discount;
            var finalPrice = totalPrice - discountAmount;



            reservationDto.Price = finalPrice;


            return reservationDto;
        }







    }
}
