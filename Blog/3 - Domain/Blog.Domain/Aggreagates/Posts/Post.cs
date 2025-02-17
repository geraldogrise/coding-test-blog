using Blog.Domain.Aggreagates.Core;
using Blog.Domain.Aggreagates.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Aggreagates.Posts
{
    public class Post : EntityCore<Post>
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "O campo Description é obrigatório.")]
        public virtual string Description { get; set; }

        public virtual int Id_user { get; set; }

        public virtual User User { get; set; }
    }
}
