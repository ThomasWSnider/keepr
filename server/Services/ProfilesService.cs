


namespace keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repository;

  public ProfilesService(ProfilesRepository repository)
  {
    _repository = repository;
  }

  internal Profile GetProfile(string profileId)
  {
    Profile profile = _repository.GetProfile(profileId);
    return profile;
  }
}
