namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;

  public KeepsController(Auth0Provider auth0Provider)
  {
    _auth0Provider = auth0Provider;
  }
}
