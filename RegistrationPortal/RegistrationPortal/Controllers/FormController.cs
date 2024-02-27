using Microsoft.AspNetCore.Authorization;
using RegistrationPortal.CustomExceptions;

namespace RegistrationPortal.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FormController : ControllerBase
{
    private readonly IFormService _formService;
    private readonly ILogger<FormController> _logger;

    public FormController(IFormService formService, ILogger<FormController> logger)
    {
        _formService = formService;
        _logger = logger;
    }

    // GET: api/Form/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<FormDto>> GetForm(string id)     
    {
        var form = await _formService.GetForm(id);

        if (form is null)
        {
            return NotFound();
        }

        return form;
    }

    // POST api/Form
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FormDto form)
    {
        try
        {
            await _formService.CreateForm(form);

            return Redirect($"/api/Form/{form.Id}");
            //return CreatedAtAction(nameof(GetForm), new { id = form.Id }, form);
        }
        catch (FormDataValidationException e)
        {
            return BadRequest(new { Error = e.Message });
        }
        catch (AgeEligibilityException e)
        {
            return BadRequest(new { Error = e.Message });
        }

        catch (DuplicateFormException e)
        {
            return Conflict(new { Error = e.Message });
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "An unexpected error occurred while creating a form.");
            return BadRequest(new { Error = "An unexpected error occurred." });   
        }
    }

    // PUT api/Form
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] FormDto form)  
    {
        try
        {
            await _formService.UpdateForm(form);
        }
        catch (FormDataValidationException e)
        {
            return BadRequest(new { Error = e.Message });
        }
        catch (AgeEligibilityException e)
        {
            return BadRequest(new { Error = e.Message });
        }
        catch (DuplicateFormException e)
        {
            return Conflict(new { Error = e.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred while updating a form.");
            return BadRequest(new { Error = "An unexpected error occurred." });
        }

        return NoContent();
    }
}
