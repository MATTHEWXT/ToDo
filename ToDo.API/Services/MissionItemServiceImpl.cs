using Grpc.Core;
using System.Threading.Tasks;
using ToDo.API;
using ToDo.API.Application.Interfaces;
using ToDo.API.Protos;

namespace ToDo.API.Services
{
    public class MissionItemServiceImpl : MissionItemService.MissionItemServiceBase
    {
        private readonly IMissionService _missionService;

        public MissionItemServiceImpl(IMissionService missionService) {
            _missionService = missionService;
        }

        public async Task<CreateRes> CreateMissionItem(CreateMissionItemReq req)
        {
            try
            {
                await _missionService.CreateMissionAsync(req);

                string message = "The task item was added successfully.";
                
                return new CreateRes { Message = message};
            }
            catch(Exception ex)
            {
                string errorMessage = $"Error occurred: {ex.Message}";

                return new CreateRes { Message = errorMessage };

            }
        }

        public async Task<GetCompletedMissionItemsResponse> GetInProgressdMissionkItem()
        {
            int categoryId = 1;

            var listMission = await _missionService.GetMissionByCategoryIdAsync(categoryId);

            var response = new GetCompletedMissionItemsResponse();

            foreach (var item in listMission) {
                var missionProtoItem = new MissionItem
                {
                    Name = item.Name,
                    Description = item.Description,
                    Status = "In progress"
                };
                response.MissionItems.Add(missionProtoItem);
            }

            return response;
        }
    }
}
