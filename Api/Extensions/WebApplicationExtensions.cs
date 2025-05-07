using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;


public static class WebApplicationExtensions
{
    public static void MapHouseEndpoints(this WebApplication app)
    {
        app.MapGet("/houses", [Authorize](IHouseRepository houseRepository) => houseRepository.GetAllAsync())
            .Produces<HouseDto[]>(StatusCodes.Status200OK);

        app.MapGet("/houses/{id:int}", [Authorize] async (int id, IHouseRepository houseRepository) =>
        {
            var house = await houseRepository.GetAsync(id);

            if (house is null)
            {
                return Results.Problem($"House with id: {id} not found", statusCode: StatusCodes.Status404NotFound);
            }

            return Results.Ok(house);
        }).ProducesValidationProblem().ProducesProblem(StatusCodes.Status404NotFound).Produces<HouseDetailDto>(StatusCodes.Status200OK);

        app.MapPost("/houses", [Authorize("admin")] async ([FromBody]HouseDetailDto houseDetailDto, IHouseRepository houseRepository) =>
        {
            if (!MiniValidator.TryValidate(houseDetailDto, out var errors))
            {
                return Results.ValidationProblem(errors);
            }
            var newHouse = await houseRepository.AddAsync(houseDetailDto);

            return Results.Created($"/houses/{newHouse.Id}", newHouse);
        }).ProducesValidationProblem().Produces<HouseDetailDto>(StatusCodes.Status201Created);

        app.MapPut("houses", [Authorize] async ([FromBody]HouseDetailDto houseDetailDto, IHouseRepository houseRepository) =>
        {
            if (!MiniValidator.TryValidate(houseDetailDto, out var errors))
            {
                return Results.ValidationProblem(errors);
            }
            var house = await houseRepository.GetAsync(houseDetailDto.Id);

            if (house is null)
            {
                return Results.Problem($"House with Id {houseDetailDto.Id} not found", statusCode: StatusCodes.Status404NotFound);
            }

            var updatedHouse = await houseRepository.UpdateAsync(houseDetailDto);

            return Results.Ok(updatedHouse);
        }).ProducesProblem(StatusCodes.Status404NotFound).Produces<HouseDetailDto>(StatusCodes.Status200OK);

        app.MapDelete("/houses/{id:int}", [Authorize] async (int id, IHouseRepository houseRepository) => 
        {
            var house = await houseRepository.GetAsync(id);
            if (house is null)
            {
                return Results.Problem($"House with Id {id} not found", statusCode: 404);
            }

            await houseRepository.DeleteAsync(id);

            return Results.Ok();
        }).ProducesProblem(StatusCodes.Status404NotFound).Produces(StatusCodes.Status200OK);
    }

    public static void MapBidEndpoints(this WebApplication app)
    {
        app.MapGet("/house/{houseId:int}/bids", [Authorize] async (int houseId, 
            IHouseRepository houseRepository, IBidRepository bidRepository) =>
        {
            if (await houseRepository.GetAsync(houseId) == null)
            {
                return Results.Problem($"House with Id {houseId} not found", statusCode: StatusCodes.Status404NotFound);
            }

            var bids = await bidRepository.Get(houseId);
            
            return Results.Ok(bids);
        }).ProducesProblem(StatusCodes.Status404NotFound).Produces(StatusCodes.Status200OK);

        app.MapPost("/house/{houseId:int}/bids", [Authorize] async (int houseId, [FromBody] BidDto dto, IBidRepository bidRepository) => 
        {   
            if (dto.HouseId != houseId)
            {
                return Results.Problem($"House Id of DTO {dto.HouseId} doesn't match with URL data {houseId}", 
                    statusCode: StatusCodes.Status400BadRequest);
            }
            if (!MiniValidator.TryValidate(dto, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            var newBid = await bidRepository.Add(dto);
            
            return Results.Created($"/houses/{newBid.HouseId}/bids", newBid);
        }).ProducesValidationProblem().ProducesProblem(StatusCodes.Status400BadRequest).Produces<BidDto>(StatusCodes.Status201Created);
    }
}
