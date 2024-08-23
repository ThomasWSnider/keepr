namespace keepr.Interfaces;

public interface IRepository<T>
{
  public T Create(T data);
  public T Update(T data);
  public void Delete(int id);
  public T GetById(int id);
}