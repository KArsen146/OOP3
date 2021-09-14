using System.Collections.Generic;

namespace BLL
{
    public interface IService<BLLobj, DALobj>
    {
        int Add(BLLobj entity);
        IEnumerable<BLLobj> GetAll();
        DALobj Convert(BLLobj obj);
        BLLobj Convert(DALobj obj);
    }
}