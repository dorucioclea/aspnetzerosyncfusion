using System.Threading.Tasks;
using Abp.Dependency;
using Abp.ObjectMapping;
using Practia.AppDemo.ApiClient;
using Practia.AppDemo.ApiClient.Models;
using Practia.AppDemo.Core.DataStorage;
using Practia.AppDemo.Models.Common;
using Practia.AppDemo.Sessions.Dto;

namespace Practia.AppDemo.Services.Storage
{
    public class DataStorageService : IDataStorageService, ISingletonDependency
    {
        private readonly IDataStorageManager _dataStorageManager;
        private readonly IObjectMapper _objectMapper;

        public DataStorageService(
            IDataStorageManager dataStorageManager,
            IObjectMapper objectMapper)
        {
            _dataStorageManager = dataStorageManager;
            _objectMapper = objectMapper;
        }

        public async Task StoreAccessTokenAsync(string newAccessToken)
        {
            var authenticateResult = _dataStorageManager.Retrieve<AuthenticateResultPersistanceModel>(DataStorageKey.CurrentSession_TokenInfo);

            authenticateResult.AccessToken = newAccessToken;

            await _dataStorageManager.StoreAsync(DataStorageKey.CurrentSession_TokenInfo, authenticateResult);
        }

        public AbpAuthenticateResultModel RetrieveAuthenticateResult()
        {
            return _objectMapper.Map<AbpAuthenticateResultModel>(
                _dataStorageManager.Retrieve<AuthenticateResultPersistanceModel>(
                    DataStorageKey.CurrentSession_TokenInfo
                )
            );
        }

        public async Task StoreAuthenticateResultAsync(AbpAuthenticateResultModel authenticateResultModel)
        {
            await _dataStorageManager.StoreAsync(
                DataStorageKey.CurrentSession_TokenInfo,
                _objectMapper.Map<AuthenticateResultPersistanceModel>(authenticateResultModel)
            );
        }

        public TenantInformation RetrieveTenantInfo()
        {
            return _objectMapper.Map<TenantInformation>(
                _dataStorageManager.Retrieve<TenantInformationPersistanceModel>(
                    DataStorageKey.CurrentSession_TenantInfo
                )
            );
        }

        public async Task StoreTenantInfoAsync(TenantInformation tenantInfo)
        {
            await _dataStorageManager.StoreAsync(
                DataStorageKey.CurrentSession_TenantInfo,
                _objectMapper.Map<TenantInformationPersistanceModel>(tenantInfo)
            );
        }

        public GetCurrentLoginInformationsOutput RetrieveLoginInfo()
        {
            return _objectMapper.Map<GetCurrentLoginInformationsOutput>(
                _dataStorageManager.Retrieve<CurrentLoginInformationPersistanceModel>(
                    DataStorageKey.CurrentSession_LoginInfo
                )
            );
        }

        public async Task StoreLoginInformationAsync(GetCurrentLoginInformationsOutput loginInfo)
        {
            await _dataStorageManager.StoreAsync(
                DataStorageKey.CurrentSession_LoginInfo,
                _objectMapper.Map<CurrentLoginInformationPersistanceModel>(
                    loginInfo
                )
            );
        }

        public void ClearSessionPersistance()
        {
            _dataStorageManager.RemoveIfExists(DataStorageKey.CurrentSession_TokenInfo);
            _dataStorageManager.RemoveIfExists(DataStorageKey.CurrentSession_TenantInfo);
            _dataStorageManager.RemoveIfExists(DataStorageKey.CurrentSession_LoginInfo);
        }
    }
}