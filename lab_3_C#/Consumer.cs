public class Consumer
{
    private int id;
    private Storage storage;
    private int itemsToConsume;

    public Consumer(int id, Storage storage, int itemsToConsume)
    {
        this.id = id;
        this.storage = storage;
        this.itemsToConsume = itemsToConsume;
    }

    public void Run()
    {
        for (int i = 0; i < itemsToConsume; i++)
        {
            storage.Consume(id);
            Thread.Sleep(150);
        }
    }
}
