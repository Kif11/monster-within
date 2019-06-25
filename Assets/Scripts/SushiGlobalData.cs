using System.Collections;

public static class SushiGlobalData
{
    public static Queue sushiQueue;
    static SushiGlobalData()
    {
        // perform initialization here
        sushiQueue = new Queue();
    }
}
