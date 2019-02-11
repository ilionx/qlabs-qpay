using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class RegisterNewTerminal
    {
        private readonly SaveNewTerminal _saveNewTerminal;

        public RegisterNewTerminal(SaveNewTerminal saveNewTerminal)
        {
            _saveNewTerminal = saveNewTerminal;
        }

        public async Task RegisterTerminal(Terminal terminal)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Terminal, DataAccess.Models.Terminal>());

            var mapper = mapperConfig.CreateMapper();

            await _saveNewTerminal.CreateNewTerminal(mapper.Map<DataAccess.Models.Terminal>(terminal));
        }
    }
}