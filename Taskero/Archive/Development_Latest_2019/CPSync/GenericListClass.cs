using System.Collections;

public class GenericTypedList<MyType> : CollectionBase
{

    public MyType this[int index]
    {
        get
        {
            return (MyType)(InnerList[index]);
        }

        set
        {
            InnerList[index] = value;
        }
    }

    public int Add(MyType value)
    {
        return InnerList.Add(value);
    }

    public int IndexOf(MyType value)
    {
        return InnerList.IndexOf(value);
    }

    public void Insert(int index, MyType value)
    {
        InnerList.Insert(index, value);
    }

    public void Remove(MyType value)
    {
        InnerList.Remove(value);
    }

    public bool Contains(MyType value)
    {
        return InnerList.Contains(value);
    }

}

