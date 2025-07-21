using AutoFixture;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondTest.Mock
{

    public class ReserveMock
    {
        private readonly Fixture _fixture;
        public ReserveMock()
        {
            _fixture = new Fixture();
        }

        public ReservationDto ReserveDto()
        {
            return _fixture.Build<ReservationDto>().Create();
        }

        public List<ReservationDto> GetListReservationDto()
        {
            return _fixture.Build<ReservationDto>().CreateMany(3).ToList();
        }


        public List<Reservation> GetListReservation()
        {
            return _fixture.Build<Reservation>().CreateMany(3).ToList();
        }

        public ReserveBookRequestDto GetReserveBookRequestDto()
        {
            return _fixture.Build<ReserveBookRequestDto>().Create();
        }

        public List<ReserveBookRequestDto> GetListReserveBookRequestDto()
        {
            return _fixture.Build<ReserveBookRequestDto>().CreateMany(3).ToList();
        }

        public ReserveAvailableDto GetReserveAvailableDto()
        {
            return _fixture.Build<ReserveAvailableDto>().Create();
        }

        public List<ReserveAvailableDto> GetListReserveAvailableDto()
        {
            return _fixture.Build<ReserveAvailableDto>().CreateMany(3).ToList();
        }
    }
}
