using System.Collections.Generic;
using RtlAPI.Data.Entity;
using RtlAPI.Helper;
using RtlAPI.Services.Base;

namespace RtlAPI.Services
{
    public interface IDataService : IBaseServices<TvShow, int>
    {
        ServiceResult<List<TvShow>> GetListPaginatedWithId(int id, int pageCount);
    }
}
