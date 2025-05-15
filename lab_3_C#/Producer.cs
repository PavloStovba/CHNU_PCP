public class Producer
{
    private int id;
    private Storage storage;
    private int itemsToProduce;

    public Producer(int id, Storage storage, int itemsToProduce)
    {
        this.id = id;
        this.storage = storage;
        this.itemsToProduce = itemsToProduce;
    }

    public void Run()
    {
        for (int i = 0; i < itemsToProduce; i++)
        {
            storage.Produce(i, id);
            Thread.Sleep(100); 
        }
    }
}
