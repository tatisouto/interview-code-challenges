using AutoFixture;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;

namespace OneBeyondTest.Mock
{
    public class FineMock
    {
        private readonly Fixture _fixture;

        public FineMock()
        {
            _fixture = new Fixture();
        }

        public FineDto GetFineDto()
        {
            return _fixture.Build<FineDto>().Create();
        }
        public Fine GetFine()
        {
            return _fixture.Build<Fine>().Create();
        }

        public List<FineDto> GetListFineDto()
        {
            return _fixture.Build<FineDto>().CreateMany(3).ToList();
        }


        public List<Fine> GetListFine()
        {
            return _fixture.Build<Fine>().CreateMany(3).ToList();
        }
    }
}
