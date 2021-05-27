
using System;
using System.Threading;

public static class MutexWaitHanldes
{
    public static Mutex _handleOne = new Mutex(true);
    public static Mutex _handleTwo = new Mutex(false);
}
public class DAO
{
    public readonly static DAO Instance = new DAO();
    public Mutex _handleOne = new Mutex(true);
    public Mutex _handleTwo = new Mutex(true);
    private DAO() { }

    // [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
    public void Create()
    {

        Console.WriteLine("Create Action Awaiting For Signal");
        this._handleOne.WaitOne();

        try
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Create Action.......");
                Thread.Sleep(1000);
                if (i == 5) { return; }
            }
        }
        finally
        {
            this._handleOne.ReleaseMutex();
        }
    }
    //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
    public void Delete()
    {

        Console.WriteLine("Delete Action Awaiting For Signal");
        this._handleOne.WaitOne();
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Delete Action.......");
            Thread.Sleep(500);
        }
        this._handleOne.ReleaseMutex();
    }

    //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
    public void Update()
    {
        Console.WriteLine("Update Action Awaiting For Signal");
        this._handleOne.WaitOne();
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Update Action.......");
            Thread.Sleep(800);
        }
        this._handleOne.ReleaseMutex();
    }
    //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
    public void Select()
    {
        Console.WriteLine("Select Action Awaiting For Signal");
        this._handleTwo.WaitOne();
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Select Action.......");
            Thread.Sleep(800);
        }

        this._handleTwo.ReleaseMutex();

    }
    // [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
    public void SelectByKey()
    {
        Console.WriteLine("SelectKey Action Awaiting For Signal");
        this._handleTwo.WaitOne();
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("SelectKey Action.......");
            Thread.Sleep(800);
        }
        this._handleTwo.ReleaseMutex();

    }
}
class SynchronizationDemo
{
    //public Mutex _handleTwo = new Mutex(true);
    static void Main()
    {
        //Mutex _handleThree = new Mutex(true);
        DAO singleton = DAO.Instance;
        new Thread(singleton.Create).Start();
        new Thread(singleton.Delete).Start();
        new Thread(singleton.Update).Start();

        new Thread(singleton.Select).Start();
        new Thread(singleton.SelectByKey).Start();

        Console.WriteLine("Click Here To Start Thread Actions");
        Console.ReadKey();
        singleton._handleOne.ReleaseMutex();
        singleton._handleTwo.ReleaseMutex();

        singleton._handleOne.WaitOne();
        singleton._handleTwo.WaitOne();
        Console.WriteLine("End OF Main");
        singleton._handleOne.ReleaseMutex();
        singleton._handleTwo.ReleaseMutex();


    }
}
