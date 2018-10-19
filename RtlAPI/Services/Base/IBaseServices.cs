using System.Collections.Generic;
using RtlAPI.Helper;

namespace RtlAPI.Services.Base
{
    public interface IBaseServices<D, T>
    {
        ServiceResult<List<D>> GetAll();
        ServiceResult<int> Count();
        ServiceResult<List<D>> BulkInsert(List<D> dtoModel);
    }
}
