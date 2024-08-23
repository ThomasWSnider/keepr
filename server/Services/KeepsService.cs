


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

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAll();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetById(keepId);
    return keep;
  }
}
