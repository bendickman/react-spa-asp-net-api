using Microsoft.EntityFrameworkCore;

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext _context;

    public HouseRepository(HouseDbContext context)
    {
        _context = context;
    }

    public async Task<List<HouseDto>> GetAllAsync()
    {
        return await _context
            .Houses
            .Select(e => new HouseDto(e.Id, e.Address, e.Country, e.Price))
            .ToListAsync();
    }

    public async Task<HouseDetailDto?> GetAsync(
        int id)
    {
        var entity = await _context
            .Houses
            .SingleOrDefaultAsync(h => h.Id == id);

        if (entity == null)
        {
            return null;
        }
            
        return ToDto(entity);
    }

    public async Task<HouseDetailDto> AddAsync(HouseDetailDto houseDetailDto)
    {
        var house = new HouseEntity();
        ToEntity(houseDetailDto, house);

        await _context.Houses.AddAsync(house);
        await _context.SaveChangesAsync();

        return ToDto(house);
    }

    public async Task<HouseDetailDto> UpdateAsync(HouseDetailDto houseDetailDto)
    {
        var house = await _context.Houses.FindAsync(houseDetailDto.Id);
        if (house is null)
        {
            throw new ArgumentException($"Trying to update house: entity with ID {houseDetailDto.Id} not found.");
        }

        ToEntity(houseDetailDto, house);
        _context.Entry(house).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return ToDto(house);
    }

    public async Task DeleteAsync(int id)
    {
        var house = await _context.Houses.FindAsync(id);
        if (house is null)
        {
            throw new ArgumentException($"Trying to delete a house: entity with Id {id} not found");
        }

        _context.Houses.Remove(house);
        await _context.SaveChangesAsync();
    }

    private static HouseDetailDto ToDto(
        HouseEntity e)
    {
        return new HouseDetailDto(e.Id, e.Address, e.Country, e.Description, e.Price, e.Photo);
    }

    private static void ToEntity(HouseDetailDto dto, HouseEntity e)
    {
        e.Address = dto.Address;
        e.Country = dto.Country;
        e.Description = dto.Description;
        e.Price = dto.Price;
        e.Photo = dto.Photo;
    }
}