using Shopia.Application.DTO_s.SubscriberDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.SubscriberServices
{
    public interface ISubscriberServices
    {
        Task<List<ResultSubscriberDTO>> GetAllSubscribers();
        Task<GetByIdSubscriberDTO> GetSubscriberById(int id);
        Task CreateSubsricer(CreateSubscriberDTO dto);
        Task UpdateSubscriber(UpdateSubscriberDTO dto);
        Task DeleteSubscriber(int id);
    }
}
