﻿namespace PoleTimeGuesser.Services
{
    public interface IF1DataGetterService
    {
        Task<CircuitInfoModel> GetCicuitInfoAsync(string id);
        Task<ConstructorInfoModel> GetConstructorInfoAsync(string id);
        Task<List<ConstructorStandingsModel>> GetConstructorStandings();
        Task<DriverInfoModel> GetDriverInfo(string id);
        Task<List<DriverStandingsModel>> GetDriverStandings();
        Task<QualifyingResultModel> GetQualifyingResult(string circuitId);
        Task<List<ScheduleModel>> GetSchedule();
        Task<List<DriverStandingsModel>> GetTopDrivers();
        Task<ScheduleModel> GetUpComingEvent();
    }
}