
namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repository;

  public KeepsService(KeepsRepository keepsRepository)
  {
    _repository = keepsRepository;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep newKeep = _repository.Create(keepData);
    return newKeep;
  }
}
