
namespace ApiDemo.DataDb
{
    using ApiDemo.Models;
    using System.Collections.Generic;

    public interface IData
    {
        IEnumerable<Cat> All();  //Get
        Cat Find(int id);        //Get
        //-----------------------------
        int Add(Cat cat);        //Post -> vrashta id na suzdadenata cat
        //-----------------------------
        void Delete(int id);

    }
}
