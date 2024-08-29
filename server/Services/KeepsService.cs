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

  internal string DeleteKeep(int keepId, string userId)
  {
    Keep keepToDelete = GetKeepById(keepId);
    if (keepToDelete.CreatorId != userId) throw new Exception("You can not delete a keep you did not create");
    _repository.Delete(keepId);
    return "Keep successfully deleted";
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAll();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetById(keepId);
    if (keep == null || keep.Id == 0) throw new Exception($"Could not find keep with id of {keepId}");
    return keep;
  }

  internal List<Keep> GetProfileKeeps(string profileId)
  {
    List<Keep> keeps = _repository.GetProfileKeeps(profileId);
    return keeps;
  }

  internal Keep IncrementViews(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);
    if (userId == keep.CreatorId) return keep;
    keep.Views++;
    _repository.Update(keep);
    return keep;
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
