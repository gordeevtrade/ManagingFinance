using AutoMapper;
using FamilyBudjetAPI.DTOModels;
using FamilyBudjetAPI.Sevices.Interface;
using ManagingFinanceAPI.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudjetAPI.Controllers
{
    //   [Authorize(AuthenticationSchemes = "Google,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FinanceTransactionDTO>> GetTransactions()
        {
            var transactions = _transactionService.GetTransactions();
            var transactionDtos = _mapper.Map<IEnumerable<FinanceTransactionDTO>>(transactions);
            return Ok(transactionDtos);
        }

        [HttpGet("TransactionWithCategoryName")]
        public ActionResult<IEnumerable<TransactionWithCategoryNameDTO>> GetTransactionsWithCategoryNames()
        {
            var transactions = _transactionService.GetTransactionsWithCategoryNames();
            var transactionDto = _mapper.Map<IEnumerable<TransactionWithCategoryNameDTO>>(transactions);

            return Ok(transactions);
        }

        [HttpPost]
        public ActionResult<FinanceTransactionDTO> CreateTransaction(FinanceTransactionDTO transactionDto)
        {
            var transaction = _mapper.Map<FinanceTransaction>(transactionDto);
            var createdTransaction = _transactionService.CreateTransaction(transaction);
            var createdTransactionDto = _mapper.Map<FinanceTransactionDTO>(createdTransaction);
            return Ok(createdTransactionDto);
        }

        [HttpGet("{id}")]
        public ActionResult<FinanceTransactionDTO> GetTransaction(int id)
        {
            try
            {
                var transaction = _transactionService.GetTransaction(id);
                var transactionDto = _mapper.Map<FinanceTransactionDTO>(transaction);
                return Ok(transactionDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<FinanceTransactionDTO>> GetTransactionsByCategory(int categoryId)
        {
            try
            {
                var transactions = _transactionService.GetTransactionsByCategory(categoryId);
                var transactionDtos = _mapper.Map<IEnumerable<FinanceTransactionDTO>>(transactions);
                return Ok(transactionDtos);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateTransaction(FinanceTransactionDTO updatedTransactionDto)
        {
            try
            {
                var updatedTransaction = _mapper.Map<FinanceTransaction>(updatedTransactionDto);
                _transactionService.UpdateTransaction(updatedTransaction);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            try
            {
                _transactionService.DeleteTransaction(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}