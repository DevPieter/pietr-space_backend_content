using PietrSpace.Content.Info.Data.Models.DTOs.Info;
using PietrSpace.Content.Info.Data.Models.Entities;

namespace PietrSpace.Content.Info.Data.Repositories;

public interface IInfoRepository
{
    Task<InfoEntryEntity?> GetInfoEntryAsync(
        string key
    );

    Task UpsertInfoEntryAsync(
        UpsertInfoEntryDto dto
    );
}