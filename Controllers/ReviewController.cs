using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;
using Microsoft.AspNetCore.Authorization;
using TraineeManagement.api.Models;
using TraineeManagement.api.Services;


[ApiController]
[Authorize]
[Route("/api/reviews")]
public class ReviewController : ControllerBase
{
    public IReviewService _ReviewService;

    public ReviewController(IReviewService ReviewService)
    {
        _ReviewService = ReviewService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res =await _ReviewService.GetAll();
            return Ok(res);
    }
    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetbyId(int Id)
    {
        ReviewResponse? ReviewResponse = await _ReviewService.GetById(Id);
            return ReviewResponse == null ? NotFound() : Ok(ReviewResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewRequest Review)
    {
        ReviewResponse ReviewResponse = await _ReviewService.Create(Review);
            return Created($"/api/reviews/{ReviewResponse.Id}",ReviewResponse);
    }
    
}
