public interface IHouseRepository
{
    Task<List<HouseDto>> GetAllAsync();

    Task<HouseDetailDto?> GetAsync(int id);

    Task<HouseDetailDto> AddAsync(HouseDetailDto houseDetailDto);

    Task<HouseDetailDto> UpdateAsync(HouseDetailDto houseDetailDto);

    Task DeleteAsync(int id);
}