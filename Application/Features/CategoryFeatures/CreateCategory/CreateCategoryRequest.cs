﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed record CreateCategoryRequest(string Name) : IRequest<CreateCategoryResponse>
    {
    }
}
