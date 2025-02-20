﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.HetCategories
{
    public record GetCategoriesCommand() : IRequest<List<string>>;
}
