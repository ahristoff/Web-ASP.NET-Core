
namespace ApiDemo.DataDb
{
    using ApiDemo.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Data: IData
    {
        private readonly List<Cat> data;

        public Data()
        {
           this.data = new  List<Cat>
            {
                new Cat { Id = 1, Name = "Ivan", Age =15, Color = "Black" },
                new Cat { Id = 2, Name = "Gosho", Age =5, Color = "Orange" },
                new Cat { Id = 3, Name = "Pesho", Age =12, Color = "Yellow"},
                new Cat { Id = 4, Name = "Vankata", Age =11, Color = "White"},
            };
        }
        //---------------------------Get-----------------------------

        public IEnumerable<Cat> All()
        {
            return this.data;
        }

        public Cat Find(int id)
        {
            return this.data.FirstOrDefault(c => c.Id == id);
        }

        //-----------------------------Post---------------

        public int Add(Cat cat)
        {
            var id = this.data.Count + 1;
            cat.Id = id;
            this.data.Add(cat);

            return id;
        }

        //-----------------------------Delete-----------------

        public void Delete(int id)
        {
            this.data.RemoveAll(c => c.Id == id);
        }
    }
}
