﻿using Dapper;
using Microsoft.Data.SqlClient;
using Observer.Constants;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Logs;
using SingleLog.Interfaces;
using SingleLog.Models;

namespace Observer.Data
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ISqlServerContext _sqlServerContext;
        private readonly ISingletonLogger<LogModel> _singleLog;

        /// <summary>
        /// Repository constructor.
        /// </summary>
        /// <param name="sqlServerContext">Object ISqlServerContext.</param>
        /// <param name="singleLog">Object ISingleLog.</param>
        public SubscriptionRepository(ISqlServerContext sqlServerContext, ISingletonLogger<LogModel> singleLog)
        {
            _sqlServerContext = sqlServerContext;
            _singleLog = singleLog;
        }

        public async Task<IResponse<Subscriptions>> InsertSubscription(Subscriptions subscriptionData) => 
            await QueryRunner<Subscriptions>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_DELETE_DATA,
            _sqlServerContext, QueryData.InsertSubscriptions, subscriptionData);

        public async Task<IResponse<Subscriptions>> SelectOneSubscription(int subscriptionId) =>
            await QueryRunner<Subscriptions>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, QueryData.SelectOneSubscriptions, subscriptionId);

        public async Task<IResponse<List<Subscriptions>>> SelectAllSubscription(int subscriptionId) =>
            await QueryRunner<Subscriptions>.ExecuteQueryListTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, QueryData.SelectOneSubscriptions, subscriptionId);

        public async Task<IResponse<bool>> UpdateSubscription(Subscriptions subscriptionData) =>
            await QueryRunner<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_UPDATE_DATA,
            _sqlServerContext, QueryData.UpdateSubscriptions, subscriptionData);

        public async Task<IResponse<bool>> DeleteSubscription(int subscriptionId) =>
            await QueryRunner<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.SUBSCRIPTION_DATABASE_DELETE_DATA,
            _sqlServerContext, QueryData.DeleteSubscriptions, subscriptionId);
    }
}
