using System;
using System.Collections.Generic;
using System.Linq;
using RtlAPI.Data;
using RtlAPI.Data.Entity.Base;
using RtlAPI.Helper;

namespace RtlAPI.Services.Base
{
    public class BaseServices<E, T, R> : IBaseServices<E, T> //, IEnumerable<D>
        where R : IDataRepository<E, T> where E : BaseEntity<T> where T : new()
    {
        protected R serviceRepository = LightInjectInstanceProvider.GetInstance<R>();

        protected ServiceResult<X> ExceptionControl<X>(Func<object, X> func, object key)
        {
            ServiceResult<X> result;
            try
            {
                result = new ServiceResult<X>(func(key));
            }
            catch (Exception ex)
            {
                result = new ServiceResult<X>(ex);
            }
            return result;
        }
        
        public ServiceResult<List<E>> BaseTransaction(Func<List<E>, List<E>> func, List<E> models)
        {
            ServiceResult<List<E>> result;
            using (var dbContextTransaction = serviceRepository.BaseContext.Database.BeginTransaction())
            {
                try
                {
                    result = new ServiceResult<List<E>>(func(models));
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    result = new ServiceResult<List<E>>(ex);
                }
            }
            return result;
        }

        public ServiceResult<List<E>> GetAll() => new ServiceResult<List<E>>(serviceRepository.GetAll().ToList());

        public ServiceResult<int> Count()
        {
            return new ServiceResult<int>(serviceRepository.Count());
        }

        public virtual ServiceResult<List<E>> BulkInsert(List<E> dtoModel)
        {
            return BaseTransaction(
                data =>
                    {
                        serviceRepository.InsertList(dtoModel);
                        return dtoModel;
                    },
                dtoModel);
        }


        #region Service result check area
        protected ServiceResult<E> ServiceResultFromList(ServiceResult<E> result)
        {
            return result.ResultType == ServiceResultType.Success
                       ? new ServiceResult<E>(result.Data)
                       : new ServiceResult<E>(result.Exception);
        }

        protected ServiceResult<List<E>> ServiceResultFromList(ServiceResult<List<E>> result)
        {
            return result.ResultType == ServiceResultType.Success
                       ? new ServiceResult<List<E>>(result.Data)
                       : new ServiceResult<List<E>>(result.Exception);
        }
        #endregion

    }
}