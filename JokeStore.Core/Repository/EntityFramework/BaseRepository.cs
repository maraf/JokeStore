﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JokeStore.Core.Repository.EntityFramework
{
    public abstract class BaseRepository
    {
        protected DataContext context = new DataContext();
    }
}
