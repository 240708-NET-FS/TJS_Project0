namespace RevatureP0TimStDennis.DAO;

public interface IDAO<T>
{
    public void Create(T item);

    public T GetByID(int ID);

    public ICollection<T> GetAll();

    public void Update(T newItem);

    public void Delete(T item);
}