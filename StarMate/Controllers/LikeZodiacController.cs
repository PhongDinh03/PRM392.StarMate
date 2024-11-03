using Application.IService;
using Application.ViewModels.LikeZodiacDTO;
using Microsoft.AspNetCore.Mvc;

namespace StarMate.Controllers
{
    /// <summary>
    /// Controller for managing LikeZodiac operations.
    /// </summary>
    [Route("api/like-zodiac")]
    [ApiController]
    public class LikeZodiacController : ControllerBase
    {
        private readonly ILikeZodiacService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="LikeZodiacController"/> class.
        /// </summary>
        /// <param name="service">The service to manage LikeZodiac operations.</param>
        public LikeZodiacController(ILikeZodiacService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all LikeZodiac records.
        /// </summary>
        /// <returns>A list of LikeZodiac records.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLikeZodiac()
        {
            var result = await _service.GetAllLikeZodiac();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Gets a LikeZodiac record by its ID.
        /// </summary>
        /// <param name="id">The ID of the LikeZodiac record.</param>
        /// <returns>The LikeZodiac record with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLikeZodiacById(int id)
        {
            var result = await _service.GetLikeZodiacById(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Adds a new LikeZodiac record.
        /// </summary>
        /// <param name="createDto">The data transfer object containing the details of the LikeZodiac record to add.</param>
        /// <returns>The created LikeZodiac record.</returns>
        [HttpPost]
        public async Task<IActionResult> AddLikeZodiacAsync(CreateLikeZodiacDTO createDto)
        {
            var result = await _service.AddLikeZodiacAsync(createDto);
            return result.Success ? CreatedAtAction(nameof(GetLikeZodiacById), new { id = result.Data.Id }, result) : BadRequest(result);
        }

        /// <summary>
        /// Updates an existing LikeZodiac record.
        /// </summary>
        /// <param name="id">The ID of the LikeZodiac record to update.</param>
        /// <param name="updateDto">The data transfer object containing the updated details of the LikeZodiac record.</param>
        /// <returns>The updated LikeZodiac record.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLikeZodiacAsync(int id, CreateLikeZodiacDTO updateDto)
        {
            var result = await _service.UpdateLikeZodiacAsync(id, updateDto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Deletes a LikeZodiac record by its ID.
        /// </summary>
        /// <param name="id">The ID of the LikeZodiac record to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikeZodiacAsync(int id)
        {
            var result = await _service.DeleteLikeZodiacAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
