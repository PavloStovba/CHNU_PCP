class MinFinderTask
{
    private readonly int[] array;
    private readonly int start;
    private readonly int end;
    private readonly ParallelMinFinder finder;

    public MinFinderTask(int[] array, int start, int end, ParallelMinFinder finder)
    {
        this.array = array;
        this.start = start;
        this.end = end;
        this.finder = finder;
    }

    public void Run()
    {
        int localMin = int.MaxValue;
        int localMinIndex = -1;

        for (int i = start; i < end; i++)
        {
            if (array[i] < localMin)
            {
                localMin = array[i];
                localMinIndex = i;
            }
        }

        finder.ReportResult(localMin, localMinIndex);
    }
}
