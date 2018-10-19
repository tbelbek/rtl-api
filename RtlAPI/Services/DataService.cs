using System;
using System.Collections.Generic;
using RtlAPI.Data;
using RtlAPI.Data.Entity;
using RtlAPI.Helper;
using RtlAPI.Services.Base;
using System.Linq;
using LightInject;

namespace RtlAPI.Services
{
    public class DataService : BaseServices<TvShow, int, ITvShowRepository>, IDataService
    {
        [Inject]
        public ICastPersonRepository CastPersonRepository { get; set; }

        [Inject]
        public IPeopleRepository PeopleRepository { get; set; }

        public ServiceResult<List<TvShow>> GetListPaginatedWithId(int id, int pageCount)
        {
            var ids = serviceRepository.GetAll().Select(t => t.Id).ToList();
            var pagedLists = SplitList(ids, pageCount).FirstOrDefault(t => t.Any(p => p == id));
            var remoteApiIds = serviceRepository.GetByExpression(t => pagedLists.Contains(t.Id)).Select(t => t.TvMazeId)
                .ToList();
            var crew = CastPersonRepository.GetByExpression(t => remoteApiIds.Contains(t.ShowId)).ToList();
            var crewPeopleId = crew.Select(k => k.PersonId).Distinct();
            var crewDetails = PeopleRepository.GetByExpression(t => crewPeopleId.Contains(t.Id)).ToList();
            crew = crew.Select(t =>
            {
                t.Person = crewDetails.FirstOrDefault(p => p.Id == t.PersonId);
                return t;
            }).ToList();
            var shows = serviceRepository.GetByExpression(t => pagedLists.Contains(t.Id)).ToList();
            shows = shows.Select(t =>
            {
                t.Crew = crew.Where(p => p.ShowId == t.TvMazeId).OrderByDescending(k => !string.IsNullOrEmpty(k.Person.Birthday) ? DateTime.ParseExact(k.Person.Birthday, "yyyy-MM-dd", null) : DateTime.MinValue).ToList();
                return t;
            }).ToList();
            return new ServiceResult<List<TvShow>>(shows);
        }

        public ServiceResult<bool> InsertWithCheck(List<TvShow> list)
        {
            var idsToCheck = list.Select(p => p.TvMazeId);
            var notInDb = serviceRepository.GetByExpression(t => idsToCheck.Contains(t.TvMazeId)).ToList();
            var toInsert = list.Where(t => !notInDb.Select(p => p.TvMazeId).Contains(t.TvMazeId)).ToList();
            BulkInsert(toInsert);
            return new ServiceResult<bool>(true);
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
    }
}