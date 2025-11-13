using AutoMapper;
using Interfaces;
using Microsoft.Extensions.Logging;
using Models;
using ModelsDLL.DTO;
using ModelsDLL.Models;

namespace ServicesDLL.Services
{
    public class KotService: IKotService
    {
        private IKotRepo _kotRepo;
        private readonly ILogger<KotService> _logger;
        private readonly IMapper _mapper;
        public KotService(ILogger<KotService> logger, IKotRepo kotRepo, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _kotRepo = kotRepo;
        }
        public List<Kot> GetAll()
        {
            _logger.LogInformation("Entering GetAll method in KotsService");
            List<KotStudentDTO> kotsDTO = _kotRepo.GetAll();

            List<Kot> kots = _mapper.Map<List<Kot>>(kotsDTO);

            _logger.LogInformation("Exiting GetAll method in KotsService");
            return kots;
        }
    }
}
