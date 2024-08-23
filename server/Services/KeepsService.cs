



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
    return keep ?? throw new Exception($"Could not find keep with the id of {keep.Id}");
  }

  internal Keep UpdateKeep(int keepId, Keep keepData, string userId)
  {
    Keep keepToUpdate = _repository.GetById(keepId);
    if (keepToUpdate.CreatorId != userId) throw new Exception("You can not edit a keep you did not create");
    keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
    keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;
    keepToUpdate.Img = keepData.Img ?? keepToUpdate.Img;
    Keep updatedKeep = _repository.Update(keepToUpdate);
    return updatedKeep;
  }
}
