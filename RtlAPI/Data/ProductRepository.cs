using System;
using System.Linq;
using System.Linq.Expressions;
using RtlAPI.Data.Entity;

namespace RtlAPI.Data
{
    public class TvShowRepository : DataRepository<int, TvShow>, ITvShowRepository
    {

    }

    public interface ITvShowRepository : IDataRepository<TvShow, int>
    {
       
    }

    public class CastPersonRepository : DataRepository<int, CastPerson>, ICastPersonRepository
    {

    }

    public interface ICastPersonRepository : IDataRepository<CastPerson, int>
    {

    }

    public class PeopleRepository : DataRepository<int, Person>, IPeopleRepository
    {

    }

    public interface IPeopleRepository : IDataRepository<Person, int>
    {

    }
}