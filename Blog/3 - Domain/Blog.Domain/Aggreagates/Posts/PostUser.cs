﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Aggreagates.Posts
{
    public class PostUser
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Login { get; set; }
        public virtual string User { get; set; }
        public virtual string Email { get; set; }
    }
}
