﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2__MVC__CodeFirst.Repositories
{
    interface IRepositoryHundler
    {
        bool Save();
        void SaveAsync();
    }
}
