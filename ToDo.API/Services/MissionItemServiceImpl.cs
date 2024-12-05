using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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

        public override async Task<messageRes> CreateMissionItem(CreateMissionItemReq req, ServerCallContext context)
        {
            try
            {
                await _missionService.CreateMissionAsync(req);

                string message = "The task item was added successfully.";
                
                return new messageRes { Message = message};
            }
            catch(Exception ex)
            {
                string errorMessage = $"Error occurred: {ex.Message}";

                return new messageRes { Message = errorMessage };
            }
        }

        public override async Task<GetMissionItemsResponse> GetInProgressMissionItem(Google.Protobuf.WellKnownTypes.Empty req, ServerCallContext context)
        {
            int categoryId = 1;

            var listMission = await _missionService.GetMissionByCategoryIdAsync(categoryId);

            var response = new GetMissionItemsResponse();

            foreach (var item in listMission) {
                var missionProtoItem = new MissionItem
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    Description = item.Description,
                    Status = "In progress"
                };
                response.MissionItems.Add(missionProtoItem);
            }

            return response;
        }

        public override async Task<GetMissionItemsResponse> GetCompletedMissionItem(Google.Protobuf.WellKnownTypes.Empty req, ServerCallContext context)
        {
            int categoryId = 2;

            var listMission = await _missionService.GetMissionByCategoryIdAsync(categoryId);

            var response = new GetMissionItemsResponse();

            foreach (var item in listMission)
            {
                var missionProtoItem = new MissionItem
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    Description = item.Description,
                    Status = "Completed"
                };
                response.MissionItems.Add(missionProtoItem);
            }

            return response;
        }

        public override async Task<messageRes> EditStatusOfMissionItem(EditStatusOfMissionItemReq req, ServerCallContext context)
        {
            try
            {
                await _missionService.EditStatusOfMissionItem(Guid.Parse(req.Id));

                string message = "The status of task item was edit successfully.";

                return new messageRes { Message = message };
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error occurred: {ex.Message}";

                return new messageRes { Message = errorMessage };
            }
        }

        public override async Task<messageRes> DeleteMissionItems(MissionItemIdReq req, ServerCallContext context)
        {
            try
            {
                List<Guid> listId = new List<Guid>();

                foreach (var item in req.MissionItemIds)
                {

                    listId.Add(Guid.Parse(item.Id));
                }

                await _missionService.DeleteMissionItemsRange(listId);

                string message = "The deleting of task item was successfully.";
                return new messageRes { Message = message };
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error occurred: {ex.Message}";

                return new messageRes { Message = errorMessage };
            }
        }

        public override async Task<messageRes> UpdateMissionItem(UpdateMissionItemReq req, ServerCallContext context)
        {
            try
            {
                await _missionService.UpdateMissionItem(Guid.Parse(req.Id), req.Name, req.Description);

                string message = "The task item was update successfully.";
                return new messageRes { Message = message };

            }
            catch (Exception ex)
            {
                string errorMessage = $"Error occurred: {ex.Message}";

                return new messageRes { Message = errorMessage };
            }
        }
    }
}
