using API.Contracts;
<<<<<<< Updated upstream
=======
using API.DTOs.Employees;
using API.DTOs.TransactionEvents;
using API.Models;
using API.Repositories;
using API.Utilities.Handler;
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

<<<<<<< Updated upstream
=======
[ApiController]
[Route("api/[controller]")]
>>>>>>> Stashed changes
public class TransactionEventController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionEventController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
<<<<<<< Updated upstream
=======

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _transactionRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (TransactionEventDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<TransactionEventDto>>(data));
    }

    [HttpGet("detail")]
    public IActionResult GetAllDetail()
    {
        var result = _transactionRepository.GetAllDetailTransaction();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (TransactionDetailDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<TransactionDetailDto>>(data));
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _transactionRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<TransactionEventDto>((TransactionEventDto)result));
    }



    [HttpPost]
    public IActionResult Create(TransactionEventDto transactionEventDto)
    {
        try
        {
            string currentYear = DateTime.Now.Year.ToString();
            TransactionEvent toCreate = transactionEventDto;
            toCreate.Invoice = GenerateHandler.Invoice(_transactionRepository.GetLastTransactionByYear(currentYear));

            var result = _transactionRepository.Create(toCreate);


            return Ok(new ResponseOKHandler<TransactionEventDto>("Data has been created successfully")
            { Data = (TransactionEventDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    [HttpPut]
    public IActionResult Update(TransactionEventDto transactionEventDto)
    {
        try
        {
            var entity = _transactionRepository.GetByGuid(transactionEventDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            TransactionEvent toUpdate = transactionEventDto;

            _transactionRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<TransactionEventDto>("Data has been updated successfully")
            { Data = (TransactionEventDto)toUpdate });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var entity = _transactionRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _transactionRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
>>>>>>> Stashed changes
}