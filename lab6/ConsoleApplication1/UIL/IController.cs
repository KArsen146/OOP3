namespace UIL
{
    public interface IController<UILobj, BLLobj>
    {
        UILobj Convert(BLLobj obj);

        BLLobj Convert(UILobj obj);

        int Add(UILobj obj);
    }
}