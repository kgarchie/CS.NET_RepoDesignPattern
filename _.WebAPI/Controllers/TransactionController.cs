using System.Text.Json.Nodes;
using _.Contracts;
using _.WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using _.WebAPI.Models;

namespace _.WebAPI.Controllers;

[ApiController]
[Route("api/v1/transactions")]
public class TransactionController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(UnitOfWork unitOfWork, ILogger<TransactionController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpPost("make-transaction")]
    public async Task<IActionResult> MakeTransaction(MakeTransactionRequest request)
    {
        // Map the request transaction values to the Transaction object
        var transaction = new Transaction
        {
            TransactionAmount = request.TransactionAmount,
            FromUser = await _unitOfWork.Users.GetUserById(request.UserFromId),
            ToUser = await _unitOfWork.Users.GetUserById(request.UserToId),
            TransactionType = 1,
            SystemTransactionId = new Guid().ToString()
        };
        
        var transactionStatus = await _unitOfWork.Transactions.MakeTransaction(transaction);
        if (!transactionStatus) return new JsonResult("Transaction Failed Due To An Internal Error") { StatusCode = 500 };

        // Find a better way to do this
        var result = "Transaction Successful" + request;
        return new JsonResult(result);
    }
}