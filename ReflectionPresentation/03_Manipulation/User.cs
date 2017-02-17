using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPresentation.Manipulation
{
    public class User
    {
        public User()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID
        {
            get;

            private set;
        }

        public string Name { get; set; }
    }
}
