using AutoMapper;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class RemoveTerminal
    {
        private readonly DeleteTerminalWithId _deleteTerminalWithId;

        public RemoveTerminal(DeleteTerminalWithId deleteTerminalWithId)
        {
            _deleteTerminalWithId = deleteTerminalWithId;
        }

        public async Task<Terminal> CheckIfTerminalIsValid(string id)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.Terminal, Terminal>());
            var mapper = mapperConfig.CreateMapper();
            return mapper.Map<Terminal>(await _deleteTerminalWithId.GetTerminal(id));
        }

        public async Task RemoveTerminalWithId(string id)
        {
            await _deleteTerminalWithId.DeleteTerminal(id);
        }
    }
}